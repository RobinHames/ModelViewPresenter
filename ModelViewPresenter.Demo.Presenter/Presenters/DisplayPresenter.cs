
namespace ModelViewPresenter.Demo.Presenter.Presenters
{
    /// <summary>
    /// Presenter to display the data to the user
    /// </summary>
    public class DisplayPresenter : PresenterBase<Views.IDisplayView>
    {
        protected override void RunPresenter()
        {
            var data = viewRedirector.getData<ViewModels.DisplayViewModel>();
            this.presenterView.setFormData(data);
        }
    }
}
