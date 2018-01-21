using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Test_1_a_App.Behaviors {

    /// <summary>
    /// Binding可能なPropertyを持つBehaviorの基底クラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindableBehavior<T> : Behavior<T> where T : BindableObject {

        #region Properties

        #region Protected properties

        /// <summary>
        /// アタッチ先のInstance
        /// </summary>
        protected T AssociatedObject { get; private set; }

        #endregion

        #endregion

        #region Attach

        /// <summary>
        /// アタッチ時
        /// </summary>
        /// <param name="bindableObject">アタッチ先のInstance</param>
        protected override void OnAttachedTo(T bindableObject) {

            base.OnAttachedTo( bindableObject );

            this.AssociatedObject = bindableObject ?? throw new ArgumentNullException( nameof( bindableObject ) );

            if ( bindableObject.BindingContext != null ) {

                //アタッチ先のContextを継承
                this.BindingContext = bindableObject.BindingContext;

            }

            //Context変更に追従
            bindableObject.BindingContextChanged += this.OnBindingContextChanged;

        }

        /// <summary>
        /// デタッチ時
        /// </summary>
        /// <param name="bindableObject">元アタッチ先のInstance</param>
        protected override void OnDetachingFrom(T bindableObject) {

            //Context追従を解除
            bindableObject.BindingContextChanged -= this.OnBindingContextChanged;

        }

        #endregion

        #region Context chang

        /// <summary>
        /// アタッチ先のContext変更を受信
        /// </summary>
        /// <param name="sender">アタッチ先のInstance</param>
        private void OnBindingContextChanged(object sender, EventArgs e) {

            //自分のContext変更を通知
            this.OnBindingContextChanged();

        }

        /// <summary>
        /// Context変更通知
        /// </summary>
        protected override void OnBindingContextChanged() {

            this.BindingContext = this.AssociatedObject?.BindingContext;
            base.OnBindingContextChanged();

        }

        #endregion

    }

}
