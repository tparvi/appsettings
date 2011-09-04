##Description

Library (.NET 4.0, C#) for reading and writing application settings in standard .config (app.config, Web.Config) files using generic type safe methods. For more information see [Wiki](https://github.com/tparvi/appsettings/wiki). The library functionality supports only the appSettings section. There is no support for custom configuration sections.

Latest stable release is v1.1 which is available for [download](https://github.com/downloads/tparvi/appsettings/AppSettings_v1.1.zip)

## Features

+ Reading and writing settings in the appSettings and connectionStrings sections
+ Supports standard .NET types, enums and nullables via generic type safe methods
+ Optional settings (default value you specify is returned if setting is not found)
+ Custom conversion functions
+ Custom IFormatProvider (e.g. CultureInfo.GetCultureInfo("fi-FI")
+ Write/read settings directly into public properties of any object
+ Ignore properties using attributes when using ReadInto/WriteInto methods

##License

Library is licensed under [LGPL v3.0](http://www.gnu.org/licenses/lgpl-3.0-standalone.html). 
