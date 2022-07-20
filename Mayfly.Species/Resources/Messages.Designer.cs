﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mayfly.Species.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mayfly.Species.Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Default species key is not specified yet. Set the species key you want to use. You can change it in &apos;Settings&apos; later..
        /// </summary>
        public static string DefaultNotSet {
            get {
                return ResourceManager.GetString("DefaultNotSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Default species key not set.
        /// </summary>
        public static string DefaultNotSetTitle {
            get {
                return ResourceManager.GetString("DefaultNotSetTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you want to completely delete {0} record or just except it from {1}?.
        /// </summary>
        public static string DeleteSpeciesContent {
            get {
                return ResourceManager.GetString("DeleteSpeciesContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0 species found.
        /// </summary>
        public static string Filtered {
            get {
                return ResourceManager.GetString("Filtered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Associating current thesis with {0} you will delete all subsequent keys. Continue anyway?.
        /// </summary>
        public static string SpeciesAssociateContent {
            get {
                return ResourceManager.GetString("SpeciesAssociateContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creation of  new thesis will delete association with {0}. Continue anyway?.
        /// </summary>
        public static string SpeciesDeassociateContent {
            get {
                return ResourceManager.GetString("SpeciesDeassociateContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Key is not refering to {0} anymore.
        /// </summary>
        public static string SpeciesDeassociated {
            get {
                return ResourceManager.GetString("SpeciesDeassociated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are about to associate {0} with {1} instead of currently set association..
        /// </summary>
        public static string SpeciesRepContent {
            get {
                return ResourceManager.GetString("SpeciesRepContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0:f} has {1} derivated taxa. How should we treat them after taxon deletion?.
        /// </summary>
        public static string TaxonDeleteInstruction {
            get {
                return ResourceManager.GetString("TaxonDeleteInstruction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current sequance is already associated with {0}. Continue to reassociate it with {1} anyway?.
        /// </summary>
        public static string TaxonReassociateContent {
            get {
                return ResourceManager.GetString("TaxonReassociateContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current sequence reassotiated from {0} to {1}.
        /// </summary>
        public static string TaxonReassociated {
            get {
                return ResourceManager.GetString("TaxonReassociated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thesis is not following to any other: &quot;{0}&quot;.
        ///You can edit sequence in edit mode or turn back and try another way..
        /// </summary>
        public static string ThesisIsOpen {
            get {
                return ResourceManager.GetString("ThesisIsOpen", resourceCulture);
            }
        }
    }
}