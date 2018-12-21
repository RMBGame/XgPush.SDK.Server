using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Linq;
using System.Security.Cryptography;

#if SYSTEM_WEB

using Utility = System.Web.HttpUtility;

#else
using Utility = System.Net.WebUtility;
#endif

using static XgPush.SDK.Server.Internal.Constants;

namespace XgPush.SDK.Server.Internal
{
    internal static class TFMs_Compat
    {
        internal static string UrlDecode(this string encodedValue) => Utility.UrlDecode(encodedValue);

        internal static string UrlEncode(this string value) => Utility.UrlEncode(value);

        internal static MD5 CreateMD5()
        {
#if HAS_MD5CRYPTOSERVICEPROVIDER
            return new MD5CryptoServiceProvider();
#else
            return MD5.Create();
#endif
        }

        internal static Action<HttpRequestHeaders, string> AddContentType { get; private set; }

        #region corefx 2.1.0 ~ 2.1.1

        /**
         * reference
         *
         * - enum(internal) [Flags] System.Net.Http.Headers.HttpHeaderType : byte
         * -- https://github.com/dotnet/corefx/blob/v2.1.1/src/System.Net.Http/src/System/Net/Http/Headers/HttpHeaderType.cs
         * -- Custom = 0b10000,
         *
         * - struct(internal/readonly) HeaderDescriptor
         * -- https://github.com/dotnet/corefx/blob/v2.1.1/src/System.Net.Http/src/System/Net/Http/Headers/HeaderDescriptor.cs
         * -- public HeaderDescriptor(KnownHeader knownHeader)
         *
         * - class(public/abstract) HttpHeaders
         * -- https://github.com/dotnet/corefx/blob/v2.1.1/src/System.Net.Http/src/System/Net/Http/Headers/HttpHeaders.cs
         * -- internal void Add(HeaderDescriptor descriptor, string value)
         */

        //private const string type_System_Net_Http_Headers_HttpHeaderType = "System.Net.Http.Headers.HttpHeaderType";
        //private const string field_System_Net_Http_Headers_HttpHeaderType_Custom = "Custom";
        private const string type_System_Net_Http_Headers_HeaderDescriptor
            = "System.Net.Http.Headers.HeaderDescriptor";

        #endregion

        #region corefx 2.0.7 ~ 1.0.0

        /*
         * reference
         * https://github.com/dotnet/corefx/blob/v2.0.7/src/System.Net.Http/src/System/Net/Http/Headers/HttpHeaders.cs
         * https://github.com/dotnet/corefx/blob/v1.0.0/src/System.Net.Http/src/System/Net/Http/Headers/HttpHeaders.cs
         * private HashSet<string> _invalidHeaders;
         */

        #endregion

        #region mono 5.12.0.273

        /*
         * reference
         * https://github.com/mono/mono/blob/mono-5.12.0.273/mcs/class/System.Net.Http/System.Net.Http.Headers/HttpHeaders.cs
         * 106 HeaderInfo.CreateSingle<MediaTypeHeaderValue> ("Content-Type", MediaTypeHeaderValue.TryParse, HttpHeaderKind.Content),
         * 243 HeaderInfo CheckName (string name)
         *
         * https://github.com/mono/mono/blob/mono-5.12.0.273/mcs/class/System.Net.Http/System.Net.Http.Headers/HttpHeaderKind.cs
         * 35 Request = 1,
         *
         * https://github.com/mono/mono/blob/mono-5.12.0.273/mcs/class/System.Net.Http/System.Net.Http.Headers/HeaderInfo.cs
         * 144 public static HeaderInfo CreateSingle<T> (string name, TryParseDelegate<T> parser, HttpHeaderKind headerKind, Func<object, string> toString = null)
         */

        #endregion

        #region .NET Framework

        /*
         * No results found
         * https://referencesource.microsoft.com/#q=System.Net.Http.Headers
         *
         * net45
         * C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Net.Http.dll
         * private HashSet<string> invalidHeaders;
         *
         * net471
         * C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.1\System.Net.Http.dll
         * private HashSet<string> invalidHeaders;
         */

        #endregion

        #region nuget package Microsoft.Net.Http 2.2.29

        /*
         * net40
         * C:\Users\%USERNAME%\.nuget\packages\Microsoft.Net.Http\2.2.29\lib\net40\System.Net.Http.dll
         * private HashSet<string> invalidHeaders;
         */

        #endregion

        private static bool isInitCompatibleMagic = false;

        internal static void InitHttpClientCompatDefaultMagic()
        {
            if (!isInitCompatibleMagic)
            {
                var magics = new Func<Dictionary<string, Action>>[] {
                    CommonCompatMagic,
                    CoreFXv21XCompatMagic,
                };
                InitHttpClientCompatCustomMagic(magics);
            }
            isInitCompatibleMagic = true;
        }

        internal static void InitHttpClientCompatCustomMagic(IEnumerable<Func<Dictionary<string, Action>>> magics)
        {
            foreach (var magic in magics)
            {
                if (magic != default)
                {
                    var keyValuePairs = magic.Invoke();
                    if (keyValuePairs != default)
                    {
                        foreach (var item in keyValuePairs)
                        {
                            try
                            {
                                item.Value?.Invoke();
                            }
                            catch (Exception ex)
                            {
                                Debug.Fail($"TFMs_Compat init {item.Key}() fail.",
                                    ex.GetAllMessage(null, byte.MaxValue));
                            }
                        }
                    }
                }
            }
        }

        private static Dictionary<string, Action> CommonCompatMagic()
        {
            var fieldNames = new[]
            {
                "invalidHeaders",
                "_invalidHeaders",
                "s_invalidHeaders",
            };

            var functionName = nameof(CommonCompatMagic);

            string keySelector(string k) => $"{functionName}[{k}]";

            Action elementSelector(string v)
            {
                return () =>
                {
                    var field = typeof(HttpRequestHeaders)
                      .GetField(v, BindingFlags.NonPublic | BindingFlags.Static);
                    if (field != null)
                    {
                        var invalidFields = (ICollection<string>)field.GetValue(null);
                        invalidFields?.Remove(ContentType);
                    }
                };
            }

            return fieldNames.ToDictionary(keySelector, elementSelector);
        }

        private static Dictionary<string, Action> CoreFXv21XCompatMagic()
        {
#if !NETSTANDARD1_3
            void action()
            {
                var typeString = typeof(string);
                var typeHttpHeaders = typeof(HttpHeaders);
                var assembly = typeHttpHeaders.Assembly;
                var typeHeaderDescriptor = assembly.GetType(type_System_Net_Http_Headers_HeaderDescriptor);
                if (typeHeaderDescriptor != null)
                {
                    var methodHttpHeadersAdd = typeHttpHeaders
                            .GetMethod(nameof(HttpHeaders.Add),
                            BindingFlags.NonPublic | BindingFlags.Instance,
                            null, new[] { typeHeaderDescriptor, typeString }, null);
                    if (methodHttpHeadersAdd != null)
                    {
                        var typeHeaderDescriptorCtor = typeHeaderDescriptor
                            .GetConstructor(
                            BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeString }, null);
                        if (typeHeaderDescriptorCtor != null)
                        {
                            var valueContentTypeHeaderDescriptor =
                                typeHeaderDescriptorCtor.Invoke(new object[] { ContentType });
                            if (valueContentTypeHeaderDescriptor != null)
                            {
                                AddContentType = (headers, value)
                                    => methodHttpHeadersAdd.Invoke(headers, new[] { valueContentTypeHeaderDescriptor, value });
                            }
                        }
                    }
                }
            }
            return new Dictionary<string, Action> { { nameof(CoreFXv21XCompatMagic), action } };
#else
            return default;
#endif
        }

        //private static Dictionary<string, Action> MonoCompatMagic()
        //{
        //    var isRunningOnMono = Type.GetType("Mono.Runtime") != null;
        //    if (isRunningOnMono)
        //    {
        //    }

        //    return default;
        //}
    }
}