JustGiving API SDK
===================

.NET Quickstart
================

If you want the C# SDK, you can install it from NuGet.
Gallery link: http://nuget.org/List/Packages/justgiving-sdk
Comes with Silverlight support.

PM> Install-Package justgiving-sdk

Then:

      var client = new JustGiving.Api.Sdk.JustGivingClient("APIKEY");
      var page = client.Page.Retrieve("pageShortName");
      

PHP Quickstart
==============

Download the latest package from downloads (or git pull master).
Reference JustGivingClient.php

$client = new JustGivingClient("https://api-staging.justgiving.com/", "your-api-key", 1, "apiunittests@justgiving.com", "password");
$page = $client->Page->Retrieve("pageShortName");


Urls to visit
==============

SDKs to help developers code against the JustGiving APIs.

Some places to visit:

https://github.com/JustGiving/JustGiving.Api.Sdk

	GitHub repository with open source SDKs

http://apimanagement.justgiving.com/

	Sign up for an API account and create API keys

https://api.justgiving.com
	
	Documentation home page and usage information

http://www.justgiving.com/developer

	Our little home page.

http://www.justgiving.com/developer/simple-donation-integration 

	If you're trying to make donations using JustGiving

https://github.com/JustGiving/JustGiving.Api.Sdk/wiki

	For some quick hack-tastic examples in a few languages


Further Support
===============

If your're having issues that the resources above don't help with, feel free to email us at apisupport@justgiving.com