﻿using System;
using System.IO;

namespace Mygod.Skylark
{
    public partial class Download : DownloadablePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = RouteData.GetRelativePath(), dataPath = Server.GetDataFilePath(path);
            if (!File.Exists(dataPath))
            {
                Response.StatusCode = 404;
                return;
            }
            try
            {
                FileHelper.WaitForReady(dataPath, 10);
                var filePath = Server.GetFilePath(path);
                TransmitFile(filePath, Path.GetFileName(filePath));
            }
            catch
            {
                Response.StatusCode = 503;
                Response.StatusDescription = "文件尚在处理中，请稍后再试";
            }
        }
    }
}