
namespace ModelViewPresenter.Demo.Presenter.ViewModels
{
    /// <summary>
    /// A ViewModel for the DisplayView
    /// </summary>
    public class DisplayViewModel
    {
        public string Name { get; set; }

        public static explicit operator DisplayViewModel(InputViewModel inputViewModel)
        {
            return new DisplayViewModel
            {
                Name = inputViewModel.Name
            };
        }
    }
}
