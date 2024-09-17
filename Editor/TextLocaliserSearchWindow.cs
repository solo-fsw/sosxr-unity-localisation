using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace SOSXR.Localiser
{
    public class TextLocaliserSearchWindow : EditorWindow
    {
        public string Value;
        public Vector2 Scroll;
        private Dictionary<string, string> _dictionary;


        public static void Open()
        {
            var window = new TextLocaliserSearchWindow();
            window.titleContent = new GUIContent("Localisation Search");

            var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            var rect = new Rect(mousePosition.x - 450, mousePosition.y + 10, 10, 10);
            window.ShowAsDropDown(rect, new Vector2(500, 300));
        }


        private void OnEnable()
        {
            _dictionary = StringLocalisationSystem.GetDictionaryForEditor();
        }


        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);

            Value = EditorGUILayout.TextField(Value);
            EditorGUILayout.EndHorizontal();

            GetSearchResults();
        }


        private void GetSearchResults()
        {
            Value ??= "";

            EditorGUILayout.BeginVertical();
            Scroll = EditorGUILayout.BeginScrollView(Scroll);

            foreach (var element in _dictionary)
            {
                if (element.Key.ToLower().Contains(Value.ToLower()) || element.Value.ToLower().Contains(Value.ToLower()))
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
                            _dictionary = StringLocalisationSystem.GetDictionaryForEditor();
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