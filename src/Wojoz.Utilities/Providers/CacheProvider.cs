using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Wojoz.Utilities
{
    /// <summary>
    /// Cache Provider 
    /// </summary>
    /// <remarks>
    /// Cache.Add方法，用法和Insert差不多，区别在于Add碰到该key原来有赋过值会失败，Insert则不会，而会替换原有值
    /// Add会返回被缓存数据项，Insert不会
    /// </remarks>
    public class CacheProvider
    {
        #region Cache Provider Singleton
        /// <summary>
        /// Cache Provider Singleton --- thread safety for static initialization
        /// </summary>
        private CacheProvider() { }
        private static readonly CacheProvider _instance = new CacheProvider();
        public static CacheProvider Instance
        {
            get { return _instance; }
        }
        #endregion

        #region 设置缓存
        /// <summary>
        /// 设置缓存,生命周期等于应用程序生命周期,不支持清除,过期,依赖性等功能
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheObject">值</param>
        public void SetCache(string cacheKey, object cacheObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;//System.Web.Caching.Cache objCache = System.Web.Hosting.HostingEnvironment.Cache;
            objCache.Insert(cacheKey, cacheObject);//objCache[cacheKey] = cacheObject; 
        }

        /// <summary>
        /// 设置缓存,指定过期时间,不支持清除,依赖性
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheObject">值</param>
        /// <param name="absoluteExpiration">固定的过期时间</param>
        public void SetCache(string cacheKey, object cacheObject, DateTime absoluteExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(
                cacheKey,
                cacheObject,
                null,
                absoluteExpiration,
                Cache.NoSlidingExpiration
            );
        }

        /// <summary>
        /// 设置缓存,指定过期时间,不支持清除,依赖性
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheObject">值</param>
        /// <param name="slidingExpiration">设置最后一次访问多长时间过期,不能小于0,不能超过1年</param>
        public void SetCache(string cacheKey, object cacheObject, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(
                cacheKey,
                cacheObject,
                null,
                Cache.NoAbsoluteExpiration,
                slidingExpiration
            );
        }

        /// <summary>
        /// 设置缓存,指定过期时间,不支持清除,依赖性
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheObject">值</param>
        /// <param name="absoluteExpiration">固定的过期时间</param>
        /// <param name="cacheItemPriority">设置内存不足,缓存自动清除时,缓存的重要性,可不可以清除</param>
        public void SetCache(string cacheKey, object cacheObject, DateTime absoluteExpiration, CacheItemPriority cacheItemPriority)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(
                cacheKey,
                cacheObject,
                null,
                absoluteExpiration,
                Cache.NoSlidingExpiration,
                cacheItemPriority,
                null
            );
        }

        /// <summary>
        /// 设置缓存,指定过期时间,不支持清除,依赖性
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheObject">值</param>
        /// <param name="slidingExpiration">设置最后一次访问多长时间过期,不能小于0,不能超过1年</param>
        /// <param name="cacheItemPriority">设置内存不足,缓存自动清除时,缓存的重要性,可不可以清除</param>
        public void SetCache(string cacheKey, object cacheObject, TimeSpan slidingExpiration, CacheItemPriority cacheItemPriority)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(
                cacheKey,
                cacheObject,
                null,
                Cache.NoAbsoluteExpiration,
                slidingExpiration,
                cacheItemPriority,
                null
            );
        }

        /// <summary>
        /// 设置缓存,指定依赖项
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheObject">值</param>
        /// <param name="dependency">依赖项</param>
        public void SetCache(string cacheKey, object cacheObject, CacheDependency dependency)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(
                cacheKey,
                cacheObject,
                dependency
            );
        }

        /// <summary>
        /// 设置缓存,指定过期时间,依赖项,优先级
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheObject">值</param>
        /// <param name="dependency">依赖项</param>
        /// <param name="absoluteExpiration">固定的过期时间</param> 
        /// <param name="slidingExpiration">设置最后一次访问多长时间过期,不能小于0,不能超过1年</param>
        /// <param name="cacheItemPriority">设置内存不足,缓存自动清除时,缓存的重要性,可不可以清除</param>
        public void SetCache(string cacheKey, object cacheObject, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority cacheItemPriority)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            CacheItemRemovedCallback onRemoveCallback = new CacheItemRemovedCallback(this.onRemovedCallback);
            objCache.Insert(
                cacheKey,
                cacheObject,
                dependency,
                absoluteExpiration,
                slidingExpiration,
                cacheItemPriority,
                onRemoveCallback
            );
        }

        /// <summary>
        /// 移除缓存回调函数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="reason">移除缓存的原因</param>
        public void onRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            if (reason == CacheItemRemovedReason.DependencyChanged) { }
            //Response.Write("文件变了，快去看看");
        }

        /// <summary>
        /// 更新缓存回调函数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="reason">更新缓存的原因</param>
        public void onUpdatedCallback(string key, object value, CacheItemUpdateReason reason)
        {
            if (reason == CacheItemUpdateReason.DependencyChanged) { }
            //Response.Write("文件变了，快去看看");
        }

        #endregion

        #region 读取缓存

        public object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        #endregion

        #region 清除缓存

        public void RemoveCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(cacheKey);
        }

        #endregion

        #region 统计当前缓存数

        public int CountCache()
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache.Count;
        }

        /// <summary>
        /// 自定义缓存刷新操作
        /// </summary>   
        /// 自定义刷新操作
        /// </summary>
        /// <param name="removedKey">移除的键</param>
        /// <param name="expiredValue">过期的值</param>
        /// <param name="removalReason">移除理由</param>
        void Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason)
        {
            if (removalReason == CacheItemRemovedReason.Expired)
            {
                SetCache(removedKey, expiredValue);
            }
        }
        #endregion
    }
}
