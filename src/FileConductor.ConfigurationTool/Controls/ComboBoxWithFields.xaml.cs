using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConfigurationTool.Controls
{
    /// <summary>
    /// Interaction logic for ComboBoxWithFields.xaml
    /// </summary>
    public partial class ComboBoxWithFields : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(ComboBoxWithFields), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(ComboBoxWithFields), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource", typeof(IEnumerable), typeof(ComboBoxWithFields), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty DisplayedPropertyProperty = DependencyProperty.Register("DisplayedProperty", typeof(String), typeof(ComboBoxWithFields), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(ComboBoxWithFields), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public ICommand EditCommand
        {
            get { return (ICommand)GetValue(EditCommandProperty); } 
            set
            {
             SetValue(EditCommandProperty,value);   
            }
        }

        public IEnumerable ItemSource
        {
            get { return GetValue(ItemSourceProperty).ToString(); }
            set { SetValue(ItemSourceProperty, value); }
        }
        public String DisplayedProperty
        {
            get { return GetValue(DisplayedPropertyProperty).ToString(); }
            set { SetValue(DisplayedPropertyProperty, value); }
        }

        public String Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty).ToString(); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public ComboBoxWithFields()
        {
            InitializeComponent();
        }
    }
}
