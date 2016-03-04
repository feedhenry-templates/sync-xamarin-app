using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Content;
using Android.Support.V4.View;

namespace syncandroidapp
{
	public class RecyclerItemClickListener: Java.Lang.Object, RecyclerView.IOnItemTouchListener
	{
		private IOnItemClickListener _listener;
		private GestureDetectorCompat _getstureDetector;

		public RecyclerItemClickListener (Context context, IOnItemClickListener listener)
		{
			_listener = listener;
			_getstureDetector = new GestureDetectorCompat (context, new SingleTap ());
		}

		public bool OnInterceptTouchEvent (RecyclerView view, MotionEvent e)
		{
			var childView = view.FindChildViewUnder (e.GetX (), e.GetY ());
			if (childView != null && _getstureDetector.OnTouchEvent (e)) 
			{
				_listener.OnItemClick (childView, view.GetChildPosition (childView));
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

	public interface IOnItemClickListener {
		void OnItemClick(View view, int position);
	}
}

