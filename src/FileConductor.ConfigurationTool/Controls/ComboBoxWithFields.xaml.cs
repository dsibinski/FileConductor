using System;
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
        //public public static readonly DependencyProperty PropertyTypeProperty = DependencyProperty.Register(
        //    "PropertyType", typeof(string), typeof(ComboBoxWithFields), new PropertyMetadata(default(string)));

        //public String PropertyType
        //{
        //    get { return (propertyType) GetValue(PropertyTypeProperty); }
        //    set { SetValue(PropertyTypeProperty, value); }
        //}

        public ComboBoxWithFields()
        {
            InitializeComponent();
        }
    }
}
