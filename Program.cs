using System;

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

        public void Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                Console.WriteLine("Admin login successful.");
                OnAdminLoginSuccess(new LoginEventArgs("Admin login successful."));
            }
            else if (username == "user" && password == "userpassword")
            {
                Console.WriteLine("User login successful.");
                OnUserLoginSuccess(new LoginEventArgs("User login successful."));
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
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

            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            loginManager.Login(username, password);

            Console.ReadLine();
        }
    }
}
