using System;

namespace RealTimeNotification
{
    public delegate void LoginEvent(string message);

    public class LoginManager
    {
        public event LoginEvent adminloginsuccess;
        public event LoginEvent userloginsuccess;

        public void Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                Console.WriteLine("Admin login successful.");
                NotifyAdminLoginSuccess("Admin login successful.");

            }
            else if (username == "user" && password == "userpassword")
            {
                Console.WriteLine("User login successful.");

                NotifyUserLoginSuccess("User login successful.");

            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }

        protected virtual void NotifyAdminLoginSuccess(string message)
        {
            adminloginsuccess?.Invoke(message);
        }
        protected virtual void NotifyUserLoginSuccess(string message)
        {
            userloginsuccess?.Invoke(message);
        }
    }

    public class NotificationMessage
    {
        public void OnAdminLoginSuccess(string message)
        {
            Console.WriteLine($"Notification: {message}");
        }

        public void OnUserLoginSuccess(string message)
        {
            Console.WriteLine($"Notification: {message}");
        }
    }

    public class Logger
    { 
        public void OnAdminLoginSuccess(string message)
        {
            Console.WriteLine($"Logger: Admin login event logged - {message}");
        }
        public void OnUserLoginSuccess(string message)
        {
            Console.WriteLine($"Logger: User login event logged - {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n Real Time Notification \n");

            LoginManager loginManager = new LoginManager();
            NotificationMessage notificationMessage = new NotificationMessage();
            Logger logger = new Logger();



            loginManager.adminloginsuccess += notificationMessage.OnAdminLoginSuccess;
            loginManager.userloginsuccess += notificationMessage.OnUserLoginSuccess;

            loginManager.adminloginsuccess += logger.OnAdminLoginSuccess;
            loginManager.userloginsuccess += logger.OnUserLoginSuccess;

            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            // Perform login and notify if successful
            loginManager.Login(username, password);
            Console.ReadLine();
        }
    }
}

