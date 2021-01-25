using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public List<SquadScript> Squads = new List<SquadScript>();
    public List<EnemyBase> aliveEnemies = new List<EnemyBase>();
    public int currentSquad = -1;
    public float delayTime = 0.5f;
    public bool isPaused = false;
    public float initialSpeed = 0;
    public float speedIncreaseAmount = 1.5f;
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
            if (isPaused == false)
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

                    aNewEnemy.SPEED += (currentSquad * speedIncreaseAmount) + initialSpeed;

                    if (aNewEnemy != null)
                    {
                        aNewEnemy.squadManager = this;
                        aliveEnemies.Add(aNewEnemy);
                        if (i < aSquad.s_formationSpots.Count)
                        {
                            aNewEnemy.formationLocation = aSquad.s_formationSpots[i];

                        }
                        else
                        {
                            aNewEnemy.startInFormation = false;
                            aNewEnemy.endInFormation = false;
                            aNewEnemy.inFormation = false;
                        }

                        if (aNewEnemy.startInFormation == true)
                        {
                            aNewEnemy.inFormation = true;
                        }
                    }
                    i++;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator PeriodicCheck(float delay)
    {
        float timer = delay;
        while (true)
        {
            if (isPaused == false)
            {


                if (timer > 0)
                {
                    timer -= 1 * Time.deltaTime;
                    yield return null;
                }
                else
                {

                    timer = delay;
                    if (aliveEnemies.Count == 0)
                    {
                        bool released = ReleaseNextSquad();
                        if (released == false)
                        {
                            StopAllCoroutines();
                            // OptionsPause.LoadNextScene();
                            GameManager gm = GameObject.FindObjectOfType<GameManager>();
                            if (gm != null)
                            {

                                gm.WinLevel();
                            }
                            else
                            {
                                OptionsPause.LoadMainMenu();
                            }
                        }
                    }
                    else
                    {
                        bool allInFormation = true;
                        for (int i = 0; i < aliveEnemies.Count; i++)
                        {
                            if(aliveEnemies[i].inFormation == false)
                            {
                                allInFormation = false;
                                break;
                            }
                        }

                        if(allInFormation == true)
                        {
                            aliveEnemies[Random.Range(0, aliveEnemies.Count)].inFormation = false;
                        }
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }


    public virtual void Pause()
    {
        isPaused = true;
    }

    public virtual void Resume()
    {
        isPaused = false;
    }

  


}
