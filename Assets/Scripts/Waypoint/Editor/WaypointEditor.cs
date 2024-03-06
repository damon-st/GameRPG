using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
   Waypoint WaypointTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        if (WaypointTarget.Points == null) return;
        for (int i = 0; i < WaypointTarget.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 pointActual = WaypointTarget.PositionActual + WaypointTarget.Points[i];
            var fmh_20_68_638449065935506551 = Quaternion.identity; Vector3 newPoint = Handles.FreeMoveHandle(pointActual, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            //Create Text
            GUIStyle txt = new GUIStyle();
            txt.fontStyle = FontStyle.Bold;
            txt.fontSize = 16;
            txt.normal.textColor = Color.black;
            Vector3 aling = Vector3.down * 0.3f+Vector3.right*0.3f;
            Handles.Label(WaypointTarget.PositionActual + WaypointTarget.Points[i]+aling,$"{i+1}",txt);

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Monve Handle");
                WaypointTarget.Points[i]= newPoint-WaypointTarget.PositionActual;
            }
        }
    }
}
