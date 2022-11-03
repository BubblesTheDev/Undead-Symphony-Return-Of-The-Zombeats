using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiClassGiver : MonoBehaviour
{
    public void giveClass(classDataContainer classToGive) {
        GameObject.Find("GameManager").GetComponent<playerManager>().classSelectHolder.GetComponent<classSelectHolder>().selectedClass = classToGive;
    }
}
