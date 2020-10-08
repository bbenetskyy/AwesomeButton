using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AwesomeButton.Controls
{
    [ContentProperty("Content")]
    public partial class AwesomeButton : TemplatedView
    {
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(
            nameof(Content), typeof(object), typeof(AwesomeButton), null,
            propertyChanged: OnContentChanged, coerceValue: CoerceContent);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize), typeof(double), typeof(AwesomeButton), (double)18, 
            propertyChanged: (b, x, y) => ((AwesomeButton)b).UpdateFontSize());

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            nameof(FontFamily), typeof(string), typeof(AwesomeButton),
            propertyChanged: (b, x, y) => ((AwesomeButton)b).UpdateFontFamily());

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor), typeof(Color), typeof(AwesomeButton), Color.White,
            propertyChanged: (b, x, y) => ((AwesomeButton)b).UpdateTextColor());
        
        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor), typeof(Color), typeof(AwesomeButton), Color.Transparent,
            propertyChanged: (b, x, y) => ((AwesomeButton)b).UpdateTextColor());

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(AwesomeButton));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius), typeof(float), typeof(AwesomeButton), 15f);

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(
            nameof(HasShadow), typeof(bool), typeof(AwesomeButton));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter), typeof(object), typeof(AwesomeButton));
        
        public static readonly BindableProperty PressedOpacityProperty = BindableProperty.Create(
            nameof(PressedOpacity), typeof(double), typeof(AwesomeButton), 0.7,
            propertyChanged:  (b, x, y) => ((AwesomeButton)b).UpdatePressedOpacity());
        
        public static readonly BindableProperty DisableOpacityProperty = BindableProperty.Create(
            nameof(DisableOpacity), typeof(double), typeof(AwesomeButton), 0.3,
            propertyChanged:  (b, x, y) => ((AwesomeButton)b).UpdateDisableOpacity());
        
        [TypeConverter(typeof(TextContentTypeConverter))]
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double) GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string FontFamily
        {
            get => (string) GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public Color TextColor
        {
            get => (Color) GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        
        public new Color BackgroundColor
        {
            get => (Color) GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public float CornerRadius
        {
            get => (float) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public double PressedOpacity
        {
            get => (double) GetValue(PressedOpacityProperty);
            set => SetValue(PressedOpacityProperty, value);
        }

        public double DisableOpacity
        {
            get => (double) GetValue(DisableOpacityProperty);
            set => SetValue(DisableOpacityProperty, value);
        }
        
        public AwesomeButton()
        {
            InitializeComponent();

            var backBox = (BoxView) GetTemplateChild("BackgroundBox");
            backBox.SetBinding(BoxView.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
            
            var border = (Frame)GetTemplateChild("BorderFrame");
            border.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
            border.SetBinding(Frame.HasShadowProperty, new Binding(nameof(HasShadow), source: this));

            var coverButton = (Button)GetTemplateChild("CoverButton");
            coverButton.SetBinding(Button.CommandProperty, new Binding(nameof(Command), source: this));
            coverButton.SetBinding(Button.CommandParameterProperty, new Binding(nameof(CommandParameter), source: this));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            View content = (View)Content;
            ControlTemplate controlTemplate = ControlTemplate;

            if (content != null && controlTemplate != null)
            {
                SetInheritedBindingContext(content, BindingContext);
            }
        }

        protected override void OnApplyTemplate()
        {
            View content = (View)Content;
            ControlTemplate controlTemplate = ControlTemplate;

            if (content != null && controlTemplate != null)
            {
                SetInheritedBindingContext(content, BindingContext);
            }
        }

        private static void OnContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newElement = (Element)newValue;
            if (newElement != null)
            {
                SetInheritedBindingContext(newElement, bindable.BindingContext);

                var button = (AwesomeButton)bindable;
                button.UpdateFontSize();
                button.UpdateFontFamily();
                button.UpdateTextColor();
            }
        }

        private static object CoerceContent(BindableObject bindable, object value)
        {
            if (value is View view)
                return view;

            return value != null 
                ? new TextContent { Text = value.ToString() } 
                : null;
        }

        private void UpdateFontSize()
        {
            if (Content is Label label)
            {
                label.FontSize = FontSize;
            }
        }

        private void UpdateFontFamily()
        {
            if (Content is Label label)
            {
                label.FontFamily = FontFamily;
            }
        }

        private void UpdateTextColor()
        {
            if (Content is Label label)
            {
                label.TextColor = TextColor;
            }
        }

        private void UpdateDisableOpacity()
        {
            var coverButton = GetTemplateChild("CoverButton") as Button;
            if (coverButton == null)
                return;
            var visualStateGroups = VisualStateManager.GetVisualStateGroups(coverButton);
            var commonGroup =  visualStateGroups.First();
            var disabledState = commonGroup.States[2];
            var opacitySetter = disabledState.Setters[0];
            opacitySetter.Value = DisableOpacity;
        }

        private void UpdatePressedOpacity()
        {
            var coverButton = GetTemplateChild("CoverButton") as Button;
            if (coverButton == null)
                return;
            var visualStateGroups = VisualStateManager.GetVisualStateGroups(coverButton);
            var commonGroup =  visualStateGroups.First();
            var pressedState = commonGroup.States[1];
            var opacitySetter = pressedState.Setters[0];
            opacitySetter.Value = PressedOpacity;
        }
    }
}