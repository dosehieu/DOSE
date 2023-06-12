using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.BASE.PlatformCore.Database.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.BL.Model
{
    public class CoreServiceCollection : ICoreServiceCollection
    {
        public IConfigService ConfigService;
        public ICacheService CacheService;
        public IAuthService AuthService;
        public IDatabaseService DatabaseService;
        public CoreServiceCollection(IConfigService configService, ICacheService cacheService, IAuthService authService, IDatabaseService databaseService)
        {
            ConfigService = configService;
            CacheService = cacheService;
            AuthService = authService;
            DatabaseService = databaseService;
        }
    }
}
