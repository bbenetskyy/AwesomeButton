using Xamarin.Forms;

namespace AwesomeButton.Controls
{
    public class TextContent : Label
    {
        public TextContent()
        {
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.Fill;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalTextAlignment = TextAlignment.Center;
        }
    }
}