// PCLCS

namespace PCLCS
{
	public static class LaunchUtils
	{
		private static readonly object ExtractPatchLock = new object();
		public static string ExtractPatch(string patchFilePath, string outputDirectory)
		{
			Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(12, 2, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("选定的资源 ");
				handler.AppendFormatted(patchFilePath);
				handler.AppendLiteral(" 输出路径：");
				handler.AppendFormatted(outputDirectory);
			}
			Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Launch\\LaunchUtils.cs");
			lock (ExtractPatchLock)
			{
				try
				{
					FileUtils.ExtractResources(patchFilePath, Path.Combine(outputDirectory, PathUtils.GetLastPart(patchFilePath)), typeof(LaunchUtils));
				}
				catch (Exception ex)
				{
					if (!FileUtils.Exists(outputDirectory))
					{
						throw new FileNotFoundException("释放 " + patchFilePath + " 失败", ex);
					}
					Logger.Warn(ex, patchFilePath + " 文件重新释放失败，将尝试更换文件名重新生成", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\PCLCS\\Launch\\LaunchUtils.cs");
					patchFilePath = Path.Combine(outputDirectory, PathUtils.GetFileNameWithoutExtension(patchFilePath) + "2." + PathUtils.GetExtension(patchFilePath));
					try
					{
						FileUtils.ExtractResources(patchFilePath, Path.Combine(outputDirectory, PathUtils.GetLastPart(patchFilePath)), typeof(LaunchUtils));
					}
					catch (Exception innerException)
					{
						throw new FileNotFoundException("释放 " + patchFilePath + " 最终尝试失败", innerException);
					}
				}
			}
			return Path.Combine(outputDirectory, PathUtils.GetLastPart(patchFilePath));
		}
	}
}
