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
		/// <typeparam name="T">The type of the value</typeparam>
		/// <param name="key">The key of the setting</param>
		/// <param name="value">The value of the setting</param>
		public static void SetValue<T>(string key, T value)
		{
			if(file == null) { Load(); }
			file.Set(key, value.ToString());
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
		/// Returns the value of a setting with type T
		/// </summary>
		/// <typeparam name="T">The type of the setting</typeparam>
		/// <param name="key">The key of the setting</param>
		/// <returns>The value of the setting</returns>
		public static T GetValue<T>(string key)
		{
			return (T)Convert.ChangeType(file.Get(key),typeof(T));
		}

		/// <summary>
		/// Returns the value of a setting with type T
		/// </summary>
		/// <typeparam name="T">The type of the setting</typeparam>
		/// <param name="key">The key of the setting</param>
		/// <param name="defaultvalue">The default value of type T</param>
		/// <returns>The value of the setting</returns>
		public static T GetValue<T>(string key, T defaultvalue)
		{
			try
			{
				return (T)Convert.ChangeType(file.Get(key), typeof(T));
			}
			catch (Exception ex)
			{
				if (ex.Message == "Couldn't find key '" + key + "' inside the settings!")
					return defaultvalue;
				throw ex;
			}
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
