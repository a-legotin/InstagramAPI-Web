# InstagramApi
Tokenless API for Instagram. Get account information, media, explore tags and user feed without any authorization, applications and other crap.

#### Current version: 1.0.0 [Under development]

#### Build status
----

[![Build status](https://ci.appveyor.com/api/projects/status/83fewc6yvre766ll?svg=true)](https://ci.appveyor.com/project/a-legotin/instagramapi)
[![Build Status](https://travis-ci.org/a-legotin/InstagramApi.svg?branch=master)](https://travis-ci.org/a-legotin/InstagramApi)

----
## Cross-platmorm by design
Build with dotnet core. Can be executed on Mac, Linux, Windows.

## Easy to use
Use builder to get Insta API instance:
```c#
var api = new InstaApiBuilder()
                .UseLogger(new SomeLogger())
                .UseHttpClient(new SomeHttpClient())
                .SetUserName(SomeUsername)
                .Build();
```

## Easy to install
Use library as dll, reference from nuget or clone source code.
