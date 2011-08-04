namespace DependencyInjectionExample
{
    using ApplicationSettings;

    /// <summary>
    /// This is the interface for the settings we like to expose.
    /// We only expose settings we need using easy to use properties.
    /// This interface can be mocked in unit tests.    
    /// </summary>
    public interface ISettings
    {
        string UserName { get; }
        string Password { get; }
    }

    public class Settings : AppSettings, ISettings
    {
        public Settings()
        {
            this.UserName = this.GetValue("Username");
            this.Password = this.GetValue("Password");
        }

        public string UserName { get; private set; }
        
        public string Password { get; private set; }
    }
}