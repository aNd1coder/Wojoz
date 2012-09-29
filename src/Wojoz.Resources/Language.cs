//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.4952
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wojoz.Resources {
    
    
    public class Language {
        
        private static ILanguage _Language;
        
        /// <summary>
        ///  Call GetLanguages() to retrieve a list of possible languages that can be used to set this property.
        ///  The default value is the default language.
        /// </summary>
        public static ILanguage Current {
            get {
                if ((_Language == null)) {
                    System.Collections.Generic.List<ILanguage> list = Language.GetLanguages();
                    for (int i = 0; (i < list.Count); i = (i + 1)) {
                        if (list[i].IsDefault) {
                            _Language = list[i];
                            return _Language;
                        }
                    }
                }
                return _Language;
            }
            set {
                _Language = value;
            }
        }
        
        /// <summary>
        ///  Gets a list of available languages defined in this assembly.
        /// </summary>
        public static System.Collections.Generic.List<ILanguage> GetLanguages() {
            System.Collections.Generic.List<ILanguage> items = new System.Collections.Generic.List<ILanguage>();
            System.Type[] exportedTypes = System.Reflection.Assembly.GetExecutingAssembly().GetExportedTypes();
            for (int i = 0; (i < exportedTypes.Length); i = (i + 1)) {
                if (exportedTypes[i].IsClass) {
                    if ((exportedTypes[i].GetInterface("ILanguage") != null)) {
                        try {
                            object obj = System.Activator.CreateInstance(exportedTypes[i]);
                            ILanguage interfaceReference = ((ILanguage)(obj));
                            if ((interfaceReference != null)) {
                                items.Add(interfaceReference);
                            }
                        }
                        catch (System.Exception ) {
                        }
                    }
                }
            }
            return items;
        }
    }
}