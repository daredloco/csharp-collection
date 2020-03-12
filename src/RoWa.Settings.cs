using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoWa
{
	public static class Settings
	{
		/// <summary>
		/// The location of the settings file
		/// </summary>
		public static string Location = "settings.cfg";
		static SaveFile file;

		/// <summary>
		/// Loads the Settingsfile
		/// </summary>
		public static void Load()
		{
			file = new SaveFile(Location);
		}

		/// <summary>
		/// Sets a value to the settings file, will load the settingsfile if not loaded already
		/// </summary>
		/// <param name="key">The key of the setting</param>
		/// <param name="value">The value of the setting</param>
		public static void SetValue(string key, dynamic value)
		{
			if (file == null) { Load(); }
			file.Set(key, value);
			file.Save();
		}

		/// <summary>
		/// Removes the setting from the file
		/// </summary>
		/// <param name="key">The key of the setting</param>
		public static void RemoveValue(string key)
		{
			file.Remove(key);
			file.Save();
		}

		/// <summary>
		/// Returns the value of the setting
		/// </summary>
		/// <param name="key">The key of the setting</param>
		/// <returns>The value of the setting</returns>
		public static dynamic GetValue(string key)
		{
			return file.Get(key);
		}

		public class SaveFile
		{
			string Location;

			Dictionary<string, string> dict { get; set; }
			public SaveFile(string fname)
			{
				Location = fname;
				Load();
			}

			public void Set(string key, string value, bool autosave = true)
			{
				if (dict.ContainsKey(key))
					dict[key] = value;
				else
					dict.Add(key, value);

				if (autosave)
					Save();
			}

			public string Get(string key)
			{
				if (!dict.ContainsKey(key))
					throw new Exception("Couldn't find key '" + key + "' inside the settings!");

				return dict[key];
			}

			public void Remove(string key)
			{
				dict.Remove(key);
			}

			public void Save()
			{
				using (StreamWriter sw = new StreamWriter(Location, false))
				{
					foreach (KeyValuePair<string, string> kvp in dict)
					{
						sw.WriteLine(kvp.Key + "=" + kvp.Value);
					}
				}
			}

			public void Load()
			{
				if (File.Exists(Location))
					throw new Exception("Couldn't find the file '" + Location + "'!");

				dict = new Dictionary<string, string>();
				foreach (string fline in File.ReadAllLines(Location))
				{
					if (fline.Contains("="))
					{
						string k = fline.Split('=')[0];
						string v = fline.Replace(k + "=", "");
						if (!dict.ContainsKey(k))
							dict.Add(k, v);
					}
				}
			}
		}
	}

}
