// MeloongCore

namespace MeloongCore
{
	public static class FileUtils
	{
		[ComImport]
		[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IShellItem
		{
		}
		[ComImport]
		[Guid("3AD05575-8857-4850-9277-11B85BDB8E09")]
		private class FileOperation
		{
		}
		[ComImport]
		[Guid("947AAB5F-0A5C-4C13-B4D6-4BF7836FC9F8")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IFileOperation
		{
			void Advise(IntPtr pfops, out uint pdwCookie);
			void Unadvise(uint dwCookie);
			void SetOperationFlags(uint dwOperationFlags);
			void SetProgressMessage([MarshalAs(UnmanagedType.LPWStr)] string pszMessage);
			void SetProgressDialog(IntPtr popd);
			void SetProperties(IntPtr pproparray);
			void SetOwnerWindow(IntPtr hwndOwner);
			void ApplyPropertiesToItem(IShellItem psiItem);
			void ApplyPropertiesToItems(IntPtr punkItems);
			void RenameItem(IShellItem psiItem, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName, IntPtr pfopsItem);
			void RenameItems(IntPtr pUnkItems, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);
			void MoveItem(IShellItem psiItem, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string pszNewName, IntPtr pfopsItem);
			void MoveItems(IntPtr punkItems, IShellItem psiDestinationFolder);
			void CopyItem(IShellItem psiItem, IShellItem psiDestinationFolder, [MarshalAs(UnmanagedType.LPWStr)] string pszCopyName, IntPtr pfopsItem);
			void CopyItems(IntPtr punkItems, IShellItem psiDestinationFolder);
			void DeleteItem(IShellItem psiItem, IntPtr pfopsItem);
			void DeleteItems(IntPtr punkItems);
			void NewItem(IShellItem psiDestinationFolder, uint dwFileAttributes, [MarshalAs(UnmanagedType.LPWStr)] string pszName, [MarshalAs(UnmanagedType.LPWStr)] string pszTemplateName, IntPtr pfopsItem);
			void PerformOperations();
			void GetAnyOperationsAborted([MarshalAs(UnmanagedType.Bool)] out bool pfAnyOperationsAborted);
		}
		public static Stream ReadAsStream(string filePath, Type? type = null)
		{
			if ((object)type == null)
			{
				return Retrier.Attempt((int _) => new FileStream(PathUtils.ForApi(filePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite), 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
			}
			return type.Assembly.GetManifestResourceStream(filePath) ?? throw new FileNotFoundException("Embedded resource \"" + filePath + "\" was not found.");
		}
		public static byte[] ReadAsBytes(string filePath, Type? type = null)
		{
			using Stream stream = ReadAsStream(filePath, type);
			using MemoryStream memoryStream = new MemoryStream();
			stream.CopyTo(memoryStream);
			return memoryStream.ToArray();
		}
		public static string ReadAsString(string filePath, Encoding? encoding = null, Type? type = null)
		{
			return ReadAsBytes(filePath, type).GetString(encoding);
		}
		public static string? TryReadAsString(string filePath, Encoding? encoding = null, Type? type = null)
		{
			try
			{
				if (type == null && !Exists(filePath))
				{
					return null;
				}
				return ReadAsBytes(filePath, type).GetString(encoding);
			}
			catch
			{
				return null;
			}
		}
		public static string[] ReadAsLines(string filePath, bool skipEmptyLines = false, Encoding? encoding = null, Type? type = null)
		{
			return ReadAsString(filePath, encoding, type).SplitLines(skipEmptyLines);
		}
		public static string[] TryReadAsLines(string filePath, bool skipEmptyLines = false, Encoding? encoding = null, Type? type = null)
		{
			return TryReadAsString(filePath, encoding, type)?.SplitLines(skipEmptyLines) ?? Array.Empty<string>();
		}
		public static JToken? ReadAsJson(string filePath, Encoding? encoding = null, Type? type = null)
		{
			return ReadAsString(filePath, encoding, type).DeserializeJson();
		}
		public static T? ReadAsJson<T>(string filePath, Encoding? encoding = null, Type? type = null) where T : JToken
		{
			return ReadAsString(filePath, encoding, type).DeserializeJson<T>();
		}
		public static void Write(string filePath, string text, Encoding? encoding = null)
		{
			Write(filePath, (encoding ?? new UTF8Encoding()).GetBytes(text));
		}
		public static void Write(string filePath, IEnumerable<byte> content)
		{
			Write(filePath, content.ToArray());
		}
		public static void Write(string filePath, byte[] content)
		{
			DirectoryUtils.Create(PathUtils.RemoveLastPart(filePath));
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(10, 2, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("写入文件：");
				handler.AppendFormatted(filePath);
				handler.AppendLiteral("（");
				handler.AppendFormatted(content.Length);
				handler.AppendLiteral(" 字节）");
			}
			Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
			Retrier.Attempt((int _) =>
			{
				File.WriteAllBytes(PathUtils.ForApi(filePath), content);
			}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
		}
		public static void Write(string filePath, Stream stream)
		{
			Retrier.Attempt((int _) =>
			{
				Delete(filePath);
				using FileStream destination = CreateAsStream(filePath);
				if (stream.CanSeek && stream.Position != 0L)
				{
					stream.Seek(0L, SeekOrigin.Begin);
				}
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(7, 3, out var isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("写入文件：");
					handler.AppendFormatted(filePath);
					handler.AppendLiteral("（");
					handler.AppendFormatted(stream.GetType().Name);
					handler.AppendFormatted(stream.CanSeek ? $" {stream.Length} 字节" : "");
					handler.AppendLiteral("）");
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				stream.CopyTo(destination);
			}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
		}
		public static FileStream CreateAsStream(string filePath)
		{
			DirectoryUtils.Create(PathUtils.RemoveLastPart(filePath));
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(6, 1, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("创建文件流：");
				handler.AppendFormatted(filePath);
			}
			Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
			return Retrier.Attempt((int _) =>
			{
				Delete(filePath);
				return new FileStream(PathUtils.ForApi(filePath), FileMode.Create);
			}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
		}
		public static string CreateRandom()
		{
			return Retrier.Attempt((int _) => Path.GetTempFileName(), 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
		}
		public static void ExtractResources(string logicalName, string localFilePath, Type type)
		{
			byte[] array = ReadAsBytes(logicalName, type);
			if (!Exists(localFilePath) || !(CryptographyUtils.ComputeFileHash(localFilePath) == CryptographyUtils.ComputeHash(array)))
			{
				Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.InfoLogLevel>(12, 2, out var isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("将资源写入到文件：");
					handler.AppendFormatted(logicalName);
					handler.AppendLiteral(" → ");
					handler.AppendFormatted(localFilePath);
				}
				Logger.Info(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				Write(localFilePath, array);
			}
		}
		public static void Copy(string sourceFilePath, string destFilePath)
		{
			sourceFilePath = PathUtils.ForCompare(sourceFilePath);
			destFilePath = PathUtils.ForCompare(destFilePath);
			bool isEnabled;
			if (string.CompareOrdinal(sourceFilePath, destFilePath) == 0)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(17, 2, out isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("复制文件到自身，不执行操作：");
					handler.AppendFormatted(sourceFilePath);
					handler.AppendLiteral(" → ");
					handler.AppendFormatted(destFilePath);
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				return;
			}
			if (string.Compare(sourceFilePath, destFilePath, StringComparison.OrdinalIgnoreCase) == 0)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(27, 2, out isEnabled);
				if (isEnabled)
				{
					handler2.AppendLiteral("复制文件到自身，但大小写不同，等效于重命名文件：");
					handler2.AppendFormatted(sourceFilePath);
					handler2.AppendLiteral(" → ");
					handler2.AppendFormatted(destFilePath);
				}
				Logger.Trace(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				Move(sourceFilePath, destFilePath);
				return;
			}
			DirectoryUtils.Create(PathUtils.RemoveLastPart(destFilePath));
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler3 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(8, 2, out isEnabled);
			if (isEnabled)
			{
				handler3.AppendLiteral("复制文件：");
				handler3.AppendFormatted(sourceFilePath);
				handler3.AppendLiteral(" → ");
				handler3.AppendFormatted(destFilePath);
			}
			Logger.Trace(ref handler3, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
			if (Exists(destFilePath))
			{
				SetReadOnly(destFilePath, readOnly: false);
			}
			Retrier.Attempt((int _) =>
			{
				File.Copy(PathUtils.ForApi(sourceFilePath), PathUtils.ForApi(destFilePath), overwrite: true);
			}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
		}
		public static void Move(string sourceFilePath, string destFilePath)
		{
			sourceFilePath = PathUtils.ForCompare(sourceFilePath);
			destFilePath = PathUtils.ForCompare(destFilePath);
			bool isEnabled;
			if (string.CompareOrdinal(sourceFilePath, destFilePath) == 0)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(17, 2, out isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("剪切文件到自身，不执行操作：");
					handler.AppendFormatted(sourceFilePath);
					handler.AppendLiteral(" → ");
					handler.AppendFormatted(destFilePath);
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				return;
			}
			if (string.Compare(sourceFilePath, destFilePath, StringComparison.OrdinalIgnoreCase) == 0)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(18, 2, out isEnabled);
				if (isEnabled)
				{
					handler2.AppendLiteral("剪切文件到自身，但大小写不同：");
					handler2.AppendFormatted(sourceFilePath);
					handler2.AppendLiteral(" → ");
					handler2.AppendFormatted(destFilePath);
				}
				Logger.Trace(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				Retrier.Attempt((int _) =>
				{
					string pathName = Path.Combine(PathUtils.RemoveLastPart(sourceFilePath), Path.GetRandomFileName());
					SetReadOnly(sourceFilePath, readOnly: false);
					if (Exists(destFilePath))
					{
						SetReadOnly(destFilePath, readOnly: false);
					}
					File.Move(PathUtils.ForApi(sourceFilePath), PathUtils.ForApi(pathName));
					File.Move(PathUtils.ForApi(pathName), PathUtils.ForApi(destFilePath));
				}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
				return;
			}
			DirectoryUtils.Create(PathUtils.RemoveLastPart(destFilePath));
			Delete(destFilePath);
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler3 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(8, 2, out isEnabled);
			if (isEnabled)
			{
				handler3.AppendLiteral("剪切文件：");
				handler3.AppendFormatted(sourceFilePath);
				handler3.AppendLiteral(" → ");
				handler3.AppendFormatted(destFilePath);
			}
			Logger.Trace(ref handler3, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
			Retrier.Attempt((int _) =>
			{
				SetReadOnly(sourceFilePath, readOnly: false);
				if (Exists(destFilePath))
				{
					SetReadOnly(destFilePath, readOnly: false);
				}
				File.Move(PathUtils.ForApi(sourceFilePath), PathUtils.ForApi(destFilePath));
			}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
		}
		public static bool? Delete(string filePath, bool toRecycleBin = false)
		{
			if (!Exists(filePath))
			{
				return null;
			}
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(1, 2, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendFormatted(toRecycleBin ? "将文件删除到回收站" : "删除文件");
				handler.AppendLiteral("：");
				handler.AppendFormatted(filePath);
			}
			Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
			if (toRecycleBin)
			{
				try
				{
					DeleteToRecycleBin(filePath);
					return true;
				}
				catch (Exception ex)
				{
					Logger.Warn(ex, "无法将文件删除到回收站，回退到永久删除：" + filePath, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				}
			}
			Retrier.Attempt((int _) =>
			{
				SetReadOnly(filePath, readOnly: false);
				File.Delete(PathUtils.ForApi(filePath));
			}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex2) => ex2 is IOException);
			return false;
		}
		internal static void DeleteToRecycleBin(string target)
		{
			if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
			{
				Retrier.Attempt((int _) =>
				{
					Run();
				}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
				return;
			}
			ExceptionDispatchInfo internalEx = null;
			Thread thread = new Thread(() =>
			{
				try
				{
					Retrier.Attempt((int _) =>
					{
						Run();
					}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => (ex is IOException || ex is COMException) ? true : false);
				}
				catch (Exception source)
				{
					internalEx = ExceptionDispatchInfo.Capture(source);
				}
			});
			thread.IsBackground = true;
			thread.Name = "DeleteToRecycleBin";
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			internalEx?.Throw();
			void Run()
			{
				IShellItem ppv = null;
				IFileOperation fileOperation = null;
				try
				{
					Guid riid = typeof(IShellItem).GUID;
					Marshal.ThrowExceptionForHR(SHCreateItemFromParsingName(PathUtils.RemoveExtendedPrefix(target), IntPtr.Zero, ref riid, out ppv));
					fileOperation = (IFileOperation)new FileOperation();
					fileOperation.SetOperationFlags(84u);
					fileOperation.DeleteItem(ppv, IntPtr.Zero);
					fileOperation.PerformOperations();
					fileOperation.GetAnyOperationsAborted(out var pfAnyOperationsAborted);
					if (pfAnyOperationsAborted)
					{
						throw new OperationCanceledException("Delete operation was aborted.");
					}
				}
				finally
				{
					if (fileOperation != null && Marshal.IsComObject(fileOperation))
					{
						Marshal.FinalReleaseComObject(fileOperation);
					}
					if (ppv != null && Marshal.IsComObject(ppv))
					{
						Marshal.FinalReleaseComObject(ppv);
					}
				}
			}
		}
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		private static extern int SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IntPtr pbc, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out IShellItem ppv);
		public static ZipArchive OpenZip(string zipFilePath, CancellationToken c = default(CancellationToken))
		{
			return Retrier.Attempt((int _) =>
			{
				try
				{
					try
					{
						return TryOpen(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true));
					}
					catch (DecoderFallbackException)
					{
						return TryOpen(Encoding.GetEncoding("GB18030"));
					}
				}
				catch (InvalidDataException innerException)
				{
					throw new InvalidDataException("文件不是压缩包，或者文件已损坏（" + zipFilePath + "）", innerException);
				}
			}, 10u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException, null, c);
			ZipArchive TryOpen(Encoding encoding)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(13, 2, out var isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("尝试以 ");
					handler.AppendFormatted(encoding.EncodingName);
					handler.AppendLiteral(" 编码打开压缩包：");
					handler.AppendFormatted(zipFilePath);
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				ZipArchive zipArchive = ZipFile.Open(PathUtils.ForApi(zipFilePath), ZipArchiveMode.Read, encoding);
				try
				{
					_ = zipArchive.Entries;
					return zipArchive;
				}
				catch
				{
					zipArchive.Dispose();
					throw;
				}
			}
		}
		public static void ExtractToDirectory(string compressionFile, string outputDirectory, CancellationToken c = default(CancellationToken), ProgressProvider? p = null)
		{
			compressionFile = PathUtils.ForApi(compressionFile);
			DirectoryUtils.Create(outputDirectory);
			bool isEnabled;
			if (compressionFile.EndsWithF(".gz", ignoreCase: true))
			{
				string text = Path.Combine(outputDirectory, PathUtils.GetFileNameWithoutExtension(compressionFile));
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(12, 2, out isEnabled);
				if (isEnabled)
				{
					handler.AppendLiteral("解压 gz 文件：");
					handler.AppendFormatted(compressionFile);
					handler.AppendLiteral(" → ");
					handler.AppendFormatted(text);
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				using Stream stream = ReadAsStream(compressionFile);
				using GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress);
				Write(text, stream2);
				p?.Finish();
				return;
			}
			using ZipArchive zipArchive = OpenZip(compressionFile, c);
			List<ZipArchiveEntry> list = zipArchive.Entries.Where((ZipArchiveEntry e) => !string.IsNullOrEmpty(e.Name)).ToList();
			int count = list.Count;
			int num = 0;
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler2 = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(19, 3, out isEnabled);
			if (isEnabled)
			{
				handler2.AppendLiteral("解压 zip 文件：");
				handler2.AppendFormatted(compressionFile);
				handler2.AppendLiteral(" → ");
				handler2.AppendFormatted(outputDirectory);
				handler2.AppendLiteral("（共 ");
				handler2.AppendFormatted(count);
				handler2.AppendLiteral(" 项）");
			}
			Logger.Trace(ref handler2, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
			foreach (ZipArchiveEntry item in list)
			{
				c.ThrowIfCancellationRequested();
				num++;
				if (count > 0)
				{
					p?.Set((double)num / (double)count);
				}
				string text2 = Path.Combine(outputDirectory, item.FullName);
				if (!PathUtils.IsParentOf(outputDirectory, text2))
				{
					throw new InvalidDataException("Zip 文件项 " + item.FullName + " 的路径在压缩包之外，这可能导致安全问题");
				}
				using Stream stream3 = item.Open();
				Write(text2, stream3);
			}
		}
		public static void CreateZipFromDirectory(string outputFullPath, string sourceDirectory)
		{
			DirectoryUtils.Create(PathUtils.RemoveLastPart(outputFullPath));
			Delete(outputFullPath);
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(22, 2, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("将文件夹中的内容压缩为 zip 文件：");
				handler.AppendFormatted(sourceDirectory);
				handler.AppendLiteral(" → ");
				handler.AppendFormatted(outputFullPath);
			}
			Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
			Retrier.Attempt((int _) =>
			{
				ZipFile.CreateFromDirectory(PathUtils.ForApi(sourceDirectory), PathUtils.ForApi(outputFullPath));
			}, 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
		}
		public static void CreateZipFromFiles(string outputFullPath, params string[] sourceFiles)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in sourceFiles)
			{
				string lastPart = PathUtils.GetLastPart(text);
				if (dictionary.ContainsKey(lastPart))
				{
					throw new ArgumentException("尝试将多个同文件名的文件放进压缩包中（" + lastPart + "）", "sourceFiles");
				}
				dictionary.Add(lastPart, text);
			}
			CreateZipFromFiles(outputFullPath, dictionary);
		}
		public static void CreateZipFromFiles(string outputFullPath, IDictionary<string, string> sources)
		{
			DirectoryUtils.Create(PathUtils.RemoveLastPart(outputFullPath));
			Delete(outputFullPath);
			using ZipArchive destination = Retrier.Attempt((int _) => ZipFile.Open(PathUtils.ForApi(outputFullPath), ZipArchiveMode.Create), 2u, (int _) => TimeSpan.FromMilliseconds(200.0), (Exception ex) => ex is IOException);
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(18, 3, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("创建 zip 文件：");
				handler.AppendFormatted(sources.Count);
				handler.AppendLiteral(" 个文件 → ");
				handler.AppendFormatted(outputFullPath);
				handler.AppendLiteral("\n");
				handler.AppendFormatted(sources.Select((KeyValuePair<string, string> p) => "- " + p.Value + " → " + p.Key).Join('\n'));
			}
			Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
			foreach (KeyValuePair<string, string> source in sources)
			{
				destination.CreateEntryFromFile(PathUtils.ForApi(source.Value), source.Key.Replace('\\', '/'), CompressionLevel.Optimal);
			}
		}
		public static bool Exists(string filePath)
		{
			return File.Exists(PathUtils.ForApi(filePath));
		}
		public static FileInfo GetInfo(string path)
		{
			return new FileInfo(PathUtils.ForApi(path));
		}
		public static void SetReadOnly(string path, bool readOnly)
		{
			path = PathUtils.ForApi(path);
			FileAttributes attributes = File.GetAttributes(path);
			FileAttributes fileAttributes = (readOnly ? (attributes | FileAttributes.ReadOnly) : (attributes & ~FileAttributes.ReadOnly));
			if (attributes != fileAttributes)
			{
				Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(16, 4, out var isEnabled);
				if (isEnabled)
				{
					handler.AppendFormatted(readOnly ? "添加" : "移除");
					handler.AppendLiteral("只读属性：");
					handler.AppendFormatted(path);
					handler.AppendLiteral("（原属性：");
					handler.AppendFormatted(attributes);
					handler.AppendLiteral("，新属性：");
					handler.AppendFormatted(fileAttributes);
					handler.AppendLiteral("）");
				}
				Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				File.SetAttributes(path, fileAttributes);
			}
		}
	}
}
