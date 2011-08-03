##Description

Library (.NET 4.0, C#) for accessing .config files using generic type safe methods. Library implements interface which can be mocked/faked in unit test.

The library does not expose 100% of the functionality provided by the wrapped configuration classes in .NET (i.e. [ConfigurationManager](http://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager.aspx), [Configuration](http://msdn.microsoft.com/en-us/library/system.configuration.configuration.aspx)). It covers the most common use cases and provides extension points for the rest.

##Status

**The library is currently under development thus there are no binary releases**. 

Following features have been implemented as of 3rd of August 2011.

+ reading .config file by giving relative or absolute path to it
+ reading standard .net types, enums and nullables type safely
+ support for reading optional parameters
+ support for custom conversion functions
+ reading connection strings

##Roadmap

The first release is still missing following features:


+ support for custom CultureInfo (currently everything is read using InvariantCulture)
+ easy way to read config file from specific location (i.e. location of executing assembly, entry assembly)
+ interface for AppSettings
+ example project
+ wiki updates

###Other things on the todo list

+ web.config support (if it requires specific support)
+ reading complete sections into type safe objects
+ support for write (save, update, replace etc.) methods

##License

Library is licensed under [LGPL v3.0](http://www.gnu.org/licenses/lgpl-3.0-standalone.html). 
