using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : EnemyBase
{

    public bool lockXRotation = true;
    public bool lockYRotation = false;
    public bool lockZRotation = false;
    private void Awake()
    {
        transform.position = StartingPosition;
        transform.localEulerAngles = StartingRotation;
        currentWaypoint = 0;

        if (myHealthBar == null)
        {
            myHealthBar = GetComponentInChildren<EnemyHealthBar>(true);
        }

        if(myHealthBar != null)
        {
            myHealthBar.enemyHealth.maxValue = s_health;
            myHealthBar.enemyHealth.value = s_health;
            myHealthBar.targetEnemy = this;
        }

    }
   public float idleRotRadius = 10.0f;
   public float idleRotSpeed = 3.5f;
    float posX, posY, angle = 0.0f;

    void FixedUpdate()
    {
        if (isPaused == false)
        {

            if (inFormation == false)
            {

                if (WAYPOINTS.Count > 0)
                {
                    if (currentWaypoint < WAYPOINTS.Count)
                    {
                        float dist = Vector3.Distance(transform.position, WAYPOINTS[currentWaypoint]);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(WAYPOINTS[currentWaypoint] - transform.position), SPEED * Time.deltaTime);
                        float x = transform.localEulerAngles.x;
                        float y = transform.localEulerAngles.y;
                        float z = transform.localEulerAngles.z;
                        float direction = WAYPOINTS[currentWaypoint].x - transform.position.x;

                        if (lockXRotation == true)
                            x = StartingRotation.x;

                        if (lockYRotation == true)
                        {
                            y = StartingRotation.y + (direction * -3);

                        }

                        if (lockZRotation == true)
                            z = StartingRotation.z;
                        transform.localEulerAngles = new Vector3(x, y, z);
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
                        if (endInFormation == true)
                        {
                            inFormation = true;
                        }
                        currentWaypoint = 0;
                    }
                }


            }
            else
            {
                float dist = Vector3.Distance(transform.position, formationLocation);
                if (dist > 1)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(formationLocation - transform.position), SPEED * Time.deltaTime);
                    float x = transform.localEulerAngles.x;
                    float y = transform.localEulerAngles.y;
                    float z = transform.localEulerAngles.z;
                    float direction = formationLocation.x - transform.position.x;

                    if (lockXRotation == true)
                        x = StartingRotation.x;

                    if (lockYRotation == true)
                    {
                        y = StartingRotation.y + (direction * -3);

                    }

                    if (lockZRotation == true)
                        z = StartingRotation.z;
                    transform.localEulerAngles = new Vector3(x, y, z);
                    transform.position = Vector3.MoveTowards(transform.position, formationLocation, SPEED * Time.deltaTime);
                    //  transform.position = Vector3.Lerp(transform.position, WAYPOINTS[currentWaypoint], SPEED * Time.deltaTime);
                }
                else
                {
                   //StartingRotation;
                    posX = transform.position.x + Mathf.Cos(angle) * idleRotRadius / 2.0f;
                    posY = transform.position.y + Mathf.Sin(angle) * idleRotRadius;
                    //Debug.Log("x= " + );
                    transform.localEulerAngles  = StartingRotation + new Vector3(posX, posY, transform.position.z);
                    angle = angle + Time.deltaTime * idleRotSpeed;
                    if (angle >= 360.0f)
                    {
                        angle = 0.0f;
                    }


                }

            }

        }
    }
}
