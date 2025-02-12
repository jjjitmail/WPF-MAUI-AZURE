using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using NNN.Core.Presentation.MAUI.Helpers;

namespace PSTouchExpress.Views
{
	public partial class AppBar : ContentView
	{
		public AppBar()
		{
            InitializeComponent();
            //Initialize();            
		}

        async void Initialize()
        {
            await PSTasks.ActionTask(() =>
            {
                InitializeComponent();
            });
        }

        public static readonly BindableProperty TargetObjectProperty = BindableProperty.Create(nameof(TargetObject), typeof(object), typeof(AppBar), null);

        public object TargetObject
        {
            get { return (object)GetValue(TargetObjectProperty); }
            set { SetValue(TargetObjectProperty, value); }
        }

        public void DrawNoiseBlocks()
        {
            //ICanvas _canvas = null;
            //_canvas.StrokeColor = Color.FromArgb(PSColor.DefaultBlueColor);
            //_canvas.StrokeSize = 10;
            //_canvas.StrokeDashPattern = new float[] { 2, 2 };
            //_canvas.DrawLine(0, 0, 0, 180);
        }

        //public static readonly BindableProperty ViewPageProperty =
        //    BindableProperty.Create(nameof(ViewPage), typeof(IView), typeof(AppBar), null);

        //public IView ViewPage
        //{
        //    get => (IView)GetValue(AppBar.ViewPageProperty);
        //    set => SetValue(AppBar.ViewPageProperty, value);
        //}

    }
}