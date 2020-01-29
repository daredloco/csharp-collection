using System;
using System.Collections.Generic;
using System.IO;

namespace RoWa
{
	public static class SaveHandler
	{
		public static string SaveDirectory = "Savefiles";
		public static string SaveExtension = ".save";

		public class SaveFile
		{
			public string Name { get; set; }
			public string Location { get; set; }
			Dictionary<string, dynamic> Data { get; set; }

			public SaveFile(string filename, string name = "", bool autoload = true)
			{
				if(name == "") { name = filename; }
				Location = Path.Combine(SaveDirectory,filename,SaveExtension);
				Name = name;
				Data = new Dictionary<string, dynamic>();
				if (autoload) { Load(); }
			}

			public void Add(string key, dynamic value, bool overwrite = true)
			{
				if (!overwrite)
					if (Data.ContainsKey(key))
						throw new Exception("Data '" + key + "' from Savefile '" + Name +"' already exists!");

				//Replace values with database values
				Type valuetype = value.GetType();

				if (valuetype == typeof(Game.Vector2))
				{
					value = "vector2:" + value.X + "/" + value.Y;
				}
				else if (valuetype == typeof(Game.Vector3))
				{
					value = "vector3:" + value.X + "/" + value.Y + "/" + value.Z;
				}
				else if (valuetype == typeof(int) || valuetype == typeof(bool) || valuetype == typeof(double) || valuetype == typeof(float) || valuetype == typeof(string))
				{
					//Ignore
				}
				else
				{
					throw new Exception("Key '" + key + "' has an illegal format and can't be added to '" + Location + "'!");
				}

				if (Data.ContainsKey(key))
				{
					Data[key] = value;
				}
				else
				{
					Data.Add(key, value);
				}
			}

			public dynamic Get(dynamic key)
			{
				if (Data.ContainsKey(key))
				{
					if (Data[key].GetType() == typeof(string))
					{
						if (Data[key].StartsWith("vector2:"))
						{
							string val = Data[key].Replace("vector2:", "");
							return Game.Vector2.Parse(val);
						}
						else if (Data[key].StartsWith("vector3"))
						{
							string val = Data[key].Replace("vector3:", "");
							return Game.Vector3.Parse(val);
						}
					}
					return Data[key];
				}
				throw new Exception("Data '" + key + "' does not exists inside Save '" + Name + "'!");
			}

			public void Remove(dynamic key)
			{
				if (!Data.ContainsKey(key))
				{
					throw new Exception("Data '" + key + "' does not exists inside Save '" + Name + "'!");
				}
				Data.Remove(key);
			}

			public void Save()
			{
				using (StreamWriter sw = new StreamWriter(Location, false))
				{
					foreach (KeyValuePair<string, dynamic> kvp in Data)
					{
						sw.WriteLine(kvp.Key + "=" + kvp.Value);
					}
				}
			}

			public void Load()
			{
				if (!File.Exists(Location))
					return;

				Data.Clear();
				foreach(string fline in File.ReadAllLines(Location))
				{
					string key = fline.Split('=')[0];
					dynamic val = fline.Replace(key + "=","");
					Data.Add(key, val);
				}			
			}
		}
	}
}
