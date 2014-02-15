using System.Windows;
using System.Windows.Controls;

namespace TWAddIn
{
    public class ScreenSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var isLoggedIn = (bool)item;
            var control = ((container as UserControl));
            var resource = isLoggedIn ? control.FindResource("QuestionScreen") : control.FindResource("LoginScreen");
            return resource as DataTemplate;
        }
    }
}