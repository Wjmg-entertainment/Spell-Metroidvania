using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManagerScript : MonoBehaviour
{
    public Image healthbar;
    public float healthAmount = 100f;

    public Animator animator;

    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    /**
     * Method that checks if player is dead. If so, restart the scene
     */
    public void checkIfDead()
    {
        if(healthAmount <= 0)
        {
            animator.SetBool("isDead", true);
            //Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthbar.fillAmount = healthAmount / 100f;
    }

    /**
     * Method that deals damage to player and plays hurt animation
     */
    public void applyDamage(float damage)
    {
        healthAmount -= damage;
        healthbar.fillAmount = healthAmount / 100f;
        //animator.SetBool("isHurt", true);
        StartCoroutine(resetIsHurt());
    }

    //method that waits a second before turning off hurt aniamtion
    private IEnumerator resetIsHurt()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isHurt", false);
    }
}