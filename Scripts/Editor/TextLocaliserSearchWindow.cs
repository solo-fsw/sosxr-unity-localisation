using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace _mrstruijk.Localisation.Editor
{
	public class TextLocaliserSearchWindow : EditorWindow
	{
		public string value;
		public Vector2 scroll;
		private Dictionary<string, string> dictionary;

		public static void Open()
		{
			var window = new TextLocaliserSearchWindow();
			window.titleContent = new GUIContent("Search in Localiser");

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
