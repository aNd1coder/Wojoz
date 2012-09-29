使用easyui 做后台管理界面，在Tab中的链接点击后添加一个新TAB的解决方法 
给链接或按钮  添加 onclick="self.parent.addTab('百度','http://www.baidu.com','icon-add')"
如：
<a href="javascript:void(0)" title="google" onclick="self.parent.addTab('百度','http://www.baidu.com','icon-add')">打开新TAB</a>
这样点击链接后会增加一个新的TAB

