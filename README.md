# InstagramApi
Tokenless API for Instagram. Get account information, media, explore tags and user feed without any applications and other crap.
This wrapper provides basic media from instagram, some of them even without authorization

#### Current version: 1.1.* [Under development]

#### Build status
----

[![Build status](https://ci.appveyor.com/api/projects/status/83fewc6yvre766ll?svg=true)](https://ci.appveyor.com/project/a-legotin/instagramapi)
[![Build Status](https://travis-ci.org/a-legotin/InstagramApi.svg?branch=master)](https://travis-ci.org/a-legotin/InstagramApi)

----
## Cross-platform by design
Build with dotnet core. Can be used on Mac, Linux, Windows.

## Easy to install
Use library as dll, reference from nuget or clone source code.

## Easy to use
#### Use builder to get Insta API instance:
```c#
var api = new InstaApiBuilder()
                .UseLogger(new SomeLogger())
                .UseHttpClient(new SomeHttpClient())
                .SetUserName(SomeUsername)
                .Build();
```
##### Note: every API method has Async implementation as well
#### Get user:
```c#
InstaUser user = api.GetUser();
```

#### Get all user posts:
```c#
InstaPostList posts = api.GetUserPosts();
```

#### Get media by its code:
```c#
InstaMedia mediaItem = api.GetMediaByCode();
```
