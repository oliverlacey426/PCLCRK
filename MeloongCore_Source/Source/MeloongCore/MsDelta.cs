// MeloongCore

namespace MeloongCore
{
	public static class MsDelta
	{
		private struct DeltaInput
		{
			public IntPtr Start;
			public IntPtr Size;
			[MarshalAs(UnmanagedType.Bool)]
			public bool Editable;
		}
		public static void Create(string oldFilePath, string newFilePath, string deltaFilePath)
		{
			if (!FileUtils.Exists(oldFilePath))
			{
				throw new FileNotFoundException("旧文件不存在：" + oldFilePath, oldFilePath);
			}
			if (!FileUtils.Exists(newFilePath))
			{
				throw new FileNotFoundException("新文件不存在：" + newFilePath, newFilePath);
			}
			DirectoryUtils.Create(PathUtils.RemoveLastPart(deltaFilePath));
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(20, 3, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("正在生成补丁：");
				handler.AppendFormatted(oldFilePath);
				handler.AppendLiteral(" → ");
				handler.AppendFormatted(newFilePath);
				handler.AppendLiteral("，补丁文件将输出到：");
				handler.AppendFormatted(deltaFilePath);
			}
			Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\MsDelta.cs");
			if (!CreateDelta(15L, 131072L, 0L, PathUtils.ForApi(oldFilePath), PathUtils.ForApi(newFilePath), null, null, default, IntPtr.Zero, 32u, PathUtils.ForApi(deltaFilePath)))
			{
				throw new Win32Exception();
			}
		}
		public static void Apply(string oldFilePath, string deltaFilePath, string newFilePath)
		{
			if (!FileUtils.Exists(oldFilePath))
			{
				throw new FileNotFoundException("旧文件不存在：" + oldFilePath, oldFilePath);
			}
			if (!FileUtils.Exists(deltaFilePath))
			{
				throw new FileNotFoundException("补丁文件不存在：" + deltaFilePath, deltaFilePath);
			}
			DirectoryUtils.Create(PathUtils.RemoveLastPart(newFilePath));
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(27, 3, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("正在应用补丁：原始文件 ");
				handler.AppendFormatted(oldFilePath);
				handler.AppendLiteral(" + 补丁文件 ");
				handler.AppendFormatted(deltaFilePath);
				handler.AppendLiteral(" → 输出至 ");
				handler.AppendFormatted(newFilePath);
			}
			Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\General\\MsDelta.cs");
			if (!ApplyDelta(1L, PathUtils.ForApi(oldFilePath), PathUtils.ForApi(deltaFilePath), PathUtils.ForApi(newFilePath)))
			{
				throw new Win32Exception();
			}
		}
		[DllImport("msdelta.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CreateDelta(long fileTypeSet, long setFlags, long resetFlags, string sourceName, string targetName, string? sourceOptionsName, string? targetOptionsName, DeltaInput globalOptions, IntPtr targetFileTime, uint hashAlgId, string deltaName);
		[DllImport("msdelta.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool ApplyDelta(long applyFlags, string sourceName, string deltaName, string targetName);
	}
}
