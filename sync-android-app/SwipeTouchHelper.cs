using System;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using sync.model;

namespace syncandroidapp
{
    public class SwipeTouchHelper : ItemTouchHelper.SimpleCallback
    {
        public SwipeTouchHelper() : base(0, ItemTouchHelper.Right)
        {
        }

        public event EventHandler<ItemSwipeEvent> ItemSwipeEvent;

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder,
            RecyclerView.ViewHolder target)
        {
            return false;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            var v = (ShoppingItemViewHolder) viewHolder;
            var handler = ItemSwipeEvent;

            handler?.Invoke(this, new ItemSwipeEvent(v.Item));
        }
    }

    public class ItemSwipeEvent : EventArgs
    {
        public ItemSwipeEvent(ShoppingItem item)
        {
            Item = item;
        }

        public ShoppingItem Item { get; private set; }
    }
}