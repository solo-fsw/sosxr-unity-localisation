namespace _mrstruijk.Components.Localisation
{
	[System.Serializable]
	public struct StringLocalised
	{
		public string key;


		public StringLocalised(string key)
		{
			this.key = key;
		}


		public string value
		{
			get => StringLocalisationSystem.GetLocalisedValue(key);
		}


		public static implicit operator StringLocalised(string key)
		{
			return new StringLocalised(key);
		}
	}
}
