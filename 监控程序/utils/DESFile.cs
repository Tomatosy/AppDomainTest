using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Tomato.FinanceMonitor.StratWinConsole
{

    /// <summary>
    /// CryptHelp
    /// </summary>
    public static class DESFile
    {
        /// <summary>
        /// 密钥，这个密码可以随便指定
        /// </summary>
        public static string sSecretKey = "?\a??64(?";



        public static void CreateZipFile(string filesPath, string zipFilePath, int compresssionLevel, string password, string comment)
        {
            if (!Directory.Exists(filesPath))
            {
                return;
            }
            ZipOutputStream stream = new ZipOutputStream(File.Create(zipFilePath));
            if (compresssionLevel != 0)
            {
                stream.SetLevel(compresssionLevel);//设置压缩级别
            }

            if (!string.IsNullOrEmpty(password))
            {
                stream.Password = password;//设置zip包加密密码
            }

            if (!string.IsNullOrEmpty(comment))
            {
                stream.SetComment(comment);//设置zip包的注释
            }

            byte[] buffer = new byte[4096]; //缓冲区大小
            string[] filenames = Directory.GetFiles(filesPath, "*.*", SearchOption.AllDirectories);
            foreach (string file in filenames)
            {
                ZipEntry entry = new ZipEntry(file.Replace(filesPath, ""));
                entry.DateTime = DateTime.Now;
                stream.PutNextEntry(entry);
                using (FileStream fs = File.OpenRead(file))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }
            stream.Finish();
            stream.Close();
        }


        #region 制作压缩包（多个文件压缩到一个压缩包，支持加密、注释）
        /// <summary>
        /// 制作压缩包（多个文件压缩到一个压缩包，支持加密、注释）
        /// </summary>
        /// <param name="topDirectoryName">压缩文件目录</param>
        /// <param name="zipedFileName">压缩包文件名</param>
        /// <param name="compresssionLevel">压缩级别 1-9</param>
        /// <param name="password">密码</param>
        /// <param name="comment">注释</param>
        public static void ZipFiles(string topDirectoryName, string zipedFileName, int compresssionLevel, string password, string comment)
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.Open(zipedFileName, FileMode.OpenOrCreate)))
            {
                if (compresssionLevel != 0)
                {
                    zos.SetLevel(compresssionLevel);//设置压缩级别
                }

                if (!string.IsNullOrEmpty(password))
                {
                    zos.Password = password;//设置zip包加密密码
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    zos.SetComment(comment);//设置zip包的注释
                }

                foreach (string file in Directory.GetFiles(topDirectoryName))
                {
                    if (File.Exists(file))
                    {
                        FileInfo item = new FileInfo(file);
                        FileStream fs = File.OpenRead(item.FullName);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);

                        ZipEntry entry = new ZipEntry(item.Name);
                        zos.PutNextEntry(entry);
                        zos.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
        #endregion

        #region 解压缩包（将压缩包解压到指定目录）
        /// <summary>
        /// 解压缩包（将压缩包解压到指定目录）
        /// </summary>
        /// <param name="zipedFileName">压缩包名称</param>
        /// <param name="unZipDirectory">解压缩目录</param>
        /// <param name="password">密码</param>
        public static void UnZipFiles(string zipedFileName, string unZipDirectory, string password)
        {
            using (ZipInputStream zis = new ZipInputStream(File.Open(zipedFileName, FileMode.OpenOrCreate)))
            {
                if (!string.IsNullOrEmpty(password))
                {
                    zis.Password = password;//有加密文件的，可以设置密码解压
                }

                ZipEntry zipEntry;
                while ((zipEntry = zis.GetNextEntry()) != null)
                {
                    //string directoryName = Path.GetDirectoryName(unZipDirectory);
                    string directoryName = unZipDirectory;
                    string pathName = Path.GetDirectoryName(zipEntry.Name);
                    string fileName = Path.GetFileName(zipEntry.Name);

                    pathName = pathName.Replace(".", "$");
                    directoryName += "\\" + pathName;

                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        FileStream fs = File.Create(Path.Combine(directoryName, fileName));
                        int size = 2048;
                        byte[] bytes = new byte[2048];
                        while (true)
                        {
                            size = zis.Read(bytes, 0, bytes.Length);
                            if (size > 0)
                            {
                                fs.Write(bytes, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        fs.Close();
                    }
                }
            }
        }
        #endregion

    }
}