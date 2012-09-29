if (!window["Wojoz"]) { Wojoz = {}; };

Wojoz["Conf"] = {
    Domain: "pt9999.com",

    langList: [{ name: 'en', charset: 'UTF-8' }, { name: 'zh-cn', charset: 'UTF-8'}],
    skinList: [{ name: 'default', charset: 'UTF-8'}],

    ProductImageUrl: "/UploadFiles/Product/",
    ProductUrlRewriteName: "/Product/Patti-Jewelry",
    Resources: {
        Root: "/WojozResources/Defualt/",
        Images: "/WojozResources/Defualt/images/",
        Scripts: "/WojozResources/Defualt/Scripts/",
        ScriptPlugins: "/WojozResources/Defualt/Scripts/Plugins/",
        Styles: "/WojozResources/Defualt/Styles/"
    },
    WordFilter: {
        DirtyWordsList: "他妈的|你妈|你奶奶|草|日|法论功",
        Separator: "|",
        SubstituteSymbol: "*"
    }
};
var $Conf = Wojoz.Conf;