using TMPro;
using UnityEngine;


namespace _mrstruijk.Localisation
{
	/// <summary>
	/// From: GameDevGuide
	/// https://youtu.be/c-dzg4M20wY
	/// https://youtu.be/E-PR0d0Jb5A
	/// </summary>
	[ExecuteAlways]
	public class StringLocaliserUI : MonoBehaviour
	{
		[Tooltip("Need only specify if TMProUGUI not on this GO, nor it's direct child, or if there can be a conflict between multiple TMProUGUIs")]
		[SerializeField] private TextMeshProUGUI textField;
		public StringLocalised localisedString;


		private void Awake()
		{
			if (textField == null)
			{
				textField = GetComponent<TextMeshProUGUI>();
			}

			if (textField == null)
			{
				textField = GetComponentInChildren<TextMeshProUGUI>();
			}
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
			if (!textField)
			{
				Debug.LogFormat("Couldn't find TextMeshProUGUI on {0}, or it's children", this.name);
				return;
			}

			textField.text = localisedString.value;
		}


		private void OnDisable()
		{
			Language.languageChanged -= UpdateLocalisedTextValue;
		}
	}
}
