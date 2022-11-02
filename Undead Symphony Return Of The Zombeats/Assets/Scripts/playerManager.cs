using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class playerManager : MonoBehaviour
{
    [Header("Class Stats")]
    public classDataContainer selectedClass;
    public zombeatManager manager;
    public musicPlayer music;
    public GameObject targetSigil;
    public Image characterImage;

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
    }

    private void Update()
    {
        animateUI();
        targetZombeat();
    }

    void targetZombeat()
    {
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
