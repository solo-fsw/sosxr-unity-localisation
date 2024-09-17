using UnityEditor;
using UnityEngine;


namespace SOSXR.Localiser
{
    public class TextLocaliserEditWindow : EditorWindow
    {
        public string Key;
        public string Value;


        public static void Open(string key)
        {
            if (Language.ChosenLanguage != Language.Lang.NL)
            {
                EditorUtility.DisplayDialog("NL Only", "Enkel Nederlands kan via de Editor worden ingevoerd", "Ok");

                return;
            }

            var window = new TextLocaliserEditWindow();
            window.titleContent = new GUIContent("Localiser Window");
            window.ShowUtility();
            window.Key = key;
        }


        public void OnGUI()
        {
            Key = EditorGUILayout.TextField("Key:", Key);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(Language.ChosenLanguage + " text:", GUILayout.MaxWidth(60));
            EditorStyles.textArea.wordWrap = true;
            Value = EditorGUILayout.TextArea(Value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(5000));
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Save to system"))
            {
                if (StringLocalisationSystem.GetLocalisedValue(Key) == null)
                {
                    StringLocalisationSystem.Add(Key, Value);
                    Close();
                }
                else
                {
                    if (EditorUtility.DisplayDialog("Replace" + Key + "?", "Key '" + Key + "' already known. Replacing will also remove other languages", "Do it", "Cancel"))
                    {
                        if (true)
                        {
                            StringLocalisationSystem.Replace(Key, Value);
                            Close();
                        }
                    }
                }
            }

            minSize = new Vector2(460, 250);
        }
    }
}