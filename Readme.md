JustGiving API SDK
===================

SDKs to help developers code against the JustGiving APIs.


.NET Quickstart
---------------

If you want the C# SDK, you can install it from [NuGet](http://nuget.org/List/Packages/justgiving-sdk).
Includes Silverlight support.

```powershell
PM> Install-Package justgiving-sdk
```

Then:
```csharp
var client = new JustGiving.Api.Sdk.JustGivingClient("APIKEY");
var page = client.Page.Retrieve("pageShortName");
```      

The pageShortName is the page [slug](https://en.wikipedia.org/wiki/Semantic_URL#Slug).

PHP Quickstart
---------------

Download the [latest snapshot](https://github.com/JustGiving/JustGiving.Api.Sdk/archive/master.zip) (or ```git pull master```).
Reference JustGivingClient.php

```php
$client = new JustGivingClient("https://api.sandbox.justgiving.com/", "your-api-key", 1, "apiunittests@justgiving.com", "password");
$page = $client->Page->Retrieve("pageShortName");
```

Urls to visit
-------------

Some places to visit:

* [GitHub repository with open source SDKs](https://github.com/JustGiving/JustGiving.Api.Sdk)
* [Sign up for an API account and create API keys](http://apimanagement.justgiving.com/)
* [Documentation home page and usage information](https://api.justgiving.com)
* [Our little home page.](http://www.justgiving.com/developer)
* [If you're trying to make donations using JustGiving](http://www.justgiving.com/developer/simple-donation-integration)
* [For some quick hack-tastic examples in a few languages](https://github.com/JustGiving/JustGiving.Api.Sdk/wiki)
* [Config file for Postman that has collections of our http endpoints. ](https://github.com/JustGiving/JustGiving.Api.Tools.Postman)
	
Further Support
---------------

If you're having issues that the resources above don't help with, feel free to email us at apisupport@justgiving.com
