# Sitecore Output Cache Module

Sitecore Output Cache Module is a module for Sitecore that dynamically sets output browser cache response headers for each page in the website.

## Pre-requisites
Currently this module is only for Sitcore 7.2-8.0 with an MVC implementation. Add your Sitecore.Kernel and Sitecore.Mvc binaries to /libs.

## Installation
Install /builds/sitecore packages/SItecore SharedSource OutputCache (Templates)-1.0.zip

*Full package inlcuding binaries to follow*

## Instructions for Use
Add the **Output Cache** template to the inheritance of your **Page Templates**. You can set the value or leave it blank to do a sliding scale output cache. The Max Age value is set in seconds.

Add the **Sitecore.SharedModules.OutputCache.config** file to your includes file.

This will now set the output cache response headers. You can check these using the Network tab in Chrome Developer tools.

See this blog post for more information on how to use this module:

[http://www.sitecorenutsbolts.net/2015/06/02/get-your-output-caching-right](http://www.sitecorenutsbolts.net/2015/06/02/get-your-output-caching-right)


