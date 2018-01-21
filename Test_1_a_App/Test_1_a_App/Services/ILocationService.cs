using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Test_1_a_App.CommonServices {

    /// <summary>
    /// GPS位置情報サービス
    /// </summary>
    public interface ILocationService {

        /// <summary>
        /// GPS位置情報を取得する
        /// </summary>
        /// <returns>現在の座標</returns>
        Task<Position> GetPositionAsync();

    }

}
