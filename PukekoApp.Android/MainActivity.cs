using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;

namespace PukekoApp.Droid
{
    [Activity(Label = "PukekoApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();

            int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                App.StatusBarHeight = (int)(Resources.GetDimensionPixelSize(resourceId) / Resources.DisplayMetrics.Density) / 20;
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                // for covering the full screen in android..
                Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);

                // clear FLAG_TRANSLUCENT_STATUS flag:
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);

                Window.SetStatusBarColor(Android.Graphics.Color.Transparent);

            }

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
}
}