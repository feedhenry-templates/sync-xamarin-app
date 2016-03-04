using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Support.V7.App;
using FHSDK.Droid;
using Android.Content;

namespace syncandroidapp
{
	[Activity (Label = "@string/sync_demo_title", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreenActivity : AppCompatActivity
	{
		protected async override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.splash_screen_activity);
			var init = await FHClient.Init ();
			if (init) {
				StartActivity(new Intent(this, typeof(ListOfItemsActivity)));
				Finish();
			}
		}
	}
}


