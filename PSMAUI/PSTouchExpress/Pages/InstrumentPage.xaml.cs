using System;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using NNN.Core.Common.Parameters;
using NNN.Core.Common.Values;
using NNN.Core.Instruments;
using NNN.Core.Instruments.Capabilities;
using NNN.Core.Instruments.Methods;
using NNN.Core.Presentation.Controls;
using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Models;
using NNN.Core.Presentation.MAUI.UserControls;
using Telerik.Maui.Controls;

namespace PSTouchExpress.Pages
{
	public partial class InstrumentPage : ContentView
    {
		public InstrumentPage()
		{
            //Initialize();
            InitializeComponent();

            //var control = new MethodControl();
            //IntrumentContent.Content = control.Content;


            //Content = _stackLayout;

            //control.Method = Method;
            //validationSummary.Method = Method;

            //var _context = this.BindingContext as InstrumentPageModel;

            //_context.ValidationErrors = control.Method.ValidationErrors;

            

            //ShowvalidationSummary(control.Method.ValidationErrors);
            //validationSummary.Content = control.Method.
            Initialize();
        }

        private StackLayout _stackLayout;

        //public static readonly BindableProperty MethodProperty = BindableProperty.Create(nameof(Method), typeof(ParameterGroup),
        //        typeof(InstrumentPage), default(ParameterGroup), BindingMode.TwoWay);

        //public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool),
        //        typeof(InstrumentPage), default(bool), BindingMode.TwoWay);

        //public static readonly BindableProperty ParameterGroupProperty = BindableProperty.Create(nameof(ParameterGroup), typeof(ParameterGroup),
        //        typeof(InstrumentPage), default(ParameterGroup), BindingMode.TwoWay);

        //public static readonly BindableProperty StringLocalizerProperty = BindableProperty.Create(nameof(StringLocalizer), typeof(IStringLocalizer),
        //        typeof(InstrumentPage), default(IStringLocalizer), BindingMode.TwoWay);

        //public static readonly BindableProperty ValidationStringLocalizerProperty = BindableProperty.Create(nameof(ValidationStringLocalizer), typeof(IStringLocalizer),
        //        typeof(InstrumentPage), default(IStringLocalizer), BindingMode.TwoWay);

        //public static readonly BindableProperty ValidationErrorsProperty = BindableProperty.Create(nameof(ValidationErrors), typeof(IEnumerable<ValidationError>),
        //        typeof(InstrumentPage), default(IEnumerable<ValidationError>), BindingMode.TwoWay);
        //public IEnumerable<ValidationError> ValidationErrors
        //{
        //    get => (IEnumerable<ValidationError>)GetValue(ValidationErrorsProperty);
        //    set => SetValue(ValidationErrorsProperty, value);
        //}

        //public ParameterGroup Method
        //{
        //    get => (ParameterGroup)GetValue(MethodProperty);
        //    set => SetValue(MethodProperty, value);
        //}
        //public bool IsReadOnly
        //{
        //    get => (bool)GetValue(IsReadOnlyProperty);
        //    set => SetValue(IsReadOnlyProperty, value);
        //}
        //public ParameterGroup ParameterGroup
        //{
        //    get => (ParameterGroup)GetValue(ParameterGroupProperty);
        //    set => SetValue(ParameterGroupProperty, value);
        //}
        //public IStringLocalizer StringLocalizer
        //{
        //    get => (IStringLocalizer)GetValue(StringLocalizerProperty);
        //    set => SetValue(StringLocalizerProperty, value);
        //}
        //public IStringLocalizer ValidationStringLocalizer
        //{
        //    get => (IStringLocalizer)GetValue(ValidationStringLocalizerProperty);
        //    set => SetValue(ValidationStringLocalizerProperty, value);
        //}

        private void CreateContent()
        {
            //var rangeCapabilities = DefaultCapabilities.GetInstrumentCapabilities(InstrumentModel.NNN4).RangeCapabilities;
            //var peripheralCapabilities = new PeripheralCapabilities { BiPotPresent = true, SupportsIRDropComp = true };
            //var methodCapabilitiesGenerator = new NNN4MethodCapabilitiesGenerator(rangeCapabilities, peripheralCapabilities);

            //bool generated = methodCapabilitiesGenerator.TryGenerate(TechniqueId.MultiStepAmperometry, out var methodCapabilities);
            //ParameterGroup = new MultiStepAmperometryMethod(methodCapabilities)
            //{
            //    MinimumCurrentRange = new MeasuringRange(1, -10),//100pA = 1e-10
            //    MaximumCurrentRange = new MeasuringRange(1, -3),
            //    StartingCurrentRange = new MeasuringRange(100, -6),
            //    ConditioningPotential = 0.0,
            //    ConditioningTime = TimeSpan.FromSeconds(0),
            //    DepositionPotential = 0.0,
            //    DepositionTime = TimeSpan.FromSeconds(0),
            //    EquilibrationTime = TimeSpan.FromSeconds(0)
            //};

            //Method = new NNN.Core.Instruments.Methods.Method(methodCapabilities);
            //ValidationErrors = Method.ValidationErrors;

            //StringLocalizer = new StringLocalizer<NNN.Core.Instruments.Methods.Method>(
            //        new ResourceManagerStringLocalizerFactory(
            //            Options.Create(new LocalizationOptions() { ResourcesPath = "Resources" }),
            //            new NullLoggerFactory()));
            var model = this.BindingContext as InstrumentPageModel;
            if (_stackLayout == null)
            {
                _stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Fill
                };
            }

            foreach (var disposable in _stackLayout.Children.OfType<IDisposable>())
            {
                disposable.Dispose();
            }

            _stackLayout.Children.Clear();

            if (model.ParameterGroup == null) return;
            bool hasRanges = false;

            var GroupGrid = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition(){ Height = GridLength.Auto },
                    new RowDefinition(), //{ Height = new GridLength(60) },
                    new RowDefinition(){ Height = GridLength.Auto }
                }
            };
            var firstStackLayout = new StackLayout();
            if (model.ParameterGroup.TryGetValue(ParameterId.Name, out var nameParameter))
            {
                var container = new StackLayout();

                var nameControl = new ParameterControl
                {
                    Margin = new Thickness(5, 1, 5, 1),
                    StringLocalizer = model.StringLocalizer,
                    Parameter = nameParameter,
                    IsReadOnly = model.IsReadOnly,
                };
                container.Children.Add(nameControl);
                firstStackLayout.Children.Add(container);
            }
            if (model.ParameterGroup.TryGetValue(ParameterId.Notes, out var notesParameter))
            {
                var container = new StackLayout();

                var notesControl = new ParameterControl
                {
                    //IsMultiLine = true,
                    Margin = new Thickness(5, 1, 5, 1),
                    StringLocalizer = model.StringLocalizer,
                    Parameter = notesParameter,
                    IsReadOnly = model.IsReadOnly,
                };
                container.Children.Add(notesControl);
                firstStackLayout.Children.Add(container);
            }

            GroupGrid.Add(firstStackLayout, 0, 0);

            var secondStackLayout = new StackLayout();

            if (model.ParameterGroup.TryGetValue(ParameterId.StartingCurrentRange, out var startingRangeParameter) &&
                model.ParameterGroup.TryGetValue(ParameterId.MinimumCurrentRange, out var minimumRangeParameter) &&
                model.ParameterGroup.TryGetValue(ParameterId.MaximumCurrentRange, out var maximumRangeParameter))
            {
                hasRanges = true;
                var RangeGrid = new Grid()
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) },
                        new ColumnDefinition() { Width = new GridLength(32, GridUnitType.Star) },
                        new ColumnDefinition() { Width = new GridLength(32, GridUnitType.Star) },
                        new ColumnDefinition() { Width = new GridLength(32, GridUnitType.Star) }
                    }
                };
                var Range_HeaderStackLayout = new StackLayout();
                var Range_StartStackLayout = new StackLayout();
                var Range_MinStackLayout = new StackLayout();
                var Range_MaxStackLayout = new StackLayout();

                Range_HeaderStackLayout.Children.Add(new Label() { Text = "i Range" });

                var Range_StartControl = new RangeComboBox()
                {
                    ComboBoxParameter = startingRangeParameter,
                    StringLocalizer = model.StringLocalizer,
                    ValidationStringLocalizer = model.ValidationStringLocalizer
                };
                var Range_StartIconBinding = new Binding("Range_StartIcon");
                Range_StartControl.SetBinding(RangeComboBox.UnitLabelProperty, Range_StartIconBinding);
                Range_StartStackLayout.Children.Add(Range_StartControl);

                var Range_MinControl = new RangeComboBox() { ComboBoxParameter = minimumRangeParameter, StringLocalizer = model.StringLocalizer };
                var Range_minimumIconBinding = new Binding("Range_MinIcon");
                Range_MinControl.SetBinding(RangeComboBox.UnitLabelProperty, Range_minimumIconBinding);
                Range_MinStackLayout.Children.Add(Range_MinControl);

                var Range_MaxControl = new RangeComboBox() { ComboBoxParameter = maximumRangeParameter, StringLocalizer = model.StringLocalizer };
                var Range_MaxIconBinding = new Binding("Range_MaxIcon");
                Range_MaxControl.SetBinding(RangeComboBox.UnitLabelProperty, Range_MaxIconBinding);
                Range_MaxStackLayout.Children.Add(Range_MaxControl);

                RangeGrid.Add(Range_StartStackLayout, 1, 0);
                RangeGrid.Add(Range_MinStackLayout, 2, 0);
                RangeGrid.Add(Range_MaxStackLayout, 3, 0);

                secondStackLayout.Children.Add(Range_HeaderStackLayout);

                secondStackLayout.Children.Add(RangeGrid);
                secondStackLayout.HeightRequest = 65;
            }

            var thirdStackLayout = new StackLayout();

            foreach (var parameterGroup in model.ParameterGroup.Children)
            {
                if (parameterGroup.Id is GroupId.CurrentRange or GroupId.PotentialRange or GroupId.DeviceSpecific ||
                   !parameterGroup.AsList().Any())
                    continue;

                var parameterGroupControl = new ParameterGroupControl
                {
                    StringLocalizer = model.StringLocalizer,
                    ValidationStringLocalizer = model.ValidationStringLocalizer,
                    ParameterGroup = parameterGroup,
                    IsReadOnly = model.IsReadOnly
                };

                thirdStackLayout.Children.Add(parameterGroupControl);
            }
            GroupGrid.Add(thirdStackLayout, 0, 2);
            GroupGrid.Add(secondStackLayout, 0, 1);
            _stackLayout.Children.Add(GroupGrid);
        }

        

        async void Initialize()
        {
            await PSTasks.ActionTask(() =>
            {
                PSMessaging.Subscribe<ValidationModel>(this, "ValidationSummary", async (arg) =>
                {
                    //ShowvalidationSummary(arg.ParameterGroup.ValidationErrors);

                    // X, Y need to be updated based on type/format of the device
                    //await this.TranslateTo(arg.IsPresented ? 255 : -255, 0, 350, Easing.CubicInOut);
                    //await this.RotateTo(360, 2000);
                });
                //this.Unfocused += FlyOutMenu_Unfocused; // still not working             
            });
        }

        private void ContentView_BindingContextChanged(object sender, EventArgs e)
        {
            CreateContent();
            IntrumentContent.Content = _stackLayout;
        }
    }

}