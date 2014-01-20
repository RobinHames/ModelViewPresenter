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
    public partial class DisplayView : System.Web.UI.Page, IDisplayView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MVP.PresenterFactory.getPresenter<IDisplayView>(this);
        }

        public void setFormData(DisplayViewModel formData)
        {
            if (formData != null)
                lblName.Text = formData.Name;
        }
    }
}