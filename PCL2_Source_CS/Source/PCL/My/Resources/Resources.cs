// PCL.My.Resources

namespace PCL.My.Resources
{
	[StandardModule]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[HideModuleName]
	internal sealed class Resources
	{
		private static ResourceManager resourceMan;
		private static CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					resourceMan = new ResourceManager("PCL.Resources", typeof(Resources).Assembly);
				}
				return resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}
		internal static byte[] CacheCow_Client => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("CacheCow.Client", resourceCulture));
		internal static byte[] CacheCow_Client_FileStore => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("CacheCow.Client.FileStore", resourceCulture));
		internal static byte[] CacheCow_Common => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("CacheCow.Common", resourceCulture));
		internal static byte[] ThrottleDebounce => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("ThrottleDebounce", resourceCulture));
		internal static byte[] Custom => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Custom", resourceCulture));
		internal static byte[] ForgeInstaller => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("ForgeInstaller", resourceCulture));
		internal static byte[] Help => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Help", resourceCulture));
		internal static byte[] Imazen_WebP => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Imazen.WebP", resourceCulture));
		internal static byte[] libwebp64 => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("libwebp64", resourceCulture));
		internal static byte[] MeloongCore => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("MeloongCore", resourceCulture));
		internal static byte[] MeloongCore_Wpf => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("MeloongCore.Wpf", resourceCulture));
		internal static byte[] NAudio => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("NAudio", resourceCulture));
		internal static byte[] Newtonsoft_Json => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Newtonsoft.Json", resourceCulture));
		internal static byte[] Ookii_Dialogs_Wpf => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Ookii.Dialogs.Wpf", resourceCulture));
		internal static byte[] PCLCS => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("PCLCS", resourceCulture));
		internal static byte[] System_Net_Http_Formatting => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("System.Net.Http.Formatting", resourceCulture));
		internal static byte[] Transformer => (byte[])RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Transformer", resourceCulture));
	}
}
