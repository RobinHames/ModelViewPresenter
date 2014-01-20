using System;

namespace ModelViewPresenter.Demo.Presenter.Views
{
    /// <summary>
    /// A view for displaying data entered by the user
    /// </summary>
    public interface IDisplayView : IViewBase
    {
        /// <summary>
        /// Pass the form data for display by the view
        /// </summary>
        /// <param name="formData"></param>
        void setFormData(ViewModels.DisplayViewModel formData);

        /// <summary>
        /// Set a callback action to run when the user submits the DisplayView
        /// </summary>
        /// <param name="onSubmit"></param>
        void setSubmitAction(Action onSubmit);
    }
}
