using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth; //set in editor inspector
    private float health, overlayBarRange, healthPercentage, inverseHealthPercentage, overlayBarPercentage;
    public bool canBeDamaged = true;
    [SerializeField] SpriteRenderer overlayBarRenderer;
    public float overlayBarStartWidth, overlayBarEndWidth, overlayBarStartHeight;

    void Start()
    {
        health = maxHealth;
        overlayBarRenderer.size = new Vector2(overlayBarStartWidth, overlayBarStartHeight);
        overlayBarRange = overlayBarEndWidth - overlayBarStartWidth; //get range between start and end width
    }

    private void UpdateHealthBar() //only needs to happen when taking damage
    {
        healthPercentage = (maxHealth / health) * 100f; //calculate percentage of health
        inverseHealthPercentage = 100f - healthPercentage; //get this as a REVERSE percentage
        overlayBarPercentage = (overlayBarRange * (inverseHealthPercentage / 100f)) + overlayBarStartWidth; //get as a percentage of the range and add start width
        overlayBarRenderer.size = new Vector2(overlayBarPercentage, overlayBarStartHeight);
    }

    /* setters */
    public void DamageFor(float damage) { if(canBeDamaged) health -= damage; UpdateHealthBar(); }

    public void HealFor(float healing) { health += healing; }

    /* UI */

}
