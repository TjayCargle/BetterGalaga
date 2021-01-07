using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : EnemyBase
{
    private void Awake()
    {
        transform.position = StartingPosition;
        transform.localEulerAngles = StartingRotation;
        currentWaypoint = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Fire();
        }
    }


    void FixedUpdate()
    {
        if (WAYPOINTS.Count > 0)
        {
            if (currentWaypoint < WAYPOINTS.Count)
            {
                float dist = Vector3.Distance(transform.position, WAYPOINTS[currentWaypoint]);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(WAYPOINTS[currentWaypoint] - transform.position), SPEED * Time.deltaTime);
                transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
                if (dist > 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, WAYPOINTS[currentWaypoint], SPEED * Time.deltaTime);
                    //  transform.position = Vector3.Lerp(transform.position, WAYPOINTS[currentWaypoint], SPEED * Time.deltaTime);
                }
                else
                {
                    currentWaypoint++;
                    if (fireAtWaypoints == true)
                    {
                        Fire();
                    }
                }
            }
            else
            {
                currentWaypoint = 0;
            }
        }



    }
}
