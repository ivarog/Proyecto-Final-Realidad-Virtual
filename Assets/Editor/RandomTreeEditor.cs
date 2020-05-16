using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomTree))]
public class RandomTreeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RandomTree randomTree = (RandomTree)target;

        if(GUILayout.Button("Generar árboles"))
        {
            randomTree.Generate();
        }
        if(GUILayout.Button("Limpiar árboles"))
        {
            randomTree.Clean();
        }
    }
}
