﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mayfly.Fish.Explorer.Resources.Reports.Section {
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
    internal class Catches {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Catches() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mayfly.Fish.Explorer.Resources.Reports.Section.Catches", typeof(Catches).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Combined Catches Summary.
        /// </summary>
        internal static string Header {
            get {
                return ResourceManager.GetString("Header", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} Classes Catches Tables.
        /// </summary>
        internal static string HeaderSingleClass {
            get {
                return ResourceManager.GetString("HeaderSingleClass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are {0} species registered in catches. Most numerous sample is of {1} ({2} ind., which is {3:P1} of total sample). Species&apos; samples sizes and also biological estimates are given in table {4}..
        /// </summary>
        internal static string ParagraphDescription {
            get {
                return ResourceManager.GetString("ParagraphDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sample from {0} class is empty..
        /// </summary>
        internal static string ParagraphEmpty {
            get {
                return ResourceManager.GetString("ParagraphEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Description of species samples.
        /// </summary>
        internal static string Table {
            get {
                return ResourceManager.GetString("Table", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Description of species samples from {0}.
        /// </summary>
        internal static string TableSingleClass {
            get {
                return ResourceManager.GetString("TableSingleClass", resourceCulture);
            }
        }
    }
}
