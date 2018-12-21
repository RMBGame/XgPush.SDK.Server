using Newtonsoft.Json;
using System.Collections.Generic;
using XgPush.SDK.Server.Internal;
using static XgPush.SDK.Server.Internal.Constants;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class TimeInterval : IsValid, IToDictionary
    {
        /// <summary>
        ///
        /// </summary>
        public TimeInterval() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="startHour"></param>
        /// <param name="startMin"></param>
        /// <param name="endHour"></param>
        /// <param name="endMin"></param>
        public TimeInterval(int startHour, int startMin, int endHour, int endMin)
        {
            StartHour = startHour;
            StartMin = startMin;
            EndHour = endHour;
            EndMin = endMin;
        }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public int StartHour { get => Start.Hour; set => Start.Hour = value; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public int StartMin { get => Start.Min; set => Start.Min = value; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public int EndHour { get => End.Hour; set => End.Hour = value; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public int EndMin { get => End.Min; set => End.Min = value; }

        [JsonProperty(start)]
        internal Internal_Item Start { get; set; } = new Internal_Item();

        [JsonProperty(end)]
        internal Internal_Item End { get; set; } = new Internal_Item();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return Start.IsValid() && End.IsValid();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                {
                    start, new Dictionary<string, object>
                    {
                        { hour, Start.HourString },
                        { min, Start.MinString },
                    }
                },
                {
                    end, new Dictionary<string, object>
                    {
                        { hour, End.HourString },
                        { min, End.MinString },
                    }
                },
            };
        }

        internal class Internal_Item : IsValid
        {
            private const string @default = "0";
            private string mHourString = @default;
            private int mHour = 0;
            private string mMinString = @default;
            private int mMin = 0;

            [JsonProperty(hour)]
            public string HourString
            {
                get => mHourString;
                set
                {
                    if (int.TryParse(value, out var @int))
                    {
                        mHour = @int;
                        mHourString = value;
                    }
                }
            }

            [JsonIgnore]
            public int Hour
            {
                get => mHour;
                set
                {
                    mHour = value;
                    mHourString = value.ToString();
                }
            }

            [JsonProperty(min)]
            public string MinString
            {
                get => mMinString;
                set
                {
                    if (int.TryParse(value, out var @int))
                    {
                        mMin = @int;
                        mMinString = value;
                    }
                }
            }

            [JsonIgnore]
            public int Min
            {
                get => mMin;
                set
                {
                    mMin = value;
                    mMinString = value.ToString();
                }
            }

            public bool IsValid()
            {
                return mHour >= 0 && mHour <= 23 && mMin >= 0 && mMin <= 23;
            }
        }
    }
}