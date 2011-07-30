###Description

Library (.NET 4.0, C#) for accessing app.config file using generic type safe methods. Library implements interface which can be mocked/faked in unit test.

The library does not expose 100% of the functionality provided by the wrapped configuration classes in .NET (i.e. [ConfigurationManager](http://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager.aspx), [Configuration](http://msdn.microsoft.com/en-us/library/system.configuration.configuration.aspx)). It covers the most common use cases and provides extension points for the rest.

###Status

**The library is currently under development thus there are no releases**. The first release will support reading of values from .config file.

###Roadmap

##Most important features will be

+ reading .config file by giving relative or absolute path to it
+ reading standard app.config file without need to give path to it
+ reading standard .net types type safely
+ reading connection string
+ support for enum types
+ support for Nullable types

##Other things on the todo list

+ support for custom conversion routines
+ web.config support

##Nice to have features

+ reading complete sections into type safe objects
+ support for write (save, update, replace etc.) methods

####License

Library is licensed under [LGPL v3.0](http://www.gnu.org/licenses/lgpl-3.0-standalone.html). 
