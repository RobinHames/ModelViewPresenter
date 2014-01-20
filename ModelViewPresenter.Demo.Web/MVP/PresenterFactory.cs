using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ModelViewPresenter.Demo.Presenter;

namespace ModelViewPresenter.Demo.Web.MVP
{
    public static class PresenterFactory
    {
        public static IPresenterBase<View> getPresenter<View>(View view) where View : IViewBase
        {
            var presenter = Global.container.Resolve<IPresenterBase<View>>();

            if (presenter != null)
                presenter.RunPresenter(view);

            return presenter;
        }
    }
}