using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace MvcWebComponents.Extensions
{
    public static class AjaxExtensions
    {
        public static AjaxOptions GeneralOptions(this AjaxOptions ajaxOptions)
        {
            ajaxOptions.OnFailure = "ajaxFailure";
            ajaxOptions.OnSuccess = "ajaxSuccess";
            return ajaxOptions;
        }

        public static IHtmlString InnerElementActionLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            string actionName,
            string controllerName,
            RouteValueDictionary routeValues,
            AjaxOptions ajaxOptions)
        {
            var targetUrl = UrlHelper.GenerateUrl(null, actionName, controllerName, null, ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, true);
            return MvcHtmlString.Create(GenerateLink(linkText, targetUrl, ajaxOptions ?? new AjaxOptions(), null));
        }


        private static string GenerateLink(string linkText,
            string targetUrl,
            AjaxOptions ajaxOptions,
            IDictionary<string, object> htmlAttributes)
        {
            var a = new TagBuilder("a")
            {
                InnerHtml = linkText
            };

            a.MergeAttributes(htmlAttributes);
            a.MergeAttribute("href", targetUrl);
            a.MergeAttributes(ajaxOptions.ToUnobtrusiveHtmlAttributes());

            return a.ToString(TagRenderMode.Normal);
        }
    }
}
