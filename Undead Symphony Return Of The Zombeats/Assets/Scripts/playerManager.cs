using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    [Header("Class Stats")]
    public classDataContainer selectedClass;
    public zombeatManager manager;
    public musicPlayer music;
    public GameObject targetSigil;
    public Image characterImage;
    public int health;
    public AudioSource playerAttack;
    public AudioSource playerUlt;
    public animHandler playerAnim;
    public AudioSource wrongChord;

    [Header("Targeting")]
    float closestDistance = 999;
    public GameObject zombeatTargeted;

    [Header("Ui Animation")]
    public float pulseSize;
    public float pulseSpeed;
    public float rotSpeed;
    public Vector3 imageScale;
    private float startingScale;

    private void Awake()
    {
        imageScale = new Vector3(targetSigil.transform.localScale.x, targetSigil.transform.localScale.y, targetSigil.transform.localScale.z);
        startingScale = targetSigil.transform.localScale.x;
        playerAttack.clip = selectedClass.classAttack;
        playerUlt.clip = selectedClass.classUlt;
        playerAnim.frames = selectedClass.animFrames;
        health = 3;
        characterImage.sprite = selectedClass.animFrames[0].animSprite;
    }

    private void Update()
    {
        animateUI();
        targetZombeat();
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
