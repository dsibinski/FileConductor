using System.Windows;
using System.Windows.Controls;

namespace ConfigurationTool.Controls
{
    /// <summary>
    ///     Interaction logic for CommonField.xaml
    /// </summary>
    public partial class CommonField : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
            typeof(CommonField),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty TextBoxWidthProperty = DependencyProperty.Register("TextBoxWidth",
            typeof(double),
            typeof(CommonField),
            new FrameworkPropertyMetadata(100.00, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string),
            typeof(CommonField),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty NumberOfCharactersProperty =
            DependencyProperty.Register("NumberOfCharacters", typeof(int), typeof(CommonField),
                new FrameworkPropertyMetadata(1000, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public CommonField()
        {
            InitializeComponent();
        }

        public double TextBoxWidth
        {
            get => (int) GetValue(TextBoxWidthProperty);
            set => SetValue(TextBoxWidthProperty, value);
        }

        public int NumberOfCharacters
        {
            get => (int) GetValue(NumberOfCharactersProperty);
            set => SetValue(NumberOfCharactersProperty, value);
        }

        public string Text
        {
            get => GetValue(TextProperty).ToString();
            set
            {
                SetValue(TextProperty, value);
                TextBox.Width = 20;
            }
        }

        public string Value
        {
            get => GetValue(ValueProperty).ToString();
            set => SetValue(ValueProperty, value);
        }
    }
}