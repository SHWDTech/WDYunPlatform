using System.Text;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;

namespace Lampblack_Platform.Common
{
    public class ExcelResult : ActionResult
    {
        private readonly ExcelPackage _excelPackage;

        private readonly string _fileName;

        public ExcelResult(ExcelPackage package, string fileName)
        {
            _excelPackage = package;
            _fileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            if (context.HttpContext.Request.UserAgent != null && context.HttpContext.Request.UserAgent.Contains("IE") || context.HttpContext.Request.Browser.Browser == "InternetExplorer")
            {
                context.HttpContext.Response.AppendHeader("Content-Disposition",
                    "attachment;  filename=" + HttpUtility.UrlEncode(_fileName, Encoding.UTF8));
            }
            else
            {
                context.HttpContext.Response.AppendHeader("Content-Disposition", "attachment;  filename=\"" + _fileName + "\"");
            }

            var bytes = _excelPackage.GetAsByteArray();
            context.HttpContext.Response.AppendHeader("Content-Length", bytes.Length.ToString());

            context.HttpContext.Response.BinaryWrite(bytes);
            context.HttpContext.Response.End();
        }
    }
}