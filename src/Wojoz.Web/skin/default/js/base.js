if (!window["Wojoz"]) { Wojoz = {}; };
Wojoz["Config"] = {
    WojozsiteConfig: {
        Domain: "pt9999.com",
        ProductImageUrl: "/UploadFiles/Product/",
        ProductUrlRewriteName: "/Product/Patti-Jewelry",
        WojozResources: "/WojozResources/Defualt/",
        WojozResourcesImages: "/WojozResources/Defualt/images/",
        WojozResourcesScripts: "/WojozResources/Defualt/Scripts/",
        WojozResourcesScriptPlugins: "/WojozResources/Defualt/Scripts/Plugins/",
        WojozResourcesStyles: "/WojozResources/Defualt/Styles/",
        UrlPathMappings: {
            WWWSite: "http://www.pt9999.com"
        }
    }
};
Wojoz["ResourceConfig"] = {
    StringResourceConfig: {
        SearchValueNull: "搜索属于我的钻饰",
        SystemErrorInfo: "系统繁忙\uff0c请稍后重试\uff0c谢谢\uff01"
    }
}
var m = document.uniqueID && document.compatMode && !window.XMLHttpRequest && document.execCommand;
try {
    if (!!m) {
        m("BackgroundImageCache", false, true)
    }
} catch (oh) { }

var SysEC = {};
var ua = navigator.userAgent.toLowerCase();
var s; (s = ua.match(/msie ([\d.]+)/)) ? SysEC.ie = s[1] : (s = ua.match(/firefox\/([\d.]+)/)) ? SysEC.firefox = s[1] : (s = ua.match(/chrome\/([\d.]+)/)) ? SysEC.chrome = s[1] : (s = ua.match(/opera.([\d.]+)/)) ? SysEC.opera = s[1] : (s = ua.match(/version\/([\d.]+).*safari/)) ? SysEC.safari = s[1] : 0;
var version;
if (SysEC.ie) {
    version = "ie ie_" + SysEC.ie.substring(0, 1)
}
if (SysEC.firefox) {
    version = "firefox firefox_" + SysEC.firefox.substring(0, 1)
}
if (SysEC.chrome) {
    version = "chrome chrome_" + SysEC.chrome.substring(0, 1)
}
if (SysEC.opera) {
    version = "opera opera_" + SysEC.opera.substring(0, 1)
}
if (SysEC.safari) {
    version = "safari safari_" + SysEC.safari.substring(0, 1)
}
document.documentElement.className = version;

///	<summary>
/// StringBuilder
///	</summary>  
function StringBuilder(value) {
    this.strings = new Array("");
    this.Append(value);
}

StringBuilder.prototype.Append = function (value) {
    if (value) {
        this.strings.push(value);
    }
}
StringBuilder.prototype.Clear = function () {
    this.strings.length = 1;
}

StringBuilder.prototype.ToString = function () {
    return this.strings.join("");
}

String.prototype.replaceAll = function (s1, s2) {
    return this.replace(new RegExp(s1, "gm"), s2);
}

///	<summary>
/// usingNamespace
///	</summary> 
window.usingNamespace = function (A) {
    var C = window;
    if (!(typeof (A) === "string" && A.length != 0)) {
        return C
    }
    var F = C;
    var D = A.split(".");
    for (var B = 0; B < D.length; B++) {
        var E = D[B];
        if (!C[E]) {
            C[E] = {}
        }
        F = C = C[E]
    }
    return F
};

usingNamespace("Wojoz.Utils")["String"] = {
    IsNullOrEmpty: function (src) {
        return !(typeof (src) === "string" && src.length != 0)
    },
    Trim: function (src) {
        return src.replace(/^\s+|\s+$/g, "")
    },
    TriWojozt: function (src, sPattern) {
        if ($String.IsNullOrEmpty(sPattern)) {
            sPattern = "\\s"
        }
        var rePattern = new RegExp("^" + sPattern + "*", "ig");
        return src.replace(rePattern, "")
    },
    TrimEnd: function (src, sPattern) {
        if ($String.IsNullOrEmpty(sPattern)) {
            sPattern = "\\s"
        }
        var rePattern = new RegExp(sPattern + "*$", "ig");
        return src.replace(rePattern, "")
    },
    SetLen: function (src, len) {
        if ($String.ActualLength(src) < len) {
            return src;
        } else {
            return src.substr(0, i) + "...";
        }
        return $.trim(src).replace(/[\u4e00-\u9fa5]/, "**").length;
    }
};
var $String = Wojoz.Utils.String;

///	<summary>
/// $State = Wojoz.State.Cookies
///	</summary>
usingNamespace("Wojoz")["State"] = {
    Cookies: {
        Name: {
            Customer: "Customer",
            Product: "Product"
        },
        Set: function (name, value) {
            var cv = "";
            if (typeof (value) == "string") {
                cv = escape(value)
            } else {
                if (typeof (value) == "object") {
                    var jsonv = Wojoz.State.Cookies.ToJson(Wojoz.State.Cookies.Get(name));
                    if (jsonv == false) {
                        jsonv = {}
                    }
                    for (var k in value) {
                        eval("jsonv." + k + '="' + value[k] + '"')
                    }
                    for (var k in jsonv) {
                        cv += k + "=" + escape(jsonv[k]) + "&"
                    }
                    cv = cv.substring(0, cv.length - 1)
                }
            }
            var expires,
            path,
            domain,
            secure;
            try {
                var c = $CookieConfig[name];
                if (null != c) {
                    domain = $WojozsiteConfig.Domain;
                    if (c.TopLevelDomain == false) {
                        domain = ""
                    }
                    var ad = $Converter.ToFloat(c.Expires);
                    if (ad > 0) {
                        var d = new Date();
                        d.setTime(d.getTime() + ad * 1000);
                        expires = d
                    }
                    path = c.Path;
                    secure = c.SecureOnly
                }
            } catch (ex) { }
            var cok = name + "=" + cv + ((expires) ? "; expires=" + expires.toGMTString() : "") + "; path=/" + ((domain) ? "; domain=" + domain : "") + ((secure) ? "; secure" : "");
            document.cookie = cok
        },
        Remove: function (cookieKey) {
            var expires = new Date();
            expires.setTime(expires.getTime() - 1);
            var cookieValue = Wojoz.State.Cookies.Get(cookieKey);
            document.cookie = cookieKey + "=" + cookieValue + "; expires=" + expires.toGMTString();
        },
        Clear: function (sName) {
            var sDomain, sPath, bSecureOnly;
            try {
                var oCookie;
                if (null != (oCookie = Wojoz.Config.CookieConfig[sName])) {
                    sDomain = $WojozsiteConfig.Domain;
                    sPath = oCookie.Path;
                    bSecureOnly = oCookie.SecureOnly
                }
            } catch (e) { }
            document.cookie = sName + "=" + ((sPath) ? ";path=" + sPath : "") + ((sDomain) ? ";domain=" + sDomain : "") + ";expires=Thu, 01-Jan-1900 00:00:01 GMT"
        },
        Get: function (G, B) {
            var D = new RegExp("(^| )" + G + "=([^;]*)(;|$)");
            var A = document.cookie.match(D);
            if (arguments.length == 2) {
                if (A != null) {
                    var C,
                    F = new RegExp("(^| |&)" + B + "=([^&]*)(&|$)");
                    var E = A[2];
                    var E = E ? E : document.cookie;
                    if (C = E.match(F)) {
                        return unescape(C[2])
                    } else {
                        return ""
                    }
                } else {
                    return ""
                }
            } else {
                if (arguments.length == 1) {
                    if (A != null) {
                        return unescape(A[2])
                    } else {
                        return ""
                    }
                }
            }
        },
        ToJson: function (cv) {
            var cv = cv.replace(new RegExp("=", "gi"), ":'").replace(new RegExp("&", "gi"), "',").replace(new RegExp(";\\s", "gi"), "',");
            return eval("({" + cv + (cv.length > 0 ? "'" : "") + "})")
        }
    }
};
var $State = Wojoz.State.Cookies;

///	<summary>
/// $Loading = Wojoz.Ajax.Loading
///	</summary>
usingNamespace("Wojoz")["UIHelper"] = {
    ImgPlayer: function (imgContainer, itemBox, curItem) {
        $(imgContainer + " > a:first").css("display", "block");
        $(imgContainer + " > a").not(":first").css('display', 'none');
        var j = 1;
        var bStart = false;
        var iFadeOut = 800;
        var iFadeIn = 800;
        var iSlide = 500;
        var iInterval = 5000;
        var itemCount = $("#itemCount").val();
        $(itemBox + ">a").each(function (i) {
            $(this).mouseover(function () {
                var iCurItem = $(imgContainer + " > a:visible").prevAll().length;
                if (i !== iCurItem) {
                    j = i;
                    $(imgContainer + " > a:visible").find("img").stop(true, true).fadeOut(iFadeOut, function () {
                        $(imgContainer + " > a:visible").css("display", "none");
                        $(imgContainer + " > a").eq(i).css("display", "block").find("img").fadeIn(iFadeIn);
                    });
                    $(this).addClass(curItem).siblings("a").removeClass(curItem);
                }
            }).find("div").css("cursor", "pointer");

        });
        //自动开始 
        bStart = setInterval(function () {
            $(itemBox + ">a").eq(j).mouseover();
            j++;
            if (j == itemCount) { j = 0; }
        }, iInterval);
    }
};
var $UIHelper = Wojoz.UIHelper;

///	<summary>
/// $Resource = Wojoz.Resource
///	</summary>
usingNamespace("Wojoz")["Resource"] = {
    BuildImage: function (sImageName) {
        return Environment.ResourceUrl + sImageName;
    },
    BuildContent: function (name) {
        return eval("Wojoz.ResourceConfig.StringResourceConfig." + name)
    }
};
var $Resource = Wojoz.Resource;

///	<summary>
/// $HttpUtility = Wojoz.HttpUtility
///	</summary>
usingNamespace("Wojoz")["HttpUtility"] = {
    UrlEncode: function (sValue) {
        return escape(sValue).replace(/\*/g, "%2A").replace(/\+/g, "%2B").replace(/-/g, "%2D").replace(/\./g, "%2E").replace(/\//g, "%2F").replace(/@/g, "%40").replace(/_/g, "%5F");
    },
    UrlDecode: function (sValue) {
        return unescape(sValue);
    }
};

var $HttpUtility = Wojoz.HttpUtility;

///	<summary>
/// $Url = Wojoz.Url
///	</summary>
usingNamespace("Wojoz")["Url"] = {
    BuildCurrentUrl: function (sPagePath) {
        return Environment.Url + "/" + $String.TriWojozt(sPagePath, "/");
    },
    BuildUrl: function (sPagePath) {
        return "/" + $String.TriWojozt(sPagePath, "/");
    },
    BuildProductUrl: function (NodeId, ProdId, ProdName) {
        return $WojozsiteConfig.ProductUrlRewriteName + "-c" + NodeId + "-" + ProdId + ".htm";
    },
    BuildImageUrl: function (imgName) {
        sResult = $WojozsiteConfig.UrlPathMappings.WWWSite;
        return sResult + $String.TriWojozt(sPagePath, "/")
    }
};
var $Url = Wojoz.Url;

var IsMouseOnOver = false;
usingNamespace("Biz.Common")["MiniCart"] = {
    Refresh: function (ItemNo, Action, o) {
        var sParam;
        var sUrl = $Url.BuildUrl("Ajax/Shopping/MiniCart.ashx");
        if (Action == "delete") {
            sParam = "Action=DelelteMiniCartItem&ItemNo=" + ItemNo;
        }
        if (Action == "load" || Action == "refresh") {
            sParam = "Action=LoadMiniCart";
        }
        $.ajax({
            type: "POST",
            dataType: "html",
            url: sUrl,
            cache: false,
            data: sParam + "&_=" + Math.random(),
            beforeSend: function (G) { },
            success: function (result) {
                Biz.Common.MiniCart.proccessed(result, Action, o)
            },
            complete: function (G, H) { },
            error: function () { }
        })
    },
    proccessed: function (result, Action, o) {
        if (Action == "delete") {
            if (result.indexOf("ERROR:") != -1) {
                alert(result.split(":")[1]);
                return;
            } else {
                window.location.reload();
                return;
            }
        } else {
            if (Action == "load") {
                Biz.Common.MiniCart.UpdateMiniCart(result);
                if (IsMouseOnOver == true) {
                    $(o).addClass("over")
                }
            } else {
                if (Action == "refresh") {
                    Biz.Common.MiniCart.UpdateMiniCart(result);
                    Biz.Common.MiniCart.UpdatePageHeader()
                }
            }
        }
    },
    UpdateMiniCart: function (result) {
        $(".cartCot").html(result);
    },
    UpdatePageHeader: function () {
        var Qty = $("#minicartTotalQty").val();
        $(".shortcut .cart b").html(Qty);
    },
    onoff: function (className) {
        var className = "." + className;
        var oElem = $(className);
        if (oElem.length) {
            if ($.browser.msie) {
                oElem.parent().hover(function () {
                    IsMouseOnOver = true;
                    Biz.Common.MiniCart.Refresh("", "load", this)
                },
				function () {
				    IsMouseOnOver = false;
				    oElem.parent().removeClass("over")
				})
            } else {
                oElem.parent().mouseover(function () {
                    IsMouseOnOver = true;
                    Biz.Common.MiniCart.Refresh("", "load", this)
                }).mouseout(function () {
                    IsMouseOnOver = false;
                    $(this).removeClass("over")
                })
            }
        }
    }
};

usingNamespace("Biz.Common")["Gold"] = {
    LoadTodayPrice: function (PattiGoldPrice, GoldBarPrice, DecorationPrice) {
        var PageName = Environment.PageName.toLowerCase();
        var url = location.href.toLowerCase();
        if (PageName.indexOf("default") != -1) {
            $("#td-gold b").html(PattiGoldPrice);
            $("#td-bar b").html(GoldBarPrice);
            $("#td-deco b").html(DecorationPrice);
        }
    }
}

usingNamespace("Biz.Common")["PageHeader"] = {
    DoSearch: function (ctrlPosition) {
        var sKeyWord = $.trim($("#txtKeyWord" + ctrlPosition).val());
        var sDefValue = $Resource.BuildContent("SearchValueNull"); 
        if (sKeyWord == null || sKeyWord == sDefValue || $String.IsNullOrEmpty(sKeyWord) == true) {
            sKeyWord = "";
        }
        location.href = "/Product/Search.aspx?keyword=" + $HttpUtility.UrlEncode(sKeyWord);
    },
    OnInit: function (ctrlPosition) {
        if ($("#txtKeyWord" + ctrlPosition) == null) {
            return
        }
        $("#txtKeyWord" + ctrlPosition).keydown(function (e) {
            if (e.keyCode == 13) {
                Biz.Common.PageHeader.DoSearch(ctrlPosition);
                return false
            }
        })
    },
    CurrentChannel: function () {
        var PageName = Environment.PageName.toLowerCase();
        var NavItems = $(".topNav li");
        if (PageName.indexOf("topic") != -1) {
            NavItems.eq(6).addClass("hover");
        } else if (PageName.indexOf("experience") != -1) {
            NavItems.eq(7).addClass("hover2");
        }
        else {
            NavItems.each(function () {
                var self = $(this);
                var curl = self.children("a").attr("href").toLowerCase();
                if (curl == PageName + ".htm") {
                    if (self.hasClass("Item1")) {
                        self.addClass("hover1");
                    } else {
                        self.addClass("hover");
                    }
                    self.siblings().removeClass("hover").removeClass("hover1").removeClass("hover2");
                }
            });
        }
    }
};

usingNamespace("Biz.Common")["PromptText"] = {
    systemError: function () {
        alert($Resource.BuildContent("SystemErrorInfo"))
    },
    textBoxFocus: function (oELem, Wojoz_UI_PromptText_initValue) {
        oELem[0].style.color = "#000000";
        if ($.trim(oELem.val()) == Wojoz_UI_PromptText_initValue) {
            oELem.val("")
        }
    },
    textBoxBlur: function (oELem, Wojoz_UI_PromptText_initValue) {
        if ($.trim(oELem.val()) == "") {
            oELem[0].style.color = "#999999";
            oELem.val(Wojoz_UI_PromptText_initValue)
        }
        if ($.trim(oELem.val()) == Wojoz_UI_PromptText_initValue) {
            oELem[0].style.color = "#999999"
        }
    },
    textBoxValueOrEmpty: function (elemId, value) {
        var oELem = $("#" + elemId);
        if (oELem[0] == null) {
            return
        }
        var Wojoz_UI_PromptText_initValue = value;
        oELem.focus(function () {
            Biz.Common.PromptText.textBoxFocus(oELem, Wojoz_UI_PromptText_initValue)
        });
        oELem.blur(function () {
            Biz.Common.PromptText.textBoxBlur(oELem, Wojoz_UI_PromptText_initValue)
        });
        Biz.Common.PromptText.textBoxBlur(oELem, Wojoz_UI_PromptText_initValue)
    }
};

usingNamespace("Biz.Common")["OnOffCtrl"] = {
    onoff_2: function (className) {
        var oElem = $(className);
        if (oElem.length) {
            oElem.mouseover(function () {
                oElem.removeClass("over");
                $(this).addClass("over")
            })
        }
    },
    onoff: function (className) {
        var sClassName = "." + className;
        var oElem = $(sClassName);
        if (oElem.length) {
            if ($.browser.msie) {
                oElem.parent().hover(function () {
                    $(this).addClass("over")
                },
				function () {
				    oElem.parent().removeClass("over")
				})
            } else {
                oElem.parent().mouseover(function () {
                    $(this).addClass("over")
                }).mouseout(function () {
                    $(this).removeClass("over")
                })
            }
        }
    },
    onoffDelay: function (className) {
        var vT1, vT2;
        var sClassName = "." + className;
        var oElem = $(sClassName);
        oElem.parent().mouseover(function () {
            var self = $(this);
            if ($(this).find(sClassName).css("display") == "block") {
                self.addClass("over");
                clearTimeout(vT2)
            } else {
                vT1 = setTimeout(function () {
                    self.addClass("over")
                },
				50)
            }
        }).mouseout(function () {
            var self = $(this);
            if (vT1) {
                clearTimeout(vT1);
                vT2 = setTimeout(function () {
                    self.removeClass("over")
                },
				50)
            }
        });
        oElem.mouseover(function () {
            $(this).parent().addClass("over")
        })
    }
};

