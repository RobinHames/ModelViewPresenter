
namespace ModelViewPresenter.Demo.Presenter
{
    /// <summary>
    /// Abstract base class for all Presenters
    /// </summary>
    /// <typeparam name="View">The View a Presenter is associated with</typeparam>
    public abstract class PresenterBase<View> : IPresenterBase<View> where View : IViewBase
    {
        /// <summary>
        /// The view object that the Presenter is working with
        /// </summary>
        protected View presenterView;

        /// <summary>
        /// View Redirector that will allow navigation between views
        /// </summary>
        /// <remarks>
        /// This has public acces to allow the instance to be injected by the IoC container. 
        /// We could use constructor injection but then all concrete presenters would need to implement this constructor
        /// </remarks>
        public IViewRedirector viewRedirector { get; set; }

        /// <summary>
        /// Initialise the presenter and run any tasks needed on loading the view
        /// </summary>
        /// <param name="view">The view object the presenter is working with</param>
        public void RunPresenter(View view)
        {
            presenterView = view;
            RunPresenter();
        }

        /// <summary>
        /// Run any tasks needed on loading the view
        /// </summary>
        /// <remarks>
        /// Concrete implementations of presenters can override this to perform any tasks on the view loading
        /// </remarks>
        protected virtual void RunPresenter()
        {
        }
    }
}
