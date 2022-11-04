using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class zombeatAI : MonoBehaviour
{

    //Zombeat Stats
    public float pointWorth = 100;
    public float moveSpeed;
    public int health;
    public elements zombieWeaknessElement;
    public chordWeight tier;

    public float weaknessChangeChance;
    public Vector3 barChances;
    public Image tierIndicator;
    public Sprite[] tierSprite;
    public AudioClip[] zombeatDmgSounds;
    public AudioClip[] deathClips;
    private AudioSource zombeatAudio;
    

    public animHandler walkAnimation;
    public animHandler hitAnimation;

    public int tierNumber;
    public int maxTierNumber;

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

        StartCoroutine(walkAnimation.playAnim());
    }

    private void Update()
    {
        if (isDead) return;

        if (Vector3.Distance(transform.position, player.transform.position) >= stoppingDistance)
        {
            transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);
        } 
        else
        {
            walkAnimation.loopAnim = false;
            walkAnimation.stopAnim();
            StartCoroutine(hitPlayer());
        }

        tierIndicator.fillAmount = (float)health / tierNumber;

        if (health <= 0 && tierNumber >= 1)
        {
            tierNumber--;
            if (Random.Range(.01f, 1f) < weaknessChangeChance) tierIndicator.sprite = tierSprite[tierNumber - 1];
            tierIndicator.sprite = tierSprite[tierNumber];
            health = tierNumber;
        }
        if (health <= 0 && tierNumber <= 0) killZombeat();


        tierIndicator.sprite = tierSprite[Mathf.Clamp(tierNumber - 1, 0, 3)];

    }

    public void createZombeat(float difficulty)
    {
        generateWeakness();

        weaknessChangeChance = .35f * (10 * difficulty);

        float healthBarChance = Random.Range(.01f, 1f);

        float difficultyChanceChange = Mathf.Clamp(Mathf.Round(-1 + (difficulty / 30f) * 100) / 100, 0, 999);

        healthBarChance -= difficultyChanceChange;

        if (healthBarChance < barChances.z / 100) maxTierNumber = 3;
        else if (healthBarChance < barChances.y / 100) maxTierNumber = 2;
        else if (healthBarChance < barChances.x / 100) maxTierNumber = 1;
        else maxTierNumber = 1;
        tierNumber = maxTierNumber;

        health = tierNumber;

        //Debug.Log("Zombeat: " + gameObject.name + "'s chances for healthbarNumbers is " + healthBarChance + "%");
        //Debug.Log("Which Means Zombeat: " + gameObject.name + " spawns with " + maxTierNumber + " Healthbars");


    }

    void generateWeakness()
    {
        int randomElementIndex = Random.Range(0, 3);
        switch (randomElementIndex)
        {
            case 0:
                zombieWeaknessElement = elements.Metal;
                break;

            case 1:
                zombieWeaknessElement = elements.Electricity;
                break;

            case 2:
                zombieWeaknessElement = elements.Fire;
                break;
        }

        tierIndicator.sprite = tierSprite[tierNumber];
    }

    public IEnumerator hitPlayer()
    {
        StartCoroutine(hitAnimation.playAnim());

        int cumulativeFramesForAnim = 0;
        for (int i = 0; i < hitAnimation.frames.Length; i++) cumulativeFramesForAnim += hitAnimation.frames[i].numberOfFramesTillNext;

        for (int x = 0; x < cumulativeFramesForAnim; x++)
        {
            yield return new WaitForEndOfFrame();
        }

        GameObject.Find("Game Manager").GetComponent<playerManager>().health--;
        for (int y = 0; y < manager.zombeats.Count; y++)
        {
            manager.difficultyNumber -= manager.zombeats.Count;
            manager.zombeats[y].GetComponent<zombeatAI>().killZombeat();
        }
        GameObject.Find("Game Manager").GetComponent<playerManager>().music.currentChordCombo -= 5;
        GameObject.Find("Game Manager").GetComponent<playerManager>().healthBar.fillAmount 
            = (float)GameObject.Find("Game Manager").GetComponent<playerManager>().health / GameObject.Find("Game Manager").GetComponent<playerManager>().classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classHealth;

    }

    public void takeDamage(int damageTaken)
    {
        health -= damageTaken;

        zombeatAudio.clip = zombeatDmgSounds[Random.Range(0, zombeatDmgSounds.Length)];
        zombeatAudio.Play();
    }

    void killZombeat()
    {
        manager.points += (int)(pointWorth * maxTierNumber * (manager.difficultyNumber));
        manager.zombeats.Remove(gameObject);
        //play death sound and particles or anim
        zombeatAudio.clip = deathClips[Random.Range(0, deathClips.Length)];
        zombeatAudio.Play();
        manager.difficultyNumber++;
        Destroy(gameObject);
    }
}
