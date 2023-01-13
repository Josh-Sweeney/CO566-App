using System;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace gha.mobile.common.converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value,
               Type targetType,
               object parameter,
               System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                byte[] decodedByteArray = System.Convert.FromBase64String(Encoding.UTF8.GetString(imageAsBytes, 0, imageAsBytes.Length));
                var stream = new MemoryStream(decodedByteArray);
                retSource = ImageSource.FromStream(() => new MemoryStream(decodedByteArray));
            }
            return retSource;
        }

        public object ConvertBack(object value,
               Type targetType,
               object parameter,
               System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

  
}
