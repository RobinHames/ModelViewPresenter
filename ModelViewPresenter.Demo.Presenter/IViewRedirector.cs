using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.Demo.Presenter
{
    /// <summary>
    /// Interface to allow navigation to a specified view
    /// </summary>
    public interface IViewRedirector
    {
        /// <summary>
        /// Redirect the user to the specified view, without passing any data
        /// </summary>
        /// <typeparam name="View">The view type to navigate to</typeparam>
        void Redirect<View>() where View : IViewBase;

        /// <summary>
        /// Redirect the user to the specified view
        /// </summary>
        /// <typeparam name="View">The view type to navigate to</typeparam>
        /// <typeparam name="ViewModel">A data model associated with the view being navigated to</typeparam>
        /// <param name="model">A instance of the ViewModel containing data to be passed to the view</param>
        void Redirect<View, ViewModel>(ViewModel model) where View : IViewBase;

        /// <summary>
        /// Retrieve data passed to the view during a redirect
        /// </summary>
        /// <typeparam name="ViewModel">A data model associated with the view</typeparam>
        /// <returns>The data passed to the view</returns>
        ViewModel getData<ViewModel>();

    }
}
