﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mayfly.Wild.Resources {
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
    public class Artifact {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Artifact() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mayfly.Wild.Resources.Artifact", typeof(Artifact).Assembly);
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
        ///   Looks up a localized string similar to There are some artifacts found in data.
        /// </summary>
        public static string ArtifactsFound {
            get {
                return ResourceManager.GetString("ArtifactsFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} individuals have issues with tally.
        /// </summary>
        public static string IndividualTallies {
            get {
                return ResourceManager.GetString("IndividualTallies", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sample Is not marked with tally.
        /// </summary>
        public static string IndividualTallyMissing {
            get {
                return ResourceManager.GetString("IndividualTallyMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No sample following tally #{0}.
        /// </summary>
        public static string IndividualTallyOdd {
            get {
                return ResourceManager.GetString("IndividualTallyOdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Length of {0} specimen not measured.
        /// </summary>
        public static string LogLength {
            get {
                return ResourceManager.GetString("LogLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Length issues in {0} log records.
        /// </summary>
        public static string LogLengths {
            get {
                return ResourceManager.GetString("LogLengths", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Record mass ({0:n3} kg) is more than detailed mass ({1:n3}) which is fine.
        /// </summary>
        public static string LogMassMissing {
            get {
                return ResourceManager.GetString("LogMassMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Record mass ({0:n3} kg) is {1:p1} less than detailed mass ({2:n3}).
        /// </summary>
        public static string LogMassOdd {
            get {
                return ResourceManager.GetString("LogMassOdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} log records have issues with mass.
        /// </summary>
        public static string LogMassOdds {
            get {
                return ResourceManager.GetString("LogMassOdds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Species definition issues in {0} log records.
        /// </summary>
        public static string LogSpecia {
            get {
                return ResourceManager.GetString("LogSpecia", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Species definition not found in index.
        /// </summary>
        public static string LogSpecies {
            get {
                return ResourceManager.GetString("LogSpecies", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} measures have {1} outliers.
        /// </summary>
        public static string ValueHasOutliers {
            get {
                return ResourceManager.GetString("ValueHasOutliers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} of {1} specimen is not recoverable.
        /// </summary>
        public static string ValueIsCritical {
            get {
                return ResourceManager.GetString("ValueIsCritical", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} of {1} specimen is obtained from length.
        /// </summary>
        public static string ValueIsRecoverable {
            get {
                return ResourceManager.GetString("ValueIsRecoverable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} of {1} specimen is obtained using model with {2} outliers.
        /// </summary>
        public static string ValueIsRecoverableButHasOutliers {
            get {
                return ResourceManager.GetString("ValueIsRecoverableButHasOutliers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No position recorded.
        /// </summary>
        public static string Where {
            get {
                return ResourceManager.GetString("Where", resourceCulture);
            }
        }
    }
}
