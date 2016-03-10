using System.Web;
using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Web_Cloud_Platform.Common
{
    /// <summary>
    /// 系统处理上下文
    /// </summary>
    public class WdContext : HttpContextBase
    {
        private WdContext()
        {

        }

        public WdContext(HttpContext context) : this()
        {
            HttpContext = context;
        }

        /// <summary>
        /// 本次请求的HTTP上下文
        /// </summary>
        public HttpContext HttpContext { get; }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public IWdUser WdUser
        {
            get
            {
                if (!HttpContext.Items.Contains("WdUser")) return null;
                return (IWdUser)HttpContext.Items["WdUser"];
            }
            set
            {
                if (HttpContext.Items.Contains("WdUser"))
                {
                    HttpContext.Items["WdUser"] = value;
                }
                else
                {
                    HttpContext.Items.Add("WdUser", value);
                }
            }
        }

        /// <summary>
        /// 当前登录用户所属域
        /// </summary>
        public IDomain Domain => WdUser?.Domain;
    }
}