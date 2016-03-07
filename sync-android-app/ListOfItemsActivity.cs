using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using sync.model;
using FHSDK.Sync;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget.Helper;

namespace syncandroidapp
{
	[Activity (ScreenOrientation = ScreenOrientation.Portrait)]
	public class ListOfItemsActivity : AppCompatActivity
	{
		private const string DatasetId = "myShoppingList";

		private RecyclerView _list;
		private ShoppingItemAdapter _adapter = new ShoppingItemAdapter ();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.list_of_items_activity);

			var toolbar = (Toolbar)FindViewById (Resource.Id.toolbar);
			SetSupportActionBar (toolbar);

			_list = (RecyclerView)FindViewById (Resource.Id.list);
			_list.SetLayoutManager (new LinearLayoutManager (this));
			_list.SetAdapter (_adapter);
			var recyclerItemClick = new RecyclerItemClickListener (ApplicationContext);
			_list.AddOnItemTouchListener (recyclerItemClick);
			recyclerItemClick.ItemClickEvent += (object sender, ItemClickEvent e) => {
				ShowPopup (_adapter.GetItem (e.Position));
			};

			var callback = new SwipeTouchHelper ();
			callback.ItemSwipeEvent += (object sender, ItemSwipeEvent e) => {
				DeleteItem(e.Item);
			};

			var touchHelper = new ItemTouchHelper (callback);
			touchHelper.AttachToRecyclerView (_list);

			var fab = (FloatingActionButton)FindViewById (Resource.Id.fab);
			fab.Click += delegate {
				ShowPopup(new ShoppingItem(""));
			};
		}

		protected override void OnStart() {
			base.OnStart();
			fireSync();
		}

		private void fireSync() 
		{
			var client = FHSyncClient.GetInstance();
			var config = new FHSyncConfig();
			client.Initialise(config);
			client.SyncCompleted += (sender, args) =>
			{
				RunOnUiThread(()=> { 
					_adapter.SetItems(client.List<ShoppingItem>(DatasetId));
					_adapter.NotifyDataSetChanged();
				});
			};
			client.Manage<ShoppingItem>(DatasetId, config, null);
		}

		private void ShowPopup(ShoppingItem item) 
		{
			var customView = View.Inflate (ApplicationContext, Resource.Layout.form_item_dialog, null);
			var name = (EditText)customView.FindViewById (Resource.Id.name);
			name.Text = item.Name;

			new Android.Support.V7.App.AlertDialog.Builder (this)
				.SetTitle(item.UID == null ? GetString(Resource.String.new_item) :
					GetString(Resource.String.edit_item) + ": " + item.Name)
				.SetPositiveButton(GetText(Resource.String.save), delegate {
					item.Name = name.Text;
					SaveItem(item);
				})
				.SetView (customView).Show ();

		}

		private void SaveItem(ShoppingItem item) 
		{
			var client = FHSyncClient.GetInstance();
			if (item.UID == null) {
				client.Create (DatasetId, item);
			} else {
				client.Update (DatasetId, item);
			}
		}

		private void DeleteItem(ShoppingItem item) 
		{
			FHSyncClient.GetInstance ().Delete<ShoppingItem> (DatasetId, item.UID);
		}
	}
}

