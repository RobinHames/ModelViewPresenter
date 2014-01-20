
namespace ModelViewPresenter.Demo.Presenter
{
    /// <summary>
    /// Provides a base interface for all presenters
    /// </summary>
    /// <typeparam name="View">The View a Presenter is associated with</typeparam>
    /// <remarks>
    /// This will be used to allow IoC container to register all presenters by convention
    /// </remarks>
    public interface IPresenterBase<View> where View : IViewBase
    {
        /// <summary>
        /// Initialise the presenter and run any tasks needed on loading the view
        /// </summary>
        /// <param name="view">The view object the presenter is working with</param>
        void RunPresenter(View view);
    }
}
