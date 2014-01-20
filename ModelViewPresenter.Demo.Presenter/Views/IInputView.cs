using System;

namespace ModelViewPresenter.Demo.Presenter.Views
{
    /// <summary>
    /// A view for capturing data from the user
    /// </summary>
    public interface IInputView : IViewBase
    {
        /// <summary>
        /// Get the submitted user data
        /// </summary>
        /// <returns></returns>
        ViewModels.InputViewModel getFormData();

        /// <summary>
        /// Set a callback action to run when the user submits the InputForm
        /// </summary>
        /// <param name="onSubmit"></param>
        void setSubmitAction(Action onSubmit);
    }
}
