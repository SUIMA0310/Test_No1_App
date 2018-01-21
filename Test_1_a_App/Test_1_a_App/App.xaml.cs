using Prism;
using Prism.Ioc;
using Test_1_a_App.ViewModels;
using Test_1_a_App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;

[assembly: XamlCompilation( XamlCompilationOptions.Compile )]
namespace Test_1_a_App {

    public partial class App : PrismApplication {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this( null ) { }

        public App(IPlatformInitializer initializer) : base( initializer ) { }

        protected override async void OnInitialized() {

            InitializeComponent();

            //初期ページを指定
            await NavigationService.NavigateAsync( "MainPage" );

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) {

            //DIコンテナに型を登録
            containerRegistry.RegisterForNavigation<MainPage>();

        }

    }

}
