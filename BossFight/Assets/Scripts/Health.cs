using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float hp; //set in inspector
    private float overlayBarRange, healthPercentage, inverseHealthPercentage, overlayBarPercentage;
    public float maxhp;
    public bool canBeDamaged = true;
    [SerializeField] SpriteRenderer overlayBarRenderer;
    public float overlayBarStartWidth, overlayBarEndWidth, overlayBarStartHeight;
    [SerializeField] PlayerHandler playerHandler;
    public float playerHitStopDuration = 0.2f, enemyHitStopDuration = 0.1f, playerRumbleDuration = 0.1f, enemyRumbleDuration = 0.05f, materialChangeWaitFor;
    [SerializeField] SpriteRenderer lowHealthIndicator;
    [SerializeField] Material playerMat;
    [SerializeField] Material playerHurtMat;
    [SerializeField] GameObject hitParticles;

    void Start()
    {
        maxhp = hp;
        overlayBarRenderer.size = new Vector2(overlayBarStartWidth, overlayBarStartHeight);
        overlayBarRange = overlayBarEndWidth - overlayBarStartWidth; //get range between start and end width
        materialChangeWaitFor = playerHitStopDuration;
    }

    private void UpdateHealthBar(bool isPlayer) //only needs to happen when taking damage
    {
        healthPercentage = (hp / maxhp) * 100f; //calculate percentage of health
        inverseHealthPercentage = 100f - healthPercentage; //get this as a REVERSE percentage
        overlayBarPercentage = (overlayBarRange * (inverseHealthPercentage / 100f)) + overlayBarStartWidth; //get as a percentage of the range and add start width
        overlayBarRenderer.size = new Vector2(overlayBarPercentage, overlayBarStartHeight);
        if (isPlayer) { PlayDataSingleton.instance.playerHealth = (int)hp; PlayDataSingleton.instance.playerMaxHealth = (int)maxhp; }
        else { PlayDataSingleton.instance.enemyHealth = (int)hp; PlayDataSingleton.instance.enemyMaxHealth = (int)maxhp; }
    }

    /* setters */
    public void DamageFor(float damage, bool isPlayer) 
    { 
        if(canBeDamaged)
        {
            hp -= damage;
            UpdateHealthBar(isPlayer);
        }  
        if (isPlayer)
        {
            playerHandler.SpawnDamagePopup(damage, this.transform.root, true); //damage popup
            Instantiate(hitParticles, transform.position, Quaternion.identity); //hit particles
            FindObjectOfType<HitStop>().StopFor(playerHitStopDuration); //hitstop
            playerHandler.HitShake(); //shake screen
            playerHandler.RumbleController(playerRumbleDuration); //rumble controller
            StartCoroutine(HitMaterialChange()); //flash red
            UpdateLowHealthIndicator(); //tint screen
        }
        else // is enemy or projectile
        {
            playerHandler.SpawnDamagePopup(damage, this.transform.root, false); //damage popup
            Instantiate(hitParticles, transform.position, Quaternion.identity); //hit particles
            FindObjectOfType<HitStop>().StopFor(enemyHitStopDuration); //hitstop
            playerHandler.HitShake(); //shake screen
            playerHandler.RumbleController(enemyRumbleDuration); //rumble controller
        }
    }

    //public void HealFor(float healing) { hp += healing; UpdateHealthBar(); }

    public void SetHealth(float health, bool isPlayer) { hp = health; UpdateHealthBar(isPlayer); }

    public bool IsDead() { return hp <= 0; }

    public void UpdateLowHealthIndicator()
    {
        float alpha = (1 - ((float)hp / (float)maxhp));
        lowHealthIndicator.color = new Color(lowHealthIndicator.color.r, 
            lowHealthIndicator.color.g, lowHealthIndicator.color.b, 
            alpha);
    }

    IEnumerator HitMaterialChange()
    {
        this.gameObject.GetComponent<SpriteRenderer>().material = playerHurtMat;
        yield return new WaitForSeconds(materialChangeWaitFor);
        this.gameObject.GetComponent<SpriteRenderer>().material = playerMat;
    }
}
