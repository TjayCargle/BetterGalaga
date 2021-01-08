using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyBase), true)]
public class AIWayPointEditor : Editor
{
    EnemyBase previewEnemy;

    private void OnEnable()
    {
        previewEnemy = target as EnemyBase;

    }

    private void OnSceneGUI()
    {
        for (int i = 0; i < previewEnemy.WAYPOINTS.Count; i++)
        {
            Vector3 waypoint = previewEnemy.WAYPOINTS[i];



            Vector3 newWorld = waypoint;
            if (i != 0)
                newWorld = Handles.PositionHandle(waypoint, Quaternion.identity);

            Handles.color = Color.red;

            if (i == 0)
            {



                Handles.DrawDottedLine(waypoint, previewEnemy.WAYPOINTS[previewEnemy.WAYPOINTS.Count - 1], 10);


            }
            else
            {
                Handles.DrawDottedLine(waypoint, previewEnemy.WAYPOINTS[i - 1], 10);
         

           
            }
        }
    }

}
