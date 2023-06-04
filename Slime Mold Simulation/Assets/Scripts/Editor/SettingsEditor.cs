using UnityEditor;
using UnityEngine;

// Taken from sebastian lauges 

namespace Editor
{
    [CustomEditor(typeof(Simulation))]
    public class SettingsEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor settingsEditor;
        private bool settingsFoldout;

        private void OnEnable()
        {
            settingsFoldout = EditorPrefs.GetBool(nameof(settingsFoldout), false);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var sim = target as Simulation;

            if (sim.simulationDataSO != null)
            {
                DrawSettingsEditor(sim.simulationDataSO, ref settingsFoldout, ref settingsEditor);
                EditorPrefs.SetBool(nameof(settingsFoldout), settingsFoldout);
            }
        }

        private void DrawSettingsEditor(Object settings, ref bool foldout, ref UnityEditor.Editor editor)
        {
            if (settings != null)
            {
                foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();
                }
            }
        }
    }
}