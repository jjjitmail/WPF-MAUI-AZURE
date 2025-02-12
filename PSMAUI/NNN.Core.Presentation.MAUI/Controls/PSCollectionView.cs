using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Controls
{
    public class PSCollectionView : CollectionView
    {
        public static readonly BindableProperty PSItemsSourceProperty = BindableProperty.Create(nameof(PSItemsSource), typeof(IEnumerable),
            typeof(PSCollectionView), default(IEnumerable), BindingMode.TwoWay, propertyChanged: PSItemsSourceChanged);

        static void PSItemsSourceChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            PSCollectionView _collection = bindableObject as PSCollectionView;
            _collection.ItemsSource = (IEnumerable)newValue;
        }

        public IEnumerable PSItemsSource
        {
            get => (IEnumerable)GetValue(PSItemsSourceProperty);
            set => SetValue(PSItemsSourceProperty, value);
        }
    }
}
