using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest.Test
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.companyname.uitest")/*.DeviceSerial("4T89571AA1952200745")*/.StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}