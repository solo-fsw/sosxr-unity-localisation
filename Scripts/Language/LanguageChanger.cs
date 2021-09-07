using UnityEngine;
using UnityEngine.UI;


namespace _mrstruijk.Components.Localisation
{
	public class LanguageChanger : MonoBehaviour
	{
		public Language.Lang language;

		private Button button;


		private void Awake()
		{
			button = GetComponent<Button>();
		}


		private void OnEnable()
		{
			Language.languageChanged += CheckButtonInteractabilityByLanguage;
		}


		private void Start()
		{
			CheckButtonInteractabilityByLanguage();
		}


		public void ChangeToThisLanguage()
		{
			Language.language = language;
		}


		private void CheckButtonInteractabilityByLanguage()
		{
			button.interactable = Language.language != language;
		}


		private void OnDisable()
		{
			Language.languageChanged -= CheckButtonInteractabilityByLanguage;
		}
	}
}
