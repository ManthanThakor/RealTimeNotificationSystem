using System;
using System.Threading;

namespace RealTimeNotification
{
    public class LoginEventArgs : EventArgs
    {
        public string Message { get; }
        public DateTime Timestamp { get; }

        public LoginEventArgs(string message)
        {
            Message = message;
            Timestamp = DateTime.Now;
        }
    }

    public class LoginManager
    {
        public event EventHandler<LoginEventArgs> AdminLoginSuccess;
        public event EventHandler<LoginEventArgs> UserLoginSuccess;

        private int failedAttempts = 0; 
        private int timeout = 0; 

        public void Login()
        {
            while (true)
            {
                if (timeout > 0)
                {
                    Console.WriteLine($"Too many failed attempts. Please wait {timeout} seconds...");
                    Thread.Sleep(timeout * 1000);
                    timeout = 0;
                }

                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                if (username == "admin" && password == "admin123")
                {
                    Console.WriteLine("Admin login successful.");
                    OnAdminLoginSuccess(new LoginEventArgs("Admin login successful."));
                    failedAttempts = 0; 
                    break;
                }
                else if (username == "user" && password == "userpassword")
                {
                    Console.WriteLine("User login successful.");
                    OnUserLoginSuccess(new LoginEventArgs("User login successful."));
                    failedAttempts = 0; 
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid username or password.\n");
                    failedAttempts++;

                    if (failedAttempts == 3)
                    {
                        timeout = 30; 
                    }
                    else if (failedAttempts == 6)
                    {
                        timeout = 60; 
                        failedAttempts = 0; 
                    }
                }
            }
        }

        protected virtual void OnAdminLoginSuccess(LoginEventArgs e)
        {
            AdminLoginSuccess?.Invoke(this, e);
        }

        protected virtual void OnUserLoginSuccess(LoginEventArgs e)
        {
            UserLoginSuccess?.Invoke(this, e);
        }
    }

    public class NotificationMessage
    {
        public void HandleAdminLoginSuccess(object sender, LoginEventArgs e)
        {
            Console.WriteLine($"UI Notification: {e.Message} (Timestamp: {e.Timestamp})");
        }

        public void HandleUserLoginSuccess(object sender, LoginEventArgs e)
        {
            Console.WriteLine($"UI Notification: {e.Message} (Timestamp: {e.Timestamp})");
        }
    }

    public class Logger
    {
        public void HandleAdminLoginSuccess(object sender, LoginEventArgs e)
        {
            Console.WriteLine($"Logger: Admin login event logged - {e.Message} (Timestamp: {e.Timestamp})");
        }

        public void HandleUserLoginSuccess(object sender, LoginEventArgs e)
        {
            Console.WriteLine($"Logger: User login event logged - {e.Message} (Timestamp: {e.Timestamp})");
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

            loginManager.AdminLoginSuccess += notificationMessage.HandleAdminLoginSuccess;
            loginManager.UserLoginSuccess += notificationMessage.HandleUserLoginSuccess;

            loginManager.AdminLoginSuccess += logger.HandleAdminLoginSuccess;
            loginManager.UserLoginSuccess += logger.HandleUserLoginSuccess;

            loginManager.Login();

            Console.ReadLine();
        }
    }
}
