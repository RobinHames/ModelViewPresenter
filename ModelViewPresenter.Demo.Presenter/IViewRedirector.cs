using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.Demo.Presenter
{
    public interface IViewRedirector
    {
        void Redirect<View, ViewModel>(ViewModel model) where View : IViewBase;

        ViewModel getData<ViewModel>();

    }
}
