using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace Test_1_a_App.ViewModels {

    /// <summary>
    /// ViewModelの基底クラス
    /// </summary>
    public abstract class ViewModelBase : INavigationAware, IDestructible {

        #region Properties

        #region Public properties

        /// <summary>
        /// 画面タイトル
        /// </summary>
        public ReactiveProperty<string> Title { get; }

        #endregion

        #region Protected properties

        /// <summary>
        /// 画面遷移サービス(DI)
        /// </summary>
        protected INavigationService NavigationService { get; private set; }
        /// <summary>
        /// Disposableコンテナ
        /// </summary>
        protected CompositeDisposable Disposable { get; } = new CompositeDisposable();

        #endregion

        #endregion

        public ViewModelBase(INavigationService nav) {

            this.Title = new ReactiveProperty<string>( "Title" );
            this.Disposable = new CompositeDisposable();
            this.NavigationService = nav;

        }

        /// <summary>
        /// 自画面より他画面へ遷移
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnNavigatedFrom(NavigationParameters parameters) { }

        /// <summary>
        /// 他画面より遷移中
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnNavigatingTo(NavigationParameters parameters) { }

        /// <summary>
        /// 他画面より遷移
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnNavigatedTo(NavigationParameters parameters) { }

        /// <summary>
        /// ViewModel破棄
        /// </summary>
        public virtual void Destroy() {

            //収集したDisposableなInstanceを破棄
            this.Disposable.Dispose();

        }
    }

}
