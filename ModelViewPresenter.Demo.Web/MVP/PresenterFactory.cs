
using ModelViewPresenter.Demo.Presenter;

namespace ModelViewPresenter.Demo.Web.MVP
{
    public static class PresenterFactory
    {
        /// <summary>
        /// Resolve and run the presenter for the specified view interface
        /// </summary>
        /// <typeparam name="View">The view type</typeparam>
        /// <param name="view">The instance of the view</param>
        /// <returns>The presenter instance</returns>
        public static IPresenterBase<View> getPresenter<View>(View view) where View : IViewBase
        {
            var presenter = Global.container.Resolve<IPresenterBase<View>>();

            if (presenter != null)
                presenter.RunPresenter(view);

            return presenter;
        }
    }
}