using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithRequirement : MonoBehaviour
{
    public Button myButton;
    public string myRequirement = "";
    // Start is called before the first frame update
    void Start()
    {
        if (myButton == null)
        {
            myButton = GetComponent<Button>();
            CheckInteractivity();
        }
    }

    private void Awake()
    {
        if (myButton == null)
        {
            myButton = GetComponent<Button>();
            CheckInteractivity();
        }
    }
    private void OnEnable()
    {
        CheckInteractivity();
    }

    public void CheckInteractivity()
    {
        if(myButton != null)
        {
            if(myRequirement == string.Empty)
            {
                myButton.interactable = true;
            }
            else
            {
                if(StatManager.Instance.completedLevels.Contains( myRequirement))
                {
                    myButton.interactable = true;
                }
                else
                {
                    myButton.interactable = false;
                }
            }
        }
    }
}
