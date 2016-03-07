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