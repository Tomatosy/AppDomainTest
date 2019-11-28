using ByeTest;
using HelloTest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class TestCla
    {
        public string saidHello()
        {
            ResultModel result = new ResultModel();
            result.SaidSomething = new HelloTestCla().write();
            return JsonConvert.SerializeObject(result);
        }

        public string saidBye()
        {
            ResultModel result = new ResultModel();
            result.SaidSomething = new ByeTestCla().write();
            return JsonConvert.SerializeObject(result);
        }
    }
}
