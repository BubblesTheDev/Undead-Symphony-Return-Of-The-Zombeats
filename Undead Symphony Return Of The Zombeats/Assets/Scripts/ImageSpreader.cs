using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSpreader : MonoBehaviour
{
    public GameObject targetSigil;
    public float distanceFromCenter;
    public GameObject[] elementChords;

    public void addAnotherNote(int chordElementIndex)
    {
        Vector3 tempVector = new Vector3(targetSigil.transform.position.x + Random.insideUnitCircle.x, targetSigil.transform.position.y + Random.insideUnitCircle.y, targetSigil.transform.position.z);
        Instantiate(elementChords[chordElementIndex], tempVector * distanceFromCenter, Quaternion.identity, targetSigil.transform.parent) ;
    }


}
