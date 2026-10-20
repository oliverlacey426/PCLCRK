// PCL

namespace PCL
{
	public class MyImage : System.Windows.Controls.Image
	{
		public TimeSpan FileCacheExpiredTime;
		public static readonly DependencyProperty EnableCacheProperty = DependencyProperty.Register("EnableCache", typeof(bool), typeof(MyImage), new PropertyMetadata(true));
		private string _Source;
		public new static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(MyImage), new PropertyMetadata([SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
		{
			if (sender != null)
			{
				((MyImage)sender).Source = e.NewValue.ToString();
			}
		}));
		private string _FallbackSource;
		private string _LoadingSource;
		private string _ActualSource;
		public bool EnableCache
		{
			get
			{
				return Conversions.ToBoolean(GetValue(EnableCacheProperty));
			}
			set
			{
				SetValue(EnableCacheProperty, value);
			}
		}
		public new string Source
		{
			get
			{
				return _Source;
			}
			set
			{
				if (Operators.CompareString(value, "", TextCompare: false) == 0)
				{
					value = null;
				}
				if (Operators.CompareString(_Source, value, TextCompare: false) != 0)
				{
					_Source = value;
					if (IsInitialized)
					{
						Load();
					}
				}
			}
		}
		public string FallbackSource
		{
			get
			{
				return _FallbackSource;
			}
			set
			{
				_FallbackSource = value;
			}
		}
		public string LoadingSource
		{
			get
			{
				return _LoadingSource;
			}
			set
			{
				_LoadingSource = value;
			}
		}
		public string ActualSource
		{
			get
			{
				return _ActualSource;
			}
			set
			{
				if (Operators.CompareString(value, "", TextCompare: false) == 0)
				{
					value = null;
				}
				if (Operators.CompareString(_ActualSource, value, TextCompare: false) == 0)
				{
					return;
				}
				_ActualSource = value;
				try
				{
					ImageSource source = ((value == null) ? null : new MyBitmap(value));
					ModBase.RunInUi([SpecialName] () =>
					{
						base.Source = source;
					});
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, $"加载图片失败（{value}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyImage.vb");
					try
					{
						if (value.StartsWithF(ModBase.PathTemp))
						{
							FileUtils.Delete(value);
						}
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
					ProjectData.ClearProjectError();
				}
			}
		}
		public MyImage()
		{
			Initialized += [SpecialName] (object a0, EventArgs a1) =>
			{
				Load();
			};
			FileCacheExpiredTime = new TimeSpan(7, 0, 0, 0);
			_Source = "";
			_FallbackSource = null;
			_LoadingSource = "pack://application:,,,/images/Icons/NoIcon.png";
			_ActualSource = null;
		}
		private void Load()
		{
			if (Source == null)
			{
				ActualSource = null;
				return;
			}
			if (!Source.StartsWithF("http"))
			{
				ActualSource = Source;
				return;
			}
			bool enableCache = EnableCache;
			string text = string.Format("{0}MyImage\\{1}.png{2}", ModBase.PathTemp, Source.GetStableHashCode(), RuntimeHelpers.GetObjectValue(enableCache ? "" : ((object)ModBase.GetUuid())));
			FileInfo info = FileUtils.GetInfo(text);
			if (enableCache && info.Exists)
			{
				ActualSource = text;
				if (DateTime.Now - info.LastWriteTime < FileCacheExpiredTime)
				{
					return;
				}
			}
			else
			{
				ActualSource = LoadingSource;
			}
			ModBase.RunInNewThread([SpecialName] () =>
			{
				bool flag = !string.IsNullOrEmpty(FallbackSource) && !FallbackSource.StartsWithF("http");
				try
				{
					ModNet.NetDownloadByLoader((FallbackSource == null || !FallbackSource.StartsWithF("http") || Operators.CompareString(Source, FallbackSource, TextCompare: false) == 0) ? new string[1] { Source } : new string[2] { Source, FallbackSource }, text, null, null, SimulateBrowserHeaders: true);
					ActualSource = text;
					if (!enableCache)
					{
						FileUtils.Delete(text);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					try
					{
						if (text != null)
						{
							FileUtils.Delete(text);
						}
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
					if (flag)
					{
						try
						{
							ActualSource = FallbackSource;
							Logger.Warn(ex2, $"下载图片失败，使用本地备用图片（{Source}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyImage.vb");
							ProjectData.ClearProjectError();
							return;
						}
						catch (Exception ex3)
						{
							ProjectData.SetProjectError(ex3);
							Exception ex4 = ex3;
							Logger.Error(ex2, $"下载图片失败（{Source}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyImage.vb");
							Logger.Error(ex4, $"加载备用图片失败（{FallbackSource}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyImage.vb");
							ProjectData.ClearProjectError();
						}
					}
					else
					{
						Logger.Error(ex2, $"下载图片失败（{Source}，备用：{FallbackSource}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyImage.vb");
					}
					ProjectData.ClearProjectError();
				}
			}, "MyImage PicLoader " + Conversions.ToString(ModBase.GetUuid()) + "#", ThreadPriority.BelowNormal);
		}
	}
}
