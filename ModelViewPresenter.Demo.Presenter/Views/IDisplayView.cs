
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
    }
}
