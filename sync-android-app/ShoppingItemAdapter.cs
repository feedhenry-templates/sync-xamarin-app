using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using sync.model;

namespace syncandroidapp
{
    public class ShoppingItemAdapter : RecyclerView.Adapter
    {
        private List<ShoppingItem> _items = new List<ShoppingItem>();

        public ShoppingItemAdapter()
        {
            HasStableIds = true;
        }

        public override int ItemCount => _items.Count;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var shoppingItemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.shopping_item, parent, false);
            return new ShoppingItemViewHolder(shoppingItemView);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var shoppingItem = GetItem(position);
            var shoppingHolder = (ShoppingItemViewHolder) holder;
            shoppingHolder.Item = shoppingItem;
        }

        public override long GetItemId(int position)
        {
            return _items[position].GetHashCode();
        }

        public ShoppingItem GetItem(int pos)
        {
            return _items[pos];
        }

        public void SetItems(List<ShoppingItem> items)
        {
            _items = items;
        }
    }

    public class ShoppingItemViewHolder : RecyclerView.ViewHolder
    {
        private ShoppingItem _item;
        private readonly TextView _itemDateField;
        private readonly TextView _itemIdField;
        private readonly TextView _itemNameField;

        public ShoppingItemViewHolder(View itemView) : base(itemView)
        {
            _itemNameField = (TextView) itemView.FindViewById(Resource.Id.item_name);
            _itemDateField = (TextView) itemView.FindViewById(Resource.Id.item_date);
            _itemIdField = (TextView) itemView.FindViewById(Resource.Id.item_id);
        }

        public ShoppingItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                _itemIdField.Text = value.UID;
                _itemNameField.Text = value.Name;
                _itemDateField.Text = value.GetCreatedTime();
            }
        }
    }
}