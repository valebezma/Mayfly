﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mayfly.Mathematics.Resources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Statistics {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Statistics() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mayfly.Mathematics.Resources.Statistics", typeof(Statistics).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Null hypothesis is not rejected so
        ///selected samples aren&apos;t differ.
        /// </summary>
        internal static string AnovaNegative {
            get {
                return ResourceManager.GetString("AnovaNegative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Null hypothesis is rejected so
        ///selected samples are differ.
        /// </summary>
        internal static string AnovaPositive {
            get {
                return ResourceManager.GetString("AnovaPositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Null hypothesis is that samples have no internal differences. Alternative hypothesis is that {0} depends on {1}..
        /// </summary>
        internal static string AnovaTestDescription {
            get {
                return ResourceManager.GetString("AnovaTestDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Homoscedasticity statistic ({0}) equals {1:N5} (P = {2:N5}),
        ///so samples are heteroscedastic.
        /// </summary>
        internal static string HomoscedasticityNegative {
            get {
                return ResourceManager.GetString("HomoscedasticityNegative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Homogeneity statistic ({0}) equals {1:N5} (P = {2:N5}),
        ///so samples are homoscedastic with {3:P1} confidence.
        /// </summary>
        internal static string HomoscedasticityPositive {
            get {
                return ResourceManager.GetString("HomoscedasticityPositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на {0} tests samples homoscedasticity. Null hypothesis is that samples are homoscedastic..
        /// </summary>
        internal static string HomoscedasticityTestDescription {
            get {
                return ResourceManager.GetString("HomoscedasticityTestDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на {0} equals {1:G5} (P = {2:G3}).
        ///So sample hasn&apos;t normal distribution.
        /// </summary>
        internal static string NormalityNegative {
            get {
                return ResourceManager.GetString("NormalityNegative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на {0} equals {1:G5} (P = {2:G3}).
        ///So sample has normal distribution.
        /// </summary>
        internal static string NormalityPositive {
            get {
                return ResourceManager.GetString("NormalityPositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на {0} checks normality of data. Null hypothesis tells that sample is not normally distributed. Alternative is that sample distribution don&apos;t differ from normal..
        /// </summary>
        internal static string NormalityTestDescription {
            get {
                return ResourceManager.GetString("NormalityTestDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Null hypothesis is not rejected so
        ///determination coefficient is not statistically confident.
        /// </summary>
        internal static string RegressionNegative {
            get {
                return ResourceManager.GetString("RegressionNegative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Null hypothesis is rejected so
        ///determination coefficient is statistically confident
        ///(regression equation assesment is confident).
        /// </summary>
        internal static string RegressionPositive {
            get {
                return ResourceManager.GetString("RegressionPositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Alternative hypothesis is that given regression value is a real feature of {0} to {1} connection..
        /// </summary>
        internal static string RegressionTestDescription {
            get {
                return ResourceManager.GetString("RegressionTestDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Unable to process test.
        /// </summary>
        internal static string UnableToCalculate {
            get {
                return ResourceManager.GetString("UnableToCalculate", resourceCulture);
            }
        }
    }
}