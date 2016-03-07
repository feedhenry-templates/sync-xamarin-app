using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using FHSDK.Droid;

namespace syncandroidapp
{
    [Activity(Label = "@string/sync_demo_title", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : AppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.splash_screen_activity);
            var init = await FHClient.Init();
            if (init)
            {
                StartActivity(new Intent(this, typeof (ListOfItemsActivity)));
                Finish();
            }
        }
    }
}