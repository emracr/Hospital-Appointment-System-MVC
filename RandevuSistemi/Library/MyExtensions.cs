using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandevuSistemi.Library
{
    public static class MyExtensions
    {
        public static MvcHtmlString Submit(this HtmlHelper helper, ButtonType type = ButtonType.submit, string value = "Gönder")
        {
            string html = string.Format("<input type = '{0}' value = '{1}' />", type.ToString(), value);
            return MvcHtmlString.Create(html);
        }
        public static MvcHtmlString Reset(this HtmlHelper helper, ButtonType type = ButtonType.submit, string value = "Temizle")
        {
            string html = string.Format("<input type = '{0}' value = '{1}' />", type.ToString(), value);
            return MvcHtmlString.Create(html);
        }
    }
    public enum ButtonType { button = 0, submit = 1, reset = 2 }
}