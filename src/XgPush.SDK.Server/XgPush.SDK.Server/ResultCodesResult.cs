using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class ResultCodesResult : BaseSerializeObject<ResultCodesResult>, IResultV2,
#if NET40
        IEnumerable<XingePushClientResultCode>
#else
        IReadOnlyList<XingePushClientResultCode>
#endif
    {
        /// <summary>
        ///
        /// </summary>
        public List<XingePushClientResultCode> Values { get; set; } = new List<XingePushClientResultCode>();

        /// <summary>
        ///
        /// </summary>
        public int Count => Values.Count;

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public XingePushClientResultCode this[int index] => Values[index];

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public void Init(JToken jToken)
        {
            if (jToken?.HasValues ?? false)
            {
                Values = jToken.Values<XingePushClientResultCode>().ToList();
            }
        }

        IEnumerator<XingePushClientResultCode> IEnumerable<XingePushClientResultCode>.GetEnumerator()
        {
            return ((IEnumerable<XingePushClientResultCode>)Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<XingePushClientResultCode>)Values).GetEnumerator();
        }

        /// <summary>
        ///
        /// </summary>
        public ResultCodesResult() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resultCodes"></param>
        public ResultCodesResult(IEnumerable<XingePushClientResultCode> resultCodes)
        {
            if (resultCodes is List<XingePushClientResultCode> list)
            {
                Values = list;
            }
            else
            {
                Values = resultCodes.ToList();
            }
        }
    }
}