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
using Android.Content;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Object = Java.Lang.Object;

namespace syncandroidapp
{
    public class RecyclerItemClickListener : Object, RecyclerView.IOnItemTouchListener
    {
        private readonly GestureDetectorCompat _getstureDetector;

        public RecyclerItemClickListener(Context context)
        {
            _getstureDetector = new GestureDetectorCompat(context, new SingleTap());
        }

        public bool OnInterceptTouchEvent(RecyclerView view, MotionEvent e)
        {
            var childView = view.FindChildViewUnder(e.GetX(), e.GetY());
            if (childView == null || !_getstureDetector.OnTouchEvent(e)) return false;
            var handler = ItemClickEvent;
            handler?.Invoke(this, new ItemClickEvent(childView, view.GetChildPosition(childView)));
            return false;
        }

        public void OnTouchEvent(RecyclerView rv, MotionEvent e)
        {
        }

        public void OnRequestDisallowInterceptTouchEvent(bool disallowIntercept)
        {
        }

        public event EventHandler<ItemClickEvent> ItemClickEvent;
    }

    public class SingleTap : GestureDetector.SimpleOnGestureListener
    {
        public override bool OnSingleTapUp(MotionEvent e)
        {
            return true;
        }
    }

    public class ItemClickEvent : EventArgs
    {
        public ItemClickEvent(View view, int position)
        {
            View = view;
            Position = position;
        }

        public View View { get; private set; }
        public int Position { get; private set; }
    }
}