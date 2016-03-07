/**
 * Copyright Red Hat, Inc, and individual contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using FHSDK.Sync;
using sync.model;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace syncandroidapp
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class ListOfItemsActivity : AppCompatActivity
    {
        private const string DatasetId = "myShoppingList";
        private readonly ShoppingItemAdapter _adapter = new ShoppingItemAdapter();

        private RecyclerView _list;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.list_of_items_activity);

            var toolbar = (Toolbar) FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _list = (RecyclerView) FindViewById(Resource.Id.list);
            _list.SetLayoutManager(new LinearLayoutManager(this));
            _list.SetAdapter(_adapter);
            var recyclerItemClick = new RecyclerItemClickListener(ApplicationContext);
            _list.AddOnItemTouchListener(recyclerItemClick);
            recyclerItemClick.ItemClickEvent +=
                (sender, e) => { ShowPopup(_adapter.GetItem(e.Position)); };

            var callback = new SwipeTouchHelper();
            callback.ItemSwipeEvent += (sender, e) => { DeleteItem(e.Item); };

            var touchHelper = new ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(_list);

            var fab = (FloatingActionButton) FindViewById(Resource.Id.fab);
            fab.Click += delegate { ShowPopup(new ShoppingItem("")); };
        }

        protected override void OnStart()
        {
            base.OnStart();
            FireSync();
        }

        private void FireSync()
        {
            var client = FHSyncClient.GetInstance();
            var config = new FHSyncConfig();
            client.Initialise(config);
            client.SyncCompleted += (sender, args) =>
            {
                RunOnUiThread(() =>
                {
                    _adapter.SetItems(client.List<ShoppingItem>(DatasetId));
                    _adapter.NotifyDataSetChanged();
                });
            };
            client.Manage<ShoppingItem>(DatasetId, config, null);
        }

        private void ShowPopup(ShoppingItem item)
        {
            var customView = View.Inflate(ApplicationContext, Resource.Layout.form_item_dialog, null);
            var name = (EditText) customView.FindViewById(Resource.Id.name);
            name.Text = item.Name;

            new AlertDialog.Builder(this)
                .SetTitle(item.UID == null
                    ? GetString(Resource.String.new_item)
                    : GetString(Resource.String.edit_item) + ": " + item.Name)
                .SetPositiveButton(GetText(Resource.String.save), delegate
                {
                    item.Name = name.Text;
                    SaveItem(item);
                })
                .SetView(customView).Show();
        }

        private static void SaveItem(ShoppingItem item)
        {
            var client = FHSyncClient.GetInstance();
            if (item.UID == null)
            {
                client.Create(DatasetId, item);
            }
            else
            {
                client.Update(DatasetId, item);
            }
        }

        private static void DeleteItem(ShoppingItem item)
        {
            FHSyncClient.GetInstance().Delete<ShoppingItem>(DatasetId, item.UID);
        }
    }
}