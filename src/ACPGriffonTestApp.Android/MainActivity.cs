﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Com.Adobe.Marketing.Mobile;

namespace ACPGriffonTestApp.Droid
{
    class CoreStartCompletionCallback : Java.Lang.Object, IAdobeCallback
    {
        public void Call(Java.Lang.Object callback)
        {
            // set launch config
            ACPCore.ConfigureWithAppID("94f571f308d5/00fc543a60e1/launch-c861fab912f7-development");
        }
    }

    [Activity(Label = "ACPGriffonTestApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
    DataScheme = "acpgriffontestapp",
    DataHost = "link",
    Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            // set the wrapper type
            ACPCore.SetWrapperType(WrapperType.Xamarin);

            // set log level
            ACPCore.LogLevel = LoggingMode.Verbose;

            // register SDK extensions
            ACPCore.Application = this.Application;
            ACPLifecycle.RegisterExtension();
            ACPIdentity.RegisterExtension();
            ACPGriffon.RegisterExtension();

            // start core
            ACPCore.Start(new CoreStartCompletionCallback());

            // register dependency service to link interface from ACPGriffonTestApp base project
            DependencyService.Register<IACPGriffonExtensionService, ACPGriffonExtensionService>();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();
            ACPCore.Application = this.Application;
            ACPCore.LifecycleStart(null);
        }

        protected override void OnPause()
        {
            base.OnPause();
            ACPCore.LifecyclePause();
        }
    }
}