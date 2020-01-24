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
		internal static bool Enabled = true;
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

		/// <summary>
		/// Writes an info message to the log
		/// </summary>
		/// <param name="msg">The message to be written to the log</param>
		internal static void Log(string msg)
		{
			if (!Enabled)
				return;

			Directory.CreateDirectory(logdir);
			string logfile = logdir + @"\" + dstring() + ".log";

			using (StreamWriter sw = new StreamWriter(logfile, true))
			{
				sw.WriteLine(DateTime.Now + ": [INFO] " + msg);
			}
		}

		/// <summary>
		/// Writes an exception to the log and throws the exception afterwards
		/// </summary>
		/// <param name="ex">The exception</param>
		internal static void ExceptionLog(Exception ex)
		{
			if (!Enabled)
				return;

			Directory.CreateDirectory(logdir);
			string logfile = logdir + @"\" + dstring() + ".log";

			using (StreamWriter sw = new StreamWriter(logfile,true))
			{
				sw.WriteLine(DateTime.Now + ": [EXCEPTION] " + ex.Message);
			}
			throw ex;
		}

		/// <summary>
		/// Writes a warning to the log and shows a popup if showPopup is true
		/// </summary>
		/// <param name="msg">The message</param>
		/// <param name="showPopup">If true, it will show a popup</param>
		internal static void WarningLog(string msg, bool showPopup = false)
		{
			if (!Enabled)
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

		/// <summary>
		/// Sends an empty line to the log
		/// </summary>
		internal static void EmptyLog()
		{
			if (!Enabled)
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
