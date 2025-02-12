using System.Collections;

namespace PSTouchExpress.UserControls;

public partial class PSComboBox : ContentView
{
    public static readonly BindableProperty psItemHeightProperty = BindableProperty.Create(nameof(psItemHeight), 
        typeof(double), typeof(PSComboBox), default(double), BindingMode.TwoWay);

    public static readonly BindableProperty psItemsSourceProperty = BindableProperty.Create(nameof(psItemsSource), 
        typeof(IEnumerable), typeof(PSComboBox), default(IEnumerable), BindingMode.TwoWay);

    public static readonly BindableProperty psBorderColorProperty = BindableProperty.Create(nameof(psBorderColor),
        typeof(string), typeof(PSComboBox), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty psBackGroundColorProperty = BindableProperty.Create(nameof(psBackGroundColor),
        typeof(string), typeof(PSComboBox), default(string), BindingMode.TwoWay);

    public static readonly ControlTemplate psComboBoxTemplate = new ControlTemplate(typeof(PSComboBoxTemplate));

    public IEnumerable psItemsSource
    {
        get => (IEnumerable)GetValue(psItemsSourceProperty);
        set => SetValue(psItemsSourceProperty, value);
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

    public PSComboBox()
	{        
        InitializeComponent();
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == psItemsSourceProperty.PropertyName)
        {
            CollectionViewRoot.ItemsSource = psItemsSource;
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
}

public class PSComboBoxTemplate : ContentView
{
    public PSComboBoxTemplate()
    {
        var _collectionView = new CollectionView();
        //_collectionView.SetBinding(CollectionView.ItemsSourceProperty, )
    }
}