// PCL

namespace PCL
{
	[StandardModule]
	public static class ModMusic
	{
		public enum MusicStates
		{
			Stop,
			Play,
			Pause
		}
		[CompilerGenerated]
		internal sealed class _Closure___17_0
		{
			public double _VB_Local_Percent;
			public _Closure___17_0(_Closure___17_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Percent = arg0._VB_Local_Percent;
				}
			}
			[SpecialName]
			internal void _Lambda___0()
			{
				ModMain.FrmMain.BtnExtraMusic.Progress = _VB_Local_Percent;
			}
		}
		public static List<string> MusicWaitingList = null;
		public static List<string> MusicAllList = null;
		public static object MusicNAudio = null;
		private static string MusicCurrent = "";
		public static MusicStates MusicState
		{
			get
			{
				if (MusicNAudio == null)
				{
					return MusicStates.Stop;
				}
				object left = NewLateBinding.LateGet(MusicNAudio, null, "PlaybackState", new object[0], null, null, null);
				if (Operators.ConditionalCompareObjectEqual(left, 0, TextCompare: false))
				{
					return MusicStates.Stop;
				}
				if (Operators.ConditionalCompareObjectEqual(left, 2, TextCompare: false))
				{
					return MusicStates.Pause;
				}
				return MusicStates.Play;
			}
		}
		private static void MusicListInit(bool ForceReload, string PreventFirst = null)
		{
			if (ForceReload)
			{
				MusicAllList = null;
			}
			try
			{
				if (MusicAllList == null)
				{
					MusicAllList = new List<string>();
					DirectoryUtils.Create(Paths.Base + "PCL\\Musics\\");
					foreach (string item in DirectoryUtils.EnumerateFiles(Paths.Base + "PCL\\Musics\\", includeSubDirectories: true))
					{
						if (!new string[7] { "ini", "jpg", "txt", "cfg", "lrc", "db", "png" }.Contains(PathUtils.GetExtension(item)))
						{
							MusicAllList.Add(item);
						}
					}
				}
				MusicWaitingList = (Settings.Get<bool>("UiMusicRandom") ? ModBase.Shuffle(new List<string>(MusicAllList)).ToList() : new List<string>(MusicAllList));
				if (PreventFirst != null && Operators.CompareString(MusicWaitingList.FirstOrDefault(), PreventFirst, TextCompare: false) == 0)
				{
					MusicWaitingList.RemoveAt(0);
					MusicWaitingList.Add(PreventFirst);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "初始化音乐列表失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
				ProjectData.ClearProjectError();
			}
		}
		private static string DequeueNextMusicAddress()
		{
			if (MusicAllList == null || !MusicAllList.Any() || !MusicWaitingList.Any())
			{
				MusicListInit(ForceReload: false);
			}
			string text;
			if (MusicWaitingList.Any())
			{
				text = MusicWaitingList[0];
				MusicWaitingList.RemoveAt(0);
			}
			else
			{
				text = null;
			}
			if (!MusicWaitingList.Any())
			{
				MusicListInit(ForceReload: false, text);
			}
			return text;
		}
		private static void MusicRefreshUI()
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				try
				{
					if (!MusicAllList.Any())
					{
						ModMain.FrmMain.BtnExtraMusic.Show = false;
					}
					else
					{
						ModMain.FrmMain.BtnExtraMusic.Show = true;
						string text;
						if (MusicState == MusicStates.Pause)
						{
							ModMain.FrmMain.BtnExtraMusic.Logo = "M803.904 463.936a55.168 55.168 0 0 1 0 96.128l-463.616 264.448C302.848 845.888 256 819.136 256 776.448V247.616c0-42.752 46.848-69.44 84.288-48.064l463.616 264.384z";
							ModMain.FrmMain.BtnExtraMusic.LogoScale = 0.8;
							text = "已暂停：" + PathUtils.GetFileNameWithoutExtension(MusicCurrent);
							text = ((MusicAllList.Count <= 1) ? (text + "\r\n左键恢复播放，右键重新从头播放。") : (text + "\r\n左键恢复播放，右键播放下一曲。"));
						}
						else
						{
							ModMain.FrmMain.BtnExtraMusic.Logo = "M348.293565 716.53287V254.797913c0-41.672348 28.004174-78.358261 68.919652-90.37913L815.994435 40.826435c62.775652-18.610087 125.907478 26.579478 125.907478 89.933913v539.158261c8.013913 42.25113-8.94887 89.177043-47.014956 127.109565a232.848696 232.848696 0 0 1-170.785392 65.758609c-61.885217-2.938435-111.081739-33.435826-129.113043-80.050087-18.031304-46.614261-2.137043-102.177391 41.672348-145.853218a232.848696 232.848696 0 0 1 170.785391-65.80313c21.014261 1.024 40.514783 5.164522 57.878261 12.065391V233.338435c0-12.109913-10.551652-20.034783-20.569044-20.034783a24.620522 24.620522 0 0 0-5.787826 0.934957L439.785739 338.18713a19.545043 19.545043 0 0 0-14.825739 19.144348v438.984348H423.846957c11.53113 43.987478-5.164522 94.208-45.412174 134.322087a232.848696 232.848696 0 0 1-170.785392 65.758609c-61.885217-2.938435-111.081739-33.435826-129.113043-80.050087-18.031304-46.614261-2.137043-102.177391 41.672348-145.853218a232.848696 232.848696 0 0 1 170.785391-65.80313c20.791652 1.024 40.069565 5.075478 57.299478 11.842783z";
							ModMain.FrmMain.BtnExtraMusic.LogoScale = 1.0;
							text = "正在播放：" + PathUtils.GetFileNameWithoutExtension(MusicCurrent);
							text = ((MusicAllList.Count <= 1) ? (text + "\r\n左键暂停，右键重新从头播放。") : (text + "\r\n左键暂停，右键播放下一曲。"));
						}
						ModMain.FrmMain.BtnExtraMusic.ToolTip = text;
					}
					if (ModMain.FrmSetupUI != null)
					{
						ModMain.FrmSetupUI.MusicRefreshUI();
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "刷新背景音乐 UI 失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
					ProjectData.ClearProjectError();
				}
			});
		}
		public static void MusicControlPause()
		{
			if (MusicNAudio == null)
			{
				ModMain.Hint("音乐播放尚未开始！", ModMain.HintType.Red);
				return;
			}
			switch (MusicState)
			{
			case MusicStates.Pause:
				MusicResume();
				break;
			case MusicStates.Play:
				MusicPause();
				break;
			default:
				Logger.Warn("音乐目前为停止状态，已强制尝试开始播放", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
				MusicRefreshPlay(ShowHint: false);
				break;
			}
		}
		public static void MusicControlNext()
		{
			if (MusicAllList.IsSingle())
			{
				MusicStartPlay(MusicCurrent);
				ModMain.Hint("重新播放：" + PathUtils.GetLastPart(MusicCurrent), ModMain.HintType.Green);
			}
			else
			{
				string text = DequeueNextMusicAddress();
				if (text == null)
				{
					ModMain.Hint("没有可以播放的音乐！", ModMain.HintType.Red);
				}
				else
				{
					MusicStartPlay(text);
					ModMain.Hint("正在播放：" + PathUtils.GetLastPart(text), ModMain.HintType.Green);
				}
			}
			MusicRefreshUI();
		}
		public static void MusicRefreshPlay(bool ShowHint, bool IsFirstLoad = false)
		{
			try
			{
				MusicListInit(ForceReload: true);
				if (!MusicAllList.Any())
				{
					if (MusicNAudio == null)
					{
						if (ShowHint)
						{
							ModMain.Hint("未检测到可用的背景音乐！", ModMain.HintType.Red);
						}
					}
					else
					{
						MusicNAudio = null;
						if (ShowHint)
						{
							ModMain.Hint("背景音乐已清除！", ModMain.HintType.Green);
						}
					}
				}
				else
				{
					string text = DequeueNextMusicAddress();
					if (text == null)
					{
						if (ShowHint)
						{
							ModMain.Hint("没有可以播放的音乐！", ModMain.HintType.Red);
						}
					}
					else
					{
						try
						{
							MusicStartPlay(text, IsFirstLoad);
							if (ShowHint)
							{
								ModMain.Hint("背景音乐已刷新：" + PathUtils.GetLastPart(text), ModMain.HintType.Green, Log: false);
							}
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ProjectData.ClearProjectError();
						}
					}
				}
				MusicRefreshUI();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "刷新背景音乐播放失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
				ProjectData.ClearProjectError();
			}
		}
		private static void MusicStartPlay(string Address, bool IsFirstLoad = false)
		{
			if (Address != null)
			{
				Logger.Info($"播放开始：{Address}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
				MusicCurrent = Address;
				ModBase.RunInNewThread([SpecialName] () =>
				{
					MusicLoop(IsFirstLoad);
				}, "Music", ThreadPriority.BelowNormal);
			}
		}
		public static bool MusicPause()
		{
			if (MusicState == MusicStates.Play)
			{
				ModBase.RunInThread([SpecialName] () =>
				{
					Logger.Info("已暂停播放", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
					object musicNAudio = MusicNAudio;
					if (musicNAudio != null)
					{
						NewLateBinding.LateCall(musicNAudio, null, "Pause", new object[0], null, null, null, IgnoreReturn: true);
					}
					MusicRefreshUI();
				});
				return true;
			}
			Logger.Info($"无需暂停播放，当前状态为 {MusicState}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
			return false;
		}
		public static bool MusicResume()
		{
			if (MusicState == MusicStates.Play || !MusicAllList.Any())
			{
				Logger.Info($"无需继续播放，当前状态为 {MusicState}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
				return false;
			}
			ModBase.RunInThread([SpecialName] () =>
			{
				Logger.Info("已恢复播放", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
				try
				{
					object musicNAudio = MusicNAudio;
					if (musicNAudio != null)
					{
						NewLateBinding.LateCall(musicNAudio, null, "Play", new object[0], null, null, null, IgnoreReturn: true);
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					object musicNAudio2 = MusicNAudio;
					if (musicNAudio2 != null)
					{
						NewLateBinding.LateCall(musicNAudio2, null, "Stop", new object[0], null, null, null, IgnoreReturn: true);
					}
					object musicNAudio3 = MusicNAudio;
					if (musicNAudio3 != null)
					{
						NewLateBinding.LateCall(musicNAudio3, null, "Play", new object[0], null, null, null, IgnoreReturn: true);
					}
					ProjectData.ClearProjectError();
				}
				MusicRefreshUI();
			});
			return true;
		}
		private static void MusicLoop(bool IsFirstLoad = false)
		{
			WaveOutEvent waveOutEvent = null;
			WaveStream waveStream = null;
			try
			{
				waveOutEvent = (WaveOutEvent)(MusicNAudio = new WaveOutEvent());
				waveOutEvent.DeviceNumber = -1;
				try
				{
					waveStream = new AudioFileReader(MusicCurrent);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "使用 AudioFileReader 加载音频文件失败，换用 MediaFoundationReader 重试", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
					waveStream = new MediaFoundationReader(MusicCurrent);
					ProjectData.ClearProjectError();
				}
				waveOutEvent.Init(waveStream);
				waveOutEvent.Play();
				if (IsFirstLoad && !Settings.Get<bool>("UiMusicAuto"))
				{
					waveOutEvent.Pause();
				}
				MusicRefreshUI();
				int num = 0;
				_Closure___17_0 obj = default;
				while (waveOutEvent.Equals(RuntimeHelpers.GetObjectValue(MusicNAudio)) && waveOutEvent.PlaybackState != PlaybackState.Stopped)
				{
					obj = new _Closure___17_0(obj);
					if (Settings.Get<int>("UiMusicVolume") != num)
					{
						num = Settings.Get<int>("UiMusicVolume");
						waveOutEvent.Volume = (float)((double)num / 1000.0);
					}
					obj._VB_Local_Percent = waveStream.CurrentTime.TotalMilliseconds / waveStream.TotalTime.TotalMilliseconds;
					ModBase.RunInUi(obj._Lambda___0);
					Thread.Sleep(100);
				}
				if (waveOutEvent.PlaybackState == PlaybackState.Stopped && MusicAllList.Any())
				{
					MusicStartPlay(DequeueNextMusicAddress());
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, $"播放音乐出现内部错误（{MusicCurrent}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
				if (ex4 is MmException && ex4.Message.Contains("AlreadyAllocated"))
				{
					ModMain.Hint("你的音频设备正被其他程序占用。请在关闭占用的程序后重启 PCL，才能恢复音乐播放功能！", ModMain.HintType.Red);
					Thread.Sleep(1000000000);
				}
				if (ex4 is MmException && (ex4.Message.Contains("NoDriver") || ex4.Message.Contains("BadDeviceId")))
				{
					ModMain.Hint("由于音频设备变更，音乐播放功能在重启 PCL 后才能恢复！", ModMain.HintType.Red);
					Thread.Sleep(1000000000);
				}
				if ((!MusicCurrent.EndsWithF(".wav", ignoreCase: true) && !MusicCurrent.EndsWithF(".mp3", ignoreCase: true) && !MusicCurrent.EndsWithF(".flac", ignoreCase: true)) || ex4.Message.Contains("0xC00D36C4"))
				{
					ModMain.Hint("播放音乐失败（" + PathUtils.GetLastPart(MusicCurrent) + "）：PCL 可能不支持此音乐格式，请将格式转换为 .wav、.mp3 或 .flac 后再试", ModMain.HintType.Red);
				}
				else
				{
					Logger.Error(ex4, $"播放音乐失败（{PathUtils.GetLastPart(MusicCurrent)}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMusic.vb");
				}
				MusicAllList.Remove(MusicCurrent);
				MusicWaitingList.Remove(MusicCurrent);
				MusicRefreshUI();
				Thread.Sleep(2000);
				if (ex4 is FileNotFoundException)
				{
					MusicRefreshPlay(ShowHint: true, IsFirstLoad);
				}
				else
				{
					MusicStartPlay(DequeueNextMusicAddress(), IsFirstLoad);
				}
				ProjectData.ClearProjectError();
			}
			finally
			{
				waveOutEvent?.Dispose();
				waveStream?.Dispose();
				MusicRefreshUI();
			}
		}
	}
}
