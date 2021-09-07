using TMPro;
using UnityEngine;


namespace _mrstruijk.Components.Localisation
{
	/// <summary>
	/// From: GameDevGuide
	/// https://youtu.be/c-dzg4M20wY
	/// https://youtu.be/E-PR0d0Jb5A
	/// </summary>
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class StringLocaliserUI : MonoBehaviour
	{
		private TextMeshProUGUI textField;
		public StringLocalised localisedString;


		private void Awake()
		{
			textField = GetComponent<TextMeshProUGUI>();
		}


		private void OnEnable()
		{
			Language.languageChanged += UpdateLocalisedTextValue;
			UpdateLocalisedTextValue();
		}


		private void Start()
		{
			UpdateLocalisedTextValue();
		}


		private void UpdateLocalisedTextValue()
		{
			textField.text = localisedString.value;
		}


		private void OnDisable()
		{
			Language.languageChanged -= UpdateLocalisedTextValue;
		}
	}
}
