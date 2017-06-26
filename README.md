# Escc.Umbraco.MediaFileSavedIncorrectlyChecker

Umbraco saves media items into folders by incrementing a counter initialised with the number of the highest existing folder found on the file system. This is a problem in a load-balanced or staged environment where Umbraco can run on multiple machines, logged as [U4-2412](http://issues.umbraco.org/issue/U4-2412). 

The counter can be initialised with different values on each machine leading to the files for multiple media items being saved into the same folder. This in turn leads to files being deleted unexpectedly, because when one of the media items is deleted its entire folder is deleted too, including the incorrectly-saved files.

The fix for this is expected to be enabled in Umbraco 8. In the meantime this tool detects and reports instances of the problem when storing media files in Azure blob storage using [UmbracoFileSystemProviders.Azure](https://github.com/east-sussex-county-council/UmbracoFileSystemProviders.Azure). 

The email reporting the problem refers to the 'Editor Tools' section of Umbraco. This is made available by the [Escc.Umbraco.EditorTools](https://github.com/east-sussex-county-council/Escc.Umbraco.EditorTools) project. 

See `app.example.config` for the required configuration settings. 