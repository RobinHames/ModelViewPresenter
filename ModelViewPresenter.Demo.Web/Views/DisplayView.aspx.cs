using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ModelViewPresenter.Demo.Presenter;
using ModelViewPresenter.Demo.Presenter.Views;
using ModelViewPresenter.Demo.Presenter.ViewModels;

namespace ModelViewPresenter.Demo.Web.Views
{
    public partial class DisplayView : System.Web.UI.Page, IDisplayView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Call this to resolve the correct presenter and run it            
            // N.B. done in the load event so that the web form controls are available to the view interface
            MVP.PresenterFactory.getPresenter<IDisplayView>(this);
        }

        Action _onSubmit;

        /// <summary>
        /// Set a callback action to run when the user submits the DisplayForm
        /// </summary>
        /// <param name="onSubmit"></param>
        /// <remarks>
        /// This is the implementation of IDisplayView.setSubmitAction
        /// </remarks>
        public void setSubmitAction(Action onSubmit)
        {
            _onSubmit = onSubmit;
        }

        /// <summary>
        /// Pass the form data for display by the view
        /// </summary>
        /// <param name="formData"></param>
        /// <remarks>
        /// This is the implementation of IDisplayView.setFormData
        /// </remarks>
        public void setFormData(DisplayViewModel formData)
        {
            if (formData != null)
                lblName.Text = formData.Name;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Call the onSubmit callback method in the presenter
            if (this._onSubmit != null)
                this._onSubmit();
        }
    }
}