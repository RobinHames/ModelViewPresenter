
namespace ModelViewPresenter.Demo.Presenter.Presenters
{
    /// <summary>
    /// Presenter to retrieve data from the user, via the InputView
    /// </summary>
    public class InputPresenter : PresenterBase<Views.IInputView>
    {
        protected override void RunPresenter()
        {
            // On load, set action to call when the user submits the data
            this.presenterView.setSubmitAction(this.onSubmit);
        }

        public void onSubmit()
        {
            // Read the input data from the view
            var inputFormData = this.presenterView.getFormData();
            // Redirect to the DisplayView - use explicit conversion to convert InputViewModel into DisplayViewModel
            viewRedirector.Redirect<Views.IDisplayView, ViewModels.DisplayViewModel>((ViewModels.DisplayViewModel)inputFormData);
        }
    }
}
