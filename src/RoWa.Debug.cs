using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace RoWa
{
	internal static class Debug
	{
		//internal static bool Enabled = true;
		static string logdir = Application.StartupPath + @"\Data\Logs";

		static string dstring()
		{
			string s = "";
			if(DateTime.Now.Day < 10)
			{
				s = "0" + DateTime.Now.Day;
			}
			else
			{
				s = DateTime.Now.Day + "";
			}
			if(DateTime.Now.Month < 10)
			{
				s += "0" + DateTime.Now.Month;
			}
			else
			{
				s += "" + DateTime.Now.Month;
			}
			s += DateTime.Now.Year;
			return s;
		}

		internal static void Log(string msg)
		{
			if (!Settings.DebugMode)
				return;

			Directory.CreateDirectory(logdir);
			string logfile = logdir + @"\" + dstring() + ".log";

			using (StreamWriter sw = new StreamWriter(logfile, true))
			{
				sw.WriteLine(DateTime.Now + ": [INFO] " + msg);
			}
		}

		internal static void ExceptionLog(Exception ex)
		{
			if (!Settings.DebugMode)
				return;

			Directory.CreateDirectory(logdir);
			string logfile = logdir + @"\" + dstring() + ".log";

			using (StreamWriter sw = new StreamWriter(logfile,true))
			{
				sw.WriteLine(DateTime.Now + ": [EXCEPTION] " + ex.Message);
			}
			throw ex;
		}

		internal static void WarningLog(string msg, bool showPopup = false)
		{
			if (!Settings.DebugMode)
				return;

			Directory.CreateDirectory(logdir);
			string logfile = logdir + @"\" + dstring() + ".log";

			using (StreamWriter sw = new StreamWriter(logfile, true))
			{
				sw.WriteLine(DateTime.Now + ": [WARNING] " + msg);
			}

			if (showPopup)
			{
				MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		internal static void EmptyLog()
		{
			if (!Settings.DebugMode)
				return;

			Directory.CreateDirectory(logdir);
			string logfile = logdir + @"\" + dstring() + ".log";

			using (StreamWriter sw = new StreamWriter(logfile, true))
			{
				sw.WriteLine("");
			}
		}
	}
}
