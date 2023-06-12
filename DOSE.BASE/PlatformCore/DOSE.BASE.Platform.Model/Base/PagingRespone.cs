using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Model.Base
{
    public class PagingRespone
    {
        public object PageData { get; set; }
        public object SummaryData { get; set; }
        public int Total { get; set; }
        public object CustomData { get; set; }
        public PagingRespone() { }
        public PagingRespone(object data, int total) {
            PageData = data;
            Total = total;
        }
    }
}
