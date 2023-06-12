using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Service
{
    public interface ICacheService
    {
    }
    
    internal interface IMemoryCacheService : ICacheService
    {

    }
    internal interface IMemoryDistributedCache : IDistributedCache
    {
    }
}
