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
		/// Checks if the settings exist or not
		/// </summary>
		/// <returns>true if the settings exist, false if not</returns>
		public static bool Exists()
		{
			if (File.Exists(Location))
				return true;
			return false;
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
		}

		/// <summary>
		/// Removes the setting from the file
		/// </summary>
		/// <param name="key">The key of the setting</param>
		public static void RemoveValue(string key)
		{
			if (file == null) { Load(); }
			file.Remove(key);
		}

		/// <summary>
		/// Returns the value of a setting with type T
		/// </summary>
		/// <typeparam name="T">The type of the setting</typeparam>
		/// <param name="key">The key of the setting</param>
		/// <returns>The value of the setting</returns>
		public static T GetValue<T>(string key)
		{
			if (file == null) { Load(); }
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
			if (file == null) { Load(); }
			try
			{
				return (T)Convert.ChangeType(file.Get(key), typeof(T));
			}
			catch (Exception ex)
			{
				if (ex.HResult == -2146233088)
					return defaultvalue;
				throw ex;
			}
		}

		public class SaveFile
		{
			readonly string Location;

			Dictionary<string, string> Dict { get; set; }
			public SaveFile(string fname)
			{
				Location = fname;
				Load();
			}

			public void Set(string key, string value, bool autosave = true)
			{
				if (Dict.ContainsKey(key))
					Dict[key] = value;
				else
					Dict.Add(key, value);

				if (autosave)
					Save();
			}

			public string Get(string key)
			{
				if (!Dict.ContainsKey(key))
					throw new KeyNotFoundException("Couldn't find key '" + key + "' inside the settings!");

				return Dict[key];
			}

			public void Remove(string key, bool autosave = true)
			{
				Dict.Remove(key);

				if (autosave)
					Save();
			}

			public void Save()
			{
				using (StreamWriter sw = new StreamWriter(Location, false))
				{
					foreach (KeyValuePair<string, string> kvp in Dict)
					{
						sw.WriteLine(kvp.Key + "=" + kvp.Value);
					}
				}
			}

			public void Load(bool createFile = false)
			{
				Dict = new Dictionary<string, string>();
				if (!File.Exists(Location) && createFile)
				{
					File.Create(Location);
					return; 
				}else if(!File.Exists(Location) && !createFile)
				{
					return;
				}

				foreach (string fline in File.ReadAllLines(Location))
				{
					if (fline.Contains("="))
					{
						string k = fline.Split('=')[0];
						string v = fline.Replace(k + "=", "");
						if (!Dict.ContainsKey(k))
							Dict.Add(k, v);
					}
				}
			}
		}

		[Serializable]
		public class KeyNotFoundException : Exception
		{
			public KeyNotFoundException(string message) : base(message)
			{
				//Use this if you use the RoWa.Debug class
				//Debug.ExceptionLog(this);
			}

			public KeyNotFoundException(string message, Exception innerException) : base(message, innerException)
			{
				//Use this if you use the RoWa.Debug class
				//Debug.ExceptionLog(this);
			}

			public KeyNotFoundException()
			{
				//Use this if you use the RoWa.Debug class
				//Debug.ExceptionLog(this);
			}

			protected KeyNotFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
			{
			}
		}

	}

}
