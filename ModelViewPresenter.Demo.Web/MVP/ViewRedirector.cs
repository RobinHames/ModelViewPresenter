using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CacheProvider;

namespace ModelViewPresenter.Demo.Web.MVP
{
    public class ViewRedirector : ModelViewPresenter.Demo.Presenter.IViewRedirector
    {
        public void Redirect<View, ViewModel>(ViewModel model) where View : Presenter.IViewBase
        {
            var pathToView = getPathToView(typeof(View));

            if (string.IsNullOrEmpty(pathToView))
                throw new Exception(string.Format("Cannot resolve a URL to view {0}", typeof(View).FullName));

            HttpContext.Current.Session.Add("ViewRedirectorData", model);
            HttpContext.Current.Response.Redirect(pathToView, true);
            //httpcontext.current.items.add("viewredirectordata", model);
            //httpcontext.current.server.transfer(pathToView);

        }

        public ViewModel getData<ViewModel>()
        {
            //object rawData = HttpContext.Current.Items["ViewRedirectorData"];
            object rawData = HttpContext.Current.Session["ViewRedirectorData"];
            if (rawData != null && rawData is ViewModel)
                return (ViewModel)rawData;

            return default(ViewModel);
        }

        #region Cache Of View Paths

        // Use Cache Provider to store the path to a view
        private ICacheProvider<string> _viewPathCache;

        public ViewRedirector(ICacheProvider<string> viewPathCache)
        {
            _viewPathCache = viewPathCache;
        }

        private string getPathToView(Type viewType)
        {
            string viewKey = viewType.FullName;

            // Retrieve the path to the view from the cache, or resolve it from the view instance if not currently chached
            return _viewPathCache.Fetch(viewKey,
                () =>
                {
                    var viewInstance = Global.container.Resolve(viewType);
                    if (viewInstance == null)
                        return string.Empty;

                    return string.Format(@"~\Views\{0}.aspx", viewInstance.GetType().Name);
                },
                null,
                new TimeSpan(24, 0, 0)
                );
        }

        #endregion
    }
}