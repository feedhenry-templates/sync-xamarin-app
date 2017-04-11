# sync-xamarin-app

Author: Erik Jan de Wit   
Level: Intermediate  
Technologies: C#, Windows, RHMAP
Summary: A demonstration of how to synchronize a single collection with RHMAP. 
Community Project : [Feed Henry](http://feedhenry.org)
Target Product: RHMAP  
Product Versions: RHMAP 3.7.0+   
Source: https://github.com/feedhenry-templates/sync-xamarin-app  
Prerequisites: fh-dotnet-sdk : 3.+, Visual Studio 2015 / 2013

## What is it?

This application manages items in a collection that is synchronized with a remote RHMAP cloud application.  The user can create, update, and delete collection items.  Refer to `sync-windows-app/sync-windows-app.Shared/fhconfig.json` for the relevant pieces of code and configuration.

If you do not have access to a RHMAP instance, you can sign up for a free instance at [https://openshift.feedhenry.com/](https://openshift.feedhenry.com/).

## How do I run it?  

### RHMAP Studio

This application and its cloud services are available as a project template in RHMAP as part of the "Sync Framework Project" template.

### Local Clone (ideal for Open Source Development)
If you wish to contribute to this template, the following information may be helpful; otherwise, RHMAP and its build facilities are the preferred solution.

## Build instructions

1. Clone this project

2. Populate ```sync-ios-app/fhconfig.plist``` or ```sync-android-app/Assets/fhconfig.properties``` with your values as explained [here](https://access.redhat.com/documentation/en-us/red_hat_mobile_application_platform/4.3/html/client_sdk/xamarin#xamarin-set-up-configuration).

3. Open sync-windows-app.sln

4. Run the project
 
## How does it work?

### Start synchronization

In ```sync-ios-app/RootController.cs``` and ```sync-android-app/ListOfItemsActivity.cs``` the synchronization loop is started.
```
  var client = FHSyncClient.GetInstance();
  var config = new FHSyncConfig();
  client.Initialise(config);   // [1]
  client.SyncCompleted += (sender, args) =>
  {
  UIApplication.SharedApplication.InvokeOnMainThread(delegate {
    _items = client.List<ShoppingItem>(DatasetId);
    TableView.ReloadData();
  });
  };
```
[1] Initialize with sync configuration.

[2] Initialize a sync client for a given dataset.
