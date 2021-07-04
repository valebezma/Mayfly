using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Mayfly
{
    public static class EntryAssemblyInfo
    {
        public static string Title
        {
            get
            {
                var attr = GetAssemblyAttribute<AssemblyTitleAttribute>();
                if (attr != null)
                    return attr.Title;
                return string.Empty;
            }
        }

        public static string Company
        {
            get
            {
                var attr = GetAssemblyAttribute<AssemblyCompanyAttribute>();
                if (attr != null)
                    return attr.Company;
                return string.Empty;
            }
        }

        public static string Copyright
        {
            get
            {
                var attr = GetAssemblyAttribute<AssemblyCopyrightAttribute>();
                if (attr != null)
                    return attr.Copyright;
                return string.Empty;
            }
        }

        public static string Version
        {
            get
            {
                var attr = GetAssemblyAttribute<AssemblyFileVersionAttribute>();
                if (attr != null)
                    return attr.Version;
                return string.Empty;
            }
        }

        private static T GetAssemblyAttribute<T>() where T : Attribute
        {
            object[] attributes = Assembly.GetEntryAssembly()
                .GetCustomAttributes(typeof(T), true);
            if (attributes == null || attributes.Length == 0) return null;
            return (T)attributes[0];
        }
    }

    public static class AssemblyInfo
    {
        public static string Title
        {
            get
            {
                var attr = GetAssemblyAttribute<AssemblyTitleAttribute>();
                if (attr != null)
                    return attr.Title;
                return string.Empty;
            }
        }

        public static string Company
        {
            get
            {
                var attr = GetAssemblyAttribute<AssemblyCompanyAttribute>();
                if (attr != null)
                    return attr.Company;
                return string.Empty;
            }
        }

        public static string Copyright
        {
            get
            {
                var attr = GetAssemblyAttribute<AssemblyCopyrightAttribute>();
                if (attr != null)
                    return attr.Copyright;
                return string.Empty;
            }
        }

        public static string Version
        {
            get
            {
                var attr = GetAssemblyAttribute<AssemblyFileVersionAttribute>();
                if (attr != null)
                    return attr.Version;
                return string.Empty;
            }
        }

        private static T GetAssemblyAttribute<T>() where T : Attribute
        {
            object[] attributes = Assembly.GetCallingAssembly()
                .GetCustomAttributes(typeof(T), true);
            if (attributes == null || attributes.Length == 0) return null;
            return (T)attributes[0];
        }
    }
}
