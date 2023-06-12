using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Model.Base
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
        /// Respone ErrorMessage
        /// </summary>
        /// 
        public string ErrorMessage { get; set; }
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
        public ServiceRespone OnError(object data, string errorMessage)
        {
            Data = data;
            Success = false;
            ErrorMessage = errorMessage;
            return this;
        }
        
        public ServiceRespone OnError(string errorMessage)
        {
            Data = null;
            Success = false;
            ErrorMessage = errorMessage;
            return this;
        }
    }
}
