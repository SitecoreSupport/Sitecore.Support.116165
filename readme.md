# Sitecore.Support.116165

After restoring an item from Recycle Bin or Archive, the SynchronousStrategy can produce the similar errors in log files: 
```
7076 13:34:41 ERROR Exception while handling event Sitecore.Data.Archiving.RestoreItemCompletedEvent
Exception: System.InvalidOperationException
Message: Could not extract parameter from event args.
Source: Sitecore.Kernel
   at Sitecore.Events.Event.ExtractParameter[T](EventArgs args, Int32 index)
   at Sitecore.ContentSearch.Maintenance.Strategies.SynchronousStrategy.Run(EventArgs args, Boolean rebuildDescendants)
   at Sitecore.ContentSearch.Maintenance.EventHub.UpdateItemHandler(Object obj)
   at Sitecore.ContentSearch.Maintenance.EventHub.OnRestoreItemCompletedHandler(RestoreItemCompletedEvent restoreItemCompletedEvent)
   at Sitecore.Eventing.Subscription`1.Invoke(Object instance, EventContext context)
   at Sitecore.Eventing.EventProvider.RaiseEvent(Object event, Type eventType, EventContext context)
```

## License  
This patch is licensed under the [Sitecore Corporation A/S License for GitHub](https://github.com/sitecoresupport/Sitecore.Support.116165/blob/master/LICENSE).  

## Download  
Downloads are available via [GitHub Releases](https://github.com/sitecoresupport/Sitecore.Support.116165/releases).  

[![Github All Releases](https://img.shields.io/github/downloads/SitecoreSupport/Sitecore.Support.116165/total.svg)](https://github.com/SitecoreSupport/Sitecore.Support.116165/releases)
