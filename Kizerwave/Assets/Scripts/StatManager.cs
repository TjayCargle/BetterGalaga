using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StatManager : MonoBehaviour
{
    #region singleton
    private static StatManager _instance;
    public static StatManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = FindObjectOfType<StatManager>();

            if (_instance != null)
                return _instance;

            Create();

            return _instance;
        }
    }
    private static StatManager Create()
    {
        GameObject TimerGameObject = new GameObject("StatManager");
        _instance = TimerGameObject.AddComponent<StatManager>();
        return _instance;
    }
    #endregion


    public int healthStat;
    public int fireRateStat;
    public int speedStat;
    public int selectedPlayer = -1;
    public string playerName = "";
    public int meshScale;
    public List<Mesh> playerMesh = new List<Mesh>();
    public Mesh selectedMesh = null;

    public List<Material> playerMaterial = new List<Material>();
    public Material selectedMaterial = null;
    public PlayerSelect playerSelect = null;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += LevelLoaded;

    }

    private void LevelLoaded(Scene arg0, Scene arg1)
    {
        playerSelect = GameObject.FindObjectOfType<PlayerSelect>(true);

    }

    private void Awake()
    {

        playerSelect = GameObject.FindObjectOfType<PlayerSelect>(true);
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
    }

    public void IncreaseHealth()
    {
        healthStat++;
    }


    public void IncreaseFireRate()
    {
        fireRateStat++;
    }


    public void IncreaseSpeed()
    {
        speedStat++;
    }

    public void SelectVolcano()
    {
        if (selectedPlayer != 1)
        {

            playerName = "Volcano";
            healthStat = 1;
            fireRateStat = 2;
            speedStat = 2;
            selectedPlayer = 1;
            meshScale = 12;
            if (playerMesh.Count > 0)
            {
                selectedMesh = playerMesh[0];
            }

            if (playerMaterial.Count > 0)
            {
                selectedMaterial = playerMaterial[0];
            }

            if (playerSelect != null)
            {
                playerSelect.UpdateBars();
            }
        }
        else
        {
            selectedPlayer = -1;
            playerName = "";
            if (playerSelect != null)
            {
                playerSelect.ZeroOut();
            }
        }

    }

    public void SelectBeret()
    {
        if (selectedPlayer != 2)
        {

            playerName = "Beret";
            healthStat = 2;
            fireRateStat = 2;
            speedStat = 1;
            selectedPlayer = 2;
            meshScale = 1;

            if (playerMesh.Count > 1)
            {
                selectedMesh = playerMesh[1];
            }

            if (playerMaterial.Count > 1)
            {
                selectedMaterial = playerMaterial[1];
            }

            if (playerSelect != null)
            {
                playerSelect.UpdateBars();
            }

        }
        else
        {
            selectedPlayer = -1;
            playerName = "";
            if (playerSelect != null)
            {
                playerSelect.ZeroOut();
            }
        }
    }

    public void SelectTundra()
    {
        if (selectedPlayer != 3)
        {

            playerName = "Tundra";
            healthStat = 2;
            fireRateStat = 1;
            speedStat = 2;
            selectedPlayer = 3;
            meshScale = 1;

            if (playerMesh.Count > 2)
            {
                selectedMesh = playerMesh[2];
            }

            if (playerMaterial.Count > 2)
            {
                selectedMaterial = playerMaterial[2];
            }

            if (playerSelect != null)
            {
                playerSelect.UpdateBars();
            }


        }
        else
        {
            selectedPlayer = -1;
            playerName = "";
            if (playerSelect != null)
            {
                playerSelect.ZeroOut();
            }
        }
    }
}
