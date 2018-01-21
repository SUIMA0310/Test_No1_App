using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Xamarin.Forms.Maps;
using Android.Gms.Location;

namespace Test_1_a_App.Droid.Services {

    /// <summary>
    /// GPS位置情報を取得する
    /// </summary>
    public class LocationService : CommonServices.ILocationService {

        /// <summary>
        /// 非同期に現在位置を取得する
        /// </summary>
        /// <returns></returns>
        public Task<Position> GetPositionAsync() {

            try {

                var locationManager = Application.Context.GetSystemService( Context.LocationService ) as LocationManager;
                var position = locationManager.GetLastKnownLocation( LocationManager.GpsProvider );
                return Task.FromResult( new Position( position.Latitude, position.Longitude ) );

            } catch {

                //取得に失敗したら、盛岡駅の座標を返しておく
                return Task.FromResult( new Position( 39.701164, 141.133516 ) );

            }

        }

    }

}