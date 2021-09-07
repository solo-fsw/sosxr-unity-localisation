using System;
using System.Collections.Generic;


namespace _mrstruijk.Components.Localisation
{
	/// <summary>
	/// From: GameDevGuide
	/// https://youtu.be/c-dzg4M20wY
	/// https://youtu.be/E-PR0d0Jb5A
	/// </summary>
	public class StringLocalisationSystem
	{

		private static Dictionary<string, string> localisedNL;
		private static Dictionary<string, string> localisedEN; // Add other languages here

		private static bool isInit;

		private static CSVLoader csvLoader;




		public static void Init()
		{
			csvLoader = new CSVLoader();

			UpdateDictionaries();

			isInit = true;
		}


		public static Dictionary<string, string> GetDictionaryForEditor()
		{
			if (!isInit)
			{
				Init();
			}

			switch (Language.language)
			{
				case Language.Lang.NL:
					return localisedNL;
				case Language.Lang.EN:
					return localisedEN; // Add other languages here
				default:
					throw new ArgumentOutOfRangeException();
			}
		}


		private static void UpdateDictionaries()
		{
			localisedNL = csvLoader.GetDictionaryValues("nl");
			localisedEN = csvLoader.GetDictionaryValues("en"); // Add other languages here

			UnityEditor.AssetDatabase.Refresh();
		}


		public static string GetLocalisedValue(string key)
		{
			if (!isInit)
			{
				Init();
			}

			string value;

			switch (Language.language)
			{
				case Language.Lang.NL:
					localisedNL.TryGetValue(key, out value);
					break;
				case Language.Lang.EN:
					localisedEN.TryGetValue(key, out value);
					break; 	// Add other languages here
				default:
					throw new ArgumentOutOfRangeException();
			}

			return value;
		}


		public static void Add(string key, string value)
		{
			if (GetLocalisedValue(key) != null)
			{
				return;
			}

			CheckForQuotationmarks(value);

			CheckForCSVLoader();

			csvLoader.Add(key, value);

			UpdateDictionaries();
		}


		public static void Replace(string key, string value)
		{
			Remove(key);
			Add(key, value);
		}


		public static void Remove(string key)
		{
			CheckForCSVLoader();

			csvLoader.Remove(key);

			UpdateDictionaries();
		}


		private static void CheckForQuotationmarks(string value)
		{
			if (value.Contains("\""))
			{
				value.Replace('"', '\"');
			}
		}


		private static void CheckForCSVLoader()
		{
			csvLoader ??= new CSVLoader();
		}


	}
}
