// MeloongCore

namespace MeloongCore
{
	public static class WindowsUtils
	{
		public static bool HasAdminRole()
		{
			using WindowsIdentity ntIdentity = WindowsIdentity.GetCurrent();
			return new WindowsPrincipal(ntIdentity).IsInRole(WindowsBuiltInRole.Administrator);
		}
	}
}
