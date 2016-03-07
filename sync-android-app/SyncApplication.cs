using System;
using Android.App;
using Android.Runtime;

namespace syncandroidapp
{
    [Application(Label = "@string/app_name", Theme = "@style/MyTheme.Base", Icon = "@mipmap/ic_launcher")]
    public class SyncApplication : Application
    {
        public SyncApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }
    }
}