##Description

Library (.NET 4.0, C#) for accessing .config files using generic type safe methods. For more information see [Wiki]https://github.com/tparvi/appsettings/wiki.

The library does not expose 100% of the functionality provided by the wrapped  [Configuration](http://msdn.microsoft.com/en-us/library/system.configuration.configuration.aspx) class. It covers the most common use cases and provides extension points through protected methods.

##Currently implemented functionality

+ reading .config files by giving relative or absolute path to it
+ reading standard .net types, enums and nullables type safely
+ support for reading optional settings
+ support for custom conversion functions
+ support for custom IFormatProvider when converting values
+ reading connection strings

##Roadmap

+ Easier methods for opening web.config (compare to WebConfigurationManager)
+ reading complete sections into type safe objects
+ support for write (save, update, replace etc.) methods

##License

Library is licensed under [LGPL v3.0](http://www.gnu.org/licenses/lgpl-3.0-standalone.html). 
