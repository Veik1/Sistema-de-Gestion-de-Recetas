using System;
using RecipeProject.Application.Interfaces;

namespace RecipeProject.Infrastructure.Services
{
    public class ConsoleNotificationService : INotificationService
    {
        public void SendNotification(int userId, string message)
        {
            Console.WriteLine($"[Notification] To User {userId}: {message}");
        }
    }
}