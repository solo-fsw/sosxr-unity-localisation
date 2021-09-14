using UnityEditor;
using UnityEngine;


namespace _mrstruijk.Localisation.Editor
{
	public class TextLocaliserEditWindow : EditorWindow
	{
		public string key;
		public string value;


		public static void Open(string key)
		{
			if (Language.language != Language.Lang.NL)
			{
				EditorUtility.DisplayDialog("NL Only", "Enkel Nederlands kan via de Editor worden ingevoerd", "Ok");
				return;
			}

			var window = new TextLocaliserEditWindow();
			window.titleContent = new GUIContent("Localiser Window");
			window.ShowUtility();
			window.key = key;
		}


		public void OnGUI()
		{
			key = EditorGUILayout.TextField("Key:", key);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(Language.language + " text:", GUILayout.MaxWidth(60));
			EditorStyles.textArea.wordWrap = true;
			value = EditorGUILayout.TextArea(value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(5000));
			EditorGUILayout.EndHorizontal();

			if (GUILayout.Button("Save to system"))
			{
				if (StringLocalisationSystem.GetLocalisedValue(key) == null)
				{
					StringLocalisationSystem.Add(key, value);
					Close();
				}
				else
				{
					if (EditorUtility.DisplayDialog("Replace" + key + "?", "Key '" + key + "' already known. Replacing will also remove other languages", "Do it", "Cancel"))
					{
						if (true)
						{
							StringLocalisationSystem.Replace(key, value);
							Close();
						}
					}
				}
			}

			minSize = new Vector2(460, 250);
		}
	}
}