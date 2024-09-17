using System;
using UnityEngine;


namespace SOSXR.Localiser
{
    public static class Language
    {
        public enum Lang
        {
            NL,
            EN // Add other languages here
        }


        public static Lang ChosenLanguage
        {
            get => _chosenLanguage;
            set
            {
                _chosenLanguage = value;
                InvokeLanguageChangedAction();
            }
        }

        private static Lang _chosenLanguage = Lang.NL;


        public static Action LanguageChanged;


        private static void InvokeLanguageChangedAction()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            LanguageChanged?.Invoke();
        }
    }
}