
namespace ModelViewPresenter.Demo.Presenter.Presenters
{
    /// <summary>
    /// Presenter to display the data to the user
    /// </summary>
    public class DisplayPresenter : PresenterBase<Views.IDisplayView>
    {
        protected override void RunPresenter()
        {
            this.presenterView.setSubmitAction(this.onSubmit);
            var data = viewRedirector.getData<ViewModels.DisplayViewModel>();
            this.presenterView.setFormData(data);
        }

        public void onSubmit()
        {
            // Redirect to the InputView (no data passed)
            viewRedirector.Redirect<Views.IInputView>();
        }
    }
}
