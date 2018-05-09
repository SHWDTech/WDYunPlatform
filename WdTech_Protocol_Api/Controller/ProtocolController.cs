using System;
using System.Web.Mvc;

namespace WdTech_Protocol_Api.Controller
{
    public class ProtocolController : System.Web.Mvc.Controller
    {
        // GET: Protocol
        public ActionResult Index(byte[] data, Guid deviceId)
        {
            Config.ProtocolService.Send(data, deviceId);
            return Json("");
        }
    }
}