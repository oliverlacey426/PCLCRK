// MeloongCore

namespace MeloongCore
{
	public static class PathUtils
	{
		private struct UnicodeString
		{
			public ushort Length;
			public ushort MaximumLength;
			public unsafe char* Buffer;
		}
		private struct GenerateNameContext
		{
			public ushort Checksum;
			public byte ChecksumInserted;
			public byte NameLength;
			public unsafe fixed char NameBuffer[8];
			public uint ExtensionLength;
			public unsafe fixed char ExtensionBuffer[4];
			public uint LastIndexValue;
		}
		public static string AddSlashSuffix(string folder)
		{
			return folder + ((folder.EndsWithF(Path.DirectorySeparatorChar) || folder.EndsWithF(Path.AltDirectorySeparatorChar)) ? "" : ((object)Path.DirectorySeparatorChar));
		}
		public static string RemoveSlashSuffix(string folder)
		{
			return folder.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
		}
		public unsafe static string ToShortPath(string pathName)
		{
			if (string.IsNullOrEmpty(pathName))
			{
				return pathName;
			}
			if (!pathName.Contains(":"))
			{
				return pathName;
			}
			if (pathName.Length <= 220)
			{
				return pathName;
			}
			pathName = ForCompare(pathName);
			string text = "";
			string text2 = pathName;
			bool isEnabled;
			if (FileUtils.Exists(pathName))
			{
				string lastPart = GetLastPart(pathName);
				if (lastPart.Length >= 180)
				{
					Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel>(17, 1, out isEnabled);
					if (isEnabled)
					{
						handler.AppendLiteral("文件名过长，将为文件名生成短路径：");
						handler.AppendFormatted(pathName);
					}
					Logger.Warn(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\PathUtils.cs");
				}
				else
				{
					text = lastPart;
					text2 = RemoveLastPart(pathName);
				}
			}
			while (!DirectoryUtils.Exists(text2) && !FileUtils.Exists(text2))
			{
				string directoryName = Path.GetDirectoryName(text2);
				if (string.IsNullOrEmpty(directoryName) || directoryName == text2)
				{
					return pathName;
				}
				text = Path.Combine(GetLastPart(text2), text);
				text2 = directoryName;
			}
			if (text.Length >= 200)
			{
				Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel>(32, 2, out isEnabled);
				if (isEnabled)
				{
					handler2.AppendLiteral("路径中的大部分内容都尚未建立，无法在内容不存在时缩短路径：");
					handler2.AppendFormatted(pathName);
					handler2.AppendLiteral(" → ");
					handler2.AppendFormatted(text2);
				}
				Logger.Warn(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\PathUtils.cs");
				return pathName;
			}
			Func<string, string> func = (string text5) =>
			{
				char[] array = new char[220];
				int shortPathNameW = GetShortPathNameW(ForApi(text5), array, array.Length);
				return ((shortPathNameW <= 220 && shortPathNameW != 0) || 1 == 0) ? new string(array, 0, shortPathNameW) : text5;
			};
			string text3 = func(text2);
			if (Path.Combine(text3, text).Length > 220)
			{
				Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel> handler3 = new Logger.LogInterpolatedStringHandler<Logger.WarnLogLevel>(24, 3, out isEnabled);
				if (isEnabled)
				{
					handler3.AppendFormatted("GetShortPathNameW");
					handler3.AppendLiteral(" 未能有效缩短路径，将尝试生成新的短名称：");
					handler3.AppendFormatted(text2);
					handler3.AppendLiteral(" → ");
					handler3.AppendFormatted(text3);
				}
				Logger.Warn(ref handler3, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\PathUtils.cs");
				List<(string, string)> list = new List<(string, string)>();
				string text4 = text2;
				while (!string.IsNullOrEmpty(text4))
				{
					string lastPart2 = GetLastPart(text4);
					if (lastPart2.Length > 12 && string.Equals(GetLastPart(func(text4)), lastPart2, StringComparison.OrdinalIgnoreCase))
					{
						list.Add((text4, lastPart2));
					}
					text4 = Path.GetDirectoryName(text4);
				}
				foreach (var item2 in list.OrderByDescending(((string Path, string Name) candidate) => candidate.Name.Length))
				{
					using SafeFileHandle safeFileHandle = CreateFileW(ForApi(item2.Item1), 1073807360u, 7u, IntPtr.Zero, 3u, 33554432u, IntPtr.Zero);
					if (safeFileHandle.IsInvalid)
					{
						continue;
					}
					bool flag = false;
					try
					{
						fixed (char* item = item2.Item2)
						{
							char* buffer = stackalloc char[12];
							UnicodeString name = new UnicodeString
							{
								Length = (ushort)(item2.Item2.Length * 2),
								MaximumLength = (ushort)((item2.Item2.Length + 1) * 2),
								Buffer = item
							};
							UnicodeString name8dot = new UnicodeString
							{
								MaximumLength = 24,
								Buffer = buffer
							};
							GenerateNameContext context = default;
							for (int num = 0; num < 20; num++)
							{
								name8dot.Length = 0;
								if (RtlGenerate8dot3Name(ref name, 0, ref context, ref name8dot) == 0)
								{
									string shortName = new string(name8dot.Buffer, 0, name8dot.Length / 2);
									if (SetFileShortNameW(safeFileHandle, shortName))
									{
										flag = true;
										break;
									}
									if (Marshal.GetLastWin32Error() != 183)
									{
										break;
									}
									continue;
								}
								break;
							}
						}
					}
					catch (EntryPointNotFoundException)
					{
					}
					if (flag)
					{
						text3 = func(text2);
						if (Path.Combine(text3, text).Length <= 220)
						{
							break;
						}
					}
				}
			}
			return RemoveSlashSuffix(RemoveExtendedPrefix(Path.Combine(text3, text)));
		}
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetShortPathNameW(string lpszLongPath, [Out] char[]? buffer, int cchBuffer);
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern SafeFileHandle CreateFileW(string fileName, uint desiredAccess, uint shareMode, IntPtr securityAttributes, uint creationDisposition, uint flagsAndAttributes, IntPtr templateFile);
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetFileShortNameW(SafeFileHandle file, string shortName);
		[DllImport("ntdll.dll")]
		private static extern int RtlGenerate8dot3Name(ref UnicodeString name, byte allowExtendedCharacters, ref GenerateNameContext context, ref UnicodeString name8dot3);
		public static string ToExtendedFormat(string pathName)
		{
			if (string.IsNullOrWhiteSpace(pathName))
			{
				return pathName;
			}
			if (!pathName.Contains(":"))
			{
				return pathName;
			}
			if (pathName.StartsWithF("\\\\?\\"))
			{
				return pathName;
			}
			pathName = RemoveSlashSuffix(pathName).Replace('/', '\\');
			if (pathName.StartsWithF("\\\\"))
			{
				return "\\\\?\\UNC\\" + pathName.Substring(2);
			}
			return "\\\\?\\" + pathName;
		}
		public static string RemoveExtendedPrefix(string pathName)
		{
			if (pathName.StartsWithF("\\\\?\\UNC\\"))
			{
				return "\\\\" + pathName.AfterLast("\\\\?\\UNC\\");
			}
			if (pathName.StartsWithF("\\\\?\\"))
			{
				return pathName.AfterLast("\\\\?\\");
			}
			return pathName;
		}
		public static string RemoveLastPart(string pathName)
		{
			if (pathName.Contains("://"))
			{
				pathName = RemoveSlashSuffix(pathName.BeforeFirst("#").BeforeFirst("?"));
			}
			return RemoveSlashSuffix(pathName).BeforeLastOfAny(new _z__ReadOnlyArray<string>(new string[2] { "\\", "/" }));
		}
		public static string GetLastPart(string pathName)
		{
			pathName = ((!pathName.Contains("://")) ? RemoveSlashSuffix(pathName) : RemoveSlashSuffix(pathName.BeforeFirst("#").BeforeFirst("?")));
			return pathName.AfterLastOfAny(new _z__ReadOnlyArray<string>(new string[2] { "\\", "/" }));
		}
		public static string GetFileNameWithoutExtension(string pathName)
		{
			return GetLastPart(pathName).BeforeLast(".");
		}
		public static string GetExtension(string pathName)
		{
			string lastPart = GetLastPart(pathName);
			if (!lastPart.Contains("."))
			{
				return "";
			}
			return lastPart.AfterLast(".").Lower();
		}
		public static char? GetDiskName(string? pathName)
		{
			if (string.IsNullOrEmpty(pathName))
			{
				return null;
			}
			pathName = RemoveExtendedPrefix(pathName);
			if (pathName.StartsWithF("\\\\") || pathName.StartsWithF("/"))
			{
				return null;
			}
			return char.ToUpper(pathName[0]);
		}
		public static string ForCompare(string pathName)
		{
			try
			{
				return RemoveSlashSuffix(RemoveExtendedPrefix(Path.GetFullPath(pathName)).Replace("/", "\\"));
			}
			catch (ArgumentException innerException)
			{
				throw new ArgumentException("路径不正确：" + pathName, innerException);
			}
		}
		public static string ForApi(string pathName)
		{
			pathName = ToExtendedFormat(pathName);
			if (pathName.EndsWithF(":"))
			{
				pathName += "\\";
			}
			if (pathName.EndsWithF(":\\"))
			{
				return pathName;
			}
			return RemoveSlashSuffix(pathName);
		}
		public static bool IsParentOf(string parentPath, string childPath)
		{
			parentPath = ForCompare(parentPath);
			childPath = ForCompare(childPath);
			if (!childPath.StartsWithF(parentPath + "\\"))
			{
				return childPath == parentPath;
			}
			return true;
		}
	}
}
