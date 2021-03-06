using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.IO;


public static class ResponseHelper {
    public static void Redirect(this HttpResponse response, string url, string target, string windowFeatures) {
        if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase))
            && String.IsNullOrEmpty(windowFeatures))
            response.Redirect(url);
        else {
            Page page = (Page)HttpContext.Current.Handler;
            if (page == null)
                throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
            url = page.ResolveClientUrl(url);

            string script;
            if (!String.IsNullOrEmpty(windowFeatures))
                script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
            else
                script = @"window.open(""{0}"", ""{1}"");";

            script = String.Format(script, url, target, windowFeatures);
            ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
        }
    }
}