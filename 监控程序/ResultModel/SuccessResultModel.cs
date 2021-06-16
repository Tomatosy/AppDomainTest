namespace Tomato.FinanceMonitor.StratWinConsole
{
    /// <summary>
    /// 成功信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SuccessResultModel<T> : BaseResultModel<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public override bool IsSuccess
        {
            get { return true; }
        }

        /// <summary>
        /// 构造
        /// </summary>
        public SuccessResultModel()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="model">值</param>
        public SuccessResultModel(T model)
        {
            this.Data = model;
        }
    }
}
