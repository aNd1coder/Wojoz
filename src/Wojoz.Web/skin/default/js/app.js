/// <reference path="jquery-1.4.1-vsdoc.js"
/// <reference path="conf.js" />
/// <reference path="Plugins/jquery.blockUI.js" />

/*
变量命名前缀规则:
数      组             -   a        -   aValues
布  尔   型             -   b        -   bFound
浮点型(数字)            -   f        -   fValue
函      数             -   fn       -   fnMethod
整  形(数字)            -   i        -   iValue
对      象             -   o        -   oType
正则表达式              -   re       -   rePattern
字  符  串              -  s         -  sValue
变型(可以是任何类型)      -  v         -  vValue
*/
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
/*
StringBuilder.prototype.AppendFormat = function (value) {
if (value) {
alert(value);
value = $String.Format(value);
alert(value);
this.strings.push(value);
}
}
*/
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

///	<summary>
/// $String = Wojoz.Utils.String
///	</summary>
var base64EncodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
var base64DecodeChars = new Array(
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63,
    52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1,
    -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
    15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1,
    -1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
    41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1);
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
    Camel: function (src) {
        return src.toLowerCase().replace(/\-([a-z])/g,
        function (B, C) {
            return C.toUpperCase()
        })
    },
    Format: function (src) {
        if (arguments.length == 0) return null;
        var args = Array.prototype.slice.call(arguments, 1);
        return src.replace(/\{(\d+)\}/g, function (m, i) {
            return args[i];
        });
    },
    ReplaceNullOrEmptyBySymbol: function (value, symbol) {
        if ($String.IsNullOrEmpty(value)) {
            return symbol;
        } else {
            return value;
        }
    },
    Repeat: function (src, iNum, sSeparator) {
        sSeparator = $String.IsNullOrEmpty(sSeparator) ? "," : sSeparator;
        for (var i = 0, aValue = new Array(iNum); i < iNum; i++) {
            aValue[i] = src
        }
        return aValue.join(sSeparator)
    },
    IsEqual: function (B, A) {
        if (B == A) {
            return true
        } else {
            return false
        }
    },
    IsNotEqual: function (B, A) {
        if (B == A) {
            return false
        } else {
            return true
        }
    },
    ActualLength: function (src) {
        return $.trim(src).replace(/[\u4e00-\u9fa5]/, "**").length;
    },
    SetLen: function (src, len) {
        if ($String.ActualLength(src) < len) {
            return src;
        } else {
            return src.substr(0, i) + "...";
        }
        return $.trim(src).replace(/[\u4e00-\u9fa5]/, "**").length;
    },
    MaxLengthKeyUp: function (C, A) {
        var B = $(C).val();
        if (B > A) {
            $(C).val(B.substring(0, A))
        }
    },
    TotalByteValidator: function (oElem) {
        var iMaxLen = oElem.attr("maxlength");
        Wojoz.Utils.String.fCkByte(oElem, iMaxLen);
    },
    MaxLengthKeyDown: function (B, A) {
        if ($(B).val().length > A) {
            return false
        }
        return true
    },
    /**
    * @author Royal
    * @输入时检查元素长度,并自动截取到默认长度
    **/
    fCkByte: function (oElem, iMaxLen) {
        if (!$String.IsNullOrEmpty(oElem)) {
            var sValue = oElem.val() + '';
            var count = 0;
            for (var i = 0; i < sValue.length; i++) {
                if (count > iMaxLen) temp = i;
                if (sValue.charCodeAt(i) > 127 || sValue.charCodeAt(i) < 0) count += 2;
                else count++;
                if (count > iMaxLen) {
                    oElem[0].value = oElem[0].value.substr(0, i);
                    alert('输入超出限制,最多可以输入' + iMaxLen + '个字符或者' + (iMaxLen / 2) + '个汉字！');
                    return;
                }
            }
        }
    },
    /**  
    * 格式化数字显示方式   
    * 用法  
    * formatNumber(12345.999,'#,##0.00');  
    * formatNumber(12345.999,'#,##0.##');  
    * formatNumber(123,'000000');  
    * @param num  
    * @param pattern  
    */
    FormatNumber: function (num, pattern) {
        var strarr = num ? num.toString().split('.') : ['0'];
        var fmtarr = pattern ? pattern.split('.') : [''];
        var retstr = '';

        // 整数部分   
        var str = strarr[0];
        var fmt = fmtarr[0];
        var i = str.length - 1;
        var comma = false;
        for (var f = fmt.length - 1; f >= 0; f--) {
            switch (fmt.substr(f, 1)) {
                case '#':
                    if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                    break;
                case '0':
                    if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                    else retstr = '0' + retstr;
                    break;
                case ',':
                    comma = true;
                    retstr = ',' + retstr;
                    break;
            }
        }
        if (i >= 0) {
            if (comma) {
                var l = str.length;
                for (; i >= 0; i--) {
                    retstr = str.substr(i, 1) + retstr;
                    if (i > 0 && ((l - i) % 3) == 0) retstr = ',' + retstr;
                }
            }
            else retstr = str.substr(0, i + 1) + retstr;
        }

        retstr = retstr + '.';
        // 处理小数部分   
        str = strarr.length > 1 ? strarr[1] : '';
        fmt = fmtarr.length > 1 ? fmtarr[1] : '';
        i = 0;
        for (var f = 0; f < fmt.length; f++) {
            switch (fmt.substr(f, 1)) {
                case '#':
                    if (i < str.length) retstr += str.substr(i++, 1);
                    break;
                case '0':
                    if (i < str.length) retstr += str.substr(i++, 1);
                    else retstr += '0';
                    break;
            }
        }
        return retstr.replace(/^,+/, '').replace(/\.$/, '');
    },
    Base64Encode: function (str) {
        var out, i, len;
        var c1, c2, c3;

        len = str.length;
        i = 0;
        out = "";
        while (i < len) {
            c1 = str.charCodeAt(i++) & 0xff;
            if (i == len) {
                out += base64EncodeChars.charAt(c1 >> 2);
                out += base64EncodeChars.charAt((c1 & 0x3) << 4);
                out += "==";
                break;
            }
            c2 = str.charCodeAt(i++);
            if (i == len) {
                out += base64EncodeChars.charAt(c1 >> 2);
                out += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
                out += base64EncodeChars.charAt((c2 & 0xF) << 2);
                out += "=";
                break;
            }
            c3 = str.charCodeAt(i++);
            out += base64EncodeChars.charAt(c1 >> 2);
            out += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
            out += base64EncodeChars.charAt(((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6));
            out += base64EncodeChars.charAt(c3 & 0x3F);
        }
        return out;
    },
    Base64Decode: function (str) {
        var c1, c2, c3, c4;
        var i, len, out;

        len = str.length;
        i = 0;
        out = "";
        while (i < len) {
            /* c1 */
            do {
                c1 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
            } while (i < len && c1 == -1);
            if (c1 == -1)
                break;

            /* c2 */
            do {
                c2 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
            } while (i < len && c2 == -1);
            if (c2 == -1)
                break;

            out += String.fromCharCode((c1 << 2) | ((c2 & 0x30) >> 4));

            /* c3 */
            do {
                c3 = str.charCodeAt(i++) & 0xff;
                if (c3 == 61)
                    return out;
                c3 = base64DecodeChars[c3];
            } while (i < len && c3 == -1);
            if (c3 == -1)
                break;

            out += String.fromCharCode(((c2 & 0XF) << 4) | ((c3 & 0x3C) >> 2));

            /* c4 */
            do {
                c4 = str.charCodeAt(i++) & 0xff;
                if (c4 == 61)
                    return out;
                c4 = base64DecodeChars[c4];
            } while (i < len && c4 == -1);
            if (c4 == -1)
                break;
            out += String.fromCharCode(((c3 & 0x03) << 6) | c4);
        }
        return out;
    },
    Utf16to8: function (str) {
        var out, i, len, c;

        out = "";
        len = str.length;
        for (i = 0; i < len; i++) {
            c = str.charCodeAt(i);
            if ((c >= 0x0001) && (c <= 0x007F)) {
                out += str.charAt(i);
            } else if (c > 0x07FF) {
                out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));
                out += String.fromCharCode(0x80 | ((c >> 6) & 0x3F));
                out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
            } else {
                out += String.fromCharCode(0xC0 | ((c >> 6) & 0x1F));
                out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
            }
        }
        return out;
    },
    Utf8to16: function (str) {
        var out, i, len, c;
        var char2, char3;

        out = "";
        len = str.length;
        i = 0;
        while (i < len) {
            c = str.charCodeAt(i++);
            switch (c >> 4) {
                case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7:
                    // 0xxxxxxx
                    out += str.charAt(i - 1);
                    break;
                case 12: case 13:
                    // 110x xxxx   10xx xxxx
                    char2 = str.charCodeAt(i++);
                    out += String.fromCharCode(((c & 0x1F) << 6) | (char2 & 0x3F));
                    break;
                case 14:
                    // 1110 xxxx  10xx xxxx  10xx xxxx
                    char2 = str.charCodeAt(i++);
                    char3 = str.charCodeAt(i++);
                    out += String.fromCharCode(((c & 0x0F) << 12) | ((char2 & 0x3F) << 6) | ((char3 & 0x3F) << 0));
                    break;
            }
        }
        return out;
    }
};

///	<summary>
/// $Converter = Wojoz.Utils.Converter
///	</summary>
usingNamespace("Wojoz.Utils")["Converter"] = {
    ToInt: function (value, defValue) {
        var defValue = defValue || 0;
        var value = $.trim(value);
        if (!value || (value.length == 0)) {
            return defValue;
        }
        if (isNaN(parseInt(value))) {
            return defValue;
        }
        return parseInt(value)
    },
    ToFloat: function (value, defValue) {
        var defValue = defValue || 0;
        var value = $.trim(value);
        if (!value || (value.length == 0)) {
            return defValue;
        }
        if (isNaN(parseFloat(value))) {
            return defValue;
        }
        return parseFloat(value)
    }
};

///	<summary>
/// $Json = Wojoz.Utils.Json
///	</summary>
usingNamespace("Wojoz.Utils")["Json"] = {
    ToJson: function (oJson) {
        try {
            return JSON.stringify(oJson)
        } catch (e) { }
        return false
    },
    FromJson: function (sJson) {
        try {
            return JSON.parse(sJson)
        } catch (e) {
            return false
        }
    },
    FormatDate: function (val) {
        if (val == null || val == "") {
            return "";
        }
        var timemap = val.replace("\/Date(", "").replace(")\/", "");
        var dateN = new Date(parseInt(timemap));
        var y; var M; var d;
        var h; var m; var s;
        y = dateN.getFullYear();
        if (dateN.getMonth() < 9) {
            M = "0" + (dateN.getMonth() + 1);
        }
        else {
            M = dateN.getMonth() + 1;
        }
        if (dateN.getDate() < 10) {
            d = "0" + dateN.getDate();
        }
        else {
            d = dateN.getDate();
        }
        if (dateN.getHours() < 10) {
            h = "0" + dateN.getHours();
        }
        else {
            h = dateN.getHours();
        }
        if (dateN.getMinutes() < 10) {
            m = "0" + dateN.getMinutes();
        }
        else {
            m = dateN.getMinutes();
        }
        if (dateN.getSeconds() < 10) {
            s = "0" + dateN.getSeconds();
        }
        else {
            s = dateN.getSeconds();
        }
        return y + "-" + M + "-" + d + " " + h + ":" + m + ":" + s;
    }
};

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

var $Converter = Wojoz.Utils.Converter;
var $String = Wojoz.Utils.String;
var $Json = Wojoz.Utils.Json;
var $Conf = Wojoz.Conf;
var $CookieConfig = Wojoz.Config.CookieConfig;
var $HttpUtility = Wojoz.HttpUtility;

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
                    domain = $Conf.Domain;
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
                    sDomain = $Conf.Domain;
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
        return $Conf.ProductUrlRewriteName + "-c" + NodeId + "-" + ProdId + ".htm";
    },
    BuildImageUrl: function (imgName) {
        sResult = $Conf.UrlPathMappings.WWWSite;
        return sResult + $String.TriWojozt(sPagePath, "/")
    },
    ///来源:
    //var myURL = parseURL('http://abc.com:8080/dir/index.html?id=255&m=hello#top');  
    //myURL.file;     // = 'index.html'  
    //myURL.hash;     // = 'top'  
    //myURL.host;     // = 'abc.com'  
    //myURL.query;    // = '?id=255&m=hello'  
    //myURL.params;   // = Object = { id: 255, m: hello }  
    //myURL.path;     // = '/dir/index.html'  
    //myURL.segments; // = Array = ['dir', 'index.html']  
    //myURL.port;     // = '8080'  
    //myURL.protocol; // = 'http'  
    //myURL.source;   // = 'http://abc.com:8080/dir/index.html?id=255&m=hello#top' 
    ParseUrl: function (url) {
        var a = document.createElement('a');
        a.href = url;
        return {
            source: url,
            protocol: a.protocol.replace(':', ''),
            host: a.hostname,
            port: a.port,
            query: a.search,
            params: (function () {
                var ret = {},
                seg = a.search.replace(/^\?/, '').split('&'),
                len = seg.length, i = 0, s;
                for (; i < len; i++) {
                    if (!seg[i]) { continue; }
                    s = seg[i].split('=');
                    ret[s[0]] = s[1];
                }
                return ret;
            })(),
            file: (a.pathname.match(/\/([^\/?#]+)$/i) || [, ''])[1],
            hash: a.hash.replace('#', ''),
            path: a.pathname.replace(/^([^\/])/, '/$1'),
            relative: (a.href.match(/tps?:\/\/[^\/]+(.+)/) || [, ''])[1],
            segments: a.pathname.replace(/^\//, '').split('/')
        };
    }
};
var $Url = Wojoz.Url;

///	<summary>
/// $Resource = Wojoz.Resource
///	</summary>
usingNamespace("Wojoz")["Resource"] = {
    BuildImage: function (sImageName) {
        return Environment.ResourceUrl + sImageName;
    },
    BuildProductImage: function (imgSize, imgName) {
        return $Conf.ProductImageUrl + imgSize + "/" + imgName;
    },
    BuildContent: function (name) {
        return eval("Wojoz.ResourceConfig.StringResourceConfig." + name)
    },
    BuildContent: function () {
        var args = arguments;
        var result = eval("Wojoz.ResourceConfig.StringResourceConfig." + arguments[0]);
        for (var i = 1; i < args.length; i++) {
            var re = new RegExp("\\{" + (i - 1) + "\\}", "gm");
            result = result.replace(re, arguments[i])
        }

        return result
    },
    DefaultImage: {

        size50: $Conf.ProductImageUrl + "50.jpg",
        size80: $Conf.ProductImageUrl + "80.jpg",
        size130: $Conf.ProductImageUrl + "130.jpg",
        size160: $Conf.ProductImageUrl + "160.jpg",
        size200: $Conf.ProductImageUrl + "200.jpg",
        size280: $Conf.ProductImageUrl + "280.jpg"
    }
};
var $Resource = Wojoz.Resource;

///	<summary>
/// $QueryString = Wojoz.QueryString
///	</summary>
usingNamespace("Wojoz")["QueryString"] = {
    Get: function (sName) {
        sName = sName.toLowerCase();
        var oParam = Wojoz.QueryString.Parse();
        var sValue = oParam[sName];
        return (sValue != null) ? sValue : ""
    },
    Set: function (sName, sValue) {
        sName = sName.toLowerCase();
        var oParam = Wojoz.QueryString.Parse();
        oParam[sName] = $HttpUtility.UrlEncode(sValue);
        return Wojoz.QueryString.ToString(oParam)
    },
    Parse: function (o) {
        var oParam = {};
        if (o == null) {
            o = location.search.substring(1, location.search.length)
        }
        if (o.length == 0) {
            return oParam
        }
        o = o.replace(/\+/g, " ");
        var aParams = o.split("&");
        for (var i = 0; i < aParams.length; i++) {
            var aParam = aParams[i].split("=");
            var sName = aParam[0].toLowerCase();
            var sValue = (aParam.length == 2) ? aParam[1] : sName;
            oParam[sName] = sValue
        }
        return oParam
    },
    ToString: function (oParam) {
        if (oParam == null) {
            oParam = Wojoz.QueryString.Parse()
        }
        var sParams = "";
        for (var p in oParam) {
            if (sParams == "") {
                sParams = "?"
            }
            sParams = sParams + p + "=" + oParam[p] + "&"
        }
        sParams = sParams.substring(0, sParams.length - 1);
        return sParams
    }
};
var $QueryString = Wojoz.QueryString;

///	<summary>
/// $Form = Wojoz.Form
///	</summary> 
usingNamespace("Wojoz")["Form"] = {
    reset: function (sFormID) {
        var oFrom = $("#" + sFormID);
        oFrom.reset()
    },
    submit: function (sFormID) {
        var oFrom = $("#" + sFormID);
        oFrom.submit()
    }
};
var $Form = Wojoz.Form;

///	<summary>
/// MessageType[Info-0,Warning-1,Error-2]
///	</summary> 
var MessageType = {
    Info: "0",
    Warning: "1",
    Error: "2"
};

///	<summary>
/// $Loading = Wojoz.Ajax.Loading
///	</summary>
usingNamespace("Wojoz.Ajax")["Loading"] = {
    Begin: function (loader, container, message) {
        loader = !$String.IsNullOrEmpty(loader) ? loader : "#globalLoader";
        var $loader = $("" + loader + "");
        var $container = $("" + container + "");
        $loader.html(message);
        var cOffset = $container.offset();
        var cWidth = $container.width();
        var cHeight = $container.height();
        var lWidth = $loader.width();
        var lHeight = $loader.height();
        var lLeft = cOffset.left + (cWidth - lWidth) / 2;
        var lTop = cOffset.top + (cHeight - lHeight) / 2; ;
        $loader.css("left", lLeft);
        $loader.css("top", lTop);
        $loader.show();
    },
    End: function (loader) {
        loader = !$String.IsNullOrEmpty(loader) ? loader : "#globalLoader";
        setTimeout("$('" + loader + "').hide()", 500);
    }
};
var $Loading = Wojoz.Ajax.Loading;

///	<summary>
/// $Dom = Wojoz.Dom
///	</summary>
usingNamespace("Wojoz")["Dom"] = {
    // 获取宽度
    AvailWidth: function () {
        var strWidth, clientWidth, bodyWidth;
        clientWidth = document.documentElement.clientWidth;
        bodyWidth = document.body.clientWidth;
        if (bodyWidth > clientWidth) {
            strWidth = bodyWidth + 20;
        } else {
            strWidth = clientWidth;
        }
        return strWidth;
    },
    //获取高度
    AvailHeight: function () {
        var strHeight, clientHeight, bodyHeight;
        clientHeight = document.documentElement.clientHeight;
        bodyHeight = document.body.clientHeight;
        if (bodyHeight > clientHeight) {
            strHeight = bodyHeight + 30;
        } else {
            strHeight = clientHeight;
        }
        return strHeight + 1000;
    },
    ScrollLeft: function () {
        return Math.max(document.documentElement.scrollLeft, document.body.scrollLeft);
    },
    ScrollTop: function () {
        return Math.max(document.documentElement.scrollTop, document.body.scrollTop);
    }
}
var $Dom = Wojoz.Dom;

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
    },
    PagerNavBar: function (result, PrevCount, NextCount, Container, PageIndex, LoadEventHandler, ImgPrev, ImgNext) {
        var TotalCount = result.TotalCount;
        var PageCount = _PageCount = result.PageCount;
        var PagerRender = new StringBuilder();
        if (PageIndex < 1) PageIndex = 1;
        if (PageIndex > PageCount) PageIndex = PageCount;
        var BeginNO = (PageIndex - 1) * PageSize + 1; //开始编号
        var EndNO = PageIndex * PageSize; //结束编号
        if (EndNO > TotalCount) EndNO = TotalCount;
        if (EndNO == 0) BeginNO = 0;
        if (!(PageIndex <= PageCount)) PageIndex = PageCount;
        //PagerRender.Append("页次:" + PageIndex + "/" + PageCount + ",共" + TotalCount + "条记录,显示: " + BeginNO + "-" + EndNO + "条</div>");

        //分页开始
        var btnFirst = "<img src=\"" + $Resource.BuildImage(ImgPrev) + "\" alt=\"首页\" ";
        var btnLast = "<img src=\"" + $Resource.BuildImage(ImgNext) + "\" alt=\"尾页\" ";
        if (PageIndex > 1) {
            PagerRender.Append(btnFirst + " onclick=\"javascript:" + LoadEventHandler + ".Load(1)\" />");
        }
        else {
            PagerRender.Append(btnFirst + " />");
        }

        var PagePrev = PrevCount - (PageIndex - 1);
        var PageNext = NextCount - (PageCount - PageIndex);
        if (PagePrev > 0 && PageNext < 0) NextCount += PagePrev; //前少后多，前剩余空位给后
        if (PageNext > 0 && PagePrev < 0) PrevCount += PageNext; //后少前多，后剩余空位给前
        var PagePrevBegin = PageIndex - PrevCount;
        if (PagePrevBegin < 1) PagePrevBegin = 1;
        var PagePrevEnd = PageIndex + NextCount;
        if (PagePrevEnd > PageCount) PagePrevEnd = PageCount;

        for (var i = PagePrevBegin; i < PageIndex; i++) {
            PagerRender.Append(" <a href=\"javascript:;\" onclick=\"javascript:" + LoadEventHandler + ".Load(" + i + ")\">" + i + "</a>");
        }
        PagerRender.Append(" <a href=\"javascript:;\" onclick=\"javascript:" + LoadEventHandler + ".Load(" + i + ")\"><b>" + PageIndex + "</b></a>");
        for (var i = PageIndex + 1; i <= PagePrevEnd; i++) {
            PagerRender.Append(" <a href=\"javascript:;\" onclick=\"javascript:" + LoadEventHandler + ".Load(" + i + ")\">" + i + "</a>");
        }
        if (PageIndex != PageCount) {
            PagerRender.Append(btnLast + " onclick=\"javascript:" + LoadEventHandler + ".Load(" + PageCount + ")\" />");
        } else {
            PagerRender.Append(btnLast + " />");
        }
        PagerRender.Append(" 共计<b>" + PageCount + "</b>页");
        $(Container).html("");
        $(Container).html(PagerRender.ToString());
    },
    AjaxAlert: function (container, message) {
        $("#globalLoader").html(message);
        $(container).block({
            message: $("#globalLoader"),
            timeout: 1000
        });
    },
    View3DFlash: function (src, title) {
        var flashRender = new StringBuilder();
        flashRender.Append("<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0' width='640' height='480'>");
        flashRender.Append("<param name='movie' value='" + src + "' />");
        flashRender.Append("<param name='quality' value='high' />");
        flashRender.Append("<embed src='" + src + "' quality='high' pluginspage='http://www.macromedia.com/go/getflashplayer' type='application/x-shockwave-flash' width='640' height='480'></embed>");
        flashRender.Append("</object>");
        $("#flashContainer").html(flashRender.ToString());
        $("#viewHeader em").html(title);
        var swidth = $Dom.AvailWidth();
        var sHeight = $Dom.AvailHeight();
        $('#screenWrap').css({ width: swidth, height: sHeight }).show();
        loginwidth = $('#flashViewer').width();
        loginheight = $('#flashViewer').height();

        loginleft = ($(window).width() - loginwidth) / 2;
        logintop = document.documentElement.scrollTop + ($(window).height() - loginheight) / 2;
        $('#flashViewer').css({ top: logintop, left: loginleft }).fadeIn(500);
        window.onscroll = function () {
            var scrollleft = ($(window).width() - loginwidth) / 2;
            var scrolltop = document.documentElement.scrollTop + ($(window).height() - loginheight) / 2;
            $('#flashViewer').css({ top: scrolltop, left: scrollleft });
        }
        window.onresize = function () {
            var left = ($(window).width() - loginwidth) / 2;
            var top = document.documentElement.scrollTop + ($(window).height() - loginheight) / 2;
            $('#flashViewer').css({ top: top, left: left });
        }
    },
    CloseViewer: function () {
        $('#flashViewer').fadeOut(500);
        $('#screenWrap').fadeOut(500);

    },
    DirtyWordsFilter: function (value) {
        // 根据文本域的id获取文本域对象内容
        var CopyValue = value;
        var DirtyWordsList = Wojoz.FilterConfig.DirtyWordsList.split(Wojoz.FilterConfig.Separator);
        var i = 0, iLen = DirtyWordsList.length;
        for (; i < iLen; i++) {
            CopyValue = $UIHelper.FilterOneWord(CopyValue, DirtyWordsList[i]); //过滤单个词语并返回过滤后的内容
        }
        return CopyValue;
    },
    FilterOneWord: function (value, oneDirtyWord) {
        // 得到value所包含的oneDirtyWord的位置, 如果不包含则返回 - 1
        var wordIndex = value.lastIndexOf(oneDirtyWord);
        var CopyValue = value;
        //循环判断并替换所有的oneDirtyWord
        while (wordIndex != -1) {
            CopyValue = CopyValue.replace(oneDirtyWord, Wojoz.FilterConfig.SubstituteSymbol);
            wordIndex = CopyValue.lastIndexOf(oneDirtyWord);
        }
        return CopyValue;
    },
    LenLimit: function (value) {
        var reg = /^[A-Za-z0-9]*$/;
        var $o = $(value);
        if ($o.val() != $Resource.BuildContent("ShoppingCart_Letter_PromptText")) {
            if ($o.val() != "") {
                if (!reg.test($o.val()) && $o.val().length > 4) {
                    alert("中文4个字以内！");
                    $o.focus();
                    return false;
                } else {
                    return true;
                }
                if ($o.val().length > 9) {
                    alert("英文8个字母以内！");
                    $o.focus();
                    return false;
                } else {
                    return true;
                }
            } else {
                return true;
            }
        } else {
            return true;
        }
    },
    BuildPopWindow: function (title, content) {
        var PopWindowRender = new StringBuilder();
        PopWindowRender.Append("<div id=\"Popup\" class=\"Popup shadow-one\" style=\"width: 260px;\">");
        PopWindowRender.Append("    <div class=\"corner-a\">");
        PopWindowRender.Append("    </div>");
        PopWindowRender.Append("    <div class=\"corner-b\">");
        PopWindowRender.Append("    </div>");
        PopWindowRender.Append("    <div class=\"shadow-two\">");
        PopWindowRender.Append("        <div class=\"shadow-three\">");
        PopWindowRender.Append("            <div class=\"shadow-four\">");
        PopWindowRender.Append("                <div class=\"title\">" + title + "</div>");
        PopWindowRender.Append("                <div class=\"content\">" + content + "</div>");
        PopWindowRender.Append("                <b class=\"arrow-tip\"></b>");
        PopWindowRender.Append("            </div>");
        PopWindowRender.Append("        </div>");
        PopWindowRender.Append("    </div>");
        PopWindowRender.Append("</div>");
        return PopWindowRender.ToString();
    }
};
var $UIHelper = Wojoz.UIHelper; 