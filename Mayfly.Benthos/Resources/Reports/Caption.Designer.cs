﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mayfly.Benthos.Resources.Reports {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Caption {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Caption() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mayfly.Benthos.Resources.Reports.Caption", typeof(Caption).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Abundance.
        /// </summary>
        public static string Abundance {
            get {
                return ResourceManager.GetString("Abundance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ind./m².
        /// </summary>
        public static string AbundanceUnits {
            get {
                return ResourceManager.GetString("AbundanceUnits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Biomass.
        /// </summary>
        public static string Biomass {
            get {
                return ResourceManager.GetString("Biomass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to g/m².
        /// </summary>
        public static string BiomassUnits {
            get {
                return ResourceManager.GetString("BiomassUnits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mass, mg.
        /// </summary>
        public static string Mass {
            get {
                return ResourceManager.GetString("Mass", resourceCulture);
            }
        }
    }
}
