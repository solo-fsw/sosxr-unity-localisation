using UnityEngine;
using UnityEngine.UI;


namespace SOSXR.Localiser
{
    public class LanguageChanger : MonoBehaviour
    {
        public Language.Lang Language;

        private Button _button;


        private void OnValidate()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
        }


        private void OnEnable()
        {
            Localiser.Language.LanguageChanged += CheckButtonInteractabilityByLanguage;
        }


        private void Start()
        {
            CheckButtonInteractabilityByLanguage();
        }


        public void ChangeToThisLanguage()
        {
            Localiser.Language.ChosenLanguage = Language;
        }


        private void CheckButtonInteractabilityByLanguage()
        {
            _button.interactable = Localiser.Language.ChosenLanguage != Language;
        }


        private void OnDisable()
        {
            Localiser.Language.LanguageChanged -= CheckButtonInteractabilityByLanguage;
        }
    }
}