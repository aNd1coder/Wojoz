/// <reference path="jquery-1.4.4.js" />
//select美化
$.fn.extend({
    jSelect: function () {
        return $(this).each(function () {
            var width = $(this).width(); //因为ff取不到auto
            $(this).after("<input type=\"hidden\" /><div class='jslct'><div class='jslct_t'><em></em></div><dl></dl></div>");
            var ipt = $(this).next("input"); //隐藏域
            var lst = ipt.next("div"); //模拟下拉菜单
            var itms = $("dl", lst); //模拟下拉菜单项
            var itm;
            var opt = $("option", $(this)); //原select下拉项
            var opts = $("option:selected", $(this));//选中项
            var opts_index = opt.index(opts);//选中项索引
            var em = $("em", lst);
            if (width != "") { lst.css("width", $(this).css("width")); em.css("width", "100%"); };
            ipt.attr("name", $(this).attr("name"));
            em.text($("option:selected", $(this)).text());
            opt.each(function (i) {
                itms.append("<dd></dd>");
                itm = $("dd", itms);
                var dd = itm.eq(i);
                dd.attr("val", $(this).val()).text($(this).text());
            });
            itm.eq(0).addClass("noborder")
            itm.eq(opts_index).addClass("jslcted");
            $(this).hide();
            lst.hover(function () { $(this).addClass("jslct_hover"); return false }, function () { $(this).removeClass("jslct_hover"); return false });
            itm.hover(function () { $(this).addClass("hover") }, function () { $(this).removeClass("hover") });
            itm.width(width - 8);
            itms.css({ width: width, "overflow-x": "hidden", "overflow-y": "auto" });
            lst.click(function () { lstshow(); });
            $(document).mouseup(function () { lsthide(); });
            itm.click(function () {
                itm.removeClass("jslcted");
                $(this).addClass("jslcted");
                em.text($(this).text());
                ipt.val($(this).attr("val"));
                lsthide();
                return false;
            })
            function lstshow() {
                var maxheight = $(document).height() - 200;
                itms.css({ height: "auto" });
                maxheight = itms.height() > maxheight ? maxheight : "auto";
                itms.css({ height: maxheight });
                itms.show();
                lst.css("z-index", "1000")
            };
            function lsthide() {
                $(".jslct dl").hide();
                $(".jslct").css("z-index", "0")
            };
        });
    }
});
