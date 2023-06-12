using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.BASE.PlatformCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DOSE.BASE.Platform.Http
{
    public static class HttpExtension
    {
        public static Task<ServiceRespone> CallInternalAPI(this IHttpService httpService, string apiUrlKey, string apiPath, HttpMethod method, object param = null, string authorizationToken = "", Dictionary<string, string> header = null)
        {
            var apiUrl = ((BaseHttpClient)httpService).GetAPIUrl(apiUrlKey);
            var fullApi = "";
            return null;
        }
    }
}
