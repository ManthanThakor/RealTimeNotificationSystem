using System;

namespace RealTimeNotification
{
    public delegate void LoginEvent(string message);

    public class LoginManager
    {
        public event LoginEvent AdminLoginSuccess;
        public event LoginEvent UserLoginSuccess;

        public void Login(string username, string password)
        {
            if (username = "admin" && password = "admin123")
            {
                Console.WriteLine("Admin login successful.");
            }
            else if (username == "user" && password == "userpassword")
            {
                Console.WriteLine("User login successful.");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }

        protected virtual void NotifyAdminLoginSuccess(string message)
        {
            AdminLoginSuccess?.Invoke(message);
        }
        protected virtual void NotifyUserLoginSuccess(string message)
        {
            UserLoginSuccess?.Invoke(message);
        }
    }

    public class UIComponent
    {
        public void onAdminLoginSuccess(string message)
        {
            Console.WriteLine($"UI Notification: {message}");
        }

        public void onUserLogin(string message)
        {
            Console.WriteLine($"UI Notification: {message}");
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


            Console.ReadLine();
        }
    }
}

