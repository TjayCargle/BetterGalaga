using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatDisplay : MonoBehaviour
{
    public GameObject healthShowcase = null;
    public GameObject fireRateShowcase = null;
    public GameObject speedShowcase = null;
    public StatManager manager = null;

    public Text nameText = null;

    [SerializeField]
    protected Image[] healthImages;

    [SerializeField]
    protected Image[] fireRateImages;

    [SerializeField]
    protected Image[] speedImages;

    public GameObject startButton = null;


}
