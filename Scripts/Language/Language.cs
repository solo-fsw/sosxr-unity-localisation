using System;
using UnityEngine;


namespace _mrstruijk.Components.Localisation
{
	public static class Language
	{
		public enum Lang
        {
        	NL,
        	EN // Add other languages here
        }

        private static Lang _language = Lang.NL;
        public static Lang language
        {
        	get => _language;
        	set
        	{
        		_language = value;
        		InvokeLanguageChangedAction();
        	}
        }


		public static Action languageChanged;

		private static void InvokeLanguageChangedAction()
		{
			if (!Application.isPlaying)
			{
				return;
			}

			languageChanged?.Invoke();
		}
	}
}
