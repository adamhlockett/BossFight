using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float hp; //set in editor inspector
    private float maxhp, overlayBarRange, healthPercentage, inverseHealthPercentage, overlayBarPercentage;
    public bool canBeDamaged = true;
    [SerializeField] SpriteRenderer overlayBarRenderer;
    public float overlayBarStartWidth, overlayBarEndWidth, overlayBarStartHeight;
    [SerializeField] PlayerHandler playerHandler;
    public float playerHitStopDuration = 0.2f, enemyHitStopDuration = 0.1f, playerRumbleDuration = 0.1f, enemyRumbleDuration = 0.05f;
    [SerializeField] SpriteRenderer lowHealthIndicator;

    void Start()
    {
        maxhp = hp;
        overlayBarRenderer.size = new Vector2(overlayBarStartWidth, overlayBarStartHeight);
        overlayBarRange = overlayBarEndWidth - overlayBarStartWidth; //get range between start and end width
    }

    private void UpdateHealthBar() //only needs to happen when taking damage
    {
        healthPercentage = (hp / maxhp) * 100f; //calculate percentage of health
        inverseHealthPercentage = 100f - healthPercentage; //get this as a REVERSE percentage
        overlayBarPercentage = (overlayBarRange * (inverseHealthPercentage / 100f)) + overlayBarStartWidth; //get as a percentage of the range and add start width
        overlayBarRenderer.size = new Vector2(overlayBarPercentage, overlayBarStartHeight);
    }

    /* setters */
    public void DamageFor(float damage, bool isPlayer) 
    { 
        if(canBeDamaged)
        {
            hp -= damage;
            if (isPlayer)
            {
                playerHandler.SpawnDamagePopup(damage, this.transform.root, true);
                FindObjectOfType<HitStop>().StopFor(playerHitStopDuration);
                playerHandler.HitShake();
                playerHandler.RumbleController(playerRumbleDuration);
                UpdateLowHealthIndicator();
            }
            else // is enemy or projectile
            {
                playerHandler.SpawnDamagePopup(damage, this.transform.root, false);
                FindObjectOfType<HitStop>().StopFor(enemyHitStopDuration);
                playerHandler.HitShake();
                playerHandler.RumbleController(enemyRumbleDuration);
            }
            UpdateHealthBar();
        }  
    }

    public void HealFor(float healing) { hp += healing; }

    public bool IsDead() { return hp <= 0; }

    private void UpdateLowHealthIndicator()
    {
        float alpha = (1 - ((float)hp / (float)maxhp));
        lowHealthIndicator.color = new Color(lowHealthIndicator.color.r, 
            lowHealthIndicator.color.g, lowHealthIndicator.color.b, 
            alpha);
        Debug.Log(alpha);
    }
}
