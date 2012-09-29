<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.ascx.cs" Inherits="Patti.Web.Controls.ShoppingCart" %>
<div id="shoppingPanel">
  <div class="modA">
        <div class="modBody">
            <table class="shopCart" id="shopCart" summary="我已挑选的商品">
                <thead>
                    <tr>
                        <th scope="col">
                            图片
                        </th>
                        <th scope="col">
                            数量
                        </th> 
                        <th scope="col">
                            单价(元)
                        </th>
                        <th scope="col">
                            小计(元)
                        </th>
                        <th scope="col">
                            操作
                        </th>
                    </tr>
                </thead>
                <tbody id="cartContainer">
                    <asp:Literal ID="ltShoppingCartPanel" runat="server"></asp:Literal>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9">
                            <div class="total clearfix">
                                <div class="coupon fl clearfix">
                                    <a href="/Product/" title="继续购物" class="button fl">继续购物</a>
                                    <a id="clearCart" href="javascript:;" title="清空购物车" class="button fl">清空购物车</a>
                                </div>
                                <ul class="priceContainer fr">
                                    <li class="hidden"><span>现金支付：</span><em>￥<asp:Literal ID="ltCashPayment" runat="server" Text="0.00" /></em></li>
                                    <li><span>合计金额：</span><b class="colorRed">￥<asp:Literal ID="ltAmountTotal" runat="server" Text="0.00" /></b></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div> 
    </div>
  <div class="closeSale">
        <a id="checkOut" href="javascript:;" title="去结算" class="button buttonE" ref="<%=UrlHelper.BuildNomalUrl("Shopping/Settlement.aspx")%>">去结算</a>
    </div>
</div>