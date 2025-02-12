using PSTouchExpress.Models;
using PSTouchExpress.Helpers;
using System.Collections;
using Range = PSTouchExpress.Models.Range;
using System.ComponentModel;

namespace PSTouchExpress.UserControls;

public partial class RangeComboBox : ContentView
{
    public static readonly BindableProperty psItemsSourceProperty = BindableProperty.Create(nameof(psItemsSource),
        typeof(IEnumerable), typeof(RangeComboBox), default(IEnumerable), BindingMode.TwoWay);

    public static readonly BindableProperty psSelectedItemProperty = BindableProperty.Create(nameof(psSelectedItem),
        typeof(Range), typeof(RangeComboBox), default(Range), BindingMode.TwoWay);

    public static readonly BindableProperty psHeightProperty = BindableProperty.Create(nameof(psHeight),
        typeof(double), typeof(RangeComboBox), default(double), BindingMode.TwoWay);

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
            RangeSelectedLbl1.Text = psSelectedItem.Name;
            RangeSelectedLbl2.Text = psSelectedItem.Name;
            RangeSelectedGrid1.BackgroundColor = psSelectedItem.BackgroundColor;
            RangeSelectedGrid2.BackgroundColor = psSelectedItem.BackgroundColor;
        }
        else if (propertyName == psHeightProperty.PropertyName)
        {
            CollectionViewRoot.HeightRequest = psHeight;
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
            ComboBoxStackLayout2.IsVisible = false;
            Border1.Stroke = Color.FromArgb(PSColor.DefaultBlueColor);
            Border2.Stroke = Color.FromArgb(PSColor.DefaultBlueColor);
        });
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        ComboBoxStackLayout2.IsVisible = !ComboBoxStackLayout2.IsVisible;
        if (ComboBoxStackLayout2.IsVisible)
        {
            CollectionViewRoot.Focus();
        }
    }

    private void CollectionViewRoot_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBoxStackLayout2.IsVisible = !ComboBoxStackLayout2.IsVisible;

        var _range = ((CollectionView)sender).SelectedItem as Range;

        psSelectedItem = _range;

        CollectionViewRoot.SelectedItem = psSelectedItem;
    }
}