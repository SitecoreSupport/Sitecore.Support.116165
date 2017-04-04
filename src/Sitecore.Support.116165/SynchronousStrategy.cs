
namespace Sitecore.Support.ContentSearch.Maintenance.Strategies
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Data;
    using Diagnostics;
    using Events;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Diagnostics;
    using Sitecore.ContentSearch.Maintenance;

    [DataContract]
    public class SynchronousStrategy : Sitecore.ContentSearch.Maintenance.Strategies.SynchronousStrategy, ISearchIndexInitializable
    {
        public SynchronousStrategy(string database) : base(database)
        {
        }

        protected T ExtractParameter<T>(EventArgs args, int index) where T : class
        {
            return Event.ExtractParameter<T>(args, index);
        }

        public void RunExtended(EventArgs args, bool rebuildDescendants)
        {
            ItemUri itemUri = this.ExtractParameter<object>(args, 1) as ItemUri;
            if (itemUri == null)
            {
                base.Run(args, rebuildDescendants);
            }
            else
            {
                base.Run(itemUri, rebuildDescendants, false, false);
            }
        }

        void ISearchIndexInitializable.Initialize(ISearchIndex index)
        {
            Assert.IsNotNull(index, "index");
            CrawlingLog.Log.Info($"[Index={index.Name}] SUPPORT Initializing SynchronousStrategy", null);
            FieldInfo field = typeof(Sitecore.ContentSearch.Maintenance.Strategies.SynchronousStrategy).GetField("index", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(field, "SUPPORT Can't find the index field...");
            field.SetValue(this, index);
            EventHub.ItemMoved += (sende, args) => base.Run(this.ExtractParameter<ItemUri>(args, 0), true);
            EventHub.ItemCopied += (sende, args) => base.Run(this.ExtractParameter<ItemUri>(args, 0), true);
            EventHub.ItemUpdated += (sender, args) => this.RunExtended(args, false);
            EventHub.ItemVersionAdded += (sender, args) => base.RunAddedVersion(args);
            EventHub.ItemVersionDeleted += (sender, args) => base.RunDeletedVersion(this.ExtractParameter<ItemUri>(args, 0));
            EventHub.ItemDeleted += (sender, args) => base.RunDeleted(this.ExtractParameter<ID>(args, 0), this.ExtractParameter<string>(args, 1));
        }
    }
}