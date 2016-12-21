# InstagramAPI-Web
Tokenless, butthurtless private API for Instagram. Get account information, media, explore tags and user feed without any applications and other crap.
This wrapper provides basic media from instagram, some of them even without authorization.
Note that: there is a [repository based on Instagram API for mobile devices](https://github.com/a-legotin/InstagramAPI). This one Instagram API based on web-version of Instagram and provides only limited set of methods to work with Instagram. 

[![Build status](https://ci.appveyor.com/api/projects/status/nb2h0hyxtkjuskhl?svg=true)](https://ci.appveyor.com/project/a-legotin/instagramapi-web)
[![Build Status](https://travis-ci.org/a-legotin/InstagramAPI-Web.svg?branch=master)](https://travis-ci.org/a-legotin/InstagramAPI-Web)

#### Current version: 1.1.* [Under development]
#### [Why two separate repos with same mission?](https://github.com/a-legotin/InstagramAPI/wiki/Difference-between-API-Web-and-just-API-repositories)
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

####[Known issues](https://github.com/a-legotin/InstagramAPI/issues?q=is%3Aopen+is%3Aissue+label%3Aknown)

####[WIKI](https://github.com/a-legotin/InstagramAPI/wiki)

# License

MIT

# Terms and conditions

- Provided project MUST NOT be used for marketing purposes
- I will not provide support to anyone who wants this API to send massive messages/likes/follows and so on
- Use this API at your own risk

## Legal

This code is in no way affiliated with, authorized, maintained, sponsored or endorsed by Instagram or any of its affiliates or subsidiaries. This is an independent and unofficial API.
