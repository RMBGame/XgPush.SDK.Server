namespace XgPush.SDK.Server.Internal
{
    /// <summary>
    ///
    /// </summary>
    public abstract class BaseSerializeObject
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Serialize();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseSerializeObject<T> : BaseSerializeObject where T : BaseSerializeObject<T>, new()
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize(string jsonString)
        {
            return jsonString.Deserialize<T>();
        }
    }
}