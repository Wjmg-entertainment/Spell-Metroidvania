using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManagerScript : MonoBehaviour
{
    public Image healthbar;
    public float healthAmount = 100f;

    /**
     * Method that checks if player is dead. If so, restart the scene
     */
    public void restartGame()
    {
        if(healthAmount <= 0)
        {
            //Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void takeDamage(float damage)
    {
        healthAmount -= damage;
        healthbar.fillAmount = healthAmount / 100f;
    }

    public void heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthbar.fillAmount = healthAmount / 100f;
    }
}
