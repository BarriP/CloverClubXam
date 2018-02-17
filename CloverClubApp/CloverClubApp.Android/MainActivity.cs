using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using ImageCircle.Forms.Plugin.Droid;

namespace CloverClubApp.Droid
{
    [Activity(Label = "CloverClubApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //Plugins
            ImageCircleRenderer.Init();
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());
            CarouselViewRenderer.Init();

            FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar, Resource.Id.tabMode);

            LoadApplication(new App());
        }
    }
}

