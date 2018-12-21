#if !HAS_SUPPRESSILDASMATTRIBUTE

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
    internal sealed class SuppressIldasmAttribute : Attribute { }
}

#endif