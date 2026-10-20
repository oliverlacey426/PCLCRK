// PCL

namespace PCL
{
	public class PclLogger : FileLogger
	{
		[SpecialName]
		private bool _STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger;
		[SpecialName]
		private StaticLocalInitFlag _STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger_Init;
		public override string Format(string Text, LogLevel Level, string FilePath, Exception Ex)
		{
			Text = base.Format(Text, Level, FilePath, Ex);
			Text = ModMinecraft.FilterUserName(ModMinecraft.FilterAccessToken(Text, '*'), '*');
			return Text;
		}
		public override void HandleBehavior(string RawMessage, string FormattedMessage, LogBehavior Behavior, Exception Ex)
		{
			if (ModBase.IsProgramEnding)
			{
				return;
			}
			base.HandleBehavior(RawMessage, FormattedMessage, Behavior, Ex);
			string raw = ((Ex == null) ? RawMessage : (((RawMessage == null) ? "" : $"{RawMessage}：") + Ex.GetDisplay(multiline: false)));
			raw = ModMinecraft.FilterUserName(ModMinecraft.FilterAccessToken(raw, '*'), '*');
			string raw2 = ((Ex == null) ? RawMessage : (((RawMessage == null) ? "" : $"{RawMessage}：") + Ex.GetDisplay(multiline: true)));
			raw2 = ModMinecraft.FilterUserName(ModMinecraft.FilterAccessToken(raw2, '*'), '*');
			switch (Behavior)
			{
			case LogBehavior.ToastIfDebug:
				if (ModBase.ModeDebug)
				{
					ModMain.Hint("[调试模式] " + raw, ModMain.HintType.Blue, Log: false);
				}
				break;
			case LogBehavior.Toast:
				ModMain.Hint(raw, ModMain.HintType.Red, Log: false);
				break;
			case LogBehavior.Alert:
				ModMain.MyMsgBox(raw2, "错误", "确定", "", "", IsWarn: true);
				break;
			case LogBehavior.AlertThenFeedback:
				if (PageSetupSystem.IsLauncherNewest() == true)
				{
					if (ModMain.MyMsgBox(raw2 + "\r\n\r\n是否反馈此问题？如果不反馈，这个问题可能永远无法得到解决！", "错误", "反馈", "取消", "", IsWarn: true) == 1)
					{
						ModMain.Feedback(ShowMsgbox: false, ForceOpenLog: true);
					}
				}
				else
				{
					ModMain.MyMsgBox(raw2 + "\r\n\r\n将 PCL 更新至最新版或许可以解决这个问题……", "错误", "确定", "", "", IsWarn: true);
				}
				break;
			case LogBehavior.AlertThenCrash:
			{
				if (_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger_Init == null)
				{
					Interlocked.CompareExchange(ref _STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger_Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger_Init, ref lockTaken);
					if (_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger_Init.State == 0)
					{
						_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger_Init.State = 2;
						_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger = true;
					}
					else if (_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger_Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger_Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger_Init);
					}
				}
				if (_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger)
				{
					_STATIC_HandleBehavior_2041EE1181E11280D1_FirstTrigger = false;
					if (PageSetupSystem.IsLauncherNewest() == true)
					{
						if (Interaction.MsgBox(raw2 + "\r\n\r\n是否反馈此问题？如果不反馈，这个问题可能永远无法得到解决！", MsgBoxStyle.YesNo | MsgBoxStyle.Critical, "错误") == MsgBoxResult.Yes)
						{
							ModMain.Feedback(ShowMsgbox: false, ForceOpenLog: true);
						}
					}
					else
					{
						Interaction.MsgBox(raw2 + "\r\n\r\n将 PCL 更新至最新版或许可以解决这个问题……", MsgBoxStyle.Critical, "错误");
					}
				}
				else
				{
					Thread.Sleep(2000);
				}
				FormMain.EndProgramForce(ModBase.ProcessReturnValues.Exception);
				break;
			}
			}
			if (Behavior >= LogBehavior.Toast)
			{
				ModMain.Telemetry("错误日志", "Exception", raw2);
			}
		}
	}
}
