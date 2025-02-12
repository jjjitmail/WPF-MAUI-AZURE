using SMMDD.Interfaces;
using SMMDD.Models;
using SMMDD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SMMDD.ViewModels
{
    public abstract class ViewModelBase : NotifyPropertyChanged
    {
        private FrameworkElement _mainPageContentPresenter;
        public FrameworkElement MainPageContentPresenter
        {
            get { return _mainPageContentPresenter; }
            set
            {
                if (value != this._mainPageContentPresenter)
                {
                    this._mainPageContentPresenter = value;
                    OnPropertyChanged();
                }
            }
        }

        public async void GoToSecurityAndMarketPepthPage()
        {
            var model = await BindContext<SecurityAndMarketPepth, SecurityAndMarketPepthViewModel>();
        }

        public async Task<ViewModel> BindContext<View, ViewModel>(Func<ActionResultModel, Task> CallBack = null)
            where ViewModel : IViewModel
            where View : Page
        {
            var model = Activator.CreateInstance<ViewModel>();
            var result = new ActionResultModel();

            await model.BtnAction(async () =>
            {
                Page currentpage = Activator.CreateInstance<View>();
                currentpage.DataContext = model;
                MainPageContentPresenter = currentpage;
            });
            CallBack?.Invoke(result);
            return model;
        }

    }
}
