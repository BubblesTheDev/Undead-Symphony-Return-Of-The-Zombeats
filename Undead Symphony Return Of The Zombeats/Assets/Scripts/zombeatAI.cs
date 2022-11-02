using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class zombeatAI : MonoBehaviour
{
    public float weaknessChangeChance;
    public Vector3 barChances;
    public float pointWorth = 100;
    public Image healthIndicator;
    public Sprite[] healthStates;
    public AudioClip[] zombeatDmgSounds;
    public AudioClip[] deathClips;
    private AudioSource zombeatAudio;
    public elements zombieWeaknessElement;
    public float moveSpeed;
    public int health;
    public int maxHealth;
    
    public int currentHealthBars;
    public int maxHealthBars;

    public float stoppingDistance;
    public GameObject player;
    public zombeatManager manager;
    public bool isDead;

    private void Awake()
    {
        player = GameObject.Find("Game Manager");
        manager = player.GetComponent<zombeatManager>();
        transform.SetSiblingIndex(2);
        stoppingDistance += Random.Range(-.5f, .5f);
        zombeatAudio = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if (isDead) return;
        
        if (Vector3.Distance(transform.position, player.transform.position) >= stoppingDistance) transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);
        
        healthIndicator.fillAmount = (float)health / maxHealth;
        if (health <= 0 && currentHealthBars >= 1) {
            Debug.Log("brug");
            currentHealthBars--;
            if (Random.Range(.01f, 1f) < weaknessChangeChance) healthIndicator.sprite = healthStates[currentHealthBars - 1];
            healthIndicator.sprite = healthStates[currentHealthBars];
            health = maxHealth;
        }
        if (health <= 0 && currentHealthBars <= 0) killZombeat();


        healthIndicator.sprite = healthStates[Mathf.Clamp( currentHealthBars-1, 0, 3)];

    }

    public void createZombeat(float difficulty)
    {
        generateWeakness();

        weaknessChangeChance = .35f * (10 * difficulty);

        float healthBarChance = Random.Range(.01f, 1f);

        float difficultyChanceChange = Mathf.Clamp(Mathf.Round(-1 + (difficulty / 30f) * 100) / 100, 0, 999);

        healthBarChance -= difficultyChanceChange;

        if (healthBarChance < barChances.z / 100) maxHealthBars = 3;
        else if (healthBarChance < barChances.y / 100) maxHealthBars = 2;
        else if (healthBarChance < barChances.x / 100) maxHealthBars = 1;
        else maxHealthBars = 1;
        currentHealthBars = maxHealthBars;

        maxHealth = 3 + Mathf.RoundToInt(1 * (difficulty / 10));
        health = maxHealth;

        //Debug.Log("Zombeat: " + gameObject.name + "'s chances for healthbarNumbers is " + healthBarChance + "%");
        //Debug.Log("Which Means Zombeat: " + gameObject.name + " spawns with " + maxHealthBars + " Healthbars");

        
    }

    void generateWeakness() {
        int randomElementIndex = Random.Range(0,4);
        switch (randomElementIndex) {
            case 0:
            zombieWeaknessElement = elements.Metal;
            healthIndicator.color = Color.grey;
            break;

            case 1:
            zombieWeaknessElement = elements.Electricity;
            healthIndicator.color = Color.blue;
            break;

            case 2:
            zombieWeaknessElement = elements.Light;
            healthIndicator.color = Color.yellow;
            break;

            case 3:
            zombieWeaknessElement = elements.Fire;
            healthIndicator.color = Color.red;
            break;
        }

        healthIndicator.sprite = healthStates[currentHealthBars];
    }

    public void takeDamage(int damageTaken)
    {
        health -= damageTaken;
        
        zombeatAudio.clip = zombeatDmgSounds[Random.Range(0, zombeatDmgSounds.Length - 1)];
        zombeatAudio.Play();
    }

    void killZombeat()
    {
        Debug.Log("Killing: " + gameObject.name);
        manager.points += (int)(pointWorth * maxHealthBars * (manager.difficultyNumber));
        manager.zombeats.Remove(gameObject);
        //play death sound and particles or anim
        zombeatAudio.clip = deathClips[Random.Range(0, deathClips.Length)];
        zombeatAudio.Play();
        Destroy(gameObject, 3f);
    }
}
