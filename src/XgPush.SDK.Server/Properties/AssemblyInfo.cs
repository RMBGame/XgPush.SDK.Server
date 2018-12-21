using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: SuppressIldasm]
[assembly: InternalsVisibleTo("XgPush.SDK.Server.Test.UnitTest")]
[assembly: InternalsVisibleTo("XgPush.SDK.Server.Test.ConsoleApp")]

namespace XgPush.SDK.Server.Properties
{
    /// <summary>
    ///
    /// </summary>
    public static class AssemblyInfo
    {
        private static readonly Lazy<Assembly> mAssembly = new Lazy<Assembly>(() =>
        {
            var type = typeof(AssemblyInfo);
#if !NETSTANDARD1_3
            return type.Assembly;
#else
            return type.GetTypeInfo().Assembly;
#endif
        });

        private static readonly Lazy<AssemblyName> mAssemblyName = new Lazy<AssemblyName>(() =>
        {
            return Assembly.GetName();
        });

        /// <summary>
        ///
        /// </summary>
        public static Assembly Assembly => mAssembly.Value;

        /// <summary>
        ///
        /// </summary>
        public static AssemblyName AssemblyName => mAssemblyName.Value;

        /// <summary>
        ///
        /// </summary>
        public static Version Version => AssemblyName.Version;
    }
}