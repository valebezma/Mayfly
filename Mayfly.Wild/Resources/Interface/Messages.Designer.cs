﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mayfly.Wild.Resources.Interface {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mayfly.Wild.Resources.Interface.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Bad age data.
        /// </summary>
        public static string AgeInacceptable {
            get {
                return ResourceManager.GetString("AgeInacceptable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Age data is lacking, so age analysis was not obtained..
        /// </summary>
        public static string AgeInacceptableInstruction {
            get {
                return ResourceManager.GetString("AgeInacceptableInstruction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Card is already opened.
        /// </summary>
        public static string AlreadyOpened {
            get {
                return ResourceManager.GetString("AlreadyOpened", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your data does not contain any artifacts.
        /// </summary>
        public static string ArtifactsNoneNotification {
            get {
                return ResourceManager.GetString("ArtifactsNoneNotification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data loaded to explorer is of very high quality. You can use it in any analysis..
        /// </summary>
        public static string ArtifactsNoneNotificationInstruction {
            get {
                return ResourceManager.GetString("ArtifactsNoneNotificationInstruction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Artifacts found in your data.
        /// </summary>
        public static string ArtifactsNotification {
            get {
                return ResourceManager.GetString("ArtifactsNotification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Click to explore artifacts in your data. We recommend to eliminate artifacts from your data before analisys.
        /// </summary>
        public static string ArtifactsNotificationInstruction {
            get {
                return ResourceManager.GetString("ArtifactsNotificationInstruction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current list of recent species contains {0} records. Do you want to clear it anyway?.
        /// </summary>
        public static string ClearRecent {
            get {
                return ResourceManager.GetString("ClearRecent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} records sumed up.
        /// </summary>
        public static string DuplicateSummed {
            get {
                return ResourceManager.GetString("DuplicateSummed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Factor name is strongly required.
        /// </summary>
        public static string FactorNameRequired {
            get {
                return ResourceManager.GetString("FactorNameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirm changind identification from &quot;{0}&quot; to &quot;{1}&quot;. If species entry doen&apos;s exist in card, it will be created..
        /// </summary>
        public static string IndRename {
            get {
                return ResourceManager.GetString("IndRename", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirm replacing selected log entry from &quot;{0}&quot; to &quot;{1}&quot;. If species entry doesn&apos;t exist, it will be created..
        /// </summary>
        public static string LogRename {
            get {
                return ResourceManager.GetString("LogRename", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Single specimen mass overtops total.
        /// </summary>
        public static string MassInequal {
            get {
                return ResourceManager.GetString("MassInequal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Single specimen count equals total.
        /// </summary>
        public static string QuantityEqual {
            get {
                return ResourceManager.GetString("QuantityEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Both single specimen count and total equal {0} inds..
        /// </summary>
        public static string QuantityEqualDetails {
            get {
                return ResourceManager.GetString("QuantityEqualDetails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Single specimen count overtops total.
        /// </summary>
        public static string QuantityInequal {
            get {
                return ResourceManager.GetString("QuantityInequal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Single specimen count is {0} but specified total is {1} inds.
        ///Double click to adjust total.
        /// </summary>
        public static string QuantitySetEqual {
            get {
                return ResourceManager.GetString("QuantitySetEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Card is saved.
        /// </summary>
        public static string Saved {
            get {
                return ResourceManager.GetString("Saved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} added.
        /// </summary>
        public static string SpeciesAdded {
            get {
                return ResourceManager.GetString("SpeciesAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirm renaming &quot;{0}&quot; to &quot;{1}&quot; in all of loaded cards..
        /// </summary>
        public static string SpeciesRename {
            get {
                return ResourceManager.GetString("SpeciesRename", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Water object is specified.
        /// </summary>
        public static string WaterSet {
            get {
                return ResourceManager.GetString("WaterSet", resourceCulture);
            }
        }
    }
}
