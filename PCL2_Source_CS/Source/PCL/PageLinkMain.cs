// PCL

namespace PCL
{
	[DesignerGenerated]
	public partial class PageLinkMain : MyPageRight
	{
		internal ModLoader.LoaderCombo<int> _LinkLoader;
		internal ModLoader.LoaderCombo<int> LinkLoader
		{
			get
			{
				return _LinkLoader;
			}
			set
			{
				_LinkLoader = value;
			}
		}
		public enum LinkStates
		{
			Waiting,
			Loading,
			Failed,
			Finished
		}
		private class Peer
		{
			public enum Types
			{
				Client,
				Server,
				Misc,
				Self
			}
			public readonly Types Type;
			public readonly double Ping;
			public readonly string Name;
			public readonly bool Relay;
			public readonly NATTypes NATType;
			public Peer(JObject Info)
			{
				string text = Info["hostname"].ToString();
				if (Operators.CompareString(Info["cost"].ToString(), "Local", TextCompare: false) == 0)
				{
					Type = Types.Self;
				}
				else if (text.StartsWithF("Client"))
				{
					Type = Types.Client;
				}
				else if ((string?)Info["ipv4"] == "10.114.114.114")
				{
					Type = Types.Server;
				}
				else
				{
					Type = Types.Misc;
				}
				double.TryParse((string?)Info["lat_ms"], NumberStyles.Any, CultureInfo.InvariantCulture, out Ping);
				Name = text;
				Relay = Info["cost"].ToString().ContainsIgnoreCase("relay");
				NATType = EnumUtils.FromString<NATTypes>((string?)Info["nat_type"]);
			}
			public override string ToString()
			{
				return $"{Type} - {Name} - Ping {Ping:0.0}ms [中继? {Relay}] - NAT {NATType}";
			}
		}
		private enum NATTypes
		{
			Unknown,
			OpenInternet,
			NoPAT,
			FullCone,
			Restricted,
			PortRestricted,
			Symmetric,
			SymUdpFirewall,
			SymmetricEasyInc,
			SymmetricEasyDec,
			Pending
		}
		private const int INVITE_CODE_VERSION = 2;
		public static LinkStates LinkState = LinkStates.Waiting;
		private static string LastCode = null;
		private static string AutoJoinCode = null;
		private string FailReason;
		private string PathEasyTier;
		private bool IsServerSide;
		private int ServerPort;
		private int ClientPort;
		private int RPCPort;
		private string ClientAddress;
		private int DiscoverNodeID;
		private string NetworkName;
		private string NetworkSecret;
		private int ServerVersion;
		private static Process ProcessCore;
		private static AutoResetEvent ProcessOutputHandle;
		private static AutoResetEvent ProcessErrorHandle;
		private Queue<string> LogHistory;
		private List<Peer> Peers;
		private NATTypes NATType;
		private ModLoader.LoaderTask<int, List<Peer>> RefreshPeerLoader;
		private int FailCount;
		[SpecialName]
		private bool _STATIC_StartTimerThread_2001_IsTimerStarted;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_StartTimerThread_2001_IsTimerStarted_Init;
		[SpecialName]
		private Socket _STATIC_Update_2001_BroadcastSocket;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_Update_2001_BroadcastSocket_Init;

		private int PeopleCount
		{
			get
			{
				if (Peers == null)
				{
					return 0;
				}
				return checked(Peers.Where([SpecialName] (Peer p) => p.Type != Peer.Types.Misc).Count() + 1);
			}
		}






























		public PageLinkMain()
		{
			Initialized += [SpecialName] (object a0, EventArgs a1) =>
			{
				LoaderInit();
			};
			PageEnter += UpdatePanelVisibility;
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				StartTimerThread();
			};
			Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				Update();
			};
			FailReason = "准备初始化";
			PathEasyTier = Paths.AppDataThenName + "EasyTier\\";
			ClientAddress = null;
			DiscoverNodeID = -1;
			LinkLoader = new ModLoader.LoaderCombo<int>("联机", new ModLoader.LoaderBase[4]
			{
				new ModLoader.LoaderTask<int, int>("获取配置", InitConfig)
				{
					ProgressWeight = 8.0
				},
				new ModLoader.LoaderTask<int, List<ModNet.NetFile>>("准备下载联机模块", InitPrepareDownload)
				{
					ProgressWeight = 2.0
				},
				new ModNet.LoaderDownload("下载联机模块", new List<ModNet.NetFile>())
				{
					ProgressWeight = 40.0
				},
				new ModLoader.LoaderTask<int, int>("启动联机模块", InitLaunch)
				{
					ProgressWeight = 50.0
				}
			});
			LogHistory = new Queue<string>(10);
			Peers = null;
			NATType = NATTypes.Pending;
			RefreshPeerLoader = new ModLoader.LoaderTask<int, List<Peer>>("EasyTier CLI", [SpecialName] (ModLoader.LoaderTask<int, List<Peer>> a0) =>
			{
				RefreshPeer();
			});
			FailCount = 0;
			InitializeComponent();
			InitConnectEvents();
		}
		private void ChangeState(LinkStates NewState)
		{
			if (LinkState == NewState)
			{
				return;
			}
			LinkStates linkState = LinkState;
			LinkState = NewState;
			Logger.Info($"主状态由 {linkState} 变更为 {NewState}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			ModBase.RunInUi([SpecialName] () =>
			{
				switch (NewState)
				{
				case LinkStates.Waiting:
					SwitchToWaiting(linkState);
					break;
				case LinkStates.Loading:
					SwitchToLoading(linkState);
					break;
				case LinkStates.Failed:
					SwitchToFailed(linkState);
					break;
				case LinkStates.Finished:
					SwitchToFinished(linkState);
					break;
				}
			});
		}
		private void LoaderInit()
		{
			PageLoaderInit(Load, PanLoad, PanContent, PanAlways, LinkLoader, null, null, AutoRun: false);
			LinkLoader.OnStateChangedUi += [SpecialName] (ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState) =>
			{
				switch (NewState)
				{
				case ModBase.LoadState.Finished:
					ChangeState(LinkStates.Finished);
					break;
				case ModBase.LoadState.Failed:
					ModMain.Telemetry("联机失败", "Exception", ModMinecraft.FilterUserName(Loader.Error.GetDisplay(multiline: true), '*'));
					ChangeState(LinkStates.Failed);
					break;
				}
			};
		}
		private void UpdatePanelVisibility()
		{
			ModMain.FrmLinkMain.PanSelect.Visibility = ((LinkState != LinkStates.Waiting) ? Visibility.Collapsed : Visibility.Visible);
			ModMain.FrmLinkMain.PanFinish.Visibility = ((LinkState != LinkStates.Finished) ? Visibility.Collapsed : Visibility.Visible);
		}
		private void SwitchToWaiting(LinkStates OldState)
		{
			LinkLoader.Cancel();
			object lockState = LinkLoader.LockState;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockState, ref lockTaken);
				LinkLoader.State = ModBase.LoadState.Waiting;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockState);
				}
			}
			ProcessStop();
			if (OldState != LinkStates.Finished)
			{
				UpdatePanelVisibility();
			}
			PageOnContentExit();
		}
		private void Create_MouseLeftButtonUp()
		{
			string text = ModMain.MyMsgBoxInput("输入端口", string.Format("在单人游戏的暂停菜单选择 {0}对局域网开放{1}，然后输入端口数字。{2}甚至可以输入其他游戏的端口……嗯……", ModBase.vbLQ, ModBase.vbRQ, "\r\n"), null, new Collection<ValidateBase>
			{
				new ValidateInteger(1024, 65535)
			}, "端口号");
			if (text == null)
			{
				return;
			}
			IsServerSide = true;
			ServerPort = Conversions.ToInteger(text);
			VB_AnonymousDelegate_5<string> obj = [SpecialName] () =>
			{
				string text2 = "";
				int num = 1;
				do
				{
					text2 += ModBase.RandomInteger(0, 35).ConvertRadix(36);
					num = checked(num + 1);
				}
				while (num <= 5);
				return text2.Replace("O", "0").Replace("I", "1");
			};
			NetworkName = $"P{ServerPort.ConvertRadix(16).PadLeft(4, '0')}-{obj()}";
			NetworkSecret = obj();
			DiscoverNodeID = -1;
			Logger.Info($"尝试创建房间，网络名 {NetworkName}，网络密码 {NetworkSecret}，端口 {ServerPort}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			ChangeState(LinkStates.Loading);
		}
		public static void Join()
		{
			string text = ModMain.MyMsgBoxInput("输入邀请码", "输入房主发给你的邀请码。", null, null, string.IsNullOrEmpty(LastCode) ? "" : "使用上一次的邀请码");
			if (!string.IsNullOrEmpty(LastCode) && text != null && Operators.CompareString(text, "", TextCompare: false) == 0)
			{
				text = LastCode;
			}
			if (text != null)
			{
				Join(text);
			}
		}
		public void JoinInternal(string Code)
		{
			if (Code == null)
			{
				return;
			}
			string text = ValidateCodeFormat(Code);
			if (text != null)
			{
				ModMain.Hint(text, ModMain.HintType.Red);
				return;
			}
			Code = FixCodeFormat(Code);
			Logger.Info($"实际使用的邀请码：{Code}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			IsServerSide = false;
			ServerPort = Conversions.ToInteger(Code.Substring(1, 4).Upper().ConvertRadix(16, 10));
			NetworkName = Code.Substring(0, 11);
			NetworkSecret = Code.Substring(12, 5);
			if (Operators.CompareString(Code.Substring(20, 3), "000", TextCompare: false) == 0)
			{
				DiscoverNodeID = -2;
			}
			else
			{
				DiscoverNodeID = Conversions.ToInteger(Code.Substring(20, 3).Upper().ConvertRadix(16, 10));
			}
			Logger.Info($"尝试加入房间，网络名 {NetworkName}，网络密码 {NetworkSecret}，端口 {ServerPort}，发现节点 {DiscoverNodeID}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			LastCode = Code;
			ChangeState(LinkStates.Loading);
		}
		public static string ValidateCodeFormat(string Code)
		{
			if (Code == null)
			{
				return "邀请码为空！";
			}
			Code = FixCodeFormat(Code);
			if (Code.Length < 14 || Code[0] != 'P' || Code[5] != '-' || Code[11] != '-')
			{
				if (Code.StartsWithF("U/"))
				{
					return "请让房主使用 PCL 创建房间！";
				}
				if (Code.Length == 10)
				{
					return "请让房主使用非社区版的 PCL 创建房间！";
				}
				return "邀请码有误，请让房主使用 PCL 创建房间！";
			}
			if (Code.Length >= 23 && Code[17] == '-' && ModBase.Val(Code.Substring(18, 2)) > 2.0)
			{
				return "你的 PCL 版本太老了，请在更新 PCL 之后再联机！";
			}
			return null;
		}
		private static string FixCodeFormat(string Code)
		{
			Code = Code.Between("【", "】").Between("[", "]");
			Code = Code.Upper().Replace("O", "0").Replace("I", "1");
			if (Code.Length >= 17 && (Code.Length < 23 || (Code.Length >= 18 && Code[17] != '-')))
			{
				Code = Code.Substring(0, 17) + "-0105E";
			}
			return Code;
		}
		public static void Join(string Code)
		{
			if (LinkState != LinkStates.Waiting)
			{
				ModMain.Hint("你已经在联机房间中了！", ModMain.HintType.Red);
				return;
			}
			if (ModMain.FrmMain.PageCurrent == FormMain.PageType.Link)
			{
				ModMain.FrmLinkMain.JoinInternal(Code);
				return;
			}
			AutoJoinCode = Code;
			ModMain.FrmMain.PageChange(FormMain.PageType.Link);
		}
		private void PanSelectJoin_Loaded()
		{
			if (AutoJoinCode != null)
			{
				if (LinkState == LinkStates.Waiting)
				{
					JoinInternal(AutoJoinCode);
				}
				AutoJoinCode = null;
			}
		}
		private void SwitchToLoading(LinkStates OldState)
		{
			Peers = null;
			FailCount = 0;
			LinkLoader.Start(null, IsForceRestart: true);
			UpdateProgressBar(0.0);
			LabLoadTitle.Text = (IsServerSide ? "创建房间中" : "加入房间中");
			UpdateLoadingPage("正在初始化……", "准备初始化");
		}
		private void SwitchToFailed(LinkStates OldState)
		{
			LinkLoader.Cancel();
			ProcessStop();
			UpdateProgressBar(1.0);
			LabLoadTitle.Text = FailReason;
			string text = LinkLoader.Error.GetDisplay(multiline: false);
			Exception ex = LinkLoader.Error;
			if (ex.Message.StartsWithF("$"))
			{
				text = ex.Message;
			}
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
				if (ex.Message.StartsWithF("$"))
				{
					text = ex.Message;
				}
			}
			LabLoadDesc.Text = (text.StartsWithF("$") ? text.TrimStart('$') : LinkLoader.Error.GetDisplay(multiline: true));
			Logger.Warn(LinkLoader.Error, LabLoadTitle.Text, LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		}
		private void UpdateLoadingPage(string Title, string FailBrief)
		{
			if (Operators.CompareString(FailReason, FailBrief + "失败", TextCompare: false) == 0)
			{
				return;
			}
			FailReason = FailBrief + "失败";
			Logger.Info($"开始步骤：{Title}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			ModBase.RunInUiWait([SpecialName] () =>
			{
				if (ModMain.FrmLinkMain != null && ModMain.FrmLinkMain.LabLoadDesc.IsLoaded)
				{
					ModMain.FrmLinkMain.LabLoadDesc.Text = Title;
				}
			});
		}
		private void BtnLoadCancel_Click()
		{
			ChangeState(LinkStates.Waiting);
		}
		private void CardLoad_MouseLeftButtonUp()
		{
			ChangeState(LinkStates.Loading);
		}
		private void UpdateProgressBar(double Value = -1.0)
		{
			if (Value == -1.0)
			{
				Value = LinkLoader.Progress;
			}
			double value = ColumnProgressA.Width.Value;
			if (Math.Round(Value - value, 3) != 0.0)
			{
				if (value <= Value)
				{
					double num = ((Value == 1.0) ? 1.0 : (value + Math.Pow(Value - value, 2.0) * 0.5));
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaGridLengthWidth(ColumnProgressA, num - ColumnProgressA.Width.Value, 200),
						ModAnimation.AaGridLengthWidth(ColumnProgressB, 1.0 - num - ColumnProgressB.Width.Value, 200)
					}, "Link Progress");
				}
				else
				{
					ColumnProgressA.Width = new GridLength(Value, GridUnitType.Star);
					ColumnProgressB.Width = new GridLength(1.0 - Value, GridUnitType.Star);
					ModAnimation.AniStop("Link Progress");
				}
			}
		}
		private void CardLoad_SizeChanged()
		{
			RectProgressClip.Rect = new Rect(0.0, 0.0, CardLoad.ActualWidth, 12.0);
		}
		private void SwitchToFinished(LinkStates OldState)
		{
			UpdatePanelVisibility();
			if (IsServerSide)
			{
				LabFinishTitle.Text = "已创建房间";
				LabFinishDesc.Text = string.Format("把邀请码发给朋友，让大家加入房间吧！{0}邀请码：{1}", "\r\n", GetInviteCode());
				BtnFinishExit.Text = "关闭";
				BtnFinishPing.ToolTip = "网络延迟";
				BtnFinishCopy.Visibility = Visibility.Visible;
				Copy();
				BtnFinishPort.Visibility = Visibility.Visible;
				LabFinishPort.Visibility = Visibility.Visible;
				BtnFinishIp.Visibility = Visibility.Collapsed;
				LabFinishIp.Visibility = Visibility.Collapsed;
				LabFinishPort.Text = Conversions.ToString(ServerPort);
			}
			else
			{
				LabFinishTitle.Text = "已加入房间";
				LabFinishDesc.Text = string.Format("在多人游戏页面的最下方就能找到联机房间！{0}注意：使用离线登录时不要手动输入 IP！", "\r\n");
				BtnFinishExit.Text = "离开";
				BtnFinishPing.ToolTip = "与房主的延迟";
				BtnFinishCopy.Visibility = Visibility.Collapsed;
				BtnFinishPort.Visibility = Visibility.Collapsed;
				LabFinishPort.Visibility = Visibility.Collapsed;
				LabFinishIp.Text = ClientAddress;
				BtnFinishIp.Visibility = Visibility.Visible;
				LabFinishIp.Visibility = Visibility.Visible;
			}
			Update();
		}
		private void BtnFinishExit_Click()
		{
			TryExit(Silent: false, Closing: false);
		}
		public bool TryExit(bool Silent, bool Closing)
		{
			if (LinkState == LinkStates.Waiting || LinkState == LinkStates.Failed)
			{
				return false;
			}
			if (!Silent)
			{
				if (IsServerSide && PeopleCount > 1)
				{
					if (ModMain.MyMsgBox("你确定要关闭联机房间吗？\r\n所有玩家都需要重新输入邀请码才可加入游戏！", "退出联机", "确定", "取消", "", IsWarn: true) == 2)
					{
						return true;
					}
				}
				else if (Closing && ModMain.MyMsgBox(IsServerSide ? "你确定要关闭联机房间吗？" : "你确定要离开联机房间吗？", "退出联机", "确定", "取消") == 2)
				{
					return true;
				}
			}
			ChangeState(LinkStates.Waiting);
			return false;
		}
		private void Copy()
		{
			string text = $"在 PCL 启动器中输入邀请码【{GetInviteCode()}】，即可加入联机房间！";
			ModBase.ClipboardSet(text, ShowSuccessHint: false);
			Settings.Set("LinkLastAutoJoinInviteCode", text);
			ModMain.Hint("已复制邀请码！", ModMain.HintType.Green);
		}
		private string GetInviteCode()
		{
			return $"{NetworkName}-{NetworkSecret}-{2.ToString().PadLeft(2, '0')}{((DiscoverNodeID != -1) ? DiscoverNodeID : 0).ConvertRadix(16).PadLeft(3, '0')}";
		}
		private void BtnFinishIp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ModBase.ClipboardSet(ClientAddress, ShowSuccessHint: false);
			ModMain.Hint("已复制服务器地址！", ModMain.HintType.Green);
		}
		private void InitConfig(ModLoader.LoaderTask<int, int> Task)
		{
			UpdateLoadingPage("正在联网获取配置……", "联网获取配置");
			ModSecret.ServerLoader.WaitForExit(null, Task);
			if (ModSecret.ServerConfig == null)
			{
				throw new Exception("无法从服务器获取配置");
			}
			string text = ModSecret.ServerConfig["Link"]?["DisableReason2"]?.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				throw new Exception("$" + text);
			}
			if (((JObject)ModSecret.ServerConfig["Link"]).ContainsKey("MinVersionCode") && 410 < ModSecret.ServerConfig["Link"]["MinVersionCode"].ToObject<int>())
			{
				throw new Exception("$你的 PCL 版本太老了，请在更新 PCL 之后再联机！");
			}
		}
		private void InitPrepareDownload(ModLoader.LoaderTask<int, List<ModNet.NetFile>> Task)
		{
			UpdateLoadingPage("正在获取 CPU 架构……", "获取 CPU 架构");
			string text = Conversions.ToString((int)typeof(string).Assembly.GetName().ProcessorArchitecture);
			string left = text;
			if (Operators.CompareString(left, Conversions.ToString(2), TextCompare: false) == 0)
			{
				text = "i686";
			}
			else if (Operators.CompareString(left, Conversions.ToString(4), TextCompare: false) == 0)
			{
				text = "x86_64";
			}
			else if (Operators.CompareString(left, Conversions.ToString(5), TextCompare: false) == 0)
			{
				text = "arm64";
			}
			else
			{
				Logger.Warn($"CPU 是不支持的 {text} 架构，这可能会导致联机模块无法启动！", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				text = "arm64";
			}
			Logger.Info($"CPU 架构：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			ModMain.Telemetry("联机开始");
			UpdateLoadingPage("正在检查联机模块版本……", "检查联机模块版本");
			if (!FileUtils.Exists(PathEasyTier + "联机模块 CLI.exe") || !FileUtils.Exists(PathEasyTier + "联机模块.exe") || !FileUtils.Exists(PathEasyTier + "Packet.dll"))
			{
				Settings.Set("LinkEasyTierVersion", -1);
			}
			int num = Settings.Get<int>("LinkEasyTierVersion");
			ServerVersion = (int)ModSecret.ServerConfig["Link"]["EasyTierVersion"];
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			Logger.Info($"EasyTier 本地版本：{num}，需求版本：{ServerVersion}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			if (num < ServerVersion)
			{
				list.Add(new ModNet.NetFile(ModSecret.ServerConfig["Link"]["Downloads"].Select([SpecialName] (JToken UrlEntry) => UrlEntry.ToString().Replace("{arch}", text)), PathEasyTier + "EasyTier.zip", new FileChecker
				{
					MinSize = 2097152L
				}));
			}
			UpdateLoadingPage("正在下载联机模块……", "下载联机模块");
			Task.Output = list;
		}
		private void InitLaunch(ModLoader.LoaderTask<int, int> Task)
		{
			UpdateLoadingPage("正在解压联机模块……", "解压联机模块");
			if (FileUtils.Exists(PathEasyTier + "EasyTier.zip"))
			{
				DirectoryUtils.Copy(DirectoryUtils.EnumerateDirectories(ModMain.RequestTaskTempFolder(), includeSubDirectories: true).First(), PathEasyTier);
				FileUtils.Move(PathEasyTier + "easytier-core.exe", PathEasyTier + "联机模块.exe");
				FileUtils.Move(PathEasyTier + "easytier-cli.exe", PathEasyTier + "联机模块 CLI.exe");
				FileUtils.Delete(PathEasyTier + "EasyTier.zip");
				Settings.Set("LinkEasyTierVersion", ServerVersion);
			}
			Task.Progress = 0.07;
			UpdateLoadingPage("正在获取节点列表……", "获取节点列表");
			string text = Settings.Get<string>("LinkCustomPeer");
			List<string> list;
			if (string.IsNullOrWhiteSpace(text))
			{
				if (DiscoverNodeID == -2 && !IsServerSide)
				{
					Panic("未填写自定义节点设置", string.Format("$你必须在 {0}自定义节点{1} 设置中填写与房主相同的内容，{2}才能进入该房间！", ModBase.vbLQ, ModBase.vbRQ, "\r\n"));
					return;
				}
				list = GetTargetPeers();
			}
			else
			{
				if (DiscoverNodeID != -2 && !IsServerSide)
				{
					ModMain.Hint("房主可能没有使用自定义节点设置，请确认你们的自定义节点设置是否一致！");
				}
				list = (from p in text.Split("，,".ToCharArray())
					select p.Trim() into p
					where !string.IsNullOrEmpty(p)
					select p).ToList();
				Logger.Info($"使用自定义节点：{text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			}
			Task.Progress = 0.13;
			UpdateLoadingPage("正在启动联机模块……", "启动联机模块");
			List<int> list2 = ModNet.FindFreePorts(5, ServerPort);
			ClientPort = list2[0];
			RPCPort = list2[1];
			int num = list2[2];
			string text2 = (string?)ModSecret.ServerConfig["Link"]["Argument"];
			text2 += $" --network-name={NetworkName} --network-secret={NetworkSecret} --listeners {num} --rpc-portal {RPCPort} --private-mode true";
			string arg = (IsServerSide ? "Server-" : "Client-") + ModBase.Identify.GetStableHashCode().ConvertRadix(36).TrimStart('-')
				.EnsureLength('0', 6);
			if (IsServerSide)
			{
				text2 += $" -i 10.114.114.114 --hostname={arg} --tcp-whitelist={ServerPort} --udp-whitelist={ServerPort}";
			}
			else
			{
				text2 += $" -d --hostname={arg} --tcp-whitelist=0 --udp-whitelist=0";
				ClientAddress = $"localhost:{ClientPort}";
				text2 += $" --port-forward tcp://[{IPAddress.IPv6Loopback}]:{ClientPort}/10.114.114.114:{ServerPort}";
				text2 += $" --port-forward udp://[{IPAddress.IPv6Loopback}]:{ClientPort}/10.114.114.114:{ServerPort}";
				text2 += $" --port-forward tcp://{IPAddress.Loopback}:{ClientPort}/10.114.114.114:{ServerPort}";
				text2 += $" --port-forward udp://{IPAddress.Loopback}:{ClientPort}/10.114.114.114:{ServerPort}";
			}
			foreach (string item in list)
			{
				text2 += $" -p=\"{item}\"";
			}
			if (Settings.Get<int>("LinkLatencyMode") == 1)
			{
				text2 += " --latency-first";
			}
			ProcessStart(text2);
			Task.Progress = 0.15;
			int num2 = 0;
			do
			{
				Thread.Sleep(500);
				CheckCrash();
				RefreshPeerLoader.WaitForExit(null, null, IsForceRestart: true);
				double peerPing = GetPeerPing();
				if (peerPing > 0.0)
				{
					Logger.Info($"已与目标建立连接，当前 Ping 为 {peerPing:0.0}ms", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
					ModMain.Telemetry("联机成功", "Server", Conversions.ToString(IsServerSide), "NAT", Conversions.ToString((int)NATType));
					break;
				}
				double progress = Task.Progress;
				int num3 = ((Peers == null) ? (-1) : Peers.Where([SpecialName] (Peer p) => p.Ping > 0.0).Count());
				switch (num3)
				{
				case -1:
					Task.Progress = (Task.Progress + 0.02).Clamp(0.15, 0.25);
					break;
				case 0:
					UpdateLoadingPage("正在连接到节点……", "连接节点");
					Task.Progress = (Task.Progress + 0.02).Clamp(IsServerSide ? 0.5 : 0.3, IsServerSide ? 0.95 : 0.5);
					break;
				default:
					UpdateLoadingPage("正在连接到房主……", "连接房主");
					Task.Progress = (Task.Progress + 0.02).Clamp(Math.Min(0.45 + (double)num3 * 0.05, 0.65), 0.95);
					break;
				}
				if (progress != Task.Progress)
				{
					num2 = 0;
					continue;
				}
				if (num2 < 60)
				{
					num2 = checked(num2 + 1);
					continue;
				}
				switch (num3)
				{
				case -1:
					Panic("无法启动联机模块", "近期日志：\r\n" + LogHistory.Join("\r\n"));
					break;
				case 0:
					Panic("无法连接到节点", "请检查你的网络环境是否良好。");
					break;
				default:
					Panic("无法连接到房主", string.Format("可能的原因：{0}- 你或者房主的网络环境不佳{1}- 房主已关闭房间{2}- 邀请码输错了", "\r\n", "\r\n", "\r\n"));
					break;
				}
			}
			while (!Task.IsCanceled);
			if (Task.IsCanceled)
			{
				throw new OperationCanceledException();
			}
			if (IsServerSide)
			{
				return;
			}
			UpdateLoadingPage("连接优化中……", "优化连接");
			Task.Progress = 0.999;
			int num4 = 1;
			do
			{
				Peer targetPeer = GetTargetPeer();
				if (targetPeer == null || targetPeer.Relay || !(targetPeer.Ping < 100.0))
				{
					if (Task.IsCanceled)
					{
						throw new OperationCanceledException();
					}
					Thread.Sleep(100);
					num4 = checked(num4 + 1);
					continue;
				}
				break;
			}
			while (num4 <= 50);
		}
		private List<string> GetTargetPeers()
		{
			List<string> list = new List<string>();
			int num = -1;
			int num2 = ModSecret.ServerConfig["Link"]["DiscoverPeerId"].ToObject<int>();
			string text = ModSecret.ServerConfig["Link"]["DiscoverPeer"].ToString();
			try
			{
				bool flag = false;
				JObject jObject;
				try
				{
					jObject = (JObject)ModNet.NetRequestByClient("https://uptime.easytier.cn/api/nodes?page=1&per_page=1000", null, null, null, "*/*", 25000, null, RequireJson: true).DeserializeJson();
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "从源站获取节点列表失败，将使用 CDN 缓存", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
					jObject = (JObject)ModNet.NetRequestByClientRetry("https://easytier.meloong.com/?page=1&per_page=1000", null, null, null, null, "*/*", null, RequireJson: true).DeserializeJson();
					flag = true;
					ProjectData.ClearProjectError();
				}
				List<JObject> list2 = new List<JObject>();
				List<string> list3 = ModSecret.ServerConfig["Link"]["PeersBlackList"].Select([SpecialName] (JToken p) => p.ToString()).ToList();
				foreach (JObject item in (IEnumerable<JToken>)jObject["data"]["items"])
				{
					int num3 = item["id"].ToObject<int>();
					if (!item["is_active"].ToObject<bool>() || !item["is_approved"].ToObject<bool>())
					{
						continue;
					}
					if (num3 != DiscoverNodeID)
					{
						if (num3 == num2 || list3.Contains(item["address"].ToString()) || (item["usage_percentage"].ToObject<double>() == 0.0 && ModBase.RandomInteger(1, 100) != 1))
						{
							continue;
						}
						List<string> list4 = item["tags"].Select([SpecialName] (JToken t) => t.ToString()).ToList();
						if (!list4.Contains("国内") || !list4.Contains("MC中继"))
						{
							continue;
						}
					}
					double num4 = item["usage_percentage"].ToObject<double>();
					num4 *= 110.0 - item["health_percentage_24h"].ToObject<double>();
					item["load"] = num4;
					list2.Add(item);
				}
				list2 = (flag ? ModBase.Shuffle(list2).ToList() : list2.OrderBy([SpecialName] (JObject n) => n["load"].ToObject<double>()).ToList());
				JObject jObject3 = null;
				if (DiscoverNodeID == -1)
				{
					jObject3 = list2.FirstOrDefault([SpecialName] (JObject n) => !n["allow_relay"].ToObject<bool>());
				}
				else if (DiscoverNodeID > 0)
				{
					jObject3 = list2.FirstOrDefault([SpecialName] (JObject n) => n["id"].ToObject<int>() == DiscoverNodeID);
					if (jObject3 == null)
					{
						Logger.Warn($"未找到 ID {DiscoverNodeID} 的发现节点", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
						Panic("房间已过期", "请让房主重新创建房间！");
						throw new OperationCanceledException();
					}
				}
				if (jObject3 == null)
				{
					jObject3 = new JObject
					{
						{ "address", text },
						{ "id", num2 }
					};
					Logger.Warn("将使用回退发现节点", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				}
				list.Add(jObject3["address"].ToString());
				num = jObject3["id"].ToObject<int>();
				Logger.Info(string.Format("发现节点：{0} (ID: {1})", jObject3["address"], num), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				if (ModBase.ModeDebug || !IsServerSide)
				{
					int num5 = ModSecret.ServerConfig["Link"]["RandomPeer"].ToObject<int>();
					List<JObject> list5 = list2.Where([SpecialName] (JObject n) => n["allow_relay"].ToObject<bool>()).ToList();
					if (list5.Count < num5)
					{
						throw new Exception($"可用的中继节点数量不足，需要 {num5} 个，实际 {list5.Count} 个");
					}
					list.AddRange(from n in list5.Take(num5)
						select n["address"].ToString());
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				ex4.ThrowIfCanceled();
				Logger.Error(ex4, "获取节点列表失败，联机质量可能受到影响", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				list.AddRange(ModSecret.ServerConfig["Link"]["Peers"].Select([SpecialName] (JToken p) => p.ToString()));
				if (num <= 0)
				{
					list.Add(text);
					num = num2;
				}
				ProjectData.ClearProjectError();
			}
			if (((JObject)ModSecret.ServerConfig["Link"]).ContainsKey("EnableForcedPeer"))
			{
				list.Add("tcp://mc1.easytier.cn:55558");
				if (num <= 0)
				{
					num = 94;
				}
			}
			list.AddRange(ModSecret.ServerConfig["Link"]["MandatoryPeers"].Select([SpecialName] (JToken p) => p.ToString()));
			DiscoverNodeID = num;
			return list.Distinct().ToList();
		}
		public static void ProcessStop()
		{
			try
			{
				Process[] processes = Process.GetProcesses();
				foreach (Process process in processes)
				{
					int id = process.Id;
					int? num = ProcessCore?.Id;
					if ((num.HasValue ? new bool?(id == num.GetValueOrDefault()) : ((bool?)null)) != true && (Operators.CompareString(process.ProcessName, "联机模块", TextCompare: false) == 0 || Operators.CompareString(process.ProcessName, "联机模块 CLI", TextCompare: false) == 0))
					{
						try
						{
							Logger.Info($"停止残留的联机模块，PID：{process.Id}，进程名：{process.ProcessName}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
							process.Kill();
							process.Close();
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							Logger.Warn(ex2, $"结束进程失败（{process.ProcessName}，PID {process.Id}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
							ProjectData.ClearProjectError();
						}
					}
				}
				if (ProcessCore != null && !ProcessCore.HasExited)
				{
					Logger.Info($"停止所启动的联机模块，PID：{ProcessCore.Id}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
					ProcessCore.Kill();
					ProcessCore.Close();
					ProcessCore = null;
				}
				ProcessOutputHandle?.Dispose();
				ProcessErrorHandle?.Dispose();
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "停止联机模块失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
		private void Panic(string Brief, string Detail)
		{
			if (Detail.Contains("failed to listen on "))
			{
				Detail = string.Format("监听端口失败。{0}请点击重试，如果还是出现此错误，可以重启电脑解决。{1}{2}{3}", "\r\n", "\r\n", "\r\n", Detail);
			}
			if (LinkState == LinkStates.Loading)
			{
				FailReason = Brief;
				LinkLoader.Failed(new Exception(Detail));
				return;
			}
			ModMain.MyMsgBox(Detail, "联机出错：" + Brief, "确定", "", "", IsWarn: true);
			ChangeState(LinkStates.Waiting);
			ModMain.Telemetry("联机失败", "Exception", ModMinecraft.FilterUserName(Brief + "：" + Detail, '*'));
		}
		private void ProcessStart(string Arguments)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = PathEasyTier + "联机模块.exe",
				Arguments = Arguments,
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				StandardOutputEncoding = Encoding.UTF8,
				StandardErrorEncoding = Encoding.UTF8
			};
			Logger.Info($"正在启动 EasyTier：{Arguments}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			ProcessCore = new Process
			{
				StartInfo = startInfo
			};
			VB_AnonymousDelegate_13<object, DataReceivedEventArgs, AutoResetEvent> obj = [SpecialName] (object sender, DataReceivedEventArgs e, AutoResetEvent Handle) =>
			{
				try
				{
					if (e.Data == null)
					{
						Handle.Set();
					}
					else
					{
						ProcessLogLine(e.Data);
					}
				}
				catch (ObjectDisposedException ex)
				{
					ProjectData.SetProjectError(ex);
					ObjectDisposedException ex2 = ex;
					ProjectData.ClearProjectError();
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, "读取 EasyTier 信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
					ProjectData.ClearProjectError();
				}
			};
			ProcessOutputHandle = new AutoResetEvent(initialState: false);
			ProcessCore.OutputDataReceived += [SpecialName] (object sender, DataReceivedEventArgs e) =>
			{
				obj(RuntimeHelpers.GetObjectValue(sender), e, ProcessOutputHandle);
			};
			ProcessErrorHandle = new AutoResetEvent(initialState: false);
			ProcessCore.ErrorDataReceived += [SpecialName] (object sender, DataReceivedEventArgs e) =>
			{
				obj(RuntimeHelpers.GetObjectValue(sender), e, ProcessErrorHandle);
			};
			ProcessCore.Start();
			ProcessCore.BeginOutputReadLine();
			ProcessCore.BeginErrorReadLine();
		}
		private void ProcessLogLine(string Line)
		{
			Logger.Info(Line, LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			LogHistory.Enqueue(Line);
			if (LogHistory.Count >= 10)
			{
				LogHistory.Dequeue();
			}
			if (Line.ContainsIgnoreCase("new peer connection added"))
			{
				Logger.Warn(string.Format("已建立连接：{0}", Line.RegexSeek("(?<=remote_addr.+?\")[^\"}]{3,}") ?? Line), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				Update();
			}
			else if (Line.ContainsIgnoreCase("peer connection removed"))
			{
				Logger.Warn(string.Format("已断开连接：{0}", Line.RegexSeek("(?<=remote_addr.+?\")[^\"}]{3,}") ?? Line), LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				Update();
			}
		}
		private void RefreshPeer()
		{
			try
			{
				string text = "";
				if (!text.Contains("lat_ms"))
				{
					throw new Exception("CLI 调用失败：\r\n" + text);
				}
				if (ModBase.GetUuid() % (ModBase.ModeDebug ? 23 : 103) == 0)
				{
					Logger.Info(string.Format("CLI 输出抽样：{0}{1}", "\r\n", text), LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				}
				List<Peer> list = new List<Peer>();
				foreach (JObject item in (JArray)text.DeserializeJson())
				{
					try
					{
						Peer peer = new Peer(item);
						if (peer.Type == Peer.Types.Self)
						{
							NATType = peer.NATType;
						}
						else
						{
							list.Add(peer);
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, $"错误的信息（{item}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
						ProjectData.ClearProjectError();
					}
				}
				Peers = list;
				FailCount = 0;
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "获取节点信息失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				checked
				{
					if (LinkState == LinkStates.Finished)
					{
						FailCount++;
						if (FailCount >= 4)
						{
							Panic("获取节点信息失败", ex4.Message);
						}
					}
					ProjectData.ClearProjectError();
				}
			}
		}
		private double GetPeerPing()
		{
			return GetTargetPeer()?.Ping ?? 0.0;
		}
		private Peer GetTargetPeer()
		{
			if (Peers == null)
			{
				return null;
			}
			IEnumerable<Peer> source = ((!IsServerSide) ? Peers.Where([SpecialName] (Peer p) => p.Type == Peer.Types.Server && p.Ping > 0.0) : Peers.Where([SpecialName] (Peer p) => p.Ping > 0.0));
			if (!source.Any())
			{
				return null;
			}
			double num = source.Min([SpecialName] (Peer p) => p.Ping);
			return source.First([SpecialName] (Peer p) => p.Ping == num);
		}
		private void StartTimerThread()
		{
			if (_STATIC_StartTimerThread_2001_IsTimerStarted_Init == null)
			{
				Interlocked.CompareExchange(ref _STATIC_StartTimerThread_2001_IsTimerStarted_Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_STATIC_StartTimerThread_2001_IsTimerStarted_Init, ref lockTaken);
				if (_STATIC_StartTimerThread_2001_IsTimerStarted_Init.State == 0)
				{
					_STATIC_StartTimerThread_2001_IsTimerStarted_Init.State = 2;
					_STATIC_StartTimerThread_2001_IsTimerStarted = false;
				}
				else if (_STATIC_StartTimerThread_2001_IsTimerStarted_Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_STATIC_StartTimerThread_2001_IsTimerStarted_Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_STATIC_StartTimerThread_2001_IsTimerStarted_Init);
				}
			}
			if (_STATIC_StartTimerThread_2001_IsTimerStarted)
			{
				return;
			}
			ModBase.RunInNewThread([SpecialName] () =>
			{
				int num = 0;
				while (true)
				{
					try
					{
						Thread.Sleep(200);
						num = checked(num + 200);
						if (num >= 1000)
						{
							num = 0;
							Update();
						}
						if (LinkState == LinkStates.Loading)
						{
							ModBase.RunInUi([SpecialName] () =>
							{
								UpdateProgressBar();
							});
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Error(ex2, "联机模块主时钟出错", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
						Thread.Sleep(10000);
						ProjectData.ClearProjectError();
					}
				}
			}, "Link Timer");
		}
		private void Update()
		{
			if (LinkState != LinkStates.Finished)
			{
				return;
			}
			object lockState = RefreshPeerLoader.LockState;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockState, ref lockTaken);
				if (RefreshPeerLoader.State != ModBase.LoadState.Loading)
				{
					RefreshPeerLoader.Start(null, IsForceRestart: true);
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockState);
				}
			}
			if (ModMain.FrmMain.PageCurrent == FormMain.PageType.Link)
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					ModAnimation.AniStart(ModAnimation.AaRotateTransform(ImgFinishLogo, 500.0, 5000), "Link Logo Rotation");
					double peerPing = GetPeerPing();
					bool flag = peerPing % 500.0 == 0.0 || Operators.CompareString($"{peerPing:0.0}", "1.0", TextCompare: false) == 0 || FailCount > 0;
					if (flag)
					{
						LabFinishPing.Text = "连接优化中";
					}
					else
					{
						LabFinishPing.Text = ((peerPing >= 10.0) ? $"{peerPing:0}\u200ams" : $"{peerPing:0.0}\u200ams");
					}
					LabFinishPlayer.Text = Conversions.ToString(PeopleCount) + "\u200a\u200a人";
					HintFinish.Visibility = Visibility.Collapsed;
					if (!IsServerSide && !flag)
					{
						Peer targetPeer = GetTargetPeer();
						if (targetPeer != null)
						{
							if (targetPeer.Relay)
							{
								if (NATType >= NATTypes.Symmetric && targetPeer.NATType >= NATTypes.Symmetric)
								{
									HintFinish.Text = "你和房主的网络环境都不太好，";
								}
								else if (NATType >= NATTypes.Symmetric)
								{
									HintFinish.Text = "你的网络环境不太好，";
								}
								else if (targetPeer.NATType >= NATTypes.Symmetric)
								{
									HintFinish.Text = "房主的网络环境不太好，";
								}
								else
								{
									HintFinish.Text = "你或者房主的网络环境不太好，";
								}
								if (string.IsNullOrWhiteSpace(Settings.Get<string>("LinkCustomPeer")))
								{
									HintFinish.Text += "正使用社区节点进行中继。";
								}
								else
								{
									HintFinish.Text += "正通过自定义节点进行中继。";
								}
								HintFinish.Visibility = Visibility.Visible;
								HintFinish.Theme = MyHint.Themes.Yellow;
							}
							else if (NATType < NATTypes.PortRestricted && targetPeer.NATType >= NATTypes.PortRestricted)
							{
								List<Peer> source = Peers.Where([SpecialName] (Peer p) => p.Type == Peer.Types.Client).ToList();
								if (source.Any() && !source.Any([SpecialName] (Peer p) => p.Relay) && !source.All([SpecialName] (Peer p) => p.NATType < NATTypes.PortRestricted))
								{
									HintFinish.Visibility = Visibility.Visible;
									HintFinish.Theme = MyHint.Themes.Blue;
									HintFinish.Text = "你的网络环境比房主更好！如果你来当房主，其他玩家或许能更加流畅！";
								}
							}
						}
					}
				});
			}
			CheckCrash();
			if (!Peers.Any())
			{
				Panic("网络连接已断开", "请检查你的网络环境是否正常。");
			}
			else if (!IsServerSide && !Peers.Any([SpecialName] (Peer p) => p.Type == Peer.Types.Server))
			{
				ModMain.MyMsgBox("房主已离开房间！", "联机结束");
				ChangeState(LinkStates.Waiting);
			}
			else
			{
				if (IsServerSide)
				{
					return;
				}
				try
				{
					if (_STATIC_Update_2001_BroadcastSocket_Init == null)
					{
						Interlocked.CompareExchange(ref _STATIC_Update_2001_BroadcastSocket_Init, new StaticLocalInitFlag(), null);
					}
					bool lockTaken2 = false;
					try
					{
						Monitor.Enter(_STATIC_Update_2001_BroadcastSocket_Init, ref lockTaken2);
						if (_STATIC_Update_2001_BroadcastSocket_Init.State == 0)
						{
							_STATIC_Update_2001_BroadcastSocket_Init.State = 2;
							_STATIC_Update_2001_BroadcastSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
						}
						else if (_STATIC_Update_2001_BroadcastSocket_Init.State == 2)
						{
							throw new IncompleteInitialization();
						}
					}
					finally
					{
						_STATIC_Update_2001_BroadcastSocket_Init.State = 1;
						if (lockTaken2)
						{
							Monitor.Exit(_STATIC_Update_2001_BroadcastSocket_Init);
						}
					}
					_STATIC_Update_2001_BroadcastSocket.SendTo(Encoding.UTF8.GetBytes($"[MOTD]PCL 联机房间[/MOTD][AD]{ClientPort}[/AD]"), SocketFlags.None, new IPEndPoint(IPAddress.Loopback, 4445));
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "广播联机房间端口失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
					ProjectData.ClearProjectError();
				}
			}
		}
		private void CheckCrash()
		{
			if (ProcessCore == null || ProcessCore.HasExited)
			{
				Panic("联机模块已崩溃", "近期日志：\r\n" + LogHistory.Join("\r\n"));
			}
		}
		/// <summary>连接表里原本由属性 setter 完成的事件挂接，XAML 编译后手动执行一次。</summary>
		private void InitConnectEvents()
		{
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							CardLoad_MouseLeftButtonUp();
						};
						SizeChangedEventHandler value3 = [SpecialName] (object a0, SizeChangedEventArgs a1) =>
						{
							CardLoad_SizeChanged();
						};
						MyCard myCard = CardLoad;
						if (myCard != null)
						{
							myCard.MouseLeftButtonUp -= value2;
							myCard.SizeChanged -= value3;
						}
						myCard = CardLoad;
						if (myCard != null)
						{
							myCard.MouseLeftButtonUp += value2;
							myCard.SizeChanged += value3;
						}
			
			}
			{
						MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
						{
							BtnLoadCancel_Click();
						};
						MyIconButton myIconButton = BtnLoadCancel;
						if (myIconButton != null)
						{
							myIconButton.Click -= obj;
						}
						myIconButton = BtnLoadCancel;
						if (myIconButton != null)
						{
							myIconButton.Click += obj;
						}
			
			}
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Create_MouseLeftButtonUp();
						};
						MyCard myCard = PanSelectCreate;
						if (myCard != null)
						{
							myCard.MouseLeftButtonUp -= value2;
						}
						myCard = PanSelectCreate;
						if (myCard != null)
						{
							myCard.MouseLeftButtonUp += value2;
						}
			
			}
			{
						MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Join();
						};
						RoutedEventHandler value3 = [SpecialName] (object a0, RoutedEventArgs a1) =>
						{
							PanSelectJoin_Loaded();
						};
						MyCard myCard = PanSelectJoin;
						if (myCard != null)
						{
							myCard.MouseLeftButtonUp -= value2;
							myCard.Loaded -= value3;
						}
						myCard = PanSelectJoin;
						if (myCard != null)
						{
							myCard.MouseLeftButtonUp += value2;
							myCard.Loaded += value3;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							Copy();
						};
						MyButton myButton = BtnFinishCopy;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnFinishCopy;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
						{
							BtnFinishExit_Click();
						};
						MyButton myButton = BtnFinishExit;
						if (myButton != null)
						{
							myButton.Click -= obj;
						}
						myButton = BtnFinishExit;
						if (myButton != null)
						{
							myButton.Click += obj;
						}
			
			}
			{
						MouseButtonEventHandler value2 = BtnFinishIp_MouseLeftButtonUp;
						StackPanel stackPanel = BtnFinishIp;
						if (stackPanel != null)
						{
							stackPanel.MouseLeftButtonUp -= value2;
						}
						stackPanel = BtnFinishIp;
						if (stackPanel != null)
						{
							stackPanel.MouseLeftButtonUp += value2;
						}
			
			}
		}

	}
}
