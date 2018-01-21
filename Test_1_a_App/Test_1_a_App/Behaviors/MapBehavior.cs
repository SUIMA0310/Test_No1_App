using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Test_1_a_App.Behaviors {

    /// <summary>
    /// Binding不可能なPropertyへのBindingを提供する
    /// </summary>
    public class MapBehavior : BindableBehavior<Map> {

        #region Properties

        #region Bindable properties

        /// <summary>
        /// MAP表示範囲
        /// </summary>
        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(
                "MapSpan",
                typeof( MapSpan ),
                typeof( MapBehavior ),
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: OnMapSpanChanged );

        /// <summary>
        /// MAP上のPin
        /// </summary>
        public static readonly BindableProperty PinsProperty = BindableProperty.Create(
                "Pins",
                typeof( IEnumerable<Pin> ),
                typeof( MapBehavior ),
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: OnPinsChanged );

        #endregion

        #region Public properties

        /// <summary>
        /// MAP表示範囲
        /// 
        /// ラッパープロパティ
        /// </summary>
        public MapSpan MapSpan {
            get { return (MapSpan)GetValue( MapSpanProperty ); }
            set { SetValue( MapSpanProperty, value ); }
        }
        /// <summary>
        /// MAP上のPin
        /// 
        /// ラッパープロパティ
        /// </summary>
        public IEnumerable<Pin> Pins {
            get { return (IEnumerable<Pin>)GetValue( PinsProperty ); }
            set { SetValue( PinsProperty, value ); }
        }

        #endregion

        #endregion

        private static void OnMapSpanChanged(BindableObject bindable, object oldValue, object newValue) {

            var behavior = bindable as MapBehavior;
            var mapSpan = newValue as MapSpan;

            if ( behavior != null && mapSpan != null ) {

                //MAPの表示位置を追従
                behavior.AssociatedObject.MoveToRegion( mapSpan );

            }

        }

        private static void OnPinsChanged(BindableObject bindable, object oldValue, object newValue) {

            var behavior = bindable as MapBehavior;
            if ( behavior != null ) {

                //古いデータを削除
                behavior.AssociatedObject.Pins.Clear();

                //新しいデータを追加
                var enumerable = newValue as IEnumerable<Pin>;
                if ( enumerable != null ) {

                    foreach ( var item in enumerable ) {

                        behavior.AssociatedObject.Pins.Add( item );

                    }

                }

                var oldNotifyCollection = oldValue as INotifyCollectionChanged;
                if ( oldNotifyCollection != null ) {

                    //Collection変更通知購読解除
                    oldNotifyCollection.CollectionChanged -= behavior.OnPinsCollectionChanged;

                }

                var newNotifyCollection = newValue as INotifyCollectionChanged;
                if ( newNotifyCollection != null ) {

                    //Collection変更通知購読開始
                    newNotifyCollection.CollectionChanged += behavior.OnPinsCollectionChanged;

                }

            }

        }

        private void OnPinsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {

            //削除されたデータを追従して削除
            foreach ( var item in e.OldItems ?? new object[] { } ) {

                var pin = item as Pin;
                if ( pin != null ) {

                    this.AssociatedObject.Pins.Remove( pin );

                }

            }

            //追加されたデータを追従して追加
            foreach ( var item in e.NewItems ?? new object[] { } ) {

                var pin = item as Pin;
                if ( pin != null ) {

                    this.AssociatedObject.Pins.Add( pin );

                }

            }

        }

    }

}
