using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManagerScript : MonoBehaviour
{
    public Image healthbar;
    public float healthAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dealDamageAndHealDamage();
    }

    /**
     * Method that checks if player is dead. If so, restart the scene
     */
    public void restartGame()
    {
        if(healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    /**
     * Method for testing taking damage and healing using inputs for now
     */
    public void dealDamageAndHealDamage()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            takeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            heal(5);
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
