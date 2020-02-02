﻿using System;
using System.Collections.Generic;
using System.IO;

namespace RoWa
{
		public static class LoCa
		{
		public static Dictionary<string, Language> Languages { get; private set; }
		public static Language UserLanguage { get; private set; }
		public static Language DefaultLanguage { get; private set; }

		/// <summary>
		/// Initializing
		/// </summary>
		/// <param name="dir">The directory of the localization files</param>
		/// <param name="defaultlanguage">The key of the default language</param>
		/// <param name="extension">The extension of the localization files</param>
		public static void Init(string dir, string defaultlanguage = "en", string extension = ".txt")
		{
			if (!Directory.Exists(dir))
				throw new LoCaException("Directory '" + dir + "' does not exist!");

			foreach(string fname in Directory.GetFiles(dir))
			{
				if(new FileInfo(fname).Extension == extension)
				{
					Language lang = new Language(fname);
					Languages.Add(lang.key, lang);
				}
			}

			if (!Languages.ContainsKey(defaultlanguage))
				throw new LoCaException("Couldn't find default language '" + defaultlanguage + "'!");

			UserLanguage = Languages[defaultlanguage];
			DefaultLanguage = Languages[defaultlanguage];
		}

		/// <summary>
		/// Sets a default language
		/// </summary>
		/// <param name="key">The key of the language</param>
		public static void SetDefault(string key)
		{
			if (!Languages.ContainsKey(key))
			{
				throw new LoCaException("Couldn't set default language to '" + key + "', because language wasn't found!");
			}
			SetDefault(Languages[key]);
		}

		/// <summary>
		/// Sets a default language
		/// </summary>
		/// <param name="lang">The language object</param>
		public static void SetDefault(Language lang)
		{
			DefaultLanguage = lang;
		}

		/// <summary>
		/// Sets the UserLanguage
		/// </summary>
		/// <param name="key">The key of the language</param>
		public static void SetLanguage(string key)
		{
			if (!Languages.ContainsKey(key))
			{
				throw new LoCaException("Couldn't set user language to '" + key + "', because language wasn't found!");
			}
			SetLanguage(Languages[key]);
		}

		/// <summary>
		/// Sets the UserLanguage
		/// </summary>
		/// <param name="lang">The language object</param>
		public static void SetLanguage(Language lang)
		{
			UserLanguage = lang;
		}

		/// <summary>
		/// Translates the key
		/// </summary>
		/// <param name="key">The key to translate</param>
		public static void Trans(string key)
		{
			if(UserLanguage == null)
			{
				throw new LoCaException("No UserLanguage set!");
			}
			UserLanguage.Trans(key);
		}

		/// <summary>
		/// Translates the key
		/// </summary>
		/// <param name="key">The key to translate</param>
		public static void Translate(string key)
		{
			Trans(key);
		}

		public class Language
		{
			public string key { get; private set; }
			public string english { get; private set; }
			public string local { get; private set; }
			public string author { get; private set; }
			public string version { get; private set; }
			public Dictionary<string,string> dict { get; private set; }

			public Language(string fname)
			{
				if (!File.Exists(fname))
					throw new LoCaException("Languagefile '" + fname + "' couldn't be found!");

				int lcount = 0;
				foreach(string fline in File.ReadAllLines(fname))
				{
					lcount++;
					if (fline.StartsWith("language_key="))
						key = fline.Replace("language_key=", "");
					else if (fline.StartsWith("language_english"))
						english = fline.Replace("language_english", "");
					else if (fline.StartsWith("language_local"))
						local = fline.Replace("language_local", "");
					else if (fline.StartsWith("language_author"))
						author = fline.Replace("language_author", "");
					else if (fline.StartsWith("language_version"))
						version = fline.Replace("language_version", "");
					else if (fline.StartsWith("#") || fline == "" || !fline.Contains("="))
					{
						//Do nothing...
					}
					else
					{
						string k = fline.Split('=')[0];
						string v = fline.Replace(k + "=", "");
						if (dict.ContainsKey(k))
							throw new LoCaException("Error on line " + lcount + ": Key '" + k + "' does already exist inside file '" + fname + "'!");

						dict.Add(k, v);
					}
				}
				if(key == null)
					throw new LoCaException("Error inside file '" + fname + "': Key 'language_key' isn't set!");
				if(english == null)
					throw new LoCaException("Error inside file '" + fname + "': Key 'language_english' isn't set!");
				if (local == null)
					throw new LoCaException("Error inside file '" + fname + "': Key 'language_local' isn't set!");
			}

			public string Trans(string key)
			{
				if (dict.ContainsKey(key))
					return dict[key];

				return "{" + key + "}";
			}

			public string Translate(string key)
			{
				return Trans(key);
			}
			}

			public class LoCaException : Exception
			{
				public LoCaException()
				{
				}

				public LoCaException(string message)
					: base(message)
				{
				}

				public LoCaException(string message, Exception inner)
					: base(message, inner)
				{
				}
			}
		}
}
