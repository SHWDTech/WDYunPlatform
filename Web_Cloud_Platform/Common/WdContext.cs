﻿using System.Web;
using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Web_Cloud_Platform.Common
{
    public class WdContext : HttpContextBase
    {
        private WdContext()
        {

        }

        public WdContext(HttpContext context) : this()
        {
            HttpContext = context;
        }

        public HttpContext HttpContext { get; }

        public IWdUser WdUser
        {
            get
            {
                return (IWdUser)HttpContext.Items["WdUser"];
            }
            set
            {
                HttpContext.Items["WdUser"] = value;
            }
        }
    }
}