using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public Slider enemyHealth = null;
    public EnemyBase targetEnemy = null;
    public float displayTime = 0.5f;
    public Image sliderFill = null;
    public bool stayOn = false;
    public void UpdateHealthBar()
    {
        if (enemyHealth != null && targetEnemy != null)
        {
            enemyHealth.value = targetEnemy.HEALTH;
            if (sliderFill != null)
                sliderFill.color = Color.Lerp(Color.red, Color.green, enemyHealth.value / enemyHealth.maxValue);

            if (stayOn == false)
            {

                transform.LookAt(Camera.main.transform);
                StartCoroutine(TurnOff(displayTime));
            }
        }
    }

    IEnumerator TurnOff(float flashDelay)
    {
        float timer = flashDelay;
        while (true)
        {
            if (targetEnemy == null)
            {
                break;
            }
            if (targetEnemy.ISPAUSED == false)
            {


                if (timer > 0)
                {
                    timer -= 1 * Time.deltaTime;
                    yield return null;
                }
                else
                {
                    gameObject.SetActive(false);
                    break;
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}
