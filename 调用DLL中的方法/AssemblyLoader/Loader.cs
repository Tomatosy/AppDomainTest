using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace Seasky.AssemblyLoader
{
    /// <summary>
    /// Class1 的摘要说明。
    /// </summary>
    public class Loader : IDisposable
    {
        public Loader()
        {
        }

        private AppDomain domain = null;
        private Hashtable domains = new Hashtable();
        private RemoteLoader rl = null;

        private void SetRemoteLoaderObject(string dllName)
        {

            //string aaaTest = "123";


            AppDomainSetup setup = new AppDomainSetup();
            // 启用影像复制程序集,将准备加载的程序集拷贝一份至CachePath,防止锁定
            setup.ShadowCopyFiles = "true";

            domain = AppDomain.CreateDomain(dllName, null, setup);

            // dll通过路径解析失败的话，
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            domains.Add(dllName, domain);
            object[] parms = { dllName };
            BindingFlags bindings = BindingFlags.CreateInstance |
                BindingFlags.Instance | BindingFlags.Public;
            try
            {
                rl = (Seasky.AssemblyLoader.RemoteLoader)domain.CreateInstanceFromAndUnwrap(
                    System.AppDomain.CurrentDomain.BaseDirectory + "Seasky.AssemblyLoader.dll", "Seasky.AssemblyLoader.RemoteLoader", true, bindings,
                    null, parms, null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string module = new AssemblyName(args.Name).Name;
            string dllDir = System.Configuration.ConfigurationManager.AppSettings["AssemblyDir"];
            string dll = Path.Combine(dllDir, string.Format("{0}.dll", module));
            return Assembly.LoadFrom(dll);
        }

        public Assembly LoadAssembly(string dllName)
        {
            try
            {
                SetRemoteLoaderObject(dllName);
                return rl.LoadAssembly(dllName);
            }
            catch (Exception ex)
            {
                throw new AssemblyLoadFailureException();
            }
        }


        public string GetVersion(string dllName)
        {
            try
            {
                SetRemoteLoaderObject(dllName);
                return rl.GetAssemblyVersion(dllName);
            }
            catch (Exception ex)
            {
                throw new AssemblyLoadFailureException();
            }
        }

        public T InvokeMethod<T>(string dllName, string methodName, object[] parameters)
        {
            try
            {
                SetRemoteLoaderObject(dllName);
                return (T)rl.InvokeMethod(dllName, methodName, parameters);
            }
            catch (Exception ex)
            {
                //throw new AssemblyLoadFailureException();
                throw;
            }
        }

        public ArrayList LoadAssemblies(ArrayList assemblyFileList)
        {
            ArrayList assemblies = new ArrayList();
            foreach (string assemblyFile in assemblyFileList)
            {
                Assembly a = LoadAssembly(assemblyFile);
                assemblies.Add(a);
            }

            return assemblies;
        }

        public void Unload(string dllName)
        {
            if (domains.ContainsKey(dllName))
            {
                AppDomain appDomain = (AppDomain)domains[dllName];
                AppDomain.Unload(appDomain);
                domains.Remove(dllName);
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
                GC.Collect();
                domain = null;
                rl = null;
            }
        }

        public void Unload()
        {
            dispose(true);
        }

        ~Loader()
        {
            dispose(false);
        }

        #region IDisposable 成员

        public void Dispose()
        {
            dispose(true);
        }

        #endregion

        private void dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (object o in domains.Keys)
                {
                    string dllName = o.ToString();
                    AppDomain appDomain = (AppDomain)domains[dllName];
                    AppDomain.Unload(appDomain);
                }
                domains.Clear();
            }
        }
    }
}
