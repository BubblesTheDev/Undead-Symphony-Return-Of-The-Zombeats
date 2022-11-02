using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zombeatAI : MonoBehaviour
{
    public float weaknessChangeChance;
    public Vector3 barChances;
    public float pointWorth = 100;
    public Image healthIndicator;
    public Sprite[] healthStates;
    public elements zombieWeaknessElement;
    public float moveSpeed;
    public int health;
    public int maxHealth;
    public int maxHealthBars;
    int currentHealthBars;

    public float stoppingDistance;
    public GameObject player;
    public zombeatManager manager;
    public bool isDead;
    private float oldOneBar, oldTwoBar, oldThreeBar;

    private void Awake()
    {
        player = GameObject.Find("Game Manager");
        manager = player.GetComponent<zombeatManager>();

    }
    private void Update()
    {
        if (isDead) return;
        transform.LookAt(player.transform, Vector3.up);
        if (Vector3.Distance(transform.position, player.transform.position) >= stoppingDistance) transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);
        healthIndicator.sprite = healthStates[currentHealthBars - 1];
        healthIndicator.fillAmount = health / maxHealth;
    }

    public void createZombeat(float difficulty)
    {
        weaknessChangeChance = .35f * (10 * difficulty);

        float healthBarChance = Random.Range(.01f, 1f);

        float difficultyChanceChange = Mathf.Clamp(Mathf.Round(-1 + (difficulty / 30f) * 100) / 100, 0, 999);

        healthBarChance -= difficultyChanceChange;

        if (healthBarChance < barChances.z / 100) maxHealthBars = 3;
        else if (healthBarChance < barChances.y / 100) maxHealthBars = 2;
        else if (healthBarChance < barChances.x / 100) maxHealthBars = 1;
        else maxHealthBars = 1;
        currentHealthBars = maxHealthBars;

        maxHealth = 1 + Mathf.RoundToInt(1 * (difficulty / 10));
        health = maxHealth;

        //Debug.Log("Zombeat: " + gameObject.name + "'s chances for healthbarNumbers is " + healthBarChance + "%");
        //Debug.Log("Which Means Zombeat: " + gameObject.name + " spawns with " + maxHealthBars + " Healthbars");

        
    }

    public void takeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            currentHealthBars--;
        }
        else if (health <= 0 && currentHealthBars == 1) killZombeat();
    }

    void killZombeat()
    {
        manager.points += (int)(pointWorth * maxHealthBars * (1000 * manager.difficultyNumber));
        manager.zombeats.Remove(gameObject);
        //play death sound and particles or anim
        Destroy(gameObject, 3f);
    }
}
