var Wjz = Wjz || {};

Wjz.use = function () {

}

Wjz.dom = function () {

}

//http://www.cnblogs.com/enix/archive/2012/03/16/2400100.html
Wjz.dom.ready = function (fn) {
    var delay;
    /complete/.test(document.readyState) ? delay = setTimeout(function () {
        arguments.callee.apply(null, arguments)
    }, 9) : delay && clearTimeout(delay), fn();
}


//http://www.cnblogs.com/rubylouvre/archive/2010/03/26/1696600.html
Wjz.extend = function (result, source) {
    for (var key in source)
        result[key] = source[key];
    return result;
}

Wjz.dom.is = function (obj, type) {
    var toString = Object.prototype.toString, undefined;
    return (type === "Null" && obj === null) ||
    (type === "Undefined" && obj === undefined) ||
    toString.call(obj).slice(8, -1) === type;
};

Wjz.dom.deepCopy = function (result, source) {
    for (var key in source) {
        var copy = source[key];
        if (source === copy) continue; //如window.window === window，会陷入死循环，需要处理一下
        if (dom.is(copy, "Object")) {
            result[key] = arguments.callee(result[key] || {}, copy);
        } else if (dom.is(copy, "Array")) {
            result[key] = arguments.callee(result[key] || [], copy);
        } else {
            result[key] = copy;
        }
    }
    return result;
};

Array.prototype.Unique = function () {
    var newArray = new Array();
    var len = this.length;
    for (var i = 0; i < len; i++) {
        for (var j = i + 1; j < len; j++) {
            if (this[i] === this[j]) {
                j = ++i;
            }
        }
        newArray.push(this[i]);
    }
    return newArray;
}

Array.prototype.Unique = function () {
    var newArray = [];
    var provisionalTable = {};
    for (var i = 0, item; (item = this[i]) != null; i++) {
        if (!provisionalTable[item]) {
            newArray.push(item);
            provisionalTable[item] = true;
        }
    }
    return newArray;
}


//http://www.cnblogs.com/enix/archive/2011/12/15/2289088.html
var sortList = function (window, doc, undefined) {
    var lis = null, replace = null, origin = [0, 0], parent = null, tag = null, tips = null, listWidth = 0,
    toolkit = {
        getEvent: function (b) { return b || window.event },
        getTarget: function (b) { return b.srcElement || b.target },
        stopEvent: function (b) {
            b = toolkit.getEvent(b);
            (b.returnValue || b.preventDefault) && (b.returnValue = false || b.preventDefault()); (b.cancelBubble || b.stopPropagation) && (b.cancelBubble = false || b.stopPropagation())
        },
        getClinetRect: function (b) {
            var g = b.getBoundingClientRect(), c = (c = { left: g.left, right: g.right, top: g.top, bottom: g.bottom, height: (g.height ? g.height : (g.bottom - g.top)), width: (g.width ? g.width : (g.right - g.left)) }); return c
        },
        getScrollPosition: function () { var b = [0, 0]; window.pageYOffset ? (b = [window.pageXOffset, window.pageYOffset]) : (b = [document.documentElement.scrollLeft, document.documentElement.scrollTop]); return b },
        addEvent: function (f, e, d, c) { var b = arguments.callee; f.attachEvent && (b = function (i, h, g) { i.attachEvent("on" + h, g) }).apply(this, arguments); f.addEventListener && (b = function (i, h, g) { i.addEventListener(h, g, c || false) }).apply(this, arguments); f["on" + e] && (b = function (i, h, g) { i["on" + h] = function () { g() } }).apply(this, arguments) },
        removeEvent: function (f, e, d, c) { var b = arguments.callee; f.detachEvent && (b = function (i, h, g) { i.detachEvent("on" + h, g) }).apply(this, arguments); f.removeEventListener && (b = function (i, h, g) { i.removeEventListener(h, g, c || false) }).apply(this, arguments); f["on" + e] && (b = function (i, h, g) { i["on" + h] = null }).apply(this, arguments) }
    };


    function mousedownSortableList(e) {
        e = toolkit.getEvent(e);
        var target = toolkit.getTarget(e), pos = null;

        while (target.nodeName.toLowerCase() !== tag) {
            target = target.parentNode;
        };
        doc.currentTarget = target, pos = toolkit.getClinetRect(target), origin = [e.clientX - pos.left, e.clientY - pos.top],
                listWidth = target.offsetWidth;
        toolkit.addEvent(doc, 'mousemove', mousemoveCheckThreshold, false);
        toolkit.addEvent(doc, 'mouseup', mouseupCancelThreshold, false);
        toolkit.stopEvent(e);
    };
    function creatReplaceElement(O, E, P) {
        replace || (
            replace = O.cloneNode(true),
            replace.style.cssText = 'height:' + (O.style.height - 4) + 'px;',
            replace.className = tips,
            replace.innerHTML = '放在这里？'
        );
        (P === -1) && E.parentNode.insertBefore(replace, E);
        (P === 1) && E.parentNode.insertBefore(replace, E.nextSibling);
    };
    function mousemoveCheckThreshold(e) {
        e = toolkit.getEvent(e);
        var target = doc.currentTarget, i = lis.length, pos = null, scroll = toolkit.getScrollPosition();

        try { target.style.cssText = 'top:' + (e.clientY - origin[1] + scroll[1]) + 'px;position:absolute;z-index:100;opacity:.9;filter:alpha(opacity="90");width:' + listWidth + 'px;overflow:hidden;'; } catch (e) { };
        for (; i > 0; ) {
            lis[--i] !== target && (
                pos = toolkit.getClinetRect(lis[i]),
                ((e.clientY >= pos.top) && (e.clientY < pos.bottom)) && (
                    creatReplaceElement(target, lis[i], e.clientY <= (pos.top + lis[i].offsetHeight / 2) ? -1 : 1)
                )
            )
        };

    };
    function mouseupCancelThreshold(e) {
        try { doc.currentTarget.style.cssText = ''; replace.parentNode.replaceChild(doc.currentTarget, replace) } catch (e) { };
        toolkit.removeEvent(doc, 'mousemove', mousemoveCheckThreshold, false);
        toolkit.removeEvent(doc, 'mouseup', mouseupCancelThreshold, false);
        doc.currentTarget = null, replace = null;
        toolkit.stopEvent(e);
    };
    return function (O) {
        parent = O.parent, tag = O.tag || 'li', tips = O.tips || 'sortListTips';
        if (!doc.getElementById(parent)) return false;
        lis = doc.getElementById(parent).getElementsByTagName(tag);
        var i = lis.length
        for (; i > 0; toolkit.addEvent(lis[--i], 'mousedown', mousedownSortableList, false), lis[i].style.cursor = 'move') { };
    };
} (window, document);

//sortList({parent:"list",tag:"li",tips:'tips'});
sortList({ parent: "prara", tag: "p" });
//拖拽排序列表单例 最好不要同时启用两个。
