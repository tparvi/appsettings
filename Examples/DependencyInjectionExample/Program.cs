using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DependencyInjectionExample
{
    using StructureMap;
    using StructureMap.Configuration.DSL;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // ** IMPORTANT **
                // Best practices when using StructureMap is out of the scope of this example.
                // StructureMap is only used to give an example how settings could be injected
                // into the application.
                ObjectFactory.Initialize(x => x.AddRegistry<StructureMapRegistry>());

                var service = ObjectFactory.GetInstance<SomeService>();

                service.Authenticate();

            }
            catch (Exception exp)
            {
                Console.Error.WriteLine(exp.Message);
            }
        }
    }

    public class SomeService
    {
        private readonly ISettings settings;

        [DefaultConstructor]
        public SomeService(ISettings settings)
        {
            this.settings = settings;
        }

        public void Authenticate()
        {
            Console.WriteLine(
                "Ready to authenticate with username {0} and password {1}",
                this.settings.UserName,
                this.settings.Password);

            // call some service and give username and password
            // this.Authentication(this.settings.UserName, this.settings.Password);
        }
    }

    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            this.For<ISettings>().Singleton().Use<Settings>();
            this.ForConcreteType<SomeService>();            
        }
    }

}
