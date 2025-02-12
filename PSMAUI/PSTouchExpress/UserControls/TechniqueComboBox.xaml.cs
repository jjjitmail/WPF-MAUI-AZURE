using PSTouchExpress.Models;
using PSTouchExpress.Helpers;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PSTouchExpress.UserControls;

public partial class TechniqueComboBox : ContentView
{
    public static readonly BindableProperty psItemHeightProperty = BindableProperty.Create(nameof(psItemHeight),
        typeof(double), typeof(TechniqueComboBox), default(double), BindingMode.TwoWay);

    public static readonly BindableProperty psItemsSourceProperty = BindableProperty.Create(nameof(psItemsSource),
        typeof(ObservableCollection<TechniqueItem>), typeof(TechniqueComboBox), default(ObservableCollection<TechniqueItem>), BindingMode.TwoWay);

    public static readonly BindableProperty psBorderColorProperty = BindableProperty.Create(nameof(psBorderColor),
        typeof(string), typeof(TechniqueComboBox), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty psBackGroundColorProperty = BindableProperty.Create(nameof(psBackGroundColor),
        typeof(string), typeof(TechniqueComboBox), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty psSelectedItemProperty = BindableProperty.Create(nameof(psSelectedItem),
        typeof(Metry), typeof(TechniqueComboBox), default(Metry), BindingMode.TwoWay);

    public static readonly ControlTemplate psComboBoxTemplate = new ControlTemplate(typeof(PSComboBoxTemplate));

    public ObservableCollection<TechniqueItem> psItemsSource
    {
        get => (ObservableCollection<TechniqueItem>)GetValue(psItemsSourceProperty);
        set { SetValue(psItemsSourceProperty, value); OnPropertyChanged("psItemsSource"); }
    }

    public Metry psSelectedItem
    {
        get
        {
            return (Metry)GetValue(psSelectedItemProperty);
        }
        set
        {
            SetValue(psSelectedItemProperty, value);
            OnPropertyChanged("psSelectedItem");
        }
    }

    public double psItemHeight
    {
        get => (double)GetValue(psItemHeightProperty);
        set => SetValue(psItemHeightProperty, value);
    }

    public string psBorderColor
    {
        get => (string)GetValue(psBorderColorProperty);
        set => SetValue(psBorderColorProperty, value);
    }

    public string psBackGroundColor
    {
        get => (string)GetValue(psBackGroundColorProperty);
        set => SetValue(psBackGroundColorProperty, value);
    }

    public TechniqueComboBox()
    {
        InitializeComponent();
        InitUI();
    }
    async void InitUI()
    {
        await Task.Delay(500);
        await PSTasks.ActionTask(() =>
        {
            ComboBoxStackLayout2.IsVisible = false;            
        });
    }
    //override 
    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == psItemsSourceProperty.PropertyName)
        {
            CollectionViewRoot.ItemsSource = psItemsSource;
        }
        else if (propertyName == psSelectedItemProperty.PropertyName)
        {
            SelectedLblName1.Text = psSelectedItem.MetryName;
            SelectedLblIcon1.Points = psSelectedItem.MetryIcon;
            SelectedLblIcon1.Stroke = Color.FromArgb(PSColor.DefaultBlueColor);
            SelectedLblName2.Text = psSelectedItem.MetryName;
            SelectedLblIcon2.Points = psSelectedItem.MetryIcon;
            SelectedLblIcon2.Stroke = Color.FromArgb(PSColor.DefaultBlueColor);
        }
        else if (propertyName == psItemHeightProperty.PropertyName)
        {
            ContentView1.HeightRequest = psItemHeight;
            ContentView2.HeightRequest = psItemHeight;
        }
        else if (propertyName == psBorderColorProperty.PropertyName)
        {
            var borderColor = Color.FromArgb(psBorderColor);
            Border1.Stroke = borderColor;
            Border2.Stroke = borderColor;
        }
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        PSTasks.ActionTask(() =>
        {
            foreach (var item in psItemsSource)
            {
                var trueItem = item.FirstOrDefault(z => z.IsSelected == true);
                if (trueItem != null)
                    trueItem.IsSelected = false;
            }
            //var previous = e.PreviousSelection.FirstOrDefault() as Metry; not working
            //previous.IsSelected = false;

            //var _metry = ((CollectionView)sender).SelectedItem as Metry;
            //_metry.IsSelected = true;
            //psSelectedItem = _metry;

            var current = e.CurrentSelection.FirstOrDefault() as Metry;
            current.IsSelected = true;
            psSelectedItem = current;
        });


        ComboBoxStackLayout2.IsVisible = !ComboBoxStackLayout2.IsVisible;
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        ComboBoxStackLayout2.IsVisible = !ComboBoxStackLayout2.IsVisible;
        if (ComboBoxStackLayout2.IsVisible)
        {
            //CollectionViewRoot.ItemsSource = psItemsSource;
        }
    }
}

public class TechniqueComboBoxHighLightItemBGColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Color.FromArgb(PSColor.DefaultBlueColor) : Color.FromArgb(PSColor.DefaultWhiteColor);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Color)value == Color.FromArgb(PSColor.DefaultBlueColor) ? true : false;
    }
}

public class TechniqueComboBoxHighLightItemTextColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Color.FromArgb(PSColor.DefaultWhiteColor) : Color.FromArgb(PSColor.DefaultTextColor);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Color)value == Color.FromArgb(PSColor.DefaultWhiteColor) ? true : false;
    }
}

public class TechniqueComboBoxHighLightItemIconColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Color.FromArgb(PSColor.DefaultWhiteColor) : Color.FromArgb(PSColor.DefaultBlueColor);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Color)value == Color.FromArgb(PSColor.DefaultBlueColor) ? true : false;
    }
}