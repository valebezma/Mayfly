//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mayfly.Wild.Resources.Reports {
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
    public class Card {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Card() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mayfly.Wild.Resources.Reports.Card", typeof(Card).Assembly);
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
        ///   Looks up a localized string similar to Comments.
        /// </summary>
        public static string Comments {
            get {
                return ResourceManager.GetString("Comments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Investigator.
        /// </summary>
        public static string Investigator {
            get {
                return ResourceManager.GetString("Investigator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sampling date and time.
        /// </summary>
        public static string When {
            get {
                return ResourceManager.GetString("When", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Coordinates.
        /// </summary>
        public static string Where {
            get {
                return ResourceManager.GetString("Where", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Spot description (if coordinates are unknown).
        /// </summary>
        public static string Where_1 {
            get {
                return ResourceManager.GetString("Where_1", resourceCulture);
            }
        }
    }
}
