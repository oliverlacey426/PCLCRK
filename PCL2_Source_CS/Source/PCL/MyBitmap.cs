// PCL

namespace PCL
{
	public class MyBitmap
	{
		private class WebPDecoder
		{
			public static Bitmap DecodeFromBytes(byte[] Bytes)
			{
				return new SimpleDecoder().DecodeFromBytes(Bytes, Bytes.Length);
			}
		}
		public Bitmap Pic;
		[SpecialName]
		private ConcurrentDictionary<string, MyBitmap> _STATIC_ctor_2011E_Cache;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_ctor_2011E_Cache_Init;
		public static implicit operator MyBitmap(System.Drawing.Image Image)
		{
			if (Image == null)
			{
				return null;
			}
			return new MyBitmap(Image);
		}
		public static implicit operator System.Drawing.Image(MyBitmap Image)
		{
			return Image?.Pic;
		}
		public static implicit operator MyBitmap(ImageSource Image)
		{
			if (Image == null)
			{
				return null;
			}
			return new MyBitmap(Image);
		}
		public static implicit operator ImageSource(MyBitmap Image)
		{
			if (Image == null)
			{
				return null;
			}
			Bitmap pic = Image.Pic;
			System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, pic.Width, pic.Height);
			BitmapData bitmapData = pic.LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			try
			{
				BitmapSource bitmapSource = BitmapSource.Create(pic.Width, pic.Height, pic.HorizontalResolution, pic.VerticalResolution, PixelFormats.Bgra32, null, bitmapData.Scan0, checked(rect.Width * rect.Height * 4), bitmapData.Stride);
				bitmapSource.Freeze();
				return bitmapSource;
			}
			finally
			{
				pic.UnlockBits(bitmapData);
			}
		}
		public static implicit operator MyBitmap(Bitmap Image)
		{
			if (Image == null)
			{
				return null;
			}
			return new MyBitmap(Image);
		}
		public static implicit operator Bitmap(MyBitmap Image)
		{
			return Image?.Pic;
		}
		public static implicit operator MyBitmap(ImageBrush Image)
		{
			if (Image == null)
			{
				return null;
			}
			return new MyBitmap(Image);
		}
		public static implicit operator ImageBrush(MyBitmap Image)
		{
			if (Image == null)
			{
				return null;
			}
			return new ImageBrush(new MyBitmap(Image.Pic));
		}
		public MyBitmap()
		{
		}
		public MyBitmap(string FilePathOrResourceName)
		{
			try
			{
				FilePathOrResourceName = FilePathOrResourceName.Replace("pack://application:,,,/images/", ModBase.PathImage);
				if (FilePathOrResourceName.StartsWithF(ModBase.PathImage))
				{
					if (_STATIC_ctor_2011E_Cache_Init == null)
					{
						Interlocked.CompareExchange(ref _STATIC_ctor_2011E_Cache_Init, new StaticLocalInitFlag(), null);
					}
					bool lockTaken = false;
					try
					{
						Monitor.Enter(_STATIC_ctor_2011E_Cache_Init, ref lockTaken);
						if (_STATIC_ctor_2011E_Cache_Init.State == 0)
						{
							_STATIC_ctor_2011E_Cache_Init.State = 2;
							_STATIC_ctor_2011E_Cache = new ConcurrentDictionary<string, MyBitmap>();
						}
						else if (_STATIC_ctor_2011E_Cache_Init.State == 2)
						{
							throw new IncompleteInitialization();
						}
					}
					finally
					{
						_STATIC_ctor_2011E_Cache_Init.State = 1;
						if (lockTaken)
						{
							Monitor.Exit(_STATIC_ctor_2011E_Cache_Init);
						}
					}
					if (_STATIC_ctor_2011E_Cache.ContainsKey(FilePathOrResourceName))
					{
						Pic = _STATIC_ctor_2011E_Cache[FilePathOrResourceName].Pic;
						return;
					}
					Pic = new MyBitmap((ImageSource)new ImageSourceConverter().ConvertFromString(FilePathOrResourceName));
					_STATIC_ctor_2011E_Cache.TryAdd(FilePathOrResourceName, Pic);
					return;
				}
				using Stream stream = FileUtils.ReadAsStream(FilePathOrResourceName);
				byte[] array = new byte[2];
				stream.Read(array, 0, 2);
				stream.Seek(0L, SeekOrigin.Begin);
				if (array[0] == 82 && array[1] == 73)
				{
					byte[] array2 = new byte[checked((int)(stream.Length - 1) + 1)];
					stream.Read(array2, 0, array2.Length);
					Pic = WebPDecoder.DecodeFromBytes(array2);
				}
				else
				{
					Pic = new Bitmap(stream);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Pic = (Bitmap)MyWpfExtension.Application.TryFindResource(FilePathOrResourceName);
				if (Pic == null)
				{
					Pic = new Bitmap(1, 1);
					if (ex2 is ArgumentException)
					{
						throw new Exception($"图片格式不支持，或图片文件损坏（{FilePathOrResourceName}）", ex2);
					}
					throw new Exception($"加载 MyBitmap 意外失败（{FilePathOrResourceName}）", ex2);
				}
				Logger.Warn(ex2, $"指定类型有误的 MyBitmap 加载（{FilePathOrResourceName}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\MyBitmap.vb");
				ProjectData.ClearProjectError();
			}
		}
		public MyBitmap(ImageSource Image)
		{
			using MemoryStream stream = new MemoryStream();
			PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
			pngBitmapEncoder.Frames.Add(BitmapFrame.Create((BitmapSource)Image));
			pngBitmapEncoder.Save(stream);
			Pic = new Bitmap(stream);
		}
		public MyBitmap(System.Drawing.Image Image)
		{
			Pic = (Bitmap)Image;
		}
		public MyBitmap(Bitmap Image)
		{
			Pic = Image;
		}
		public MyBitmap(ImageBrush Image)
		{
			using MemoryStream stream = new MemoryStream();
			BmpBitmapEncoder bmpBitmapEncoder = new BmpBitmapEncoder();
			bmpBitmapEncoder.Frames.Add(BitmapFrame.Create((BitmapSource)Image.ImageSource));
			bmpBitmapEncoder.Save(stream);
			Pic = new Bitmap(stream);
		}
		public MyBitmap Clip(int X, int Y, int Width, int Height)
		{
			Bitmap bitmap = new Bitmap(Width, Height, Pic.PixelFormat);
			bitmap.SetResolution(Pic.HorizontalResolution, Pic.VerticalResolution);
			checked
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
					graphics.TranslateTransform(-X, -Y);
					graphics.DrawImage(Pic, new System.Drawing.Rectangle(0, 0, Pic.Width, Pic.Height));
				}
				return bitmap;
			}
		}
		public MyBitmap RotateFlip(RotateFlipType Type)
		{
			Bitmap bitmap = new Bitmap(Pic);
			bitmap.SetResolution(Pic.HorizontalResolution, Pic.VerticalResolution);
			bitmap.RotateFlip(Type);
			return bitmap;
		}
		public void Save(string FilePath)
		{
			BitmapEncoder bitmapEncoder = new PngBitmapEncoder();
			bitmapEncoder.Frames.Add(BitmapFrame.Create((BitmapSource)(ImageSource)this));
			using FileStream stream = FileUtils.CreateAsStream(FilePath);
			bitmapEncoder.Save(stream);
		}
	}
}
