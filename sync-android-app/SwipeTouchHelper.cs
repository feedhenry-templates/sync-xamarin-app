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
using Android.Support.V7.Widget.Helper;

namespace syncandroidapp
{
	public class SwipeTouchHelper: ItemTouchHelper.SimpleCallback
	{
		public event EventHandler<ItemSwipeEvent> ItemSwipeEvent;

		public SwipeTouchHelper() : base(0, ItemTouchHelper.Right) {}

		public override bool OnMove (RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
		{
			return false;
		}

		public override void OnSwiped (RecyclerView.ViewHolder viewHolder, int direction)
		{
			var v = (ShoppingItemViewHolder)viewHolder;
			EventHandler<ItemSwipeEvent> handler = ItemSwipeEvent;

			handler(this, new ItemSwipeEvent(v.Item));
		}
	}

	public class ItemSwipeEvent: EventArgs 
	{
		public ShoppingItem Item { get; private set; }

		public ItemSwipeEvent (ShoppingItem item) {
			Item = item;
		}
	}

}

