using UnityEditor;
using UnityEngine;
using System.Collections;

#if UNITY_EDITOR  // This excludes the autosave script from builds
[InitializeOnLoad]
public class AutoSaveEditor
{
    private static float saveInterval = 300f; // Autosave every 300 seconds (5 minutes)
    private static double lastSaveTime;

    static AutoSaveEditor()
    {
        lastSaveTime = EditorApplication.timeSinceStartup;
        EditorApplication.update += AutoSave;

        // Subscribe to playmode state changes to trigger save
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void AutoSave()
    {
        // Autosave every x seconds
        if (EditorApplication.timeSinceStartup - lastSaveTime >= saveInterval)
        {
            SaveAllAssets();
            lastSaveTime = EditorApplication.timeSinceStartup;
        }
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            SaveAllAssets();
        }
    }

    private static void SaveAllAssets()
    {
        Debug.Log("Auto-saving project...");
        AssetDatabase.SaveAssets();
        AssetDatabase.SaveAssets();
        UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
        Debug.Log("Project autosaved at: " + System.DateTime.Now);
    }
}
#endif