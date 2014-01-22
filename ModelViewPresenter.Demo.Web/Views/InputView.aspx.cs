using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ModelViewPresenter.Demo.Presenter;
using ModelViewPresenter.Demo.Presenter.Views;
using ModelViewPresenter.Demo.Presenter.ViewModels;

namespace ModelViewPresenter.Demo.Web.Views
{
    public partial class InputView : System.Web.UI.Page, IInputView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Call this to resolve the correct presenter and run it            
            // N.B. done in the load event so that the web form controls are available to the view interface
            MVP.PresenterFactory.getPresenter<IInputView>(this);
        }

        Action _onSubmit;

        /// <summary>
        /// Set a callback action to run when the user submits the InputForm
        /// </summary>
        /// <param name="onSubmit"></param>
        /// <remarks>
        /// This is the implementation of IInputView.setSubmitAction
        /// </remarks>
        public void setSubmitAction(Action onSubmit)
        {
            _onSubmit = onSubmit;
        }

        /// <summary>
        /// Get the submitted user data
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// This is the implementation of IInputView.getFormData
        /// </remarks>
        public InputViewModel getFormData()
        {
            var inputViewData = new InputViewModel();
            inputViewData.Name = txtName.Text;
            return inputViewData;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Call the onSubmit callback method in the presenter
            if (this._onSubmit != null)
                this._onSubmit();
        }
    }
}