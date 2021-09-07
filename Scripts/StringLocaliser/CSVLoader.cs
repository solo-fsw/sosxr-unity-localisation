using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;


namespace _mrstruijk.Components.Localisation
{
	/// <summary>
	/// From: GameDevGuide
	/// https://youtu.be/c-dzg4M20wY
	/// https://youtu.be/E-PR0d0Jb5A
	/// </summary>
	public class CSVLoader
	{
		public TextAsset CSVFile
		{
			get;
			private set;
		}

		private readonly char lineSeperator = '\n';
		private const char surround = '"';
		private readonly string[] fieldSeperator = {"\",\""};
		private const string fileName = "localisation";
		private const string filePath = "Assets/_mrstruijk/Components/Localisation/Resources/";
		private bool csvLoaded;


		public CSVLoader()
		{
			LoadCSV();
		}


		private void LoadCSV()
		{
			CSVFile = Resources.Load<TextAsset>(fileName);
			csvLoaded = true;
		}


		public Dictionary<string, string> GetDictionaryValues(string selectedLanguage)
		{
			if (csvLoaded == false)
			{
				LoadCSV();
			}

			var dictionary = new Dictionary<string, string>();

			string[] lines = CSVFile.text.Split(lineSeperator);

			int languageIndex = -1;

			string[] headers = lines[0].Split(fieldSeperator, StringSplitOptions.None);

			for (int i = 0; i < headers.Length; i++)
			{
				if (headers[i].Contains(selectedLanguage))
				{
					languageIndex = i;
					break;
				}
			}

			var CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

			for (int i = 1; i < lines.Length; i++)
			{
				string line = lines[i];

				string[] fields = CSVParser.Split(line);

				for (int f = 0; f < fields.Length; f++)
				{
					fields[f] = fields[f].TrimStart(' ', surround);
					fields[f] = fields[f].TrimEnd(surround);
				}

				if (fields.Length > languageIndex)
				{
					var key = fields[0];

					if (dictionary.ContainsKey(key))
					{
						continue;
					}

					var value = fields[languageIndex];

					dictionary.Add(key, value);
				}
			}

			return dictionary;
		}


		#if UNITY_EDITOR
		public void Add(string key, string value)
		{
			LoadCSV();

			string appended = $"\n\"{key}\",\"{value}\",\"\"";

			File.AppendAllText(filePath + fileName + ".csv", appended);

			UnityEditor.AssetDatabase.Refresh();

			LoadCSV();
		}


		public void Remove(string key)
		{
			LoadCSV();

			string[] lines = CSVFile.text.Split(lineSeperator);
			string[] keys = new string[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];
				keys[i] = line.Split(fieldSeperator, StringSplitOptions.None)[0];
			}

			int index = -1;

			for (int i = 0; i < keys.Length; i++)
			{
				if (keys[i].Contains(key))
				{
					index = i;
					break;
				}
			}

			if (index > -1)
			{
				var newLines = lines.Where(w => w != lines[index]).ToArray();

				string replaced = string.Join(lineSeperator.ToString(), newLines);

				File.WriteAllText(filePath + fileName + ".csv", replaced);
			}

			UnityEditor.AssetDatabase.Refresh();

			LoadCSV();
		}
		#endif
	}
}


















































