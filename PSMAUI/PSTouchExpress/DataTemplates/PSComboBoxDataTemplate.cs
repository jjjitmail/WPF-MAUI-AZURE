using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.DataTemplates
{
    public class PSComboBoxDataTemplate : DataTemplateSelector
    {
        public PSComboBoxDataTemplate()
        {

        }
        public DataTemplate TechniqueComboBoxTemplate { get; set; }

        public DataTemplate RangeComboBoxTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return TechniqueComboBoxTemplate;
        }
    }
}
