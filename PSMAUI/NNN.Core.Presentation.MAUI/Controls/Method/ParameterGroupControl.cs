using Microsoft.Extensions.Localization;
using NNN.Core.Common.Parameters;
using NNN.Core.Common.Values;
using NNN.Core.Instruments;
using NNN.Core.Instruments.Capabilities;
using NNN.Core.Instruments.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNN.Core.Presentation.Controls;
using NNN.Core.Presentation.MAUI.Controls.Parameters;
using NNN.Core.Presentation.MAUI.Helpers;
using System.ComponentModel;
using NNN.Core.Presentation.MAUI.Models;

namespace NNN.Core.Presentation.Controls
{
    public class ParameterGroupControl : ContentView, IDisposable
    {
        public static readonly BindableProperty HeaderIndexProperty = BindableProperty.Create(nameof(HeaderIndex), typeof(int),
            typeof(ParameterGroupControl), default(int), BindingMode.TwoWay);

        public static readonly BindableProperty ParameterGroupProperty = BindableProperty.Create(nameof(ParameterGroup), typeof(ParameterGroup),
            typeof(ParameterGroupControl), default(ParameterGroup), BindingMode.TwoWay, propertyChanged: OnParameterGroupChanged);

        private static void OnParameterGroupChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ParameterGroupControl)bindable).UpdateParameterList();
        }

        public static readonly BindableProperty StringLocalizerProperty = BindableProperty.Create(nameof(StringLocalizer), typeof(IStringLocalizer),
            typeof(ParameterGroupControl), default(IStringLocalizer), BindingMode.TwoWay);

        public static readonly BindableProperty ValidationStringLocalizerProperty = BindableProperty.Create(nameof(ValidationStringLocalizer), typeof(IStringLocalizer),
            typeof(ParameterGroupControl), default(IStringLocalizer), BindingMode.TwoWay);

        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool),
            typeof(ParameterGroupControl), default(bool), BindingMode.TwoWay, propertyChanged: OnIsReadOnlyChanged);

        private static void OnIsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == oldValue ||
                newValue is not bool boolValue ||
                bindable is not ParameterGroupControl groupControl) return;

            var stackLayout = groupControl._stackLayout;

            if (stackLayout == null || stackLayout.Children.Count == 0) return;

            foreach (var child in stackLayout.Children)
            {
                if (child is ParameterControl methodParameter)
                {
                    methodParameter.IsReadOnly = boolValue;
                }
                else if (child is ParameterGroupControl parameterGroup)
                {
                    parameterGroup.IsReadOnly = boolValue;
                }
            }
        }

        public int HeaderIndex
        {
            get => (int)GetValue(HeaderIndexProperty);
            set => SetValue(HeaderIndexProperty, value);
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
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private void UpdateParameterList()
        {
            if (_expander == null)
            {
                ConstructExpander();
            }

            CreateOrClearStackPanel();

            if (ParameterGroup == null) return;

            //_expander.IsExpanded = ParameterGroup.Capabilities.CollapsePredicate == null || !ParameterGroup.Capabilities.CollapsePredicate(ParameterGroup);

            ConstructParameterControls();
            try
            {
                PrepareTheExpander();
                _expander.Children.Add(_stackLayout);
                var errors = ParameterGroup.ValidationErrors;

                if (ParameterGroup.ValidationErrors.Count > 0)
                {
                    PSTasks.ActionTask(() =>
                    {
                        PSMessaging.Publish<ValidationModel>(new ValidationModel { ParameterGroup = ParameterGroup }, "ValidationSummary");
                    });
                }

                Content = _expander;
            }
            catch (Exception err)
            {

                throw;
            }            

            //SetExpanderVisibility();
        }
        private void PrepareTheExpander()
        {
            var container = ((StackLayout)_expander).Children;
            if (container.Count > 1)
            {
                for (int i = 1; i < container.Count; i++) // for exception: The specified child already has a parent
                {
                    ((StackLayout)_expander).Children.RemoveAt(i);
                }
            }
        }

        private void ConstructParameterControls()
        {
            foreach (var parameter in ParameterGroup)
            {
                if (parameter.Id.EndsWith(ParameterId.OCP) ||
                    parameter.Id.StartsWith(ParameterId.Use) ||
                    parameter.Id.EndsWith(ParameterId.Range)) continue;

                switch (parameter.Type)
                {
                    case ParameterType.ParameterGroup:
                    {
                        if (parameter.IsMultiValue) AddParameter(parameter);
                        var i = 1;
                        foreach (var parameterGroup in parameter.Values.Select(p => p.AsParameterGroup()))
                        {
                            if (!parameterGroup.AsList().Any()) continue;

                            try
                            {
                                var parameterGroupControl = new ParameterGroupControl();
                                parameterGroupControl.HeaderIndex = i++;
                                parameterGroupControl.StringLocalizer = StringLocalizer;
                                parameterGroupControl.ValidationStringLocalizer = ValidationStringLocalizer;
                                parameterGroupControl.ParameterGroup = parameterGroup;
                                parameterGroupControl.IsReadOnly = IsReadOnly;

                                //var parameterGroupControl = new ParameterGroupControl
                                //{
                                //    HeaderIndex = i++,
                                //    StringLocalizer = StringLocalizer,
                                //    ValidationStringLocalizer = ValidationStringLocalizer,
                                //    ParameterGroup = parameterGroup,
                                //    IsReadOnly = IsReadOnly
                                //};
                                _stackLayout.Children.Add(parameterGroupControl);
                            }
                            catch (Exception err)
                            {

                                throw;
                            }
                            
                        }

                        break;
                    }
                    case ParameterType.Action:
                    case ParameterType.Boolean:
                    case ParameterType.Integer:
                    case ParameterType.Double:
                    case ParameterType.String:
                    case ParameterType.MeasuringRange:
                    AddParameter(parameter);
                    break;
                }
            }
        }

        private void AddParameter(Parameter parameter)
        {
            var ocpParameter = ParameterGroup.AsList().FirstOrDefault(x => x.Id == parameter.Id + ParameterId.OCP);
            var enablerParameter = ParameterGroup.AsList().FirstOrDefault(x => x.Id == ParameterId.Use + parameter.Id);
            var rangeParameter = ParameterGroup.AsList().FirstOrDefault(x => x.Id == parameter.Id + ParameterId.Range);

            _stackLayout.Children.Add(new ParameterControl
            {
                OCPParameter = ocpParameter,
                EnablerParameter = enablerParameter,
                StringLocalizer = StringLocalizer,
                ValidationStringLocalizer = ValidationStringLocalizer,
                IsReadOnly = IsReadOnly,
                RangeParameter = rangeParameter,
                Parameter = parameter,
            });

            parameter.PropertyChanged += Parameter_PropertyChanged; // this triggers the entry changes
        }

        private void Parameter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is not Parameter parameter) return;

            if (e.PropertyName == nameof(Parameter.Values) && parameter.Type == ParameterType.ParameterGroup)
            {
                UpdateParameterList();
            }            
        }

        private void UnsubscribePropertyGroupItems()
        {
            if (ParameterGroup != null)
            {
                foreach (var item in ParameterGroup)
                {
                    item.PropertyChanged -= Parameter_PropertyChanged;
                }
            }
        }

        private void CreateOrClearStackPanel()
        {
            if (_stackLayout == null)
            {
                _stackLayout = new StackLayout
                {
                    Margin = new Thickness(10, 0, 10, 0),
                    //Orientation = Orientation.Vertical,
                };
            }
            else
            {
                UnsubscribePropertyGroupItems();
                ClearStackPanel();
            }
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

        private void ConstructExpander()
        {
            _expander = new StackLayout
            {
                //HorizontalAlignment = HorizontalAlignment.Stretch,
                //HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 1, 0, 1)
            };

            _validatorControl = new()
            {
                //Foreground = new SolidColorBrush(Microsoft.UI.Colors.Black),
                StringLocalizer = StringLocalizer,
                //VerticalAlignment = VerticalAlignment.Center,
                ValidationControlType = ValidationControlType.Header,
                ValidationStringLocalizer = ValidationStringLocalizer
            };
            _validatorControl.SetBinding(IsVisibleProperty, new Binding
            {
                Source = _expander,
                //Path = new PropertyPath(nameof(Expander.IsExpanded)),
                //Converter = new BoolNegationConverter(),
                Mode = BindingMode.OneWay
            });
            var validationBinding = new Binding
            {
                Source = ParameterGroup,
                //Path = new PropertyPath(nameof(ParameterGroup.ValidationErrors)),
                Mode = BindingMode.OneWay
            };
            _validatorControl.SetBinding(ValidationControl.ValidationErrorsProperty, validationBinding);

            Grid grid = new() { ColumnSpacing = 5 };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            var headerLabel = new Label
            {
                FontSize = 14,
                Text = " > " + GetHeaderLabel(ParameterGroup) + (HeaderIndex > 0 ? $" {HeaderIndex}" : ""),
                BackgroundColor = Color.FromArgb(PSColor.DefaultBlueColor),
                TextColor = Color.FromArgb(PSColor.DefaultWhiteColor),
                 Padding = new Thickness(5, 5, 5, 5)
            };
            //var headerTextBinding = new Binding
            //{
            //    Source = this,
            //    Path = new PropertyPath(nameof(HeaderText)),
            //    Mode = BindingMode.OneWay
            //};
            //headerTextBlock.SetBinding(TextBlock.TextProperty, headerTextBinding);

            grid.Children.Add(headerLabel);
            grid.Children.Add(_validatorControl);
            Grid.SetColumn(_validatorControl, 1);

            _expander.Children.Add(grid);
        }

        private string GetHeaderLabel(ParameterGroup parameterGroup)
        {
            if (parameterGroup == null)
            {
                return null;
            }

            string _label = "Default Header";
            try
            {
                _label = StringLocalizer?.GetString($"{parameterGroup.Id}_Label") ?? parameterGroup.Id;
            }
            catch (Exception err)
            {
                //
            }
            return _label;

            //return StringLocalizer?.GetString($"{parameterGroup.Id}_Label") ?? parameterGroup.Id;
        }

        public ParameterGroupControl()
        {
            //var rangeCapabilities = DefaultCapabilities.GetInstrumentCapabilities(InstrumentModel.NNN4).RangeCapabilities;
            //var peripheralCapabilities = new PeripheralCapabilities { BiPotPresent = true, SupportsIRDropComp = true };
            //var methodCapabilitiesGenerator = new NNN4MethodCapabilitiesGenerator(rangeCapabilities, peripheralCapabilities);
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

            //foreach (var item in ParameterGroup)
            //{
            //    if (item.Capabilities.Type == ParameterType.String)
            //    {
                    
            //    }
            //}
        }
        public void Dispose()
        {
            UnsubscribePropertyGroupItems();
            ClearStackPanel();
        }

        private StackLayout _stackLayout;
        private StackLayout _expander;
        private ValidationControl _validatorControl;
    }
}
