using XgPush.SDK.Server.BaseTypes;

#pragma warning disable CS0618 // 类型或成员已过时

namespace XgPush.SDK.Server.Test.UnitTest
{
    public class Model
    {
        public AudienceType AudienceType { get; set; }
        public TimeInterval TimeInterval { get; set; }
        public Operator Operator1 { get; set; }
        public Operator Operator2 { get; set; }
        public DigitBoolean True { get; set; }
        public DigitBoolean False { get; set; }
        public DigitBoolean? Null { get; set; }
        public DigitBoolean? NullableTrue { get; set; }
        public DigitBoolean? NullableFalse { get; set; }
        public Second ThreeDays { get; set; }
        public Second UInt32S { get; set; }
        public Second Int32S { get; set; }
        public Second? NullableSeconds { get; set; }
        public Second? NullableThreeDays { get; set; }
        public Second? NullableUInt32S { get; set; }
        public Second? NullableInt32S { get; set; }
        public TagTokenPair TagTokenPair { get; set; }
        public Compat.TagTokenPair TagTokenPairCompat { get; set; }
        public NullableDateTime Now1 { get; set; }
        public NullableDateTime? Now2 { get; set; }
        public NullableDateTime TimeEmpty1 { get; set; }
        public NullableDateTime? TimeEmpty2 { get; set; }
        public NullableDateTime? TimeNull2 { get; set; }
        public NullableDateTime D1 { get; set; }
        public NullableDateTime? D2 { get; set; }
        public iOSEnvironment iOSEnv1 { get; set; }
        public iOSEnvironment iOSEnv2 { get; set; }
        public iOSEnvironment? iOSEnvNullable { get; set; }
        public iOSEnvironment? iOSEnv1Nullable { get; set; }
        public iOSEnvironment? iOSEnv2Nullable { get; set; }

        public iOSEnvironmentV3 iOSEnv1_V3 { get; set; }
        public iOSEnvironmentV3 iOSEnv2_V3 { get; set; }
        public iOSEnvironmentV3? iOSEnvNullable_V3 { get; set; }
        public iOSEnvironmentV3? iOSEnv1Nullable_V3 { get; set; }
        public iOSEnvironmentV3? iOSEnv2Nullable_V3 { get; set; }
    }
}