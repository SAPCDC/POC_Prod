using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Notifications : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        try
        {
            string body = string.Empty;
            context.Request.InputStream.Position = 0;

            using (var inputStream = new StreamReader(context.Request.InputStream))
            {
                body = inputStream.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(body))
            {
                dynamic json = JObject.Parse(body);

                // Access the webhook payload data ie, get first answer:
                var answers = json.form_response.asnwers;
                //Console.WriteLine(answers);
                context.Response.Write("<script>console.log('" + answers + "');</script>");
            }

            context.Response.StatusCode = 200;

            context.Response.Write("WebHook Handler is working fine...");

            context.Response.Flush();
            context.Response.SuppressContent = true;
            context.ApplicationInstance.CompleteRequest();

            //context.Response.End();
        }
        catch (Exception ex)
        {
            context.Response.Write("<script>console.log('Error is WebHook.ashx Is : " + ex.ToString().Replace("'", "_").Replace("\"", "_").Replace("\r\n", "\\n").Replace("\r", "\\n").Replace("\\", "\\\\") + "');</script>");
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
}