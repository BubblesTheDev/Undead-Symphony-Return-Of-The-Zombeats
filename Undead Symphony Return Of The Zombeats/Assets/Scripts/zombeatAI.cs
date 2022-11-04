using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class zombeatAI : MonoBehaviour {

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
    float tierChance;
    float difficultyChanceChange;
    public TMP_Text text;

    private void Awake() {
        player = GameObject.Find("Game Manager");
        manager = player.GetComponent<zombeatManager>();
        transform.SetSiblingIndex(2);
        stoppingDistance += Random.Range(-.5f, .5f);
        zombeatAudio = GetComponent<AudioSource>();

        createZombeat();

        StartCoroutine(walkAnimation.playAnim());
    }

    private void Update() {
        if (isDead) return;

        if (Vector3.Distance(transform.position, player.transform.position) >= stoppingDistance) {
            transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);
        } else {
            walkAnimation.loopAnim = false;
            walkAnimation.stopAnim();
            StartCoroutine(hitPlayer());
        }

        tierIndicator.fillAmount = 1;
    }

    public void createZombeat() {

        weaknessChangeChance = .35f * (10 * manager.difficultyNumber);

        tierChance = Random.Range(.01f, 1f);

        difficultyChanceChange = (float)System.Math.Round(Mathf.Clamp((-1 + (manager.difficultyNumber / 30f) * 100) / 100, 0.01f, 999),2);

        tierChance -= difficultyChanceChange;
        tierChance = (float)System.Math.Round(tierChance, 2);
        if (tierChance < 0) tierChance = 0.01f;
        Debug.Log("The Tier Chance was: " + tierChance + " For " + gameObject.name) ;

        if (tierChance < barChances.z / 100) maxTierNumber = 3;
        else if (tierChance < barChances.y / 100) maxTierNumber = 2;
        else if (tierChance < barChances.x / 100) maxTierNumber = 1;
        else maxTierNumber = 1;
        generateWeakness();

    }

    void generateWeakness() {
        int randomElementIndex = Random.Range(0, 3);
        switch (randomElementIndex) {
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

        tierNumber = maxTierNumber;

        health = 1;

        tier = (chordWeight)maxTierNumber - 1;

        switch ((int)tier) {
            case 0:
            tierIndicator.sprite = tierSprite[((int)zombieWeaknessElement) + (int)tier];
            break;
            case 1:
            tierIndicator.sprite = tierSprite[((int)zombieWeaknessElement) + 2 + (int)tier];
            break;
            case 2:
            tierIndicator.sprite = tierSprite[((int)zombieWeaknessElement) + 5 + (int)tier];
            break;
        }
    }

    public IEnumerator hitPlayer() {
        StartCoroutine(hitAnimation.playAnim());

        int cumulativeFramesForAnim = 0;
        for (int i = 0; i < hitAnimation.frames.Length; i++) cumulativeFramesForAnim += hitAnimation.frames[i].numberOfFramesTillNext;

        for (int x = 0; x < cumulativeFramesForAnim; x++) {
            yield return new WaitForEndOfFrame();
        }

        GameObject.Find("Game Manager").GetComponent<playerManager>().health--;
        for (int y = 0; y < manager.zombeats.Count; y++) {
            manager.difficultyNumber -= manager.zombeats.Count;
            manager.zombeats[y].GetComponent<zombeatAI>().killZombeat();
        }
        GameObject.Find("Game Manager").GetComponent<playerManager>().music.currentChordCombo -= 5;
        if (GameObject.Find("Game Manager").GetComponent<playerManager>().music.currentChordCombo < 0) GameObject.Find("Game Manager").GetComponent<playerManager>().music.currentChordCombo = 0;
        GameObject.Find("Game Manager").GetComponent<playerManager>().healthBar.fillAmount
            = (float)GameObject.Find("Game Manager").GetComponent<playerManager>().health / GameObject.Find("Game Manager").GetComponent<playerManager>().classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classHealth;

    }

    public void takeDamage(int damageTaken) {
        health -= damageTaken;

        zombeatAudio.clip = zombeatDmgSounds[Random.Range(0, zombeatDmgSounds.Length)];
        zombeatAudio.Play();

        /*if (health <= 0 && tierNumber >= 1) {
            tierNumber--;
            tierIndicator.sprite = tierSprite[((int)zombieWeaknessElement) * 1 + (int)tier];
            tier = (chordWeight)tierNumber;
            health = tierNumber;
        }*/

        if (health <= 0 ) killZombeat();
    }

    void killZombeat() {
        manager.points += (int)(pointWorth * maxTierNumber * (1+manager.difficultyNumber/10));
        manager.zombeats.Remove(gameObject);
        //play death sound and particles or anim
        zombeatAudio.clip = deathClips[Random.Range(0, deathClips.Length)];
        zombeatAudio.Play();
        manager.difficultyNumber++;
        Destroy(gameObject);
    }
}
