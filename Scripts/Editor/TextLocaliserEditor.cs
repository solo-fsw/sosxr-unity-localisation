using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace _mrstruijk.Components.Localisation.Editor
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


	public class TextLocaliserSearchWindow : EditorWindow
	{
		public string value;
		public Vector2 scroll;
		private Dictionary<string, string> dictionary;

		public static void Open()
		{
			var window = new TextLocaliserSearchWindow();
			window.titleContent = new GUIContent("Localisation Search");

			var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
			var rect = new Rect(mousePosition.x - 450, mousePosition.y + 10, 10, 10);
			window.ShowAsDropDown(rect, new Vector2(500,300));
		}


		private void OnEnable()
		{
			dictionary = StringLocalisationSystem.GetDictionaryForEditor();
		}


		public void OnGUI()
		{
			EditorGUILayout.BeginHorizontal("Box");
			EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);

			value = EditorGUILayout.TextField(value);
			EditorGUILayout.EndHorizontal();

			GetSearchResults();
		}


		private void GetSearchResults()
		{
			value ??= "";

			EditorGUILayout.BeginVertical();
			scroll = EditorGUILayout.BeginScrollView(scroll);

			foreach (var element in dictionary)
			{
				if (element.Key.ToLower().Contains(value.ToLower()) || element.Value.ToLower().Contains(value.ToLower()))
				{
					EditorGUILayout.BeginHorizontal("box");
					var removeIcon = (Texture) Resources.Load("removeIcon");
					var guiStyle = new GUIStyle();
					var removeButton = new GUIContent(removeIcon);

					if (GUILayout.Button(removeButton, guiStyle, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
					{
						if (EditorUtility.DisplayDialog("Remove " + element.Key + "?", "This will remove '" + element.Key + "' from localisation system, are you sure?", "Do it", "NOPE!"))
						{
							StringLocalisationSystem.Remove(element.Key);
							AssetDatabase.Refresh();
							StringLocalisationSystem.Init();
							dictionary = StringLocalisationSystem.GetDictionaryForEditor();
						}
					}

					EditorGUILayout.TextField(element.Key);
					EditorGUILayout.LabelField(element.Value);
					EditorGUILayout.EndHorizontal();
				}
			}

			EditorGUILayout.EndScrollView();
			EditorGUILayout.EndVertical();
		}
	}
}
