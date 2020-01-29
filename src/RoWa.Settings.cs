using System;
using System.Collections.Generic;
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
		static SaveHandler.SaveFile file;

		/// <summary>
		/// Loads the Settingsfile
		/// </summary>
		public static void Load()
		{
			file = new SaveHandler.SaveFile(Location);
		}

		/// <summary>
		/// Sets a value to the settings file, will load the settingsfile if not loaded already
		/// </summary>
		/// <param name="key">The key of the setting</param>
		/// <param name="value">The value of the setting</param>
		public static void SetValue(string key, dynamic value)
		{
			if (file == null) { Load(); }
			file.Add(key, value);
			file.Save();
		}

		/// <summary>
		/// Removes the setting from the file
		/// </summary>
		/// <param name="key">The key of the setting</param>
		public static void RemoveValue(string key)
		{
			file.Remove(key);
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
	}

}
