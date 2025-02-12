using NNN.Core.Presentation.MAUI.Helpers;
using System.Collections;
using Range = NNN.Core.Presentation.MAUI.Models.Range;
using System.ComponentModel;
using NNN.Core.Common.Parameters;
using Microsoft.Extensions.Localization;
using NNN.Core.Presentation.MAUI.Models;
using Telerik.Maui.Controls;

namespace NNN.Core.Presentation.MAUI.UserControls;

public partial class RangeComboBox : ContentView
{
    public static readonly BindableProperty SelectedItemBgColorProperty = BindableProperty.Create(nameof(SelectedItemBgColor),
        typeof(Color), typeof(RangeComboBox), default(Color), BindingMode.TwoWay);

    public static readonly BindableProperty UnitLabelProperty = BindableProperty.Create(nameof(UnitLabel),
        typeof(string), typeof(RangeComboBox), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty ComboBoxParameterProperty = BindableProperty.Create(nameof(ComboBoxParameter),
        typeof(Parameter), typeof(RangeComboBox), default(Parameter), BindingMode.TwoWay, propertyChanged: OnComboBoxParameterChanged);

    public static readonly BindableProperty StringLocalizerProperty = BindableProperty.Create(nameof(StringLocalizer),
        typeof(IStringLocalizer), typeof(RangeComboBox), default(IStringLocalizer), BindingMode.TwoWay);

    public static readonly BindableProperty psItemsSourceProperty = BindableProperty.Create(nameof(psItemsSource),
        typeof(IEnumerable), typeof(RangeComboBox), default(IEnumerable), BindingMode.TwoWay);

    public static readonly BindableProperty psSelectedItemProperty = BindableProperty.Create(nameof(psSelectedItem),
        typeof(Range), typeof(RangeComboBox), default(Range), BindingMode.TwoWay);

    public static readonly BindableProperty psHeightProperty = BindableProperty.Create(nameof(psHeight),
        typeof(double), typeof(RangeComboBox), default(double), BindingMode.TwoWay);

    public static readonly BindableProperty ValidationStringLocalizerProperty = BindableProperty.Create(nameof(ValidationStringLocalizer),
        typeof(IStringLocalizer), typeof(RangeComboBox), default(IStringLocalizer), BindingMode.TwoWay);

    private static void OnComboBoxParameterChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is not Parameter parameter ||
                bindable is not RangeComboBox control) return;        

        List<Range> RangeItemSources = Enum.GetValues(typeof(RangeType)).Cast<RangeType>().Select(t=> new Range { RangeType = t }).ToList();

        var r_result = parameter.Enum.Join(RangeItemSources,
            a => a.AsMeasuringRange().ToString(),
            range => range.Value.ToString(),
            (_enum, range) => range).ToList();

        control.psSelectedItem = r_result.First(); // todo: data from viewmodel
        control.psSelectedItem.SelectionName = control.psSelectedItem.Name;
        //control.RangeSelectedLbl1.Text = r_result.First().Name; // todo: data from viewmodel
        control.CollectionViewRoot.SelectedItem = control.psSelectedItem;
        var selectedItem = r_result.FirstOrDefault(t => t.IsSelected); // todo: data from viewmodel
        if (selectedItem != null)
        {
            control.CollectionViewRoot.SelectedItem = selectedItem;
            control.psSelectedItem = selectedItem;
            //control.RangeSelectedLbl1.Text = selectedItem.Name;
        }
        control.psItemsSource = r_result;
    }

    public IStringLocalizer ValidationStringLocalizer
    {
        get => (IStringLocalizer)GetValue(ValidationStringLocalizerProperty);
        set => SetValue(ValidationStringLocalizerProperty, value);
    }

    public Color SelectedItemBgColor
    {
        get => (Color)GetValue(SelectedItemBgColorProperty);
        set => SetValue(SelectedItemBgColorProperty, value);
    }

    public string UnitLabel
    {
        get => (string)GetValue(UnitLabelProperty);
        set => SetValue(UnitLabelProperty, value);
    }
    public Parameter ComboBoxParameter
    {
        get => (Parameter)GetValue(ComboBoxParameterProperty);
        set => SetValue(ComboBoxParameterProperty, value);
    }

    public IStringLocalizer StringLocalizer
    {
        get => (IStringLocalizer)GetValue(StringLocalizerProperty);
        set => SetValue(StringLocalizerProperty, value);
    }

    public IEnumerable psItemsSource
    {
        get => (IEnumerable)GetValue(psItemsSourceProperty);
        set => SetValue(psItemsSourceProperty, value);
    }

    public Range psSelectedItem
    {
        get
        {
            return (Range)GetValue(psSelectedItemProperty);
        }
        set
        {
            SetValue(psSelectedItemProperty, value);
            OnPropertyChanged("psSelectedItem");
        }
    }

    public double psHeight
    {
        get
        {
            return (double)GetValue(psHeightProperty);
        }
        set
        {
            SetValue(psHeightProperty, value);
        }
    }
    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        
        if (propertyName == psItemsSourceProperty.PropertyName)
        {
            CollectionViewRoot.ItemsSource = psItemsSource;            
        }
        else if (propertyName == psSelectedItemProperty.PropertyName)
        {
            CollectionViewRoot.BackgroundColor = psSelectedItem.BackgroundColor;
            CollectionViewRoot.TextColor = Color.FromArgb(PSColor.DefaultWhiteColor);
            //RangeSelectedLbl1.Text = psSelectedItem.Name;
            //RangeSelectedLbl2.Text = psSelectedItem.Name;
            //RangeSelectedGrid1.BackgroundColor = psSelectedItem.BackgroundColor;
            //RangeSelectedGrid2.BackgroundColor = psSelectedItem.BackgroundColor;
        }
        else if (propertyName == psHeightProperty.PropertyName)
        {
            CollectionViewRoot.HeightRequest = psHeight;
        }
        else if (propertyName == UnitLabelProperty.PropertyName)
        {
            RangeUnitLbl.Text = UnitLabel;
        }
    }

    public RangeComboBox()
	{
		InitializeComponent();
        InitUI();
    }
    async void InitUI()
    {
        await Task.Delay(100);
        await PSTasks.ActionTask(()=> 
        {            
            //ComboBoxStackLayout2.IsVisible = false;
            //Border1.Stroke = Color.FromArgb(PSColor.DefaultBlueColor);
            //Border2.Stroke = Color.FromArgb(PSColor.DefaultBlueColor);
        });
    }
    
    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        //ComboBoxStackLayout2.IsVisible = !ComboBoxStackLayout2.IsVisible;
        //if (ComboBoxStackLayout2.IsVisible)
        //{
        //    CollectionViewRoot.Focus();
        //}
    }
    
    private void TapGestureRecognizer_Tapped_Item(object sender, EventArgs e)
    {
        //ComboBoxStackLayout2.IsVisible = !ComboBoxStackLayout2.IsVisible;
        //var _range = ((CollectionView)sender).SelectedItem as Range;

        //psSelectedItem = _range;

        //CollectionViewRoot.SelectedItem = psSelectedItem;
    }

    private void CollectionViewRoot_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //ComboBoxStackLayout2.IsVisible = !ComboBoxStackLayout2.IsVisible;

        //var _range = ((CollectionView)sender).SelectedItem as Range;

        //psSelectedItem = _range;

        //CollectionViewRoot.SelectedItem = psSelectedItem;
    }

    public object GetComboBoxItems(Parameter parameter)
    {
        if (parameter.Enum is not { } enumValues) return null;

        return enumValues.Select(GetLabel);
    }

    private LabelAndColor GetLabel(ParameterValue value)
    {
        LabelAndColor labelAndColor;
        var parameterValue = value.ToString().Split("%COLOR: %");
        if (StringLocalizer == null) labelAndColor = new LabelAndColor(parameterValue[0]);
        else
        {
            var label = StringLocalizer.GetString($"{parameterValue[0]}_Label");
            if (label.ResourceNotFound) label = StringLocalizer.GetString($"{parameterValue[0]}");
            labelAndColor = new LabelAndColor(label.ResourceNotFound ? parameterValue[0] : label);
        }

        if (parameterValue.Count() > 1 && uint.TryParse(parameterValue[1], out var argbValue))
        {
            labelAndColor.Color = new Color(argbValue);
        }

        return labelAndColor;
    }

    public int GetParameterValue(Parameter parameter)
    {
        return parameter.Enum!.ToList().IndexOf(parameter.Value);
    }

    public void SetParameterValue(int i)
    {
        ComboBoxParameter.Value = ComboBoxParameter.Enum![i];
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {

    }

    private void CollectionViewRoot_SelectionChanged_1(object sender, ComboBoxSelectionChangedEventArgs e)
    {
        //ComboBoxStackLayout2.IsVisible = !ComboBoxStackLayout2.IsVisible;

        var _range = ((RadComboBox)sender).SelectedItem as Range;

        psSelectedItem = _range;
        psSelectedItem.SelectionName = _range.Name;
        CollectionViewRoot.SelectedItem = psSelectedItem;
    }
}

public class LabelAndColor
{
    public Color? Color { get; set; }
    public string Label { get; }
    public bool HasColor => Color != null;

    public LabelAndColor(string label)
    {
        Label = label;
    }
}