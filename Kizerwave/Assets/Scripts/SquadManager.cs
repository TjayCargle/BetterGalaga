using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public List<SquadScript> Squads = new List<SquadScript>();
    public int currentSquad = -1;
    public float delayTime = 0.5f;

    private void Start()
    {
        ReleaseNextSquad();
    }

    public void ReleaseNextSquad()
    {
        currentSquad++;
        if (Squads.Count > 0)
        {
            if (currentSquad < Squads.Count)
            {
                SquadScript someSquad = Squads[currentSquad];
                StartCoroutine(DelayedSpawn(someSquad, delayTime));
            }
        }

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
                Instantiate(aSquad.enemiesInSquad[i]);
                i++;
            }
        }
    }

}
