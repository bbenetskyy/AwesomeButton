using Xamarin.Forms;
using AwesomeButton.Views;

namespace AwesomeButton
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AboutPage();
        }
    }
}
