using UnityEditor;
using UnityEngine;


namespace _mrstruijk.Components.Localisation.Editor
{
	[CustomPropertyDrawer(typeof(StringLocalised))]
	public class LocalisedStringDrawer : PropertyDrawer
	{
		private bool dropdown = true;
		private float height;


		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (dropdown == true)
			{
				return height + 25;
			}
			else
			{
				return 20;
			}
		}


		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			position.width -= 34;
			position.height = 18;

			var valueRect = new Rect(position);
			valueRect.x += 15;
			valueRect.width -= 15;

			var foldButtonRect = new Rect(position);
			foldButtonRect.width = 15;

			dropdown = EditorGUI.Foldout(foldButtonRect, dropdown, "");

			position.x += 15;
			position.width -= 15;

			var key = property.FindPropertyRelative("key");
			key.stringValue = EditorGUI.TextField(position, key.stringValue);

			position.x += position.width + 2;
			position.width = 17;
			position.height = 17;

			var searchIcon = (Texture) Resources.Load("searchIcon");
			var searchContent = new GUIContent(searchIcon);
			var guiStyle = new GUIStyle();

			if (GUI.Button(position, searchContent, guiStyle))
			{
				TextLocaliserSearchWindow.Open();
			}

			position.x += position.width + 2;

			var storeIcon = (Texture) Resources.Load("storeIcon");
			var storeContent = new GUIContent(storeIcon);
			guiStyle = new GUIStyle();

			if (GUI.Button(position, storeContent, guiStyle))
			{
				TextLocaliserEditWindow.Open(key.stringValue);
			}

			if (dropdown == true)
			{
				var value = StringLocalisationSystem.GetLocalisedValue(key.stringValue);
				var style = GUI.skin.box;
				height = style.CalcHeight(new GUIContent(value), valueRect.width);

				valueRect.height = height;
				valueRect.y += 21;
				EditorGUI.LabelField(valueRect, value, EditorStyles.wordWrappedLabel);
			}

			EditorGUI.EndProperty();
		}
	}
}
