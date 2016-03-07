using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Content;
using Android.Support.V4.View;

namespace syncandroidapp
{
	public class RecyclerItemClickListener: Java.Lang.Object, RecyclerView.IOnItemTouchListener
	{
		private GestureDetectorCompat _getstureDetector;
		public event EventHandler<ItemClickEvent> ItemClickEvent;

		public RecyclerItemClickListener (Context context)
		{
			_getstureDetector = new GestureDetectorCompat (context, new SingleTap ());
		}

		public bool OnInterceptTouchEvent (RecyclerView view, MotionEvent e)
		{
			var childView = view.FindChildViewUnder (e.GetX (), e.GetY ());
			if (childView != null && _getstureDetector.OnTouchEvent (e)) 
			{
				EventHandler<ItemClickEvent> handler = ItemClickEvent;
				handler(this, new ItemClickEvent(childView, view.GetChildPosition (childView)));
			}
			return false;
		}

		public void OnTouchEvent (RecyclerView rv, MotionEvent e)
		{
		}

		public void OnRequestDisallowInterceptTouchEvent (bool disallowIntercept) 
		{
		}
	}

	public class SingleTap: GestureDetector.SimpleOnGestureListener
	{
		public override bool OnSingleTapUp(MotionEvent e)
		{
			return true;
		}
	}

	public class ItemClickEvent: EventArgs {
		public View View { get; private set; }
		public int Position { get; private set; }
		public ItemClickEvent(View view, int position) 
		{
			View = view;
			Position = position;
		}
	}
}

