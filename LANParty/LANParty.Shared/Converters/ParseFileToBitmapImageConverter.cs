using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace LANParty.Converters
{
    public class ParseFileToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            ParseFile asd = (ParseFile)ParseUser.CurrentUser["profilePic"];
            BitmapImage image = new BitmapImage();
            image.UriSource = new Uri(asd.Url.ToString(), UriKind.RelativeOrAbsolute);

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
