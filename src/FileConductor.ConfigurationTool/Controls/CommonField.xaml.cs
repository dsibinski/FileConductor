using System;
using System.Windows;
using System.Windows.Controls;

namespace ConfigurationTool.Controls
{
    /// <summary>
    /// Interaction logic for CommonField.xaml
    /// </summary>
    public partial class CommonField : UserControl
    {

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String),typeof(CommonField), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(String), typeof(CommonField), new FrameworkPropertyMetadata(string.Empty));
        public String Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }

        public String Value
        {
            get { return GetValue(ValueProperty).ToString(); }
            set { SetValue(ValueProperty, value); }
        }
        public CommonField()
        {
            InitializeComponent();
            Name = "CustomField";
        }
    }
}
