using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static AwesomeButton.Constants.IconCodes;

namespace AwesomeButton.ViewModels
{
    public class AboutViewModel
    {
        public string ButtonText { get; }
        public Color TextColor { get; }
        
        public List<string> IconsCollection { get; }
        
        public ICommand ClickCommand { get; }
        public ICommand WithParameterCommand { get; }
        public ICommand DisabledCommand { get; }
        
        public AboutViewModel()
        {
            ClickCommand = new Command(() =>
            {
                Application.Current.MainPage.DisplayAlert("","Button Clicked", "OK");
            });
            WithParameterCommand = new Command<Color>((color) =>
            {
                Application.Current.MainPage.DisplayAlert("", $"Text Color -> {color}", "OK");
            });
            DisabledCommand = new Command(() => { }, () => false);
            
            IconsCollection = new List<string>
            {
                MagicWand, Refresh, Gradient, Radial,
                Palette, Layers, Gallery, Code, Bolt, Paint
            };

            ButtonText = "Bindable Text";
            TextColor = Color.Blue;
        }
    }
}