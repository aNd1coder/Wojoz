
/*
* 加载js/css/html内容或文件
* 首先将js文件入队，当队列不为空时，在document ready后再执行加载js操作，反之，则直接加载js
*/
if (window.NC) {
    NC.add('loader', function (S) {
        var doc = document;
        var queue = [], readyBound = false;
        var testNode = doc.createElement('script'), fn, node;
        fn = testNode.readyState ? function (node, callback) {
            node.onreadystatechange = function () {
                var rs = node.readyState;
                if (rs === 'loaded' || rs === 'complete') {
                    // handle memory leak in IE
                    node.onreadystatechange = null;
                    callback.call(this);
                }
            };
        } : function (node, callback) {
            node.onload = callback;
        };
        function dequeue(url, callback, charset) {
            if ((url == undefined) || (url && url.type)) {
                var currentScript = queue.shift();
                if (currentScript) {
                    function plugins() {
                        currentScript.callback();
                        dequeue();
                    }
                    dequeue(currentScript.url, plugins, currentScript.charset);

                }
            }
            else {
                var head = doc.getElementsByTagName('head')[0] || doc.documentElement, node = doc.createElement('script');
                node.src = url;
                if (charset)
                    node.charset = charset;
                //node.async = true;
                if (NC.isFunction(callback)) {
                    fn(node, callback);
                }
                head.appendChild(node);
            }
        }
        function enqueue(url, callback, charset) {
            queue.push({
                'url': url,
                'callback': callback,
                'charset': charset
            });
            domReady();
        }

        function domReady() {
            if (readyBound) {
                return;
            }

            readyBound = true;

            if (document.readyState === "complete") {
                return dequeue();
            }

            if (document.addEventListener) {
                document.addEventListener("DOMContentLoaded", dequeue, false);
                window.addEventListener("load", dequeue, false);
            }
            else
                if (document.attachEvent) {
                    document.attachEvent("onreadystatechange", dequeue);

                    window.attachEvent("onload", dequeue);
                    var toplevel = false;
                    try {
                        toplevel = window.frameElement == null;
                    }
                    catch (e) {
                    }

                    if (document.documentElement.doScroll && toplevel) {
                        try {
                            // If IE is used, use the trick by Diego Perini
                            // http://javascript.nwbox.com/IEContentLoaded/
                            document.documentElement.doScroll("left");
                        }
                        catch (error) {
                            setTimeout(arguments.callee, 10);
                            return;
                        }
                        dequeue();
                    }
                }
        }

        S.loader = {
            getScript: function (url, callback, queue, charset) {
                if (!queue) {
                    dequeue(url, callback, charset);
                }
                else {
                    enqueue(url, callback, charset)
                }
            },
            getCss: function (url, callback, charset) {
                var head = document.getElementsByTagName('head').item(0);
                var node = document.createElement('link');
                node.href = url;
                node.type = 'text/css';
                node.rel = "stylesheet";
                if (NC.isFunction(callback)) {
                    fn(node, callback);
                }
                head.appendChild(node);
            },
            _globalEval: function () {

            }
        };

    })
}

