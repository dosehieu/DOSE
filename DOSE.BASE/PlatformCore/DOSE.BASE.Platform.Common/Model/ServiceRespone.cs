using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Model
{
    public class ServiceRespone
    {
        /// <summary>
        /// Data respone
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Status respone
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Respone message to display in UI
        /// </summary>
        public string UserMessage { get; set; }
        /// <summary>
        /// Respone message for fix bug
        /// </summary>
        public string SystemMessage { get; set; }
        /// <summary>
        /// The constructor
        /// </summary>
        public ServiceRespone()
        {
            Data = null;
            Success = true;
            UserMessage = "";
            SystemMessage = "";
        }
        public ServiceRespone OnSuccess()
        {
            Success = true;
            return this;
        }
        public ServiceRespone OnSuccess(object data )
        {
            Data = data;
            Success = true;
            UserMessage = "";
            return this;
        }
        public ServiceRespone OnSuccess(object data, string userMessage)
        {
            Data = data;
            Success = true;
            UserMessage = userMessage;
            return this;
        }
        
        public ServiceRespone OnError(string userMessage, string systemMessage = "", object data = null)
        {
            Data = data;
            Success = false;
            UserMessage = userMessage;
            SystemMessage = systemMessage;
            return this;
        }
    }
}
