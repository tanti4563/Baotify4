using System;
using System.Web;
using System.Web.Hosting;

namespace Service.Helper
{
    public class PathFileHelper
    {
        public static string GetFolderAbsolutePath(string FolderPath = "~/")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FolderPath))
                {
                    FolderPath = HttpContext.Current.Server.MapPath("~/");
                }

                bool isAbsolutePath = false;
                Uri uri;
                if (!Uri.TryCreate(FolderPath, UriKind.RelativeOrAbsolute, out uri))
                {
                    isAbsolutePath = false;
                }
                else if (!uri.IsAbsoluteUri)
                {
                    isAbsolutePath = false;
                }
                else if (uri.IsFile)
                {
                    if (uri.IsUnc)
                    {
                        isAbsolutePath = false;
                    }
                    else
                    {
                        isAbsolutePath = true;
                    }
                }

                if (isAbsolutePath == false)
                {
                    FolderPath = HostingEnvironment.MapPath(FolderPath);

                }
            }
            catch (Exception e)
            {

            }

            return FolderPath;
        }
    }
}
