using System;
using NNN.Core.Presentation.MAUI.Helpers;
//using PSTouchExpress.Models;

namespace PSTouchExpress.Views
{    
    public partial class PlotDataView
    {        
        public PlotDataView()
		{
            Initialize();

        }

        async void Initialize()
        {
            await PSTasks.ActionTask(() => 
            {
                InitializeComponent();
                //var model = (DataPageModel)this.BindingContext;
                //this.TranslationY = 310;
                //MessagingCenter.Subscribe<PlotDataViewStatus>(this, "PlotDataView", async (arg) =>
                //{
                //    await this.TranslateTo(0, 310, 220, Easing.Linear);
                //});
            });
        }
	}
    //public class PlotDataViewStatus
    //{
    //    public bool IsPresented { get; set; }
    //}
}