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

namespace syncandroidapp
{
	[Activity (ScreenOrientation = ScreenOrientation.Portrait)]
	public class ListOfItemsActivity : AppCompatActivity, IOnItemClickListener
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
			_list.AddOnItemTouchListener (new RecyclerItemClickListener(ApplicationContext, this));
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

		public void OnItemClick(View view, int position)
		{
			ShowPopup (_adapter.GetItem(position));
		}

		private void ShowPopup(ShoppingItem item) 
		{
			var customView = View.Inflate (ApplicationContext, Resource.Layout.form_item_dialog, null);
			var name = (EditText)customView.FindViewById (Resource.Id.name);
			name.Text = item.Name;

			new Android.Support.V7.App.AlertDialog.Builder (this)
				.SetView (customView).Show (); 

		}
	}
}

