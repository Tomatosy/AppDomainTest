using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Tomato.FinanceMonitor.StratWinConsole
{
    public class WebApiUtil
    {
        /// <summary>
        /// 调用WebApi,参数为单值
        /// </summary>
        /// <param name="url"></param>
        public string UploadData(string baseUrl, string methodType)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");

            byte[] responseData = client.UploadData(baseUrl, methodType, new byte[0]);

            client.Dispose();

            string retStr = Encoding.UTF8.GetString(responseData);

            return retStr;
        }

        /// <summary>
        /// 调用WebApi,参数为Json
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="strJson"></param>
        /// <param name="methodType"></param>
        /// <returns></returns>
        public string UploadJson(string baseUrl, string strJson, string methodType)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            client.Encoding = Encoding.UTF8;

            string responseData = client.UploadString(baseUrl, methodType, strJson);

            client.Dispose();

            return responseData;
        }

        /// <summary>
        /// 调用WebApi,参数为单直+HttpPostFile
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="filePath"></param>
        /// <param name="methodType"></param>
        /// <returns></returns>
        public string UploadFile(string baseUrl, string filePath, string methodType)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            byte[] responseData = client.UploadFile(baseUrl, methodType, filePath);
            client.Dispose();
            string retStr = Encoding.UTF8.GetString(responseData);
            return retStr;
        }

        /// <summary>
        /// 下载文件方法
        /// </summary>
        /// <param name="serverPath">被下载的文件地址（服务器地址包括文件）</param>
        /// <param name="filePath">另存放的路径（本地需要存储文件的文件夹地址）</param>
        public string Download(string serverPath, string filePath)
        {
            WebClient client = new WebClient();
            string fileName = serverPath.Substring(serverPath.LastIndexOf("/") + 1); ;//被下载的文件名
            if (serverPath.LastIndexOf("\\") != -1)
            {
                fileName = serverPath.Substring(serverPath.LastIndexOf("\\") + 1); ;//被下载的文件名
            }
            filePath = filePath + fileName;
            client.DownloadFile(serverPath, filePath);
            return filePath;
        }
    }
}
