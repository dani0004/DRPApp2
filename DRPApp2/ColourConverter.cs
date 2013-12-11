using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DRPApp2
{
    public class ColourConverter : IValueConverter
    {
        public object Convert(object status1, Type targetType, object parameter, CultureInfo culture)
        {
            int value;
            String bb = (String)status1;
            value = System.Convert.ToInt32(bb);
            // bool aa = int.TryParse(bb, out value);
            //GREEN
            if (value == 3) return new SolidColorBrush(Color.FromArgb(255, 7, 166, 45));
            //RED
            if (value == 1) return new SolidColorBrush(Color.FromArgb(255, 237,28, 36));
           
            //YELLOW
            if (value == 2) return new SolidColorBrush(Color.FromArgb(255, 189, 211, 3));

            // Color default
            return new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return DependencyProperty.UnsetValue;
        }
    }
}
