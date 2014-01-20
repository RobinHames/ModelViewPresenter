using System;
using System.Collections.Generic;
using System.Linq;
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
            MVP.PresenterFactory.getPresenter<IInputView>(this);
        }

        Action _onSubmit;

        public void setSubmitAction(Action onSubmit)
        {
            _onSubmit = onSubmit;
        }

        public InputViewModel getFormData()
        {
            var inputViewData = new InputViewModel();
            inputViewData.Name = txtName.Text;
            return inputViewData;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this._onSubmit != null)
                this._onSubmit();
        }
    }
}