using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Text_Manager : MonoBehaviour
{
    
    public GameObject squadM;

    // Update is called once per frame
    void Update()
    {
        if (squadM.GetComponent<SquadManager>().currentSquad == 0) 
        {
            uPdateT0(); 
        }
        if (squadM.GetComponent<SquadManager>().currentSquad == 1)
        {
            uPdateT1();
        }
        if (squadM.GetComponent<SquadManager>().currentSquad == 2)
        {
            uPdateT2();
        }
        if (squadM.GetComponent<SquadManager>().currentSquad == 3)
        {
            uPdateT3();
        }
        if (squadM.GetComponent<SquadManager>().currentSquad == 4)
        {
            uPdateT4();
        }


    }

    public void uPdateT0()
    {
        GetComponent<UnityEngine.UI.Text>().text = "These are the enemy ships that want to destroy you, Destroy them first by useing Space Bar";

    }
    public void uPdateT1()
    {
        GetComponent<UnityEngine.UI.Text>().text = "These are Power ups some enemys can drop giving you'r ship diffrent attacks, Try them out";
    }
    public void uPdateT2()
    {
        GetComponent<UnityEngine.UI.Text>().text = "change you currect attack with Q to switch to you scoundary, " +
            "when picking up a power up it will replace you'r current attack";
    }
    public void uPdateT3()
    {
        GetComponent<UnityEngine.UI.Text>().text = "You also have powerful bomb use them by pressing E, they will explode a deal good damage in a big area";
    }
    public void uPdateT4()
    {
        GetComponent<UnityEngine.UI.Text>().text = "The bar at the bottom is you speciel when full unlease a powerful super laser to destroy the enemy";
    }

}
