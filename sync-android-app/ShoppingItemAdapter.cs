using System;
using sync.model;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using System.Linq;
using Android.Widget;

namespace syncandroidapp
{
	public class ShoppingItemAdapter: RecyclerView.Adapter
	{
		private List<ShoppingItem> _items = new List<ShoppingItem>();

		public ShoppingItemAdapter() {
			base.HasStableIds = true;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var shoppingItemView = LayoutInflater.From (parent.Context)
				.Inflate (Resource.Layout.shopping_item, parent, false);
			return new ShoppingItemViewHolder(shoppingItemView);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var shoppingItem = GetItem (position);
			var shoppingHolder = (ShoppingItemViewHolder)holder;
			shoppingHolder.Item = shoppingItem;
		}

		public override int ItemCount
		{
			get { return _items.Count; }
		}

		public override long GetItemId(int position)
		{
			return _items[position].GetHashCode();
		}

		public ShoppingItem GetItem(int pos) 
		{
			return _items [pos];
		}

		public void SetItems(List<ShoppingItem> items) 
		{
			_items = items;
		}
	}

	public class ShoppingItemViewHolder: RecyclerView.ViewHolder 
	{
		private TextView _itemIdField;
		private TextView _itemNameField;
		private TextView _itemDateField;

		private ShoppingItem _item;

		public ShoppingItem Item { 
			get 
			{ return _item;
			}
			set 
			{ _item = value;
				_itemIdField.Text = value.UID;
				_itemNameField.Text = value.Name;
				_itemDateField.Text = value.GetCreatedTime ();
			} 
		}
		public ShoppingItemViewHolder(View itemView): base(itemView)
		{
			_itemNameField = (TextView)itemView.FindViewById (Resource.Id.item_name);
			_itemDateField = (TextView)itemView.FindViewById (Resource.Id.item_date);
			_itemIdField = (TextView)itemView.FindViewById (Resource.Id.item_id);
		}
	}
}