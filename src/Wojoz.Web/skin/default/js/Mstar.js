/// <reference path="plugins/jquery-1.4.4.js" />


/*
shotcut - 替换空行正则:^[\t]*$\n
*/


/// <summary>Browser checks </summary>
var isFF = !!(1 * ({ toString: 0, valueOf: function (x) { return !!x; } }));
var isIE = !isFF && '\v' == 'v';
var isChrome = !isFF && !isIE && !!/\c[a]/('ca');
var isSafari = !isFF && !isIE && !isChrome && /a/.__proto__ == '//';
var isOpera = !isFF && !isIE && !isChrome && !isSafari && 'object' == (typeof /./);
var isWebKit = isChrome || isSafari;
var isOther = !isFF && !isIE && !isWebKit && !isOpera;


/// <summary>比较2个对象是否相等</summary> 
/// <param name="url" type="String">目标对象</param>
Object.prototype.equals = function (obj) {
    if (this == obj)
        return true;
    if (typeof (obj) == "undefined" || obj == null || typeof (obj) != "object")
        return false;
    var length = 0; var length1 = 0;
    for (var ele in this) {
        length++;
    }
    for (var ele in obj) {
        length1++;
    }
    if (length != length1)
        return false;
    if (obj.constructor == this.constructor) {
        for (var ele in this) {
            if (typeof (this[ele]) == "object") {
                if (!this[ele].equals(obj[ele]))
                    return false;
            }
            else if (typeof (this[ele]) == "function") {
                if (!this[ele].toString().equals(obj[ele].toString()))
                    return false;
            }
            else if (this[ele] != obj[ele])
                return false;
        }
        return true;
    }
    return false;
};


/// <summary>延迟加载js文件</summary> 
/// <param name="url" type="String">脚本路径</param>
/// <param name="callback" type="Function">回调函数</param>
/// loadScript("url1.js", loadScript("url2.js"));
var loadScript = function (url, callback) {
    var script = document.createElement("script");
    script.setAttribute("type", "text/javascript");

    if (script.readyState) {
        script.onReadyStateChange = function () {//在ie里检查脚本是否已经加载好
            if (script.readyState == "loaded" || script.readyState == "completed") {
                script.onReadyStateChange = null;
                if (callback) { callback(); }
            }
        }
    } else {
        window.onload = function () {//非ie
            if (callback) {
                callback();
            }
        }
    }

    script.setAttribute("src", url);
    document.getElementsByTagName("head")[0].appendChild(script); //追加到head
}

/// <summary>数据延迟加载</summary>
$(window).bind("scroll", function () {

})