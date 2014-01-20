using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CacheProvider;

namespace ModelViewPresenter.Demo.Web.MVP
{
    public class ViewRedirector : ModelViewPresenter.Demo.Presenter.IViewRedirector
    {
        /// <summary>
        /// Redirect the user to the specified view, without passing any data
        /// </summary>
        /// <typeparam name="View">The view type to navigate to</typeparam>
        public void Redirect<View>() where View : Presenter.IViewBase
        {
            var pathToView = getPathToView(typeof(View));

            if (string.IsNullOrEmpty(pathToView))
                throw new Exception(string.Format("Cannot resolve a URL to view {0}", typeof(View).FullName));

            HttpContext.Current.Response.Redirect(pathToView, true);
            //httpcontext.current.server.transfer(pathToView);
        }
        
        /// <summary>
        /// Redirect the user to the specified view
        /// </summary>
        /// <typeparam name="View">The view type to navigate to</typeparam>
        /// <typeparam name="ViewModel">A data model associated with the view being navigated to</typeparam>
        /// <param name="model">A instance of the ViewModel containing data to be passed to the view</param>
        public void Redirect<View, ViewModel>(ViewModel model) where View : Presenter.IViewBase
        {
            if (!EqualityComparer<ViewModel>.Default.Equals(model, default(ViewModel)))
            {
                HttpContext.Current.Session.Add("ViewRedirectorData", model);
                //httpcontext.current.items.add("viewredirectordata", model);
            }

            // Call the Redirect method
            Redirect<View>();

        }

        /// <summary>
        /// Retrieve data passed to the view during a redirect
        /// </summary>
        /// <typeparam name="ViewModel">A data model associated with the view</typeparam>
        /// <returns>The data passed to the view</returns>
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