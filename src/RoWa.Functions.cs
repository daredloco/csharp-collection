using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace RoWa
{
	internal static class Functions
	{
		/// <summary>
		/// Checks if the actual user has Admin Rights
		/// </summary>
		/// <returns>True if has Adminrights and false if not</returns>
		[DllImport("shell32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsUserAnAdmin();

		internal enum HashType
		{
			SHA256,
			SHA512
		}

		/// <summary>
		/// Converts a String to a Hash
		/// </summary>
		/// <param name="str">The string to convert</param>
		/// <param name="hashtype">The HashType (SHA256, SHA512)</param>
		/// <returns>A string of the hash</returns>
		internal static string GetHash(string str, HashType hashtype = HashType.SHA256)
		{
			byte[] hash;
			if(hashtype == HashType.SHA256)
			{
				SHA256 sha = SHA256Managed.Create();
				byte[] bytes = Encoding.UTF8.GetBytes(str);
				hash = sha.ComputeHash(bytes);
				
			}
			else
			{
				SHA512 sha = SHA512Managed.Create();
				byte[] bytes = Encoding.UTF8.GetBytes(str);
				hash = sha.ComputeHash(bytes);
			}
			StringBuilder result = new StringBuilder();
			for(int i = 0; i < hash.Length; i++)
			{
				result.Append(hash[i].ToString("X2"));
			}
			return result.ToString();
		}

		/// <summary>
		/// Convers a decimal value to a currency value
		/// </summary>
		/// <param name="d">the decimal value</param>
		/// <param name="currency">the currency prefix</param>
		/// <returns>A string containing the currency value</returns>
		internal static string NumberToCurrency(decimal d, string currency = "$", bool prefixBefore = true)
		{
			d = Math.Round(d, 2);
			string prefix = "";
			string suffix = "";
			if (prefixBefore)
				prefix = currency;
			else
				suffix = " " + currency;

			bool isMinus = false;
			if (d < 0) { isMinus = true; }
			d = Math.Abs(d);
			string str = d + "";
			if (str.Contains(","))
			{
				str = str.Replace(",", ".");
			}
			else if (str.Contains("."))
			{
				//INGORE BECAUSE IT HAS ALREADY THE CORRECT FORMAT
			}
			else
			{
				if (isMinus)
				{
					str = "-" + prefix + str + ".00" + suffix;
				}
				else
				{
					str = prefix + str + ".00" + suffix;
				}
				return str;
			}
			string before = str.Split('.')[0];
			string after = str.Split('.')[1];
			if (after.Length < 2)
			{
				after += "0";
			}

			if (isMinus)
			{
				str = "-" + prefix + before + "." + after + suffix;
			}
			else
			{
				str = prefix + before + "." + after + suffix;
			}
			return str;
		}

		/// <summary>
		/// Convers a double value to a currency value
		/// </summary>
		/// <param name="d">the double value</param>
		/// <param name="currency">the currency prefix</param>
		/// <returns>A string containing the currency value</returns>
		internal static string NumberToCurrency(double d, string currency = "$", bool prefixBefore = true)
		{
			d = Math.Round(d, 2);
			string prefix = "";
			string suffix = "";
			if (prefixBefore)
				prefix = currency;
			else
				suffix = " " + currency;

			bool isMinus = false;
			if (d < 0) { isMinus = true; }
			d = Math.Abs(d);
			string str = d + "";
			if (str.Contains(","))
			{
				str = str.Replace(",", ".");
			}
			else if (str.Contains("."))
			{
				//INGORE BECAUSE IT HAS ALREADY THE CORRECT FORMAT
			}
			else
			{
				if (isMinus)
				{
					str = "-" + prefix + str + ".00" + suffix;
				}
				else
				{
					str = prefix + str + ".00" + suffix;
				}
				return str;
			}
			string before = str.Split('.')[0];
			string after = str.Split('.')[1];
			if (after.Length < 2)
			{
				after += "0";
			}

			if (isMinus)
			{
				str = "-" + prefix + before + "." + after + suffix;
			}
			else
			{
				str = prefix + before + "." + after + suffix;
			}
			return str;
		}

		/// <summary>
		/// Convers a double value to a currency value
		/// </summary>
		/// <param name="d">the double value</param>
		/// <param name="currency">the currency prefix</param>
		/// <returns>A string containing the currency value</returns>
		[Obsolete("Use NumberToCurrency() instead.")]
		internal static string DoubleToCurrency(double d, string currency = "$", bool prefixBefore = true)
		{
			d = Math.Round(d, 2);
			string prefix = "";
			string suffix = "";
			if (prefixBefore)
				prefix = currency;
			else
				suffix = " " + currency;

			bool isMinus = false;
			if (d < 0) { isMinus = true; }
			d = Math.Abs(d);
			string str = d + "";
			if (str.Contains(","))
			{
				str = str.Replace(",", ".");
			}
			else if (str.Contains("."))
			{
				//INGORE BECAUSE IT HAS ALREADY THE CORRECT FORMAT
			}
			else
			{
				if (isMinus)
				{
					str = "-" + prefix + str + ".00" + suffix;
				}
				else
				{
					str = prefix + str + ".00" + suffix;
				}
				return str;
			}
			string before = str.Split('.')[0];
			string after = str.Split('.')[1];
			if (after.Length < 2)
			{
				after += "0";
			}

			if (isMinus)
			{
				str = "-" + prefix + before + "." + after + suffix;
			}
			else
			{
				str = prefix + before + "." + after + suffix;
			}
			return str;
		}

		/// <summary>
		/// Uses the DoubleToCurrency system to create a Value with a prefix/suffix
		/// </summary>
		/// <param name="value">the double value</param>
		/// <param name="prefix">the prefix</param>
		/// <param name="isbefore">Is it a prefix (true) or a suffix (false)?</param>
		/// <returns></returns>
		internal static string PrefixValue(double value, string prefix, bool isbefore = false)
		{
			if (isbefore) { return prefix + NumberToCurrency(value, ""); }
			return NumberToCurrency(value, "") + prefix;
		}

		/// <summary>
		/// Uses the DoubleToCurrency system to create a Value with a prefix/suffix
		/// </summary>
		/// <param name="value">the double value</param>
		/// <param name="prefix">the prefix</param>
		/// <param name="isbefore">Is it a prefix (true) or a suffix (false)?</param>
		/// <returns></returns>
		internal static string PrefixValue(decimal value, string prefix, bool isbefore = false)
		{
			if (isbefore) { return prefix + NumberToCurrency(value, ""); }
			return NumberToCurrency(value, "") + prefix;
		}

		/// <summary>
		/// Returns the Week of a specific DateTime and Culture
		/// </summary>
		/// <param name="dt">The datetime</param>
		/// <param name="ci">The culture</param>
		/// <returns>An integer value with the week</returns>
		public static int GetWeekOfDate(DateTime dt, CultureInfo ci)
		{
			Calendar calendar = ci.Calendar;
			CalendarWeekRule calendarweekrule = ci.DateTimeFormat.CalendarWeekRule;
			DayOfWeek firstdayofweek = ci.DateTimeFormat.FirstDayOfWeek;
			return calendar.GetWeekOfYear(dt, calendarweekrule, firstdayofweek);
		}

		/// <summary>
		/// Returns the days of the week as DateTime objects from the actual week and the current culture
		/// </summary>
		/// <returns>An array of DateTime objects representing the days of the week</returns>
		public static DateTime[] GetDaysOfWeek()
		{
			return GetDaysOfWeek(DateTime.Now);
		}

		/// <summary>
		/// Returns the days of the week as DateTime objects from a DateTime object and the current culture
		/// </summary>
		/// <param name="dt">The datetime object</param>
		/// <returns>An array of DateTime objects representing the days of the week</returns>
		public static DateTime[] GetDaysOfWeek(DateTime dt)
		{
			return GetDaysOfWeek(dt, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Returns the days of the week as DateTime objects from a DateTime object and CultureInfo
		/// </summary>
		/// <param name="dt">The datetime object</param>
		/// <param name="ci">The cultureinfo</param>
		/// <returns>An array of DateTime objects representing the days of the week</returns>
		public static DateTime[] GetDaysOfWeek(DateTime dt, CultureInfo ci)
		{
			Calendar calendar = ci.Calendar;
			DayOfWeek firstdayofweek = ci.DateTimeFormat.FirstDayOfWeek;
			DayOfWeek today = calendar.GetDayOfWeek(dt);
			List<DateTime> days = new List<DateTime>();
			int firstday = (int)firstdayofweek;
			int daynow = (int)today;
			for (int i = 0; i <= 6; i++)
			{
				DateTime d = dt.AddDays((firstday - daynow) + i);
				days.Add(d.Date);
			}
			return days.ToArray();
		}

		/// <summary>
		/// Do not use this function at the moment!
		/// </summary>
		/// <param name="plainText"></param>
		/// <param name="Key"></param>
		/// <param name="IV"></param>
		/// <returns></returns>
		internal static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
		{
			if (plainText == null || plainText.Length <= 0)
				throw new ArgumentNullException(nameof(plainText));
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException(nameof(Key));
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException(nameof(IV));
			byte[] encrypted;

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}
			return encrypted;

		}

		/// <summary>
		/// Do not use this function at the moment!
		/// </summary>
		/// <param name="cipherText"></param>
		/// <param name="Key"></param>
		/// <param name="IV"></param>
		/// <returns></returns>
		internal static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
		{
			if (cipherText == null || cipherText.Length <= 0)
				throw new ArgumentNullException(nameof(cipherText));
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException(nameof(Key));
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException(nameof(IV));

			string plaintext = null;

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}

			}

			return plaintext;

		}

		/// <summary>
		/// Resizes the control
		/// </summary>
		/// <param name="control">The control to resize</param>
		internal static void ResizeControl(object control)
		{
			if(control.GetType() == typeof(ComboBox))
			{
				ComboBox c = (ComboBox)control;
				int maxwidth = 0;
				int tmp;

				foreach(var obj in c.Items)
				{
					tmp = TextRenderer.MeasureText(obj.ToString(), c.Font).Width;
					if (tmp > maxwidth)
						maxwidth = tmp;
				}
				c.Width = maxwidth;
				return;
			}

			if(control.GetType() == typeof(Button))
			{
				Button b = (Button)control;
				int width = TextRenderer.MeasureText(b.Text,b.Font).Width;
				b.Width = width + 40;
				return;
			}
			throw new Exception("Invalid Control type!");
		}

		/// <summary>
		/// Shuffles a list from type T
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="list">The list to shuffle</param>
		public static void Shuffle<T>(this IList<T> list)
		{
			Random rng = new Random();
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}

		/// <summary>
		/// Removes 'amount' bytes from an array 'bytes' from the front 'fromFront' or back
		/// </summary>
		/// <param name="bytes">The source bytes</param>
		/// <param name="amount">The amount of bytes to be removed</param>
		/// <param name="fromFront">If true will remove from the front, if false from the back</param>
		/// <returns></returns>
		public static byte[] SliceBytes(byte[] bytes, int amount, bool fromFront = true)
		{
			if (amount <= 0)
				throw new Exception("Invalid amount '" + amount + "', needs to be bigger than 0!");

			if (amount >= bytes.Length)
				throw new Exception("Invalid amount '" + amount + "', needs to be smaller than '" + amount + "'");

			if (fromFront)
			{
				var sourceStartIndex = amount;
				var destinationStartIndex = 0;
				var destination = new byte[bytes.Length - amount];
				Array.Copy(bytes, sourceStartIndex, destination, destinationStartIndex, destination.Length);
				return destination;
			}
			else
			{
				var sourceStartIndex = 0;
				var destinationStartIndex = 0;
				var destination = new byte[bytes.Length - amount];
				Array.Copy(bytes, sourceStartIndex, destination, destinationStartIndex, destination.Length);
				return destination;
			}			
		}

		/// <summary>
		/// Converts a value in degrees to radiants
		/// </summary>
		/// <param name="degrees">Degrees</param>
		/// <returns>Radiants</returns>
		public static double DegreesToRadians(double degrees)
		{
			return degrees * Math.PI / 180;
		}

		/// <summary>
		/// Get the distance of two coordinates
		/// </summary>
		/// <param name="c1">First coordinate</param>
		/// <param name="c2">Second coordinate</param>
		/// <returns>The distance in KM</returns>
		public static decimal GetDistance(Coordinates c1, Coordinates c2, decimal earthRadiusKm = 6371)
		{
			var dLat = DegreesToRadians(lat2 - lat1);
			var dLon = DegreesToRadians(lon2 - lon1);

			lat1 = DegreesToRadians(lat1);
			lat2 = DegreesToRadians(lat2);

			var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
					Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);

			var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			return earthRadiusKm * c;
		}

		/// <summary>
		/// Get the distance of two coordinates
		/// </summary>
		/// <param name="lat1">Latitude of the first coordinate</param>
		/// <param name="long1">Longitude of the first coordinate</param>
		/// <param name="lat2">Latitude of the second coordinate</param>
		/// <param name="long2">Longitude of the second coordinate</param>
		/// <returns>The distance in KM</returns>
		public static decimal GetDistance(decimal lat1, decimal long1, decimal lat2, decimal long2, decimal earthRadiusKm = 6371)
		{
			return GetDistance(new Coordinates(lat1, long1), new Coordinates(lat2, long2), earthRadiusKm);
		}

		/// <summary>
		/// Coordinates Object
		/// </summary>
		public class Coordinates : IComparable<Coordinates>
		{
			public decimal Latitude { get; set; }
			public decimal Longitude { get; set; }

			public Coordinates()
			{
				Latitude = 0;
				Longitude = 0;
			}

			public Coordinates(decimal latitude, decimal longitude)
			{
				Latitude = latitude;
				Longitude = longitude;
			}

			public decimal CompareTo(Coordinates that)
			{
				return GetDistance(new Coordinates(Latitude, Longitude), new Coordinates(that.Latitude, that.Longitude));
			}
		}
	}
}
