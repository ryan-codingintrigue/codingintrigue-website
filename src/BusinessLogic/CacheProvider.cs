using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Framework.Cache.Redis;
using Newtonsoft.Json;

namespace BusinessLogic
{
    public class CacheProvider : ICacheProvider
    {
        private readonly RedisCache _redisCache;
        private bool _isConnected;

        public CacheProvider(RedisCache redisCache)
        {
            _redisCache = redisCache;
        }

        public void Connect()
        {
            try
            {
                _redisCache.Connect();
                _isConnected = true;
            }
            catch (Exception)
            {
                _isConnected = false;
            }
        }

        public BlogEntryResult GetCachedBlogEntries()
        {
            return GetBlogEntryCache<BlogEntryResult>("BlogEntries");
        }

        public IEnumerable<BlogEntry> GetCachedRecentEntries()
        {
            return GetBlogEntryCache<IEnumerable<BlogEntry>>("RecentBlogEntries");
        }

        private T GetBlogEntryCache<T>(string key)
        {
            if (!_isConnected) return default(T);
            Stream outStream;
            if (!_redisCache.TryGetValue(key, out outStream)) return default(T);
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(outStream))
            using (var textReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(textReader);
            }
        } 

        public void SetCachedBlogEntries(BlogEntryResult blogEntries)
        {
             SetCachedBlogEntriesForKey(blogEntries, "BlogEntries");
        }

        public void SetCachedRecentEntries(IEnumerable<BlogEntry> blogEntries)
        {
            SetCachedBlogEntriesForKey(blogEntries, "RecentBlogEntries");
        }

        private void SetCachedBlogEntriesForKey(object blogEntries, string key)
        {
            if(!_isConnected) return;
            _redisCache.Set(key, null, context =>
            {
                context.SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1));
                var serializer = new JsonSerializer();
                using (var sw = new StreamWriter(context.Data))
                using (var textWriter = new JsonTextWriter(sw))
                {
                    serializer.Serialize(textWriter, blogEntries);
                }
            });
        }
    }
}