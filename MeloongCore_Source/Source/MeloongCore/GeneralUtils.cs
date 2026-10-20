// MeloongCore

namespace MeloongCore
{
	public static class GeneralUtils
	{
		public static bool IsGenericInstanceOf(object? instance, Type genericTypeDefinition)
		{
			if (instance == null)
			{
				return false;
			}
			Type type = instance.GetType();
			while ((object)type != null)
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}
	}
}
