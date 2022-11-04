using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager
{
    public class TextBoxExtension : DependencyObject
    {
        public static readonly DependencyProperty NodeBindingProperty = DependencyProperty.RegisterAttached("NodeBinding",
            typeof(Node), typeof(TextBoxExtension), new PropertyMetadata(null));
        public static Node GetNodeBinding(DependencyObject d)
        {
            return (Node)d.GetValue(NodeBindingProperty);
        }
        public static void SetNodeBinding(DependencyObject d, Node bindingNode)
        {
            d.SetValue(NodeBindingProperty, bindingNode);
        }
    }
}
