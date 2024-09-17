using System;


namespace SOSXR.Localiser
{
    [Serializable]
    public struct StringLocalised
    {
        public string Key;


        public StringLocalised(string key)
        {
            Key = key;
        }


        public string value => StringLocalisationSystem.GetLocalisedValue(Key);


        public static implicit operator StringLocalised(string key)
        {
            return new StringLocalised(key);
        }
    }
}