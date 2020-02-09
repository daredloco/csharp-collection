using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

namespace RoWa
{
	namespace Xamarin { 
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
			/// Convers a double value to a currency value
			/// </summary>
			/// <param name="d">the double value</param>
			/// <param name="currency">the currency prefix</param>
			/// <returns>A string containing the currency value</returns>
			internal static string DoubleToCurrency(double d, string currency = "$", bool prefixBefore = true)
			{
				string prefix = "";
				string suffix = "";
				if (prefixBefore)
					prefix = currency;
				else
					suffix = " " + currency;

				bool isMinus = false;
				if(d < 0) { isMinus = true; }
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
					else {
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
			/// Do not use this function at the moment!
			/// </summary>
			/// <param name="plainText"></param>
			/// <param name="Key"></param>
			/// <param name="IV"></param>
			/// <returns></returns>
			internal static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
			{
				if (plainText == null || plainText.Length <= 0)
					throw new ArgumentNullException("plainText");
				if (Key == null || Key.Length <= 0)
					throw new ArgumentNullException("Key");
				if (IV == null || IV.Length <= 0)
					throw new ArgumentNullException("IV");
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
					throw new ArgumentNullException("cipherText");
				if (Key == null || Key.Length <= 0)
					throw new ArgumentNullException("Key");
				if (IV == null || IV.Length <= 0)
					throw new ArgumentNullException("IV");

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

		}
	}
}
