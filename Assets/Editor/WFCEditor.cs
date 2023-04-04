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
            //wfc.CreateMap();
            wfc.SetIslands();
        }

        if (GUILayout.Button("Generate"))
        {
            //wfc.CreateMap();
            wfc.SetIslands();
        }

        if(GUILayout.Button("Clear"))
        {
            wfc.ClearMap();
        }
    }

}
