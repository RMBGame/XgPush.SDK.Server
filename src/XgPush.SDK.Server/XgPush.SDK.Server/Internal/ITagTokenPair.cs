namespace XgPush.SDK.Server.Internal
{
    internal interface ITagTokenPair
    {
        string Tag { get; set; }

        string Token { get; set; }

        string[] ToArray();
    }
}