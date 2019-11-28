using System;

namespace Seasky.AssemblyLoader
{
	/// <summary>
	/// LoadException 的摘要说明。
	/// </summary>
	public class AssemblyLoadFailureException:Exception
	{
		public AssemblyLoadFailureException()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public override string Message
		{
			get
			{
				return "Assembly Load Failure";
			}
		}

	}
}
