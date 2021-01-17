using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu = null;
    public GameObject GameOverMenu = null;
    private bool paused = false;
    public void PauseGame()
    {
        if (PauseMenu != null)
        {
            OpenPanel(PauseMenu);
        }
        ShipBase[] ships = GameObject.FindObjectsOfType<ShipBase>();
        ProjectileBase[] projectiles = GameObject.FindObjectsOfType<ProjectileBase>();
        SquadManager[] squads = GameObject.FindObjectsOfType<SquadManager>();
        PowerUp[] powers = GameObject.FindObjectsOfType<PowerUp>();

        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].Pause();
        }

        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].Pause();
        }

        for (int i = 0; i < squads.Length; i++)
        {
            squads[i].Pause();
        }

        for (int i = 0; i < powers.Length; i++)
        {
            powers[i].Pause();
        }
    }

    public void ResumeGame()
    {
        if (PauseMenu != null)
        {
            ClosePanel(PauseMenu);
        }
        ShipBase[] ships = GameObject.FindObjectsOfType<ShipBase>();
        ProjectileBase[] projectiles = GameObject.FindObjectsOfType<ProjectileBase>();
        SquadManager[] squads = GameObject.FindObjectsOfType<SquadManager>();
        PowerUp[] powers = GameObject.FindObjectsOfType<PowerUp>();

        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].Resume();
        }

        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].Resume();
        }

        for (int i = 0; i < squads.Length; i++)
        {
            squads[i].Resume();
        }

        for (int i = 0; i < powers.Length; i++)
        {
            powers[i].Resume();
        }
    }

    public void GameOver()
    {
        if (GameOverMenu != null)
        {
            PauseGame();
            ClosePanel(PauseMenu);
            OpenPanel(GameOverMenu);

        }
    }

    public void Retry()
    {
        //unknown if will be used.
        //ProjectileBase[] projectiles = GameObject.FindObjectsOfType<ProjectileBase>();

        //SquadManager[] squads = GameObject.FindObjectsOfType<SquadManager>();
        //for (int i = 0; i < squads.Length; i++)
        //{
        //    squads[i].currentSquad = -1;
        //    squads[i].ReleaseNextSquad();

        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (paused == true)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void OpenPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.gameObject.SetActive(true);
        }
    }


    public void ClosePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.gameObject.SetActive(false);
        }
    }
}
