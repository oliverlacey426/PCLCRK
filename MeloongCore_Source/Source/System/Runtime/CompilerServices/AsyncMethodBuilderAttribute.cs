// System.Runtime.CompilerServices

namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	[ExcludeFromCodeCoverage]
	internal sealed class AsyncMethodBuilderAttribute : Attribute
	{
		public Type BuilderType { get; }
		public AsyncMethodBuilderAttribute(Type builderType)
		{
			BuilderType = builderType;
		}
	}
}
