using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;

namespace Test_1_a_App.Droid {

    [Activity( Label = "Test_1_a_App", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate( bundle );

            global::Xamarin.Forms.Forms.Init( this, bundle );
            Xamarin.FormsMaps.Init( this, bundle ); //Mapを初期化
            LoadApplication( new App( new AndroidInitializer() ) );
        }
    }

    /// <summary>
    /// Platform固有の処理を登録する
    /// </summary>
    public class AndroidInitializer : IPlatformInitializer {

        public void RegisterTypes(IContainerRegistry container) {

            //DIコンテナに型を登録
            container.Register( typeof( CommonServices.ILocationService ), typeof( Services.LocationService ) );

        }

    }

}

