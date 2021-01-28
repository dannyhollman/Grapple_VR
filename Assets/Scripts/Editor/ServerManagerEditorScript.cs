using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Menu))]
public class ServerManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        Menu menu = (Menu)target;
        if (GUILayout.Button("Start Server"))
            menu.StartServer();
        if (GUILayout.Button("Start Client"))
            menu.StartClient();
        if (GUILayout.Button("Auto Join"))
            menu.JoinRandomRoom();
    }
}
