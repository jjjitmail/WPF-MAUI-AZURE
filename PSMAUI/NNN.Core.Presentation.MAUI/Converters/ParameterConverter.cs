using NNN.Core.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Converters
{
    public class ParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                if (value is ParameterValue parameterValue && parameter is Parameter p)
                {
                    if (parameterValue.Type == ParameterType.ParameterGroup)
                    {
                        return p.Values.Count.ToString();
                    }
                    else
                    {
                        return parameterValue.ToString();
                    }
                }
            }
            //else if (targetType == typeof(double))
            //{
            //    return ((ParameterValue)value).as();
            //}
            //else
            { return string.Empty; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value == null || string.IsNullOrEmpty(value.ToString())) return new ParameterValue("");
            try
            {
                var parameterValueResult = new ParameterValue("");
                if (parameter is Parameter p)
                {
                    switch (p.Type)
                    {
                        case ParameterType.String:
                            parameterValueResult = new ParameterValue(value.ToString());
                            break;
                        case ParameterType.Double:
                            if (Double.TryParse(value.ToString(), out var _doubleValue))
                            {
                                parameterValueResult =  new ParameterValue(_doubleValue);
                            }
                            else parameterValueResult = new ParameterValue(0);
                            break;
                        case ParameterType.Integer:
                            if (Int32.TryParse(value.ToString(), out var _intValue))
                            {
                                parameterValueResult = new ParameterValue(_intValue);
                            }
                            else parameterValueResult = new ParameterValue(0);
                            break;
                        case ParameterType.Boolean:
                            if (Boolean.TryParse(value.ToString(), out var _boolValue))
                            {
                                parameterValueResult = new ParameterValue(_boolValue);
                            }
                            break;
                        case ParameterType.ParameterGroup:
                            if (Int32.TryParse(value.ToString(), out var _groupValue))
                            {
                                return SetCollectionCount(_groupValue, p);
                            }
                            else return new ParameterValue(0);
                        default: return new ParameterValue("");
                    }
                }
            }
            catch (Exception err)
            {

                throw;
            }
            
            throw new NotImplementedException();
        }

        private ParameterValue[] SetCollectionCount(int count, Parameter parameter)
        {
            //if (parameter == null || parameter.Values.Count == 0 || count < 1) return ;

            int prevCount = parameter.Values.Count;
            var values = parameter.Values.ToArray();
            var last = values[prevCount - 1];
            Array.Resize(ref values, count);

            for (int i = prevCount; i < count; ++i)
            {
                var re = last.AsParameterGroup().Capabilities;
                values[i] = last.Type == ParameterType.ParameterGroup ? new ParameterGroup(last.AsParameterGroup().Capabilities) : last;
            }
            return values;
        }
    }
}
