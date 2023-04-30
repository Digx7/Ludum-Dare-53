using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flower : MonoBehaviour
{
    public int currentHealth;
    public UnityEvent OnTakeDamage, OnDeath;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            if(currentHealth > 0) 
            {
                currentHealth -= col.gameObject.GetComponent<fiend>().damage;
                Destroy(col.gameObject);
                OnTakeDamage.Invoke();
            }
            if(currentHealth <= 0)
            {
                OnDeath.Invoke();
            }
        }
    }
}
