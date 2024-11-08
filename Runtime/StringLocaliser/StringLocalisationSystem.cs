using System;
using System.Collections.Generic;
using UnityEditor;


namespace SOSXR.Localiser
{
    /// <summary>
    ///     From: GameDevGuide
    ///     https://youtu.be/c-dzg4M20wY
    ///     https://youtu.be/E-PR0d0Jb5A
    /// </summary>
    public class StringLocalisationSystem
    {
        private static Dictionary<string, string> _localisedNL;
        private static Dictionary<string, string> _localisedEN; // Add other languages here

        private static bool _isInit;

        private static CSVLoader _csvLoader;


        public static void Init()
        {
            _csvLoader = new CSVLoader();

            UpdateDictionaries();

            _isInit = true;
        }


        public static Dictionary<string, string> GetDictionaryForEditor()
        {
            if (!_isInit)
            {
                Init();
            }

            switch (Language.ChosenLanguage)
            {
                case Language.Lang.NL:
                    return _localisedNL;
                case Language.Lang.EN:
                    return _localisedEN; // Add other languages here
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private static void UpdateDictionaries()
        {
            _localisedNL = _csvLoader.GetDictionaryValues("nl");
            _localisedEN = _csvLoader.GetDictionaryValues("en"); // Add other languages here
            #if UNITY_EDITOR
            AssetDatabase.Refresh();
            #endif
        }


        public static string GetLocalisedValue(string key)
        {
            if (!_isInit)
            {
                Init();
            }

            string value;

            switch (Language.ChosenLanguage)
            {
                case Language.Lang.NL:
                    _localisedNL.TryGetValue(key, out value);

                    break;
                case Language.Lang.EN:
                    _localisedEN.TryGetValue(key, out value);

                    break; // Add other languages here
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

            #if UNITY_EDITOR
            _csvLoader.Add(key, value);
            #endif

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

            #if UNITY_EDITOR
            _csvLoader.Remove(key);
            #endif

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
            _csvLoader ??= new CSVLoader();
        }
    }
}