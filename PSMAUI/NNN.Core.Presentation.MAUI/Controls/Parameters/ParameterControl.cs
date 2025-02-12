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
using NNN.Core.Presentation.Extensions;
using Microsoft.Extensions.Localization;
using NNN.Core.Presentation.MAUI.UserControls;
using NNN.Core.Presentation.MAUI;

namespace NNN.Core.Presentation.Controls
{
    public sealed class ParameterControl : ContentView, IDisposable
    {
        public ParameterControl()
        {
            //this.DefaultStyleKey = typeof(ParameterControl);
            //this.FontSize = 11;
            this.Margin = new Thickness(0, 1, 0, 1);

            CreateGrid();
            Content = _grid;
        }

        public Parameter EnablerParameter
        {
            get => (Parameter)GetValue(EnablerParameterProperty);
            set => SetValue(EnablerParameterProperty, value);
        }

        //public ParameterHeaderPosition HeaderPosition = ParameterHeaderPosition.Left;

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }
       
        public bool IsMultiLine
        {
            get => (bool)GetValue(IsMultiLineProperty);
            set => SetValue(IsMultiLineProperty, value);
        }


        public Parameter OCPParameter
        {
            get => (Parameter)GetValue(OCPParameterProperty);
            set => SetValue(OCPParameterProperty, value);
        }

        public Parameter Parameter
        {
            get => (Parameter)GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }

        public ParameterGroup ParameterGroup
        {
            get => (ParameterGroup)GetValue(ParameterGroupProperty);
            set => SetValue(ParameterGroupProperty, value);
        }

        public Parameter RangeParameter
        {
            get => (Parameter)GetValue(RangeParameterProperty);
            set => SetValue(RangeParameterProperty, value);
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

        public ParameterGroup Method
        {
            get => (ParameterGroup)GetValue(MethodProperty);
            set => SetValue(MethodProperty, value);
        }

        #region Properties
        public static readonly BindableProperty EnablerParameterProperty = BindableProperty.Create(nameof(EnablerParameter), typeof(Parameter),
                typeof(ParameterControl), default(Parameter), BindingMode.TwoWay);

        public static readonly BindableProperty IsMultiLineProperty = BindableProperty.Create(nameof(IsMultiLine), typeof(bool),
                typeof(ParameterControl), default(bool), BindingMode.TwoWay);

        public static readonly BindableProperty OCPParameterProperty = BindableProperty.Create(nameof(OCPParameter), typeof(Parameter),
                typeof(ParameterControl), default(Parameter), BindingMode.TwoWay);

        public static readonly BindableProperty StringLocalizerProperty = BindableProperty.Create(nameof(StringLocalizer), typeof(IStringLocalizer),
                typeof(ParameterControl), default(IStringLocalizer), BindingMode.TwoWay);

        public static readonly BindableProperty ValidationStringLocalizerProperty = BindableProperty.Create(nameof(ValidationStringLocalizer), typeof(IStringLocalizer),
                typeof(ParameterControl), default(IStringLocalizer), BindingMode.TwoWay);

        public static readonly BindableProperty ParameterGroupProperty = BindableProperty.Create(nameof(ParameterGroup), typeof(ParameterGroup),
                typeof(ParameterControl), default(ParameterGroup), BindingMode.TwoWay);

        public static readonly BindableProperty ParameterProperty = BindableProperty.Create(nameof(Parameter), typeof(Parameter),
                typeof(ParameterControl), default(Parameter), BindingMode.TwoWay, propertyChanged: Parameter_PropertyChanged);

        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool),
                typeof(ParameterControl), default(bool), BindingMode.TwoWay, propertyChanged: OnIsReadOnlyChanged);

        public static readonly BindableProperty MethodProperty = BindableProperty.Create(nameof(Method), typeof(ParameterGroup),
                typeof(ParameterControl), default(ParameterGroup), BindingMode.TwoWay);

        public static readonly BindableProperty RangeParameterProperty = BindableProperty.Create(nameof(RangeParameter), typeof(Parameter),
                typeof(ParameterControl), default(Parameter), BindingMode.TwoWay, propertyChanged: RangePropertyChanged);

        private static void RangePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is not bool isReadOnly ||
                bindable is not ParameterControl control) return;

            control.CreateContent();
        }

        private static void OnIsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is not bool isReadOnly ||
                bindable is not ParameterControl control) return;

            control.EnableControls(control._body, !isReadOnly);
            control.EnableControls(control._header, !isReadOnly);
        }
        private void EnableControls(Grid grid, bool isEnabled)
        {
            if (grid != null) grid.Children.OfType<VisualElement>().ToList().ForEach(e => e.IsEnabled = isEnabled);
        }

                #endregion

        public void CreateContent()
        {
            if (Parameter == null) return;

            _header.Children.Clear();
            _body.Children.Clear();

            _stackLayout = new StackLayout();

            if (EnablerParameter != null)
            {
                //var enablerCheckBox = new CheckBoxControl
                //{
                //    CheckBoxContent = GetLabel(Parameter),
                //    CheckBoxParameter = EnablerParameter
                //};
                //if (!IsReadOnly)
                //{
                //    enablerCheckBox.IsCheckedChanged += (s, e) =>
                //    {
                //        EnableControls(_body, ((CheckBoxControl)s).IsChecked);
                //    };
                //}
                //_header.Children.Add(enablerCheckBox);
            }

            var hasEnum = Parameter.Capabilities.IsEnum;
            //if (hasEnum && Parameter.Type is not ParameterType.Action)
            //{
            //    ComboBoxControl comboBox = new() { StringLocalizer = StringLocalizer, ComboBoxParameter = Parameter };
            //    _body.Children.Add(comboBox);
            //    Grid.SetColumnSpan(comboBox, 3);
            //}
            
            switch (Parameter.Type)
            {
                case ParameterType.Boolean:
                    if (!Parameter.IsMultiValue)
                    {
                        string _labelValue = GetLabel(Parameter);
                        var _checkBoxLabel = new LabelCheckBox();
                        _checkBoxLabel.Parameter = Parameter;
                        _checkBoxLabel.Title = _labelValue;
                    _stackLayout.Children.Add(_checkBoxLabel);
                }
                break;
                case ParameterType.Integer:
                case ParameterType.Double:
                case ParameterType.ParameterGroup:
                if (EnablerParameter == null) _header.Children.Add(CreateHeader());
                if (!hasEnum) // Group Only
                {
                    string _labelValue = GetLabel(Parameter);

                    if (Parameter.Id.EndsWith(ParameterId.MinimumCurrentLimit) ||
                        Parameter.Id.EndsWith(ParameterId.MaximumCurrentLimit) ||
                        Parameter.Id.EndsWith(ParameterId.IRDropCompRes))
                    {
                        var _checkBoxLabelEntry = new CheckBoxLabelEntry();
                        _checkBoxLabelEntry.Parameter = Parameter;
                        _checkBoxLabelEntry.Title = _labelValue;
                        _stackLayout.Children.Add(_checkBoxLabelEntry);
                    }
                    else
                    {
                        var error = Parameter.ValidationErrors;
                        var errorMessage = "";
                        if (error.Count > 0)
                        {
                            errorMessage = error.ToString();
                        }
                        var _label = new LabelEntryNumberic()
                        {
                            Parameter = Parameter,
                            Title = _labelValue,
                            ErrorLabel = errorMessage
                        };

                        if (Parameter.IsMultiValue) // Levels
                        {
                            //todo: check level
                            var tt = Parameter;
                        }
                        
                        _stackLayout.Children.Add(_label);
                    }
                }

                if (OCPParameter != null)
                {
                    // Temporary disabled
                    //var ocpControl = new OCPControl { OCPParameter = OCPParameter };
                    //_body.Children.Add(ocpControl);
                    //Grid.SetColumn(ocpControl, 2);
                }

                if (RangeParameter != null)
                {
                    var rangeControl = new RangeComboBox()
                    { ComboBoxParameter = RangeParameter, StringLocalizer = StringLocalizer };
                    _stackLayout.Children.Add(rangeControl);
                }

                break;
                case ParameterType.String:
                if (EnablerParameter == null) _header.Children.Add(CreateHeader());
                if (!hasEnum) // static control
                {
                    string _labelValue = GetLabel(Parameter);
                    var _label = new LabelEntryH
                    {
                        Parameter = Parameter,
                        Title = _labelValue,
                        UnitLabel = "" //Parameter.Unit;
                    };

                    _stackLayout.Children.Add(_label);
                }
                break;
                case ParameterType.Action:
                //_header.Children.Add(CreateActionButtons());
                //Grid.SetColumnSpan(_header, 2);
                break;
                default:
                throw new ArgumentOutOfRangeException();
            }

            //var validationControl = new ValidationControl
            //{
            //    StringLocalizer = StringLocalizer,
            //    ValidationControlType = ValidationControlType.Parameter,
            //    ValidationStringLocalizer = ValidationStringLocalizer,
            //};
            //validationControl.SetBinding(ValidationControl.ValidationErrorsProperty,
            //    new Binding
            //    {
            //        Source = Parameter,
            //        Path = new PropertyPath(nameof(Parameter.ValidationErrors)),
            //        Mode = BindingMode.OneWay
            //    });
            //_header.Children.Add(validationControl);
            //Grid.SetColumn(validationControl, 1);

            //_header.HorizontalAlignment = Parameter.Type == ParameterType.Action ? HorizontalAlignment.Center : HorizontalAlignment.Left;
            //_header.Content = headerGrid;
            //_body.Content = bodyGrid;


            if (EnablerParameter != null) EnableControls(_body, EnablerParameter.Value!.AsBoolean());

            UpdateHeaderGridSection();
            UpdateBodyGridSection();
       
            Content = _stackLayout;
        }

        private Label CreateHeader()
        {
            return new Label()
            {
                 
                Text = GetLabel(Parameter),
                //FontSize = FontSize,
                //HorizontalAlignment = HorizontalAlignment.Stretch,
                //VerticalAlignment = VerticalAlignment.Center
            };
        }

        private Grid CreateCheckBoxArray()
        {
            Grid grid = new();
            for (var i = 0; i < Parameter.Values.Count; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                //CheckBox chk = new()
                //{
                //    Content = $"d{i}",
                //    FontSize = FontSize,
                //    Height = 18
                //};
                //chk.Click += Chk_Click;
                //var chkBinding = new Binding
                //{
                //    Source = Parameter,
                //    Path = new PropertyPath($"{nameof(Parameter.Values)}[{i}]"),
                //    Mode = BindingMode.TwoWay
                //};
                //chk.SetBinding(ToggleButton.IsCheckedProperty, chkBinding);
                //grid.Children.Add(chk);
                //Grid.SetColumn(chk, i);
            }

            return grid;
        }

        private StackLayout _stackLayout;

        private void CreateGrid()
        {
            // Create a grid if it doesn't exist yet
            if (_grid == null)
            {
                _grid = new Grid { ColumnSpacing = 3 };
            }

            // Clean up the grid
            _grid.RowDefinitions.Clear();
            _grid.ColumnDefinitions.Clear();

            // Create rows or columns depending on the header position
            //switch (HeaderPosition)
            //{
            //    case ParameterHeaderPosition.Left:
            //    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Auto) });
            //    _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });
            //    _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.2, GridUnitType.Star) });
            //    break;

            //    case ParameterHeaderPosition.Top:
            //    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Auto) });
            //    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Auto) });
            //    break;

            //    default:
            //    throw new InvalidOperationException("Invalid HeaderPosition");
            //}
             
            // Put the header and body in the correct row/column
            UpdateHeaderGridSection();
            UpdateBodyGridSection();
        }

        private void UpdateHeaderGridSection()
        {
            if (_header == null)
            {
                _header = new() { ColumnSpacing = 5 };
                _header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                _header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                _grid.Children.Add(_header);
            }
            //SetElementGridSection(_header, 0);
        }

        private void UpdateBodyGridSection()
        {
            if (_body == null)
            {
                _body = new() { ColumnSpacing = 5 };
                _body.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                _body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(85) });
                _body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                _body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                _body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                _grid.Children.Add(_body);
            }

            //SetElementGridSection(_body, 1);
        }


        private static void Parameter_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue ||
                newValue is not Parameter parameter ||
                bindable is not ParameterControl control) return;

            control.CreateContent();
            control.BindParameters(oldValue as Parameter, parameter);
        }
        private void BindParameters(Parameter oldParameter, Parameter newParameter)
        {
            //if (oldParameter != null) { oldParameter.PropertyChanged -= Parameter_PropertyChanged; }
            //newParameter.PropertyChanged += Parameter_PropertyChanged;

            this.IsVisible = newParameter.IsVisible ? true : false;
        }

        private string GetLabel(Parameter parameter)
        {
            if (StringLocalizer == null)
            {
                return parameter.Id;
            }
            string _label = "Default";
            try
            {
                _label = StringLocalizer.GetNestedString(parameter, "Label");
            }
            catch (Exception err)
            {
                //
            }
            return _label;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        private Grid _grid;
        private Grid _header;
        private Grid _body;
    }
}
