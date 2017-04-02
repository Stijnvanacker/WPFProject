using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPFProject.util
{
    //deze converter klasse dient voor het berekenen van het subtotaal van een OrderLine
    public class MultiplyConverter: IMultiValueConverter
    {
        
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double a = System.Convert.ToDouble(values[0]);
            double b = System.Convert.ToDouble(values[1]);
            double ab = a*b;
            string totaal = ab.ToString();
            return totaal;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        
    }
}
