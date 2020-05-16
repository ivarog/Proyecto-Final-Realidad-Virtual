using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MeshCombiner))]
public class MeshCombinerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MeshCombiner meshCombiner = (MeshCombiner)target;

        if(GUILayout.Button("Combinar"))
        {
            meshCombiner.Combine();
        }

    }
}
