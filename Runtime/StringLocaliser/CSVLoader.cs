using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


namespace SOSXR.Localiser
{
    /// <summary>
    ///     From: GameDevGuide
    ///     https://youtu.be/c-dzg4M20wY
    ///     https://youtu.be/E-PR0d0Jb5A
    /// </summary>
    public class CSVLoader
    {
        private readonly char _lineSeparator = '\n';
        private readonly string[] _fieldSeparator = {"\",\""};
        private bool _csvLoaded;


        public CSVLoader()
        {
            LoadCSV();
        }


        public TextAsset CSVFile { get; private set; }
        private const char _surround = '"';
        private const string _fileName = "localisation";
        private const string _filePath = "Assets/Resources/";


        private void LoadCSV()
        {
            CSVFile = Resources.Load<TextAsset>(_fileName);
            _csvLoaded = true;
        }


        public Dictionary<string, string> GetDictionaryValues(string selectedLanguage)
        {
            if (_csvLoaded == false)
            {
                LoadCSV();
            }

            var dictionary = new Dictionary<string, string>();

            var lines = CSVFile.text.Split(_lineSeparator);

            var languageIndex = -1;

            var headers = lines[0].Split(_fieldSeparator, StringSplitOptions.None);

            for (var i = 0; i < headers.Length; i++)
            {
                if (headers[i].Contains(selectedLanguage))
                {
                    languageIndex = i;

                    break;
                }
            }

            var CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];

                var fields = CSVParser.Split(line);

                for (var f = 0; f < fields.Length; f++)
                {
                    fields[f] = fields[f].TrimStart(' ', _surround);
                    fields[f] = fields[f].TrimEnd(_surround);
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

            var appended = $"\n\"{key}\",\"{value}\",\"\"";

            File.AppendAllText(_filePath + _fileName + ".csv", appended);

            AssetDatabase.Refresh();

            LoadCSV();
        }


        public void Remove(string key)
        {
            LoadCSV();

            var lines = CSVFile.text.Split(_lineSeparator);
            var keys = new string[lines.Length];

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                keys[i] = line.Split(_fieldSeparator, StringSplitOptions.None)[0];
            }

            var index = -1;

            for (var i = 0; i < keys.Length; i++)
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

                var replaced = string.Join(_lineSeparator.ToString(), newLines);

                File.WriteAllText(_filePath + _fileName + ".csv", replaced);
            }

            AssetDatabase.Refresh();

            LoadCSV();
        }
        #endif
    }
}