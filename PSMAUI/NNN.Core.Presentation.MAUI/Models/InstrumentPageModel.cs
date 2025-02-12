using NNN.Core.Presentation.MAUI.Attributes;
using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Interfaces;
using NNN.Core.Presentation.MAUI.UserControls;
//using PSTouchExpress.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NNN.Core.Presentation.MAUI.Models;
using Range = NNN.Core.Presentation.MAUI.Models.Range;
using NNN.Core.Presentation.MAUI;
using NNN.Core.Instruments;
using NNN.Core.Common.Parameters;
using NNN.Core.Instruments.Capabilities;
using NNN.Core.Instruments.Methods;
using NNN.Core.Common.Values;
using NNN.Core.Presentation.Controls;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging.Abstractions;

namespace NNN.Core.Presentation.MAUI.Models
{
    public class InstrumentPageModel : BasePageModel, IPageModel
    {
        public InstrumentPageModel() : base()
        {
            Initialize();
            InitInstrumentMethod();
        }

        public InstrumentModel SelectedInstrument { get; set; }

        private InstrumentValidationModel _instrumentValidationModel;
        public InstrumentValidationModel InstrumentValidationModel
        {
            get { return _instrumentValidationModel; }
            set
            {
                _instrumentValidationModel = value;
                OnPropertyChanged();
            }
        }

        private ParameterGroup _parameterGroup;
        public ParameterGroup ParameterGroup
        {
            get { return _parameterGroup; }
            set
            {
                _parameterGroup = value;
                OnPropertyChanged();
            }
        }

        private IStringLocalizer _stringLocalizer;
        public IStringLocalizer StringLocalizer
        {
            get { return _stringLocalizer;}
            set { _stringLocalizer = value; OnPropertyChanged(); }
        }
        private IStringLocalizer _validationStringLocalizer;
        public IStringLocalizer ValidationStringLocalizer
        {
            get { return _validationStringLocalizer;}
            set { _validationStringLocalizer = value; OnPropertyChanged(); }
        }
        public bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly;}
            set { _isReadOnly = value; OnPropertyChanged(); }
        }
        private View _methodContentView;
        public View MethodContentView
        {
            get { return _methodContentView; }
            set { _methodContentView = value; OnPropertyChanged(); }
        }

        private StackLayout _stackLayout;

        public void InitInstrumentMethod()
        {
            SelectedInstrument = InstrumentModel.NNN4;

            var rangeCapabilities = DefaultCapabilities.GetInstrumentCapabilities(SelectedInstrument).RangeCapabilities;
            var peripheralCapabilities = new PeripheralCapabilities { BiPotPresent = true, SupportsIRDropComp = true };
            var methodCapabilitiesGenerator = new NNN4MethodCapabilitiesGenerator(rangeCapabilities, peripheralCapabilities);

            bool generated = methodCapabilitiesGenerator.TryGenerate(TechniqueId.MultiStepAmperometry, out var methodCapabilities);
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
            ParameterGroup = new NNN.Core.Instruments.Methods.Method(methodCapabilities);

            ParameterGroup.PropertyChanged += (s, e) =>
            {
                bool boop = true;
            };

            //ValidationErrors = MethodParameterGroup.ValidationErrors;

            StringLocalizer = new StringLocalizer<Core.Instruments.Methods.Method>(
                    new ResourceManagerStringLocalizerFactory(
                        Options.Create(new LocalizationOptions() { ResourcesPath = "Resources" }),
                        new NullLoggerFactory()));

            //if (_stackLayout == null)
            //{
            //    _stackLayout = new StackLayout
            //    {
            //        Orientation = StackOrientation.Vertical,
            //        HorizontalOptions = LayoutOptions.Fill
            //    };
            //}

            //foreach (var disposable in _stackLayout.Children.OfType<IDisposable>())
            //{
            //    disposable.Dispose();
            //}

            //_stackLayout.Children.Clear();

            //if (ParameterGroup == null) return;
            //bool hasRanges = false;

            //var GroupGrid = new Grid()
            //{
            //    RowDefinitions =
            //    {
            //        new RowDefinition(){ Height = GridLength.Auto },
            //        new RowDefinition(), //{ Height = new GridLength(60) },
            //        new RowDefinition(){ Height = GridLength.Auto }
            //    }
            //};
            //var firstStackLayout = new StackLayout();
            //if (MethodParameterGroup.TryGetValue(ParameterId.Name, out var nameParameter))
            //{
            //    var container = new StackLayout();

            //    var nameControl = new ParameterControl
            //    {
            //        Margin = new Thickness(5, 1, 5, 1),
            //        StringLocalizer = StringLocalizer,
            //        Parameter = nameParameter,
            //        IsReadOnly = IsReadOnly,
            //    };
            //    container.Children.Add(nameControl);
            //    firstStackLayout.Children.Add(container);
            //}
            //if (MethodParameterGroup.TryGetValue(ParameterId.Notes, out var notesParameter))
            //{
            //    var container = new StackLayout();

            //    var notesControl = new ParameterControl
            //    {
            //        //IsMultiLine = true,
            //        Margin = new Thickness(5, 1, 5, 1),
            //        StringLocalizer = StringLocalizer,
            //        Parameter = notesParameter,
            //        IsReadOnly = IsReadOnly,
            //    };
            //    container.Children.Add(notesControl);
            //    firstStackLayout.Children.Add(container);
            //}

            //GroupGrid.Add(firstStackLayout, 0, 0);

            //var secondStackLayout = new StackLayout();

            //if (MethodParameterGroup.TryGetValue(ParameterId.StartingCurrentRange, out var startingRangeParameter) &&
            //    MethodParameterGroup.TryGetValue(ParameterId.MinimumCurrentRange, out var minimumRangeParameter) &&
            //    MethodParameterGroup.TryGetValue(ParameterId.MaximumCurrentRange, out var maximumRangeParameter))
            //{
            //    hasRanges = true;
            //    var RangeGrid = new Grid()
            //    {
            //        ColumnDefinitions =
            //        {
            //            new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) },
            //            new ColumnDefinition() { Width = new GridLength(32, GridUnitType.Star) },
            //            new ColumnDefinition() { Width = new GridLength(32, GridUnitType.Star) },
            //            new ColumnDefinition() { Width = new GridLength(32, GridUnitType.Star) }
            //        }
            //    };
            //    var Range_HeaderStackLayout = new StackLayout();
            //    var Range_StartStackLayout = new StackLayout();
            //    var Range_MinStackLayout = new StackLayout();
            //    var Range_MaxStackLayout = new StackLayout();

            //    Range_HeaderStackLayout.Children.Add(new Label() { Text = "i Range" });

            //    var Range_StartControl = new RangeComboBox()
            //    {
            //        ComboBoxParameter = startingRangeParameter,
            //        StringLocalizer = StringLocalizer,
            //        ValidationStringLocalizer = ValidationStringLocalizer
            //    };
            //    var Range_StartIconBinding = new Binding("Range_StartIcon");
            //    Range_StartControl.SetBinding(RangeComboBox.UnitLabelProperty, Range_StartIconBinding);
            //    Range_StartStackLayout.Children.Add(Range_StartControl);

            //    var Range_MinControl = new RangeComboBox() { ComboBoxParameter = minimumRangeParameter, StringLocalizer = StringLocalizer };
            //    var Range_minimumIconBinding = new Binding("Range_MinIcon");
            //    Range_MinControl.SetBinding(RangeComboBox.UnitLabelProperty, Range_minimumIconBinding);
            //    Range_MinStackLayout.Children.Add(Range_MinControl);

            //    var Range_MaxControl = new RangeComboBox() { ComboBoxParameter = maximumRangeParameter, StringLocalizer = StringLocalizer };
            //    var Range_MaxIconBinding = new Binding("Range_MaxIcon");
            //    Range_MaxControl.SetBinding(RangeComboBox.UnitLabelProperty, Range_MaxIconBinding);
            //    Range_MaxStackLayout.Children.Add(Range_MaxControl);

            //    RangeGrid.Add(Range_StartStackLayout, 1, 0);
            //    RangeGrid.Add(Range_MinStackLayout, 2, 0);
            //    RangeGrid.Add(Range_MaxStackLayout, 3, 0);

            //    secondStackLayout.Children.Add(Range_HeaderStackLayout);

            //    secondStackLayout.Children.Add(RangeGrid);
            //    secondStackLayout.HeightRequest = 65;

            //    //RangeGrid.SetBinding(RangeComboBoxContent
            //    //RangeComboBoxContent
            //}

            //var thirdStackLayout = new StackLayout();
            ////if (hasRanges)
            ////    thirdStackLayout.Margin = new Thickness(0, 40, 0, 0);

            ////ParameterGroup.ValidationErrors

            //foreach (var parameterGroup in ParameterGroup.Children)
            //{
            //    if (parameterGroup.Id is GroupId.CurrentRange or GroupId.PotentialRange or GroupId.DeviceSpecific ||
            //       !parameterGroup.AsList().Any())
            //        continue;

            //    var parameterGroupControl = new ParameterGroupControl
            //    {
            //        StringLocalizer = StringLocalizer,
            //        ValidationStringLocalizer = ValidationStringLocalizer,
            //        ParameterGroup = parameterGroup,
            //        IsReadOnly = IsReadOnly
            //    };

            //    thirdStackLayout.Children.Add(parameterGroupControl);
            //}
            //GroupGrid.Add(thirdStackLayout, 0, 2);
            //GroupGrid.Add(secondStackLayout, 0, 1);
            //_stackLayout.Children.Add(GroupGrid);

            //MethodContentView = _stackLayout;
        }



        async void Initialize()
        {
            await PSTasks.ActionTask(() =>
            {
                var pswidth = DeviceDisplay.MainDisplayInfo;

                IsVisibleTechniqueCombo = true;
                TechniqueComboBoxMaximumHeight = 50;
                InstrumentTextColor = Color.FromArgb(PSColor.DefaultLightBlueColor);
                ComboBoxRows = GetComboBoxRows(30);                
                TagTechniqueComboCommand = new Command(OnTagTechniqueComboCommand);
                TagMetryCommand = new Command(OnTagMetryCommand);

                var ranges = RangeItemSources;
                //ranges.Sort();
                MinRangeSelected = ranges.FirstOrDefault(x => x.RangeType == RangeType.nA10);
                StartRangeSelected = ranges.FirstOrDefault(x => x.RangeType == RangeType.µA1);
                MaxRangeSelected = ranges.FirstOrDefault(x => x.RangeType == RangeType.mA100);
            });
        }

        private bool _displayTechniqueTriangleDown;
        public bool DisplayTechniqueTriangleDown
        {
            get
            {
                return _displayTechniqueTriangleDown;
            }
            set
            {
                _displayTechniqueTriangleDown = value;
                OnPropertyChanged();
            }
        }
        async void InitializeUI()
        {
            await PSTasks.ActionTask(async () =>
            {
                await Task.Delay(10);
                IsVisibleTechniqueCombo = false;
            });
        }

        public double RangeComboBoxWidth => AppWidth / 3 - 30;

        private RowDefinitionCollection _comboBoxRows;
        public RowDefinitionCollection ComboBoxRows
        {
            get
            {
                return this._comboBoxRows;
            }

            set
            {
                if (value != this._comboBoxRows)
                {
                    this._comboBoxRows = value;
                    OnPropertyChanged();
                }
            }
        }
        private RowDefinitionCollection GetComboBoxRows(double RowHeight = 30)
        {
            return new RowDefinitionCollection()
            {
                new RowDefinition { Height = RowHeight }
            };
        }
        public ICommand TagMetryCommand { get; private set; }
        async void OnTagMetryCommand(object value)
        {
            await PSTasks.ActionTask(() =>
            {
                //SelectedMetry = (Metry)value;
            });
        }

        public ICommand TagTechniqueComboCommand { get; private set; }
        async void OnTagTechniqueComboCommand(object value)
        {
            await PSTasks.ActionTask(() => 
            {
                IsVisibleTechniqueCombo = !IsVisibleTechniqueCombo;
                if (IsVisibleTechniqueCombo)
                {
                    TechniqueComboBoxMaximumHeight = TechniqueComboBoxMaximumHeight;
                    ComboBoxRows = GetComboBoxRows(TechniqueComboBoxMaximumHeight);
                }
                else
                {
                    ComboBoxRows = GetComboBoxRows();
                    TechniqueComboBoxMaximumHeight = 30;
                }
            });            
        }

        public async Task BtnPressAction(Func<Task> func)
        {
            await func();
            InitializeUI();
            await Task.Delay(10);
            await PSTasks.ActionTask(() =>
            {
                InstrumentTextColor = DefaultWhiteTextColor;
                InstrumentFontAttributes = DefaultSelectedFontAttributes;                
            });
        }
        public string InstrumentText { get; set; } = "Instrument";
        public string InstrumentIcon { get; set; } = "\uE9E9";

        public Color InstrumentTextColor { get; set; }

        private FontAttributes _instrumentFontAttributes;

        public FontAttributes InstrumentFontAttributes
        {
            get
            {
                return _instrumentFontAttributes;
            }
            set
            {
                _instrumentFontAttributes = value;
                OnPropertyChanged();
            }
        }

        private double _techniqueComboBoxMaximumHeight;

        public double TechniqueComboBoxMaximumHeight
        {
            get 
            {
                if (_techniqueComboBoxMaximumHeight < 30)
                {
                    _techniqueComboBoxMaximumHeight = 30;
                }
                else if (IsVisibleTechniqueCombo)
                {
                    //int HeaderCount = TechniqueItemSources.Where(x => x.MetryList.Count > 0).Count();
                    //int MetryCount = TechniqueItemSources.SelectMany(x => x.MetryList).Count();

                    int HeaderCount = TechniqueItemSources.Where(x => x.Count > 0).Count();
                    int MetryCount = TechniqueItemSources.SelectMany(x => x).Count();

                    _techniqueComboBoxMaximumHeight = HeaderCount * 60 + MetryCount * 40;
                }
                
                return _techniqueComboBoxMaximumHeight; 
            }
            set { _techniqueComboBoxMaximumHeight = value; OnPropertyChanged(); }
        }

        private bool _isVisibleTechniqueCombo;
        public bool IsVisibleTechniqueCombo
        {
            get
            {
                return _isVisibleTechniqueCombo;
            }
            set
            {
                _isVisibleTechniqueCombo = value;
                OnPropertyChanged();
            }
        }

        private Method _selectedMethod;
        public Method SelectedMethod
        {
            get
            {
                if (_selectedMethod == null)
                {
                    var itemSelected = TechniqueItemSources.FirstOrDefault(x => x.FirstOrDefault(y => y.IsSelected) != null)?
                        .FirstOrDefault(z=> z.IsSelected);
                    if (itemSelected != null)
                    {
                        return itemSelected;
                    }
                }
                return _selectedMethod;
            }
            set
            {
                _selectedMethod = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<ValidationError> _ValidationErrors;
        public IEnumerable<ValidationError> ValidationErrors 
        {
            get { return _ValidationErrors; }
            set { _ValidationErrors = value; OnPropertyChanged(); }
        }

        private Range _minRangeSelected;
        public Range MinRangeSelected
        {
            get { return _minRangeSelected; }
            set { _minRangeSelected = value; OnPropertyChanged(); }
        }
        private Range _startRangeSelected;
        public Range StartRangeSelected
        {
            get { return _startRangeSelected; }
            set { _startRangeSelected = value; OnPropertyChanged(); }
        }
        private Range _maxRangeSelected;
        public Range MaxRangeSelected
        {
            get { return _maxRangeSelected; }
            set { _maxRangeSelected = value; OnPropertyChanged(); }
        }
        public List<Range> RangeItemSources => Enum.GetValues(typeof(RangeType)).Cast<RangeType>().Select(t=> new Range { RangeType = t }).ToList();

        public ObservableCollection<MethodGroup> TechniqueItemSources =>
           new ObservableCollection<MethodGroup>()
           {
               new MethodGroup(MethodGroupType.VoltammetricTechniques, new List<Method>()
               {
                   new Method() { MethodId = 101, MethodType = MethodType.LinearSweepVoltammetry }
               }),
               new MethodGroup(MethodGroupType.AmperometricTechniques, new List<Method>()
               {
                   new Method() { MethodId = 201, MethodType = MethodType.Chronoamperometry },
                    new Method() { MethodId = 202, MethodType = MethodType.LinearSweepVoltammetry, IsSelected = true,
                    BackgroundColor = Color.FromRgba(PSColor.DefaultBlueColor), 
                           TextColor = Color.FromRgba(PSColor.DefaultWhiteColor)},
                     new Method() { MethodId = 203, MethodType = MethodType.MultistepAmperometry }
               })
           };
    }

    

    


    
    
}
