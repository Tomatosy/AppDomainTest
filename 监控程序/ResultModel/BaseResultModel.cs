namespace Tomato.FinanceMonitor.StratWinConsole
{

    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    public abstract class BaseResultModel<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public abstract bool IsSuccess { get; }

        /// <summary>
        /// 值
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public virtual string ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public virtual string ErrorMessage { get; set; }
    }
}
