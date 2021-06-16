using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;

namespace Tomato.FinanceMonitor.StratWinConsole
{
    internal class Program
    {
        // 接口路径
        private static string ConnStr = "http://19366/TomatoFinanceMonitor/";

        // 程序名
        private static string exeFilName = "Tomato.financemonitor.winclient.exe";

        private static void Main(string[] args)
        {
            try
            {

                WriteLine("启动-----" + DateTime.Now);
                //// todo 打包文件夹 Test
                //PackZipByUrl("这是一个文件夹路径");
                //// end todo

                // 取服务器版本号
                WebApiUtil webClient = new WebApiUtil();
                string baseUrl = ConnStr + "Update/GetWinfromVersionVer";
                string result = webClient.UploadData(baseUrl, "POST");
                string downloadVer = result.Replace("\"", "");

                string localUrl = System.AppDomain.CurrentDomain.BaseDirectory + "downWinClient/";
                if (!Directory.Exists(localUrl))
                {
                    Directory.CreateDirectory(localUrl);
                }
                Process[] vProcesses = Process.GetProcesses();


                // 本地已有下载的最新版本
                if (System.IO.File.Exists(localUrl + exeFilName) && GetVersion(localUrl + exeFilName) == downloadVer)
                {
                    WriteLine($"本地{GetVersion(localUrl + exeFilName)},服务器{downloadVer}");
                    WriteLine("本地已有最新版本-----" + DateTime.Now);
                    // 判断是否已启动
                    foreach (Process vProcess in vProcesses)
                    {
                        if (vProcess.ProcessName.ToLower().Contains("Tomato.financemonitor.winclient"))
                        {
                            return;
                        }
                    }
                }
                else
                {
                    WriteLine("本地无最新版本-----" + DateTime.Now);
                    foreach (Process vProcess in vProcesses)
                    {
                        if (vProcess.ProcessName.ToLower().Contains("Tomato.financemonitor.winclient"))
                        {
                            vProcess.Kill();
                            vProcess.WaitForExit();
                            break;
                        }
                    }

                    webClient = new WebApiUtil();
                    baseUrl = ConnStr + "Update/GetWinfromVersion";
                    result = webClient.UploadData(baseUrl, "POST");

                    string downloadUrl = result.Replace("\"", "");
                    string downloadFileName = Path.GetFileName(downloadUrl);

                    WriteLine("更新本地版本-----" + DateTime.Now);
                    webClient = new WebApiUtil();
                    webClient.Download(downloadUrl, localUrl);
                    DESFile.UnZipFiles(localUrl + downloadFileName, localUrl, "Tomato");
                    WriteLine("本地版本已更新-----" + downloadUrl + DateTime.Now);
                }

                // 直接启动程序集  会导致AssemblyLoader.dll 的路径会从 lunch 目录底下开始找，修改AssemblyLoader为绝对路径
                System.Diagnostics.Process.Start(localUrl + exeFilName);

                WriteLine("正常启动-----");
            }
            catch (Exception ex)
            {
                WriteLine("异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 根据文件夹路径打包为zip
        /// </summary>
        private static void PackZipByUrl(string targetUrl)
        {
            string exeVer = GetVersion(targetUrl + @"Release\Tomato.FinanceMonitor.WinClient.exe");
            DESFile.CreateZipFile(targetUrl + "Release", targetUrl + $"{exeVer}.ZIP", 1, "Tomato", "海天前置机winfrom包");
        }


        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetVersion(string path)
        {
            string version = string.Empty;
            FileVersionInfo file = System.Diagnostics.FileVersionInfo.GetVersionInfo(path);
            //版本号显示为“主版本号.次版本号.内部版本号.专用部件号”。
            //version = String.Format("{0}.{1}.{2}.{3}", file.FileMajorPart, file.FileMinorPart, file.FileBuildPart, file.FilePrivatePart);
            //使用文件版本信息
            version = file.FileVersion;
            return version;
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="text"></param>
        private static void WriteLine(string text)
        {
            string logFile = AppDomain.CurrentDomain.BaseDirectory + @"Logs\";

            if (!Directory.Exists(logFile))
            {
                Directory.CreateDirectory(logFile);
            }
            string logPath = Path.Combine(logFile, DateTime.Now.ToString("yyyyMMdd") + ".txt");
            FileStream fileStream = new FileStream(logPath, FileMode.Append);
            try
            {
                text += "\r\n";
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    sw.WriteLine();
                    sw.WriteLine("######Error:######");
                    sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] "));
                    sw.WriteLine(ex.Message);
                }
            }
            finally
            {
                fileStream.Close();
            }
        }
    }
}
