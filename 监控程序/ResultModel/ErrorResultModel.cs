namespace Tomato.FinanceMonitor.StratWinConsole
{
    /// <summary>
    /// 错误返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ErrorResultModel<T> : BaseResultModel<T>
    {
        /// <summary>
        ///是否成功
        /// </summary>
        public override bool IsSuccess
        {
            get { return false; }
        }

        /// <summary>
        /// 错误编号
        /// </summary>
        public override string ErrorCode { get; set; }

        /// <summary>
        /// 错误内容
        /// </summary>
        public override string ErrorMessage { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public ErrorResultModel()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="errorCode">错误编号</param>
        /// <param name="errorMessage">错误内容</param>
        public ErrorResultModel(string errorCode,string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }
    }
}
