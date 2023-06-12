using DOSE.BASE.PlatformCore.Common.Constant;
using DOSE.BASE.PlatformCore.Common.Utility;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DOSE.BASE.PlatformCore.Common.Service
{
    internal class CacheService: BaseCacheService, ICacheService
    {
        public CacheService(IDistributedCache cache, IConfigService configService, ILogService logService): base(configService, logService)
        {
            _cache = cache;
        }
    }

    internal class MemoryCacheService : BaseCacheService, IMemoryCacheService
    {
        public MemoryCacheService(IMemoryDistributedCache cache, IConfigService configService, ILogService logService) : base(configService, logService)
        {
            _cache = cache;
        }
    }

    internal class BaseCacheService 
    {
        protected IDistributedCache _cache;
        private readonly IConfigService _configService;
        private readonly ILogService _logService;

        public BaseCacheService(IConfigService configService, ILogService logService)
        {
            _configService = configService;
            _logService = logService;
        }

        public async Task Delete<T>(string key, bool isAppendAppCodeToKey = true)
        {
            key = AppendCacheKey(key, isAppendAppCodeToKey);
            try
            {
                await _cache.RemoveAsync(key);
            }catch(Exception ex)
            {
                _logService.LogError(ex, ex.Message);
            }
        }
        public async Task<T> Get<T>(string key, bool isAppendAppCodeToKey = true)
        {
            key = AppendCacheKey(key, isAppendAppCodeToKey);
            byte[] cacheValue = null;

            try
            {
                cacheValue = await _cache.GetAsync(key);
                
            }catch(Exception ex)
            {
                _logService.LogError(ex, ex.Message);
            }
            if (cacheValue != null && cacheValue.Length > 0)
            {
                try
                {
                    var jsonValue = Encoding.UTF8.GetString(cacheValue);
                    return Converter.Deserialize<T>(jsonValue);
                }
                catch
                {
                    return cacheValue.ToObject<T>();
                }
            }
            return default;
        }
        public async Task Set(string key, object value, TimeSpan timeout, bool isAbsoluteExpiration = true, bool isAppendAppCodeToKey = true)
        {
            key = AppendCacheKey(key, isAppendAppCodeToKey);
            var option = new DistributedCacheEntryOptions();
            if (isAbsoluteExpiration)
            {
                option.SetAbsoluteExpiration(timeout);
            }
            else
            {
                option.SetSlidingExpiration(timeout);
            }
            try
            {
                var jsonValue = Converter.Serialize(value);
                var cacheValue = Encoding.UTF8.GetBytes(jsonValue);
                await _cache.SetAsync(key, cacheValue, option);

                if (cacheValue.Length > 100 * 1024)
                {
                    _logService.LogInfo("Cache too long (>100 KB)");
                }

            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
            }
        }

        public async Task Set(string key, object value, bool isAbsoluteExpiration = true, bool isAppendAppCodeToKey = true)
        {
            await Set(key, value, TimeSpan.FromMinutes(20), isAbsoluteExpiration, isAppendAppCodeToKey);
        }
        public string AppendCacheKey(string key, bool isAppendAppCodeToKey)
        {
            var applicationCode = _configService.GetAppSetting(AppSettingKeys.ApplicationCode);
            return isAppendAppCodeToKey ? $"{(applicationCode ?? "")}_{key}" : key;
        }
    }
}
