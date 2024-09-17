using TMPro;
using UnityEngine;


namespace SOSXR.Localiser
{
    /// <summary>
    ///     From: GameDevGuide
    ///     https://youtu.be/c-dzg4M20wY
    ///     https://youtu.be/E-PR0d0Jb5A
    /// </summary>
    [ExecuteAlways]
    public class StringLocaliserUI : MonoBehaviour
    {
        [Tooltip("Need only specify if TMProUGUI not on this GO, nor it's direct child, or if there can be a conflict between multiple TMProUGUIs")]
        [SerializeField] private TextMeshProUGUI m_textField;
        public StringLocalised LocalisedString;


        private void Awake()
        {
            if (m_textField == null)
            {
                m_textField = GetComponent<TextMeshProUGUI>();
            }

            if (m_textField == null)
            {
                m_textField = GetComponentInChildren<TextMeshProUGUI>();
            }
        }


        private void OnEnable()
        {
            Language.LanguageChanged += UpdateLocalisedTextValue;
            UpdateLocalisedTextValue();
        }


        private void Start()
        {
            UpdateLocalisedTextValue();
        }


        private void UpdateLocalisedTextValue()
        {
            if (!m_textField)
            {
                Debug.LogFormat("Couldn't find TextMeshProUGUI on {0}, or it's children", name);

                return;
            }

            m_textField.text = LocalisedString.value;
        }


        private void OnDisable()
        {
            Language.LanguageChanged -= UpdateLocalisedTextValue;
        }
    }
}