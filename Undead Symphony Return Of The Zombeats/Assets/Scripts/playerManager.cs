using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    [Header("Class Stats")]
    public zombeatManager manager;
    public musicPlayer music;
    public GameObject targetSigil;
    public Image characterImage;
    public int health;
    public AudioSource playerAttack;
    public AudioSource playerUlt;
    public animHandler playerAnim;
    public AudioSource wrongChord;
    public GameObject classSelectHolder;

    [Header("Targeting")]
    float closestDistance = 999;
    public GameObject zombeatTargeted;

    [Header("Ui Animation")]
    public float pulseSize;
    public float pulseSpeed;
    public float rotSpeed;
    public Vector3 imageScale;
    private float startingScale;
    public GameObject lightChordControls;
    public GameObject medChordControls;
    public GameObject HeavyChordControls;
    public Image healthBar;

    public void OnEnable() {
        classSelectHolder.GetComponent<classSelectHolder>();

        imageScale = new Vector3(targetSigil.transform.localScale.x, targetSigil.transform.localScale.y, targetSigil.transform.localScale.z);
        startingScale = targetSigil.transform.localScale.x;
        playerAttack.clip = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classAttack;
        playerUlt.clip = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classUlt;
        playerAnim.frames = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.animFrames;
        characterImage.sprite = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.animFrames[0].animSprite;
        targetSigil.GetComponent<Image>().sprite = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classArcaneSigil;
        health = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classHealth;
        healthBar.fillAmount = (float)health / classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classHealth;
    }

    private void Awake() {
        classSelectHolder.GetComponent<classSelectHolder>();

        imageScale = new Vector3(targetSigil.transform.localScale.x, targetSigil.transform.localScale.y, targetSigil.transform.localScale.z);
        startingScale = targetSigil.transform.localScale.x;
        playerAttack.clip = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classAttack;
        playerUlt.clip = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classUlt;
        playerAnim.frames = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.animFrames;
        characterImage.sprite = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.animFrames[0].animSprite;
        targetSigil.GetComponent<Image>().sprite = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classArcaneSigil;
        health = classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classHealth;
        healthBar.fillAmount = (float)health / classSelectHolder.GetComponent<classSelectHolder>().selectedClass.classHealth;
    }

    private void Update()
    {
        animateUI();
        targetZombeat();

        /*if (zombeatTargeted.GetComponent<zombeatAI>().tier == chordWeight.light) lightChordControls.SetActive(true);
        else lightChordControls.SetActive(false);
        if (zombeatTargeted.GetComponent<zombeatAI>().tier == chordWeight.medium) medChordControls.SetActive(true);
        else medChordControls.SetActive(false);
        if (zombeatTargeted.GetComponent<zombeatAI>().tier == chordWeight.heavy) HeavyChordControls.SetActive(true);
        else HeavyChordControls.SetActive(false);*/
    }

    void targetZombeat()
    {
        if (zombeatTargeted == null) closestDistance = 999;

        foreach (GameObject zombeat in manager.zombeats)
        {
            if(Vector3.Distance(zombeat.transform.position, transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(zombeat.transform.position, transform.position);
                zombeatTargeted = zombeat;
            }
        }
    }

    void animateUI()
    {
        if (zombeatTargeted != null)
        {
            targetSigil.transform.position = zombeatTargeted.transform.position;
            zombeatTargeted.transform.SetSiblingIndex(zombeatTargeted.transform.parent.transform.childCount - 2);
            targetSigil.transform.SetAsLastSibling();
            
        }
        targetSigil.transform.Rotate(transform.forward, transform.localRotation.z + rotSpeed * Time.deltaTime);

        Vector3 scaleVector = Vector3.one * (startingScale + pulseSize/2 + (Mathf.Sin(pulseSpeed * Time.time)) * pulseSize/2);
        targetSigil.transform.localScale = scaleVector;
    }
}
