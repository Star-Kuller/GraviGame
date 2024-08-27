using Audio;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    [CustomEditor(typeof(Dj))]
    public class DjEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var dj = (Dj)target;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300));
            
            var songStyle = new GUIStyle(EditorStyles.textField)
            {
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white, background = Texture2D.blackTexture },
                fontSize = 14
            };
            
            GUILayout.Space(10);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label(dj.CurrentSongName, songStyle, GUILayout.Height(40));
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);

            EditorGUI.BeginChangeCheck();
            var newProgress = EditorGUILayout.Slider(dj.AudioProgress, 0f, 0.98f);
            if (EditorGUI.EndChangeCheck())
            {
                dj.SetAudioProgress(newProgress);
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(dj.IsPaused ? "Resume" : "Pause", GUILayout.Width(80)))
            {
                dj.TogglePause();
            }
            if (GUILayout.Button("Next", GUILayout.Width(80)))
            {
                dj.Next();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();

            if (Application.isPlaying)
            {
                Repaint();
            }
        }
    }
}