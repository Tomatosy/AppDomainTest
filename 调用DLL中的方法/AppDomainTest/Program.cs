using Newtonsoft.Json;
using Seasky.AssemblyLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test;

namespace AppDomainTest
{
    internal class Program
    {

        //private static string assemblyDir = System.Configuration.ConfigurationManager.AppSettings["AssemblyDir"];
        private static string assemblyDir = System.AppDomain.CurrentDomain.BaseDirectory + @"Test.dll";

        private static void Main(string[] args)
        {
            // 1.通过Loader加载dll反射其方法
            // 2.反射方法中调用HelloTest的方法(实际中也可通过WebApi调用HelloTest中的方法)
            // 3.版本迭代通过更新HelloTest接口方法，和TestCla中的接口调用实现，这里只是调用的壳子
            Loader loader = new Loader();
            string result = loader.InvokeMethod<string>(assemblyDir, "saidHello", null);
            loader.Unload(assemblyDir);
            ResultModel resultModel = JsonConvert.DeserializeObject<ResultModel>(result);
            Console.WriteLine(resultModel?.SaidSomething);//just hello

            loader = new Loader();
            result = loader.InvokeMethod<string>(assemblyDir, "saidBye", null);
            loader.Unload(assemblyDir);
            resultModel = JsonConvert.DeserializeObject<ResultModel>(result);
            Console.WriteLine(resultModel?.SaidSomething);// just bye
        }
    }
}
