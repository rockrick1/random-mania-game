using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad]
public class SceneSwitchLeftButton
{
    // static SceneSwitchLeftButton()
    // {
    //     ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
    //     EditorApplication.playModeStateChanged += LoadDefaultScene;
    // }
    //
    // static void OnToolbarGUI()
    // {
    //     GUILayout.FlexibleSpace();
    //
    //     if(GUILayout.Button(new GUIContent("1", "Start Scene 1")))
    //     {
    //         // SceneHelper.StartScene("Assets/ToolbarExtender/Example/Scenes/Scene1.unity");
    //         EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
    //     }
    //
    //     if(GUILayout.Button(new GUIContent("2", "Start Scene 2")))
    //     {
    //         // SceneHelper.StartScene("Assets/ToolbarExtender/Example/Scenes/Scene2.unity");
    //     }
    // }
    //
    // static void LoadDefaultScene(PlayModeStateChange state){
    //     if (state == PlayModeStateChange.ExitingEditMode) {
    //         EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
    //     }
    //
    //     if (state == PlayModeStateChange.EnteredPlayMode) {
    //         EditorSceneManager.LoadScene(0);
    //     }
    // }
}