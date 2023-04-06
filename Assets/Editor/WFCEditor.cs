using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaveFunctionCollapse))]
public class WFCEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WaveFunctionCollapse wfc = (WaveFunctionCollapse)target;

        if (DrawDefaultInspector())
        {
            wfc.CreateTilemap();
        }

        if (GUILayout.Button("Generate"))
        {
            wfc.CreateTilemap();
        }
    }

}
