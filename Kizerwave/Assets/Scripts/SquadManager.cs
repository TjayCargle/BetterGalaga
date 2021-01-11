using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public List<SquadScript> Squads = new List<SquadScript>();
    public List<EnemyBase> aliveEnemies = new List<EnemyBase>();
    public int currentSquad = -1;
    public float delayTime = 0.5f;

    private void Start()
    {
        ReleaseNextSquad();
        StartCoroutine(PeriodicCheck(delayTime));

    }

    public bool ReleaseNextSquad()
    {
        currentSquad++;
        if (Squads.Count > 0)
        {
            if (currentSquad < Squads.Count)
            {
                SquadScript someSquad = Squads[currentSquad];
                StartCoroutine(DelayedSpawn(someSquad, delayTime));
                return true;
            }
        }
        return false;
    }

    IEnumerator DelayedSpawn(SquadScript aSquad, float delay)
    {
        float timer = delay;
        for (int i = 0; i < aSquad.enemiesInSquad.Count;)
        {
            if (timer > 0)
            {
                timer -= 1 * Time.deltaTime;
                yield return null;
            }
            else
            {

                timer = delay;
                EnemyBase aNewEnemy = Instantiate(aSquad.enemiesInSquad[i]);
                if (aNewEnemy != null)
                {
                    aNewEnemy.squadManager = this;
                    aliveEnemies.Add(aNewEnemy);
                }
                i++;
            }
        }
    }

    IEnumerator PeriodicCheck(float delay)
    {
        float timer = delay;
        while (true)
        {

            if (timer > 0)
            {
                timer -= 1 * Time.deltaTime;
                yield return null;
            }
            else
            {

                timer = delay;
               if(aliveEnemies.Count == 0)
                {
                    bool released = ReleaseNextSquad();
                    if(released == false)
                    {
                        StopAllCoroutines();                 
                        OptionsPause.LoadMainMenu();
                    
                    }
                }
            }
        }
    }





}
