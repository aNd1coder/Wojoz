namespace Wojoz.Payment
{
    /// <summary>
    /// 支付信息
    /// </summary>
    public class PaymentInfo
    {
        #region Properties
        /// <summary>
        /// 系统订单编号
        /// </summary>
        public string SysOrderNo { get; set; }

        /// <summary>
        /// 支付网关地址
        /// </summary>
        public string PayOnlineProviderUrl { get; set; }

        /// <summary>
        /// 商户编号
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// 商户密钥
        /// </summary>
        public string MerchantKey { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public string OrderAmount { get; set; }

        /// <summary>
        /// 返回结果通知地址
        /// </summary>
        public string ResultNotifyURL { get; set; }

        #endregion
    }
}
