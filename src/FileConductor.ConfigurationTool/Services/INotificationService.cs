using System.Windows;

namespace ConfigurationTool.Services
{
    public interface INotificationService
    {
        void ShowNotification(string message);
        MessageBoxResult ShowQuestion(string caption,string question);
    }
}