using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NNN.Core.Common.Parameters;
using NNN.Core.Common.Values;
using NNN.Core.Instruments;
using NNN.Core.Instruments.Capabilities;
using NNN.Core.Instruments.Methods;
using NNN.Core.Presentation.MAUI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.Controls
{
    public class MethodControl : ContentView, IDisposable
    {
        public MethodControl()
        {
            UpdateBody();
            //Content = new ScrollView
            //{
            //    VerticalScrollBarVisibility = ScrollBarVisibility.Default,
            //    Content = _stackLayout,
            //    Padding = new Thickness(3)
            //};
        }

        public static readonly BindableProperty MethodProperty = BindableProperty.Create(nameof(Method), typeof(ParameterGroup),
                typeof(MethodControl), default(ParameterGroup), BindingMode.TwoWay );
        
        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool),
                typeof(MethodControl), default(bool), BindingMode.TwoWay);
        
        public static readonly BindableProperty ParameterGroupProperty = BindableProperty.Create(nameof(ParameterGroup), typeof(ParameterGroup),
                typeof(MethodControl), default(ParameterGroup), BindingMode.TwoWay);
        
        public static readonly BindableProperty StringLocalizerProperty = BindableProperty.Create(nameof(StringLocalizer), typeof(IStringLocalizer),
                typeof(ParameterControl), default(IStringLocalizer), BindingMode.TwoWay);

        public static readonly BindableProperty ValidationStringLocalizerProperty = BindableProperty.Create(nameof(ValidationStringLocalizer),
                typeof(IStringLocalizer), typeof(ParameterControl), default(IStringLocalizer), BindingMode.TwoWay);

        public static readonly BindableProperty ValidationErrorsProperty = BindableProperty.Create(nameof(ValidationErrors), typeof(IEnumerable<ValidationError>),
                typeof(ValidationSummary), default(IEnumerable<ValidationError>), BindingMode.TwoWay);
        public IEnumerable<ValidationError> ValidationErrors
        {
            get => (IEnumerable<ValidationError>)GetValue(ValidationErrorsProperty);
            set => SetValue(ValidationErrorsProperty, value);
        }

        public ParameterGroup Method
        {
            get => (ParameterGroup)GetValue(MethodProperty);
            set => SetValue(MethodProperty, value);
        }
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }
        public ParameterGroup ParameterGroup
        {
            get => (ParameterGroup)GetValue(ParameterGroupProperty);
            set => SetValue(ParameterGroupProperty, value);
        }
        public IStringLocalizer StringLocalizer
        {
            get => (IStringLocalizer)GetValue(StringLocalizerProperty);
            set => SetValue(StringLocalizerProperty, value);
        }
        public IStringLocalizer ValidationStringLocalizer
        {
            get => (IStringLocalizer)GetValue(ValidationStringLocalizerProperty);
            set => SetValue(ValidationStringLocalizerProperty, value);
        }

        private StackLayout _stackLayout;

        private void UpdateBody()
        {
            // testing code.. todo: update later...
            var rangeCapabilities = DefaultCapabilities.GetInstrumentCapabilities(InstrumentModel.NNN4).RangeCapabilities;
            var peripheralCapabilities = new PeripheralCapabilities { BiPotPresent = true, SupportsIRDropComp = true };
            var methodCapabilitiesGenerator = new NNN4MethodCapabilitiesGenerator(rangeCapabilities, peripheralCapabilities);

            //bool generated = methodCapabilitiesGenerator.TryGenerate(TechniqueId.LinearSweepVoltammetry, out var methodCapabilities);
            //ParameterGroup = new LinearSweepVoltammetryMethod(methodCapabilities)
            //{
            //    MinimumCurrentRange = new MeasuringRange(1, -10),//100pA = 1e-10
            //    MaximumCurrentRange = new MeasuringRange(1, -3),
            //    StartingCurrentRange = new MeasuringRange(100, -6),
            //    ConditioningPotential = 0.0,
            //    ConditioningTime = TimeSpan.FromSeconds(0),
            //    DepositionPotential = 0.0,
            //    DepositionTime = TimeSpan.FromSeconds(0),
            //    EquilibrationTime = TimeSpan.FromSeconds(0),
            //    BeginPotential = -.5,
            //    EndPotential = .5,
            //    StepPotential = .01,
            //    ScanRate = 1.0
            //};

            bool generated = methodCapabilitiesGenerator.TryGenerate(TechniqueId.MultiStepAmperometry, out var methodCapabilities);
            ParameterGroup = new MultiStepAmperometryMethod(methodCapabilities)
            {
                MinimumCurrentRange = new MeasuringRange(1, -10),//100pA = 1e-10
                MaximumCurrentRange = new MeasuringRange(1, -3),
                StartingCurrentRange = new MeasuringRange(100, -6),
                ConditioningPotential = 0.0,
                ConditioningTime = TimeSpan.FromSeconds(0),
                DepositionPotential = 0.0,
                DepositionTime = TimeSpan.FromSeconds(0),
                EquilibrationTime = TimeSpan.FromSeconds(0)
            };

            Method = new NNN.Core.Instruments.Methods.Method(methodCapabilities);
            ValidationErrors = Method.ValidationErrors;

            StringLocalizer = new StringLocalizer<Core.Instruments.Methods.Method>(
                    new ResourceManagerStringLocalizerFactory(
                        Options.Create(new LocalizationOptions() { ResourcesPath = "Resources" }),
                        new NullLoggerFactory()));

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

            if (ParameterGroup == null) return;
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
            if (Method.TryGetValue(ParameterId.Name, out var nameParameter))
            {
                var container = new StackLayout();

                var nameControl = new ParameterControl
                {
                    Margin = new Thickness(5, 1, 5, 1),
                    StringLocalizer = StringLocalizer,
                    Parameter = nameParameter,
                    IsReadOnly = IsReadOnly,
                };
                container.Children.Add(nameControl);
                firstStackLayout.Children.Add(container);
            }
            if (Method.TryGetValue(ParameterId.Notes, out var notesParameter))
            {
                var container = new StackLayout();

                var notesControl = new ParameterControl
                {
                    //IsMultiLine = true,
                    Margin = new Thickness(5, 1, 5, 1),
                    StringLocalizer = StringLocalizer,
                    Parameter = notesParameter,
                    IsReadOnly = IsReadOnly,
                };
                container.Children.Add(notesControl);
                firstStackLayout.Children.Add(container);
            }

            GroupGrid.Add(firstStackLayout, 0, 0);

            var secondStackLayout = new StackLayout();

            if (Method.TryGetValue(ParameterId.StartingCurrentRange, out var startingRangeParameter) &&
                Method.TryGetValue(ParameterId.MinimumCurrentRange, out var minimumRangeParameter) &&
                Method.TryGetValue(ParameterId.MaximumCurrentRange, out var maximumRangeParameter))
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
                    StringLocalizer = StringLocalizer,
                    ValidationStringLocalizer = ValidationStringLocalizer
                };
                var Range_StartIconBinding = new Binding("Range_StartIcon");
                Range_StartControl.SetBinding(RangeComboBox.UnitLabelProperty, Range_StartIconBinding);
                Range_StartStackLayout.Children.Add(Range_StartControl);               

                var Range_MinControl = new RangeComboBox() { ComboBoxParameter = minimumRangeParameter, StringLocalizer = StringLocalizer };
                var Range_minimumIconBinding = new Binding("Range_MinIcon");
                Range_MinControl.SetBinding(RangeComboBox.UnitLabelProperty, Range_minimumIconBinding);
                Range_MinStackLayout.Children.Add(Range_MinControl);                

                var Range_MaxControl = new RangeComboBox() { ComboBoxParameter = maximumRangeParameter, StringLocalizer = StringLocalizer };
                var Range_MaxIconBinding = new Binding("Range_MaxIcon");
                Range_MaxControl.SetBinding(RangeComboBox.UnitLabelProperty, Range_MaxIconBinding);
                Range_MaxStackLayout.Children.Add(Range_MaxControl);

                RangeGrid.Add(Range_StartStackLayout, 1, 0);
                RangeGrid.Add(Range_MinStackLayout, 2, 0);
                RangeGrid.Add(Range_MaxStackLayout, 3, 0);

                secondStackLayout.Children.Add(Range_HeaderStackLayout);
                
                secondStackLayout.Children.Add(RangeGrid);
                secondStackLayout.HeightRequest = 65;
                
                //RangeGrid.SetBinding(RangeComboBoxContent
                //RangeComboBoxContent
            }

            var thirdStackLayout = new StackLayout();
            //if (hasRanges)
            //    thirdStackLayout.Margin = new Thickness(0, 40, 0, 0);

            //ParameterGroup.ValidationErrors

            foreach (var parameterGroup in ParameterGroup.Children)
            {
                if (parameterGroup.Id is GroupId.CurrentRange or GroupId.PotentialRange or GroupId.DeviceSpecific ||
                   !parameterGroup.AsList().Any())
                    continue;

                var parameterGroupControl = new ParameterGroupControl
                {
                    StringLocalizer = StringLocalizer,
                    ValidationStringLocalizer = ValidationStringLocalizer,
                    ParameterGroup = parameterGroup,
                    IsReadOnly = IsReadOnly
                };

                thirdStackLayout.Children.Add(parameterGroupControl);                
            }
            GroupGrid.Add(thirdStackLayout, 0, 2);
            GroupGrid.Add(secondStackLayout, 0, 1);
            _stackLayout.Children.Add(GroupGrid);

            Content = _stackLayout;
        }
        private void ClearStackPanel()
        {
            if (_stackLayout != null)
            {
                foreach (var disposable in _stackLayout.Children.OfType<IDisposable>())
                {
                    disposable.Dispose();
                }

                _stackLayout.Children.Clear();
            }
        }
        private void UnsubscribePropertyGroupItems()
        {
            if (ParameterGroup != null)
            {
                foreach (var item in ParameterGroup)
                {
                    //item.PropertyChanged -= Parameter_PropertyChanged;
                }
            }
        }

        public void Dispose()
        {
            UnsubscribePropertyGroupItems();
            ClearStackPanel();
        }
    }
}
