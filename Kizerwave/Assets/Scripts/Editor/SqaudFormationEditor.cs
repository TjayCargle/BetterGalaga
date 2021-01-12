using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SquadScript), true)]
public class SqaudFormationEditor : Editor
{
    SquadScript previewEnemy;

    private void OnEnable()
    {
        previewEnemy = target as SquadScript;

    }

    private void OnSceneGUI()
    {
        for (int i = 0; i < previewEnemy.s_formationSpots.Count; i++)
        {
            Vector3 waypoint = previewEnemy.s_formationSpots[i];



            Vector3 newWorld = waypoint;
            if (i != 0)
                newWorld = Handles.PositionHandle(waypoint, Quaternion.identity);

            Handles.color = Color.red;

            if (i == 0)
            {



               // Handles.DrawDottedLine(waypoint, previewEnemy.s_formationSpots[previewEnemy.s_formationSpots.Count - 1], 10);


            }
            else
            {
                Handles.DrawDottedLine(waypoint, previewEnemy.s_formationSpots[i - 1], 10);



            }
        }
    }
}
