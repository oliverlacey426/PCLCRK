// PCL

namespace PCL
{
	public class CustomEvent : DependencyObject
	{
		public enum EventType
		{
			None,
			打开网页,
			打开文件,
			打开帮助,
			执行命令,
			启动游戏,
			复制文本,
			刷新主页,
			刷新页面,
			刷新帮助,
			今日人品,
			内存优化,
			清理垃圾,
			弹出窗口,
			弹出提示,
			切换页面,
			导入整合包,
			安装整合包,
			下载文件,
			修改设置,
			写入设置,
			修改变量,
			写入变量,
			加入房间,
			检查更新
		}
		public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(EventType), typeof(CustomEvent), new PropertyMetadata(EventType.None));
		public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(string), typeof(CustomEvent), new PropertyMetadata(null));
		public EventType Type
		{
			get
			{
				EventType result = EventType.None;
				ModBase.RunInUiWait([SpecialName] () =>
				{
					result = (EventType)Conversions.ToInteger(GetValue(TypeProperty));
				});
				return result;
			}
			set
			{
				SetValue(TypeProperty, value);
			}
		}
		public string Data
		{
			get
			{
				string result = null;
				ModBase.RunInUiWait([SpecialName] () =>
				{
					result = Conversions.ToString(GetValue(DataProperty));
				});
				return result;
			}
			set
			{
				SetValue(DataProperty, value);
			}
		}
		public CustomEvent()
		{
		}
		public CustomEvent(EventType Type, string Data)
		{
			this.Type = Type;
			this.Data = Data;
		}
		public void Raise()
		{
			Raise(Type, Data);
		}
		public static void Raise(EventType Type, string Arg)
		{
			if (Type == EventType.None)
			{
				return;
			}
			Logger.Info($"执行自定义事件：{Type}, {Arg}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
			if (Arg == null)
			{
				Arg = "";
			}
			string[] array = Arg.Split("|");
			string text = Arg.BeforeFirst("|");
			string text2 = Arg.AfterFirst("|");
			try
			{
				switch (Type)
				{
				case EventType.打开网页:
					Arg = Arg.Replace("\\", "/");
					if (!Arg.Contains("://") || Arg.StartsWithF("file", ignoreCase: true))
					{
						ModMain.MyMsgBox("EventData 必须为一个网址。\r\n如果想要启动程序，请将 EventType 改为 打开文件。", "事件执行失败");
						break;
					}
					ModMain.Hint("正在开启，请稍候：" + Arg);
					ModBase.RunInThread([SpecialName] () =>
					{
						ModBase.OpenWebsite(Arg);
					});
					break;
				case EventType.打开文件:
				case EventType.打开帮助:
				case EventType.执行命令:
					ModBase.RunInThread([SpecialName] () =>
					{
						try
						{
							string[] absoluteUrls = GetAbsoluteUrls(text, Type);
							string text3 = absoluteUrls[0];
							string text4 = absoluteUrls[1];
							Logger.Info($"打开类自定义事件实际路径：{text3}，工作目录：{text4}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
							if (Type == EventType.打开帮助)
							{
								PageOtherHelp.EnterHelpPage(text3);
							}
							else if (EventSafetyConfirm("即将执行：" + text3 + ((array.Length >= 2) ? (" " + text2) : "")))
							{
								ModBase.StartProcess(new ProcessStartInfo
								{
									Arguments = ((array.Length >= 2) ? text2 : ""),
									FileName = text3,
									WorkingDirectory = text4
								});
							}
						}
						catch (Exception ex5)
						{
							ProjectData.SetProjectError(ex5);
							Exception ex6 = ex5;
							Logger.Error(ex6, "执行打开类自定义事件失败", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
							ProjectData.ClearProjectError();
						}
					});
					break;
				case EventType.启动游戏:
					if (Operators.CompareString(text, "\\current", TextCompare: false) == 0)
					{
						if ((object)ModMinecraft.McInstanceSelected == null)
						{
							ModMain.Hint("请先选择一个 Minecraft 版本！", ModMain.HintType.Red);
							break;
						}
						text = ModMinecraft.McInstanceSelected.Name;
					}
					ModBase.RunInUi([SpecialName] () =>
					{
						if (ModLaunch.McLaunchStart(new ModLaunch.McLaunchOptions
						{
							ServerIp = ((array.Length >= 2) ? array[1] : null),
							Instance = new ModMinecraft.McInstance(text)
						}))
						{
							ModMain.Hint($"正在启动 {text}……");
						}
					});
					break;
				case EventType.复制文本:
					ModBase.ClipboardSet(Arg);
					break;
				case EventType.刷新主页:
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMain.FrmLaunchRight.ForceRefresh();
						if (string.IsNullOrEmpty(Arg))
						{
							ModMain.Hint("已刷新！", ModMain.HintType.Green);
						}
					});
					break;
				case EventType.刷新页面:
					if (ModMain.FrmMain.PageRight is IRefreshable)
					{
						ModBase.RunInUiWait([SpecialName] () =>
						{
							((IRefreshable)ModMain.FrmMain.PageRight).Refresh();
						});
						if (string.IsNullOrEmpty(Arg))
						{
							ModMain.Hint("已刷新！", ModMain.HintType.Green);
						}
					}
					else
					{
						ModMain.Hint("当前页面不支持刷新操作！", ModMain.HintType.Red);
					}
					break;
				case EventType.刷新帮助:
					ModBase.RunInUiWait([SpecialName] () =>
					{
						PageOtherLeft.RefreshHelp();
					});
					if (string.IsNullOrEmpty(Arg))
					{
						ModMain.Hint("已刷新！", ModMain.HintType.Green);
					}
					break;
				case EventType.今日人品:
					PageOtherTest.Jrrp();
					break;
				case EventType.内存优化:
					ModBase.RunInThread([SpecialName] () =>
					{
						PageOtherTest.MemoryOptimize(ShowHint: true);
					});
					break;
				case EventType.清理垃圾:
					ModBase.RunInThread([SpecialName] () =>
					{
						PageOtherTest.RubbishClear();
					});
					break;
				case EventType.弹出窗口:
					if (array.Length == 1)
					{
						throw new Exception($"EventType {Type} 需要至少 2 个以 | 分割的参数，例如 弹窗标题|弹窗内容");
					}
					ModMain.MyMsgBox(text2.BeforeLast("|").Replace("\\n", "\r\n"), text.Replace("\\n", "\r\n"), (array.Length > 2) ? text2.AfterLast("|") : "确定");
					break;
				case EventType.弹出提示:
					ModMain.Hint(text.Replace("\\n", "\r\n"), (array.Length != 1) ? EnumUtils.FromString<ModMain.HintType>(text2) : ModMain.HintType.Blue);
					break;
				case EventType.切换页面:
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMain.FrmMain.PageChange(EnumUtils.FromString<FormMain.PageType>(text), (array.Length != 1) ? EnumUtils.FromString<FormMain.PageSubType>(text2) : FormMain.PageSubType.Default);
					});
					break;
				case EventType.导入整合包:
				case EventType.安装整合包:
					ModBase.RunInUi([SpecialName] () =>
					{
						ModModpack.ModpackInstall();
					});
					break;
				case EventType.下载文件:
					text = text.Replace("\\", "/");
					if (!text.StartsWithF("http://", ignoreCase: true) && !text.StartsWithF("https://", ignoreCase: true))
					{
						ModMain.MyMsgBox("EventData 必须为以 http:// 或 https:// 开头的网址。\r\nPCL 不支持其他乱七八糟的下载协议。", "事件执行失败");
					}
					else
					{
						if (!EventSafetyConfirm("即将从该网址下载文件：\r\n" + text))
						{
							break;
						}
						ModBase.RunInUi([SpecialName] () =>
						{
							try
							{
								switch (array.Length)
								{
								case 1:
									PageOtherTest.StartCustomDownload(text, PathUtils.GetLastPart(text));
									break;
								case 2:
									PageOtherTest.StartCustomDownload(text, text2);
									break;
								default:
									PageOtherTest.StartCustomDownload(text, Conversions.ToString(Arg[1]), array[2]);
									break;
								}
							}
							catch (Exception projectError)
							{
								ProjectData.SetProjectError(projectError);
								PageOtherTest.StartCustomDownload(text, "未知");
								ProjectData.ClearProjectError();
							}
						});
					}
					break;
				case EventType.修改设置:
				case EventType.写入设置:
					if (array.Length == 1)
					{
						throw new Exception($"EventType {Type} 需要至少 2 个以 | 分割的参数，例如 UiLauncherTransparent|400");
					}
					Settings.SetSafe(text, text2, ModMinecraft.McInstanceSelected);
					if (array.Length == 2)
					{
						ModMain.Hint($"已写入设置：{text} → {text2}", ModMain.HintType.Green);
					}
					break;
				case EventType.修改变量:
				case EventType.写入变量:
					if (array.Length == 1)
					{
						throw new Exception($"EventType {Type} 需要至少 2 个以 | 分割的参数，例如 VariableName|SomeValue");
					}
					try
					{
						RegistryUtils.Write(string.Format("HKEY_CURRENT_USER\\Software\\{0}", "PCL"), $"CustomEvent{text}", text2);
						if (array.Length == 2)
						{
							ModMain.Hint($"已写入变量：{text} → {text2}", ModMain.HintType.Green);
						}
						break;
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, $"写入变量失败：{text} → {text2}", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
						ProjectData.ClearProjectError();
						break;
					}
				case EventType.加入房间:
					ModBase.RunInUi([SpecialName] () =>
					{
						if (string.IsNullOrWhiteSpace(Arg))
						{
							PageLinkMain.Join();
						}
						else if (EventSafetyConfirm("即将加入第三方联机房间。"))
						{
							PageLinkMain.Join(Arg);
						}
					});
					break;
				case EventType.检查更新:
					ModSecret.UpdateCheckByButton();
					break;
				default:
					ModMain.MyMsgBox("未知的事件类型：" + Conversions.ToString((int)Type) + "\r\n请检查事件类型填写是否正确，或者 PCL 是否为最新版本。", "事件执行失败");
					break;
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, $"事件执行失败（{Type}, {Arg}）", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static string[] GetAbsoluteUrls(string RelativeUrl, EventType Type)
		{
			if (RelativeUrl.StartsWithF("http", ignoreCase: true))
			{
				if (ModBase.RunInUi())
				{
					throw new Exception("能打开联网帮助页面的 MyListItem 必须手动设置 Title、Info 属性！");
				}
				string lastPart;
				try
				{
					lastPart = PathUtils.GetLastPart(RelativeUrl);
					if (!lastPart.EndsWithF(".json", ignoreCase: true))
					{
						throw new Exception("未指向 .json 后缀的文件");
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("联网帮助页面须指向一个帮助 JSON 文件，并在同路径下包含相应 XAML 文件！\r\n例如：\r\n - https://www.baidu.com/test.json（填写这个路径）\r\n - https://www.baidu.com/test.xaml（同时也需要包含这个文件）", innerException);
				}
				string text = ModMain.RequestTaskTempFolder() + lastPart;
				Logger.Info($"转换网络资源：{RelativeUrl} -> {text}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
				try
				{
					ModNet.NetDownloadByClient(RelativeUrl, text);
					ModNet.NetDownloadByClient(RelativeUrl.Replace(".json", ".xaml"), text.Replace(".json", ".xaml"));
				}
				catch (Exception ex2)
				{
					ProjectData.SetProjectError(ex2);
					Exception innerException2 = ex2;
					throw new Exception("下载指定的文件失败！\r\n注意，联网帮助页面须指向一个帮助 JSON 文件，并在同路径下包含相应 XAML 文件！\r\n例如：\r\n - https://www.baidu.com/test.json（填写这个路径）\r\n - https://www.baidu.com/test.xaml（同时也需要包含这个文件）", innerException2);
				}
				RelativeUrl = text;
			}
			RelativeUrl = RelativeUrl.Replace("/", "\\").Lower().TrimStart('\\');
			string text2 = Paths.Base + "PCL";
			ModMain.HelpTryExtract();
			string text3;
			if (RelativeUrl.Contains(":\\"))
			{
				text3 = RelativeUrl;
				Logger.Info($"自定义事件中由绝对路径{Type}：{text3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
			}
			else if (FileUtils.Exists(Paths.Base + "PCL\\" + RelativeUrl))
			{
				text3 = Paths.Base + "PCL\\" + RelativeUrl;
				Logger.Info($"自定义事件中由相对 PCL 文件夹的路径{Type}：{text3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
			}
			else if (FileUtils.Exists(Paths.Base + "PCL\\Help\\" + RelativeUrl))
			{
				text3 = Paths.Base + "PCL\\Help\\" + RelativeUrl;
				text2 = Paths.Base + "PCL\\Help\\";
				Logger.Info($"自定义事件中由相对 PCL 本地帮助文件夹的路径{Type}：{text3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
			}
			else if (Type == EventType.打开帮助 && FileUtils.Exists(ModBase.PathTemp + "Help\\" + RelativeUrl))
			{
				text3 = ModBase.PathTemp + "Help\\" + RelativeUrl;
				text2 = ModBase.PathTemp + "Help\\";
				Logger.Info($"自定义事件中由相对 PCL 自带帮助文件夹的路径{Type}：{text3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
			}
			else
			{
				if (Type != EventType.打开文件 && Type != EventType.执行命令)
				{
					throw new FileNotFoundException("未找到 EventData 指向的本地 xaml 文件：" + RelativeUrl, RelativeUrl);
				}
				text3 = RelativeUrl;
				Logger.Info($"自定义事件中直接{Type}：{text3}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModEvent.vb");
			}
			return new string[2] { text3, text2 };
		}
		private static bool EventSafetyConfirm(string Message)
		{
			if (Settings.Get<bool>("HintCustomCommand"))
			{
				return true;
			}
			switch (ModMain.MyMsgBox(Message + "\r\n请在确认没有安全隐患后再继续。", "执行确认", "继续", "继续且今后不再要求确认", "取消"))
			{
			case 1:
				return true;
			case 2:
				Settings.Set("HintCustomCommand", true);
				return true;
			default:
				return false;
			}
		}
	}
}
