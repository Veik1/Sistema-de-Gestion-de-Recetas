namespace RecipeProject.Application.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(int userId, string message);
    }
}