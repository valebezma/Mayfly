﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mayfly.Fish.Legal.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NoteContent {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal NoteContent() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mayfly.Fish.Legal.Resources.NoteContent", typeof(NoteContent).Assembly);
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
        ///   Looks up a localized string similar to Вес (кг) или количество (шт.).
        /// </summary>
        internal static string CaptionMass {
            get {
                return ResourceManager.GetString("CaptionMass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to №.
        /// </summary>
        internal static string CaptionNo {
            get {
                return ResourceManager.GetString("CaptionNo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Виды водных биологических ресурсов.
        /// </summary>
        internal static string CaptionSpecies {
            get {
                return ResourceManager.GetString("CaptionSpecies", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ф.И.О..
        /// </summary>
        internal static string HintName {
            get {
                return ResourceManager.GetString("HintName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to подпись.
        /// </summary>
        internal static string HintSign {
            get {
                return ResourceManager.GetString("HintSign", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ИТОГО.
        /// </summary>
        internal static string Total {
            get {
                return ResourceManager.GetString("Total", resourceCulture);
            }
        }
    }
}