namespace Wojoz.Payment
{
    /// <summary>
    /// 支付方式统一接口
    /// </summary>
    public interface IPayment
    {
        #region Methods
        /// <summary>
        /// POST处理支付
        /// </summary>
        /// <param name="order">订单支付信息</param> 
        void PostProcessPayment(PaymentInfo order);
        #endregion
    }
}
