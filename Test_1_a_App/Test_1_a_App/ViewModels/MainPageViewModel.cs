using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_1_a_App.CommonServices;
using Xamarin.Forms.Maps;

namespace Test_1_a_App.ViewModels {

    /// <summary>
    /// DIコンテナにより依存関係を注入された上で、
    /// PrismのViewModelLocatorによりInstance化されます。
    /// </summary>
    public class MainPageViewModel : ViewModelBase {

        #region Properties

        #region Public properties

        /// <summary>
        /// MAP上に表示するPin
        /// </summary>
        public ReactiveProperty<Pin[]> Pins { get; }
        /// <summary>
        /// MAPで表示する範囲
        /// </summary>
        public ReactiveProperty<MapSpan> MapSpan { get; }

        #endregion

        #region Private properties

        /// <summary>
        /// GPS位置情報サービス(DI)
        /// </summary>
        private ILocationService LocationService { get; }
        /// <summary>
        /// 位置情報ストリーム
        /// </summary>
        private IObservable<Position> LocationObservable { get; }

        #endregion

        #endregion

        public MainPageViewModel(ILocationService location) : base( null ) {

            this.Title.Value = "Map";
            this.LocationService = location ?? throw new ArgumentNullException( nameof( location ) );

            //5秒毎に現在位置を取得
            this.LocationObservable = Observable.Timer( TimeSpan.FromMilliseconds( 500 ), TimeSpan.FromSeconds( 5 ) )
                    .Select( _ => this.LocationService.GetPositionAsync().Result );

            //現在位置をMAP表示範囲にする
            this.MapSpan = this.LocationObservable
                    .FirstAsync() //最初の位置情報取得時
                    .Select( position => new MapSpan( position, 0.01, 0.01 ) )
                    .ToReactiveProperty()
                    .AddTo( this.Disposable );

            //現在位置をPinとして表示
            this.Pins = this.LocationObservable
                    .Select( position => new Pin { Position = position, Label = "" } )
                    .Select( pin => new Pin[] { pin } )
                    .ToReactiveProperty()
                    .AddTo( this.Disposable );

        }

    }

}
