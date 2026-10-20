// MeloongCore

namespace MeloongCore
{
	public static class CryptographyUtils
	{
		public enum HashMethod
		{
			Md5,
			Sha1,
			Sha256,
			Sha512
		}
		private const byte AesDataVersion = 1;
		private const int AesIvSize = 16;
		private const int AesCheckSize = 4;
		private static HashAlgorithm GetHashAlgorithm(HashMethod method)
		{
			return method switch
			{
				HashMethod.Md5 => (HashAlgorithm)MD5.Create(), 
				HashMethod.Sha1 => SHA1.Create(), 
				HashMethod.Sha256 => SHA256.Create(), 
				HashMethod.Sha512 => SHA512.Create(), 
				_ => throw new ArgumentOutOfRangeException("method"), 
			};
		}
		public static string ComputeFileHash(string filePath, HashMethod method = HashMethod.Md5)
		{
			using HashAlgorithm hashAlgorithm = GetHashAlgorithm(method);
			Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel> handler = new Logger.LogInterpolatedStringHandler<Logger.TraceLogLevel>(6, 2, out var isEnabled);
			if (isEnabled)
			{
				handler.AppendLiteral("计算文件 ");
				handler.AppendFormatted(method);
				handler.AppendLiteral("：");
				handler.AppendFormatted(filePath);
			}
			Logger.Trace(ref handler, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\MeloongCore\\Shared\\Utils\\CryptographyUtils.cs");
			using Stream inputStream = FileUtils.ReadAsStream(filePath);
			return BitConverter.ToString(hashAlgorithm.ComputeHash(inputStream)).Replace("-", "").Lower();
		}
		public static string ComputeHash(byte[] input, HashMethod method = HashMethod.Md5)
		{
			using HashAlgorithm hashAlgorithm = GetHashAlgorithm(method);
			return BitConverter.ToString(hashAlgorithm.ComputeHash(input)).Replace("-", "").Lower();
		}
		public static string ComputeHash(string input, HashMethod method = HashMethod.Md5)
		{
			return ComputeHash(Encoding.UTF8.GetBytes(input), method);
		}
		public static string? AesEncrypt(string? sourceString, string key = "EncryptKey")
		{
			if (string.IsNullOrEmpty(sourceString))
			{
				return sourceString;
			}
			byte[] array = new byte[16];
			using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(array);
			using SHA256 sHA = SHA256.Create();
			byte[] rgbKey = sHA.ComputeHash(Encoding.UTF8.GetBytes(key));
			byte[] key2 = sHA.ComputeHash(Encoding.UTF8.GetBytes("AES-HMAC|" + key));
			using Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			using ICryptoTransform cryptoTransform = aes.CreateEncryptor(rgbKey, array);
			byte[] bytes = Encoding.UTF8.GetBytes(sourceString);
			byte[] array2 = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			byte[] array3 = new byte[1 + array.Length + array2.Length + 4];
			array3[0] = 1;
			Array.Copy(array, 0, array3, 1, array.Length);
			Array.Copy(array2, 0, array3, 1 + array.Length, array2.Length);
			using HMACSHA256 hMACSHA = new HMACSHA256(key2);
			Array.Copy(hMACSHA.ComputeHash(array3, 0, 1 + array.Length + array2.Length), 0, array3, 1 + array.Length + array2.Length, 4);
			return Convert.ToBase64String(array3);
		}
		public static string? AesDecrypt(string? encryptedString, string key = "EncryptKey")
		{
			if (string.IsNullOrEmpty(encryptedString))
			{
				return encryptedString;
			}
			byte[] array;
			try
			{
				array = Convert.FromBase64String(encryptedString);
			}
			catch (FormatException inner)
			{
				throw new CryptographicException("数据不是一个有效的 Base64 字符串（" + encryptedString + "）", inner);
			}
			if (array.Length == 0)
			{
				throw new CryptographicException("对称加密数据长度有误（" + encryptedString + "）");
			}
			int num = array.Length - 1 - 16 - 4;
			if (array[0] != 1 || num < 16 || num % 16 != 0)
			{
				throw new CryptographicException("对称加密数据长度有误（" + encryptedString + "）");
			}
			using SHA256 sHA = SHA256.Create();
			using HMACSHA256 hMACSHA = new HMACSHA256(sHA.ComputeHash(Encoding.UTF8.GetBytes("AES-HMAC|" + key)));
			byte[] array2 = hMACSHA.ComputeHash(array, 0, 17 + num);
			int num2 = 17 + num;
			int num3 = 0;
			for (int i = 0; i < 4; i++)
			{
				num3 |= array[num2 + i] ^ array2[i];
			}
			if (num3 != 0)
			{
				throw new CryptographicException("对称加密数据校验失败（" + encryptedString + "）");
			}
			byte[] rgbKey = sHA.ComputeHash(Encoding.UTF8.GetBytes(key));
			byte[] array3 = new byte[16];
			Array.Copy(array, 1, array3, 0, array3.Length);
			using Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			using ICryptoTransform cryptoTransform = aes.CreateDecryptor(rgbKey, array3);
			byte[] bytes = cryptoTransform.TransformFinalBlock(array, 1 + array3.Length, num);
			return Encoding.UTF8.GetString(bytes);
		}
		public static void EcdsaVerify(string sourceString, string sign, string publicKey)
		{
			using HashAlgorithm hashAlgorithm = GetHashAlgorithm(HashMethod.Sha256);
			byte[] array = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(sourceString));
			IntPtr algorithmHandle = IntPtr.Zero;
			IntPtr keyHandle = IntPtr.Zero;
			try
			{
				int num = BCryptOpenAlgorithmProvider(out algorithmHandle, "ECDSA_P256", null, 0);
				if (num < 0)
				{
					throw new CryptographicException(string.Format("{0} 失败，错误码 {1}", "BCryptOpenAlgorithmProvider", num));
				}
				num = BCryptImportKeyPair(algorithmHandle, IntPtr.Zero, "ECCPUBLICBLOB", out keyHandle, Convert.FromBase64String(publicKey), 72, 0);
				if (num < 0)
				{
					throw new CryptographicException(string.Format("{0} 失败，错误码 {1}", "BCryptImportKeyPair", num));
				}
				num = BCryptVerifySignature(keyHandle, IntPtr.Zero, array, array.Length, Convert.FromBase64String(sign), 64, 0);
				if (num == -1073700864)
				{
					throw new SecurityException("签名验证失败");
				}
				if (num < 0)
				{
					throw new CryptographicException(string.Format("{0} 失败，错误码 {1}", "BCryptVerifySignature", num));
				}
			}
			finally
			{
				if (keyHandle != IntPtr.Zero)
				{
					BCryptDestroyKey(keyHandle);
				}
				if (algorithmHandle != IntPtr.Zero)
				{
					BCryptCloseAlgorithmProvider(algorithmHandle, 0);
				}
			}
		}
		public static string EcdsaSign(string sourceString, string privateKey)
		{
			using HashAlgorithm hashAlgorithm = GetHashAlgorithm(HashMethod.Sha256);
			byte[] array = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(sourceString));
			byte[] array2 = Convert.FromBase64String(privateKey);
			IntPtr algorithmHandle = IntPtr.Zero;
			IntPtr keyHandle = IntPtr.Zero;
			try
			{
				int num = BCryptOpenAlgorithmProvider(out algorithmHandle, "ECDSA_P256", null, 0);
				if (num < 0)
				{
					throw new CryptographicException(string.Format("{0} 失败，错误码 {1}", "BCryptOpenAlgorithmProvider", num));
				}
				num = BCryptImportKeyPair(algorithmHandle, IntPtr.Zero, "ECCPRIVATEBLOB", out keyHandle, array2, array2.Length, 0);
				if (num < 0)
				{
					throw new CryptographicException(string.Format("{0} 失败，错误码 {1}", "BCryptImportKeyPair", num));
				}
				num = BCryptSignHash(keyHandle, IntPtr.Zero, array, array.Length, null, 0, out var resultSize, 0);
				if (num < 0)
				{
					throw new CryptographicException(string.Format("{0} 失败，错误码 {1}", "BCryptSignHash", num));
				}
				byte[] array3 = new byte[resultSize];
				num = BCryptSignHash(keyHandle, IntPtr.Zero, array, array.Length, array3, array3.Length, out var resultSize2, 0);
				if (num < 0)
				{
					throw new CryptographicException(string.Format("{0} 失败，错误码 {1}", "BCryptSignHash", num));
				}
				if (resultSize2 != array3.Length)
				{
					Array.Resize(ref array3, resultSize2);
				}
				return Convert.ToBase64String(array3);
			}
			finally
			{
				if (keyHandle != IntPtr.Zero)
				{
					BCryptDestroyKey(keyHandle);
				}
				if (algorithmHandle != IntPtr.Zero)
				{
					BCryptCloseAlgorithmProvider(algorithmHandle, 0);
				}
			}
		}
		[DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
		private static extern int BCryptOpenAlgorithmProvider(out IntPtr algorithmHandle, string algorithmId, string? implementation, int flags);
		[DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
		private static extern int BCryptImportKeyPair(IntPtr algorithmHandle, IntPtr importKey, string blobType, out IntPtr keyHandle, byte[] input, int inputSize, int flags);
		[DllImport("bcrypt.dll")]
		private static extern int BCryptVerifySignature(IntPtr keyHandle, IntPtr paddingInfo, byte[] hash, int hashSize, byte[] signature, int signatureSize, int flags);
		[DllImport("bcrypt.dll")]
		private static extern int BCryptDestroyKey(IntPtr keyHandle);
		[DllImport("bcrypt.dll")]
		private static extern int BCryptCloseAlgorithmProvider(IntPtr algorithmHandle, int flags);
		[DllImport("bcrypt.dll")]
		private static extern int BCryptSignHash(IntPtr keyHandle, IntPtr paddingInfo, byte[] hash, int hashSize, byte[]? signature, int signatureSize, out int resultSize, int flags);
	}
}
