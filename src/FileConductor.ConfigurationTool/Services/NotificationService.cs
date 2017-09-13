using System.Windows;
using ConfigurationTool.MessageBox;
using MessageBoxImage = ConfigurationTool.MessageBox.MessageBoxImage;

namespace ConfigurationTool.Services
{
    public class NotificationService : INotificationService
    {
        public void ShowNotification(string message)
        {
            CustomMessageBox.Show(message);
        }

        public MessageBoxResult ShowQuestion(string caption, string question)
        {
            return CustomMessageBox.Show(caption, question, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }
    }
}