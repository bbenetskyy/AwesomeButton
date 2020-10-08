using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AwesomeButton.Controls
{
    [TypeConversion(typeof(TextContent))]
    public class TextContentTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            return new TextContent
            {
                Text = value
            };
        }
    }
}