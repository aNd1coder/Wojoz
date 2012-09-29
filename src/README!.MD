Wojoz说明文档
============

解决方案说明
----------
1.Wojoz.Document		存放项目相关文档
	- Readme!.txt			必读
2.Wojoz.Framework		基础框架
	- Wojoz.BLL				业务层
	- Wojoz.Cache			缓存
	- Wojoz.Config			配置
	- Wojoz.DALFactory		数据工厂
	- Wojoz.Data			基于企业库的数据辅助工具
	- Wojoz.Data.SqlServer	Sql Server 2005数据层
	- Wojoz.IDAL			数据接口层
	- Wojoz.Model			实体层
	- Wojoz.Services		服务层
	- Wojoz.Utilities		通用工具
3.Wojoz.Plugins			插件
	- Wojoz.Payment			支付
	- Wojoz.Shipping		物流
	- Wojoz.URLRewriter		URL重写
4.Wojoz.CodeBuilder		代码生成器
	- Wojoz.CodeBuilder		代码生成器(T4)
5.Wojoz.Presentation	用户界面展示相关
	- Wojoz.Resources		国际化资源
	- Wojoz.UI				用户接口相关逻辑
	- Wojoz.Web				基于WebForm站点
	- Wojoz.WebPortal		基于Mvc站点


数据库约定
---------
-为了方便重用,每个表必须包含以下字段:
-State:     状态
-IsDeleted: 是否删除(默认为0,4代表是),
-Sort	 : 排序字段(默认为0) 

开发原则
-------
安全性、可靠性、扩展性(主要是分布事务)、性能