using Android.App;
using Android.Content.Res;
using Android.Support.V4.OS;
using Android.Util;
using Java.IO;
using System;
using System.Collections.Generic;
using System.IO;

/*
 * Modified Version of RoWa.LoCa.cs from github.com/daredloco/csharp-collection version 1.0
 */
namespace RoWa
{
	namespace Xamarin
	{
		public static class LoCa
		{
			public static Dictionary<string, Language> Languages { get; private set; }
			public static Language UserLanguage { get; private set; }
			public static Language DefaultLanguage { get; private set; }

			static Activity activity;

			/// <summary>
			/// Initializing
			/// </summary>
			/// <param name="asset">The asset folder of the localization files</param>
			/// <param name="defaultlanguage">The key of the default language</param>
			/// <param name="extension">The extension of the localization files</param>
			public static void Init(string asset, Activity a, string defaultlanguage = "en")
			{
				activity = a;
				Languages = new Dictionary<string, Language>();
				foreach (string fname in activity.Assets.List(asset))
				{
						Language lang = new Language(asset + "/" + fname);
						Languages.Add(lang.key, lang);
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
			/// Sets the language to the device language (en-US) or the language prefix (en) or to nothing if not found
			/// </summary>
			public static void SetUserLanguage()
			{
				LocaleListCompat lst = ConfigurationCompat.GetLocales(Resources.System.Configuration);
				if (Languages.ContainsKey(lst.ToLanguageTags())){
					SetLanguage(Languages[lst.ToLanguageTags()]);
					Log.Debug("LoCa", "Set language to " + lst.ToLanguageTags());
					return;
				}
				if (Languages.ContainsKey(lst.ToLanguageTags().Split('-')[0]))
				{
					SetLanguage(Languages[lst.ToLanguageTags().Split('-')[0]]);
					Log.Debug("LoCa", "Set language to " + lst.ToLanguageTags().Split('-')[0]);
					return;
				}
			}

			/// <summary>
			/// Translates the key
			/// </summary>
			/// <param name="key">The key to translate</param>
			public static string Trans(string key)
			{
				if (UserLanguage == null)
				{
					throw new LoCaException("No UserLanguage set!");
				}
				return UserLanguage.Trans(key);
			}

			/// <summary>
			/// Translates the key and replaces the values
			/// </summary>
			/// <param name="key">The key to translate</param>
			/// <param name="values">A list of KeyValuePairs where the 'key' will be replaced with the 'value'</param>
			/// <returns>The translated string</returns>
			public static string Trans(string key, params KeyValuePair<string,string>[] values)
			{
				string transstr = Trans(key);
				foreach(KeyValuePair<string,string> value in values)
				{
					transstr = transstr.Replace(value.Key, value.Value);
				}
				return transstr;
			}

			/// <summary>
			/// Translates the key and replaces the values
			/// </summary>
			/// <param name="key">The key to translate</param>
			/// <param name="values">A Dictionary where the 'key' will be replaced with the 'value'</param>
			/// <returns>The translated string</returns>
			public static string Trans(string key, Dictionary<string,string> values)
			{
				string transstr = Trans(key);
				foreach(KeyValuePair<string,string> value in values)
				{
					transstr = transstr.Replace(value.Key, value.Value);
				}
				return transstr;
			}

			/// <summary>
			/// Translates the key
			/// </summary>
			/// <param name="key">The key to translate</param>
			public static string Translate(string key)
			{
				return Trans(key);
			}

			public class Language
			{
				public string key { get; private set; }
				public string english { get; private set; }
				public string local { get; private set; }
				public string author { get; private set; }
				public string version { get; private set; }
				public Dictionary<string, string> dict { get; private set; }

				public Language(string fname)
				{
					BufferedReader reader = null;
					dict = new Dictionary<string, string>();
					using(reader = new BufferedReader(new InputStreamReader(activity.Assets.Open(fname))))
					{
						string fline;
						int lcount = 0;
						while((fline = reader.ReadLine()) != null)
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
					}
					if (key == null)
						throw new LoCaException("Error inside file '" + fname + "': Key 'language_key' isn't set!");
					if (english == null)
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
}
