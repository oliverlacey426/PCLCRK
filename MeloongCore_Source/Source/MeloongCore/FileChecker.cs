// MeloongCore

namespace MeloongCore
{
	public class FileChecker
	{
		public long ActualSize = -1L;
		public long MinSize = -1L;
		public string? Hash;
		public bool IsJson;
		public string? Check(string localPath)
		{
			try
			{
				FileInfo info = FileUtils.GetInfo(localPath);
				if (!info.Exists)
				{
					return "文件不存在：" + localPath;
				}
				long length = info.Length;
				if (ActualSize >= 0 && ActualSize != length)
				{
					return $"文件大小应为 {ActualSize} B，实际为 {length} B" + ((length < 2000) ? ("，内容为：" + FileUtils.ReadAsString(localPath)) : "");
				}
				if (MinSize >= 0 && MinSize > length)
				{
					return $"文件大小应至少为 {MinSize} B，实际为 {length} B" + ((length < 2000) ? ("，内容为：" + FileUtils.ReadAsString(localPath)) : "");
				}
				string hash = Hash;
				if (!string.IsNullOrEmpty(hash))
				{
					hash = hash.Lower();
					switch (hash.Length)
					{
					case 32:
					{
						string text4 = CryptographyUtils.ComputeFileHash(localPath);
						if (hash != text4)
						{
							return "文件 MD5 应为 " + hash + "，实际为 " + text4;
						}
						break;
					}
					case 40:
					{
						string text2 = CryptographyUtils.ComputeFileHash(localPath, CryptographyUtils.HashMethod.Sha1);
						if (hash != text2)
						{
							return "文件 SHA1 应为 " + hash + "，实际为 " + text2;
						}
						break;
					}
					case 64:
					{
						string text3 = CryptographyUtils.ComputeFileHash(localPath, CryptographyUtils.HashMethod.Sha256);
						if (hash != text3)
						{
							return "文件 SHA256 应为 " + hash + "，实际为 " + text3;
						}
						break;
					}
					case 128:
					{
						string text = CryptographyUtils.ComputeFileHash(localPath, CryptographyUtils.HashMethod.Sha512);
						if (hash != text)
						{
							return "文件 SHA512 应为 " + hash + "，实际为 " + text;
						}
						break;
					}
					default:
						return $"无法识别的 Hash 类型（长度为 {hash.Length}）：{hash}";
					}
				}
				if (IsJson)
				{
					string text5 = FileUtils.ReadAsString(localPath);
					if (text5 == "")
					{
						return "读取到的文件为空";
					}
					text5.DeserializeJson();
				}
				return null;
			}
			catch (Exception ex)
			{
				Logger.Warn(ex, "检查文件出错", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\FileUtils.cs");
				return ex.GetDisplay(multiline: false);
			}
		}
	}
}
