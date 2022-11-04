using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum classWeapon
{
    keytana,
    MegaAxyl,
    IsThatAFret
}

[CreateAssetMenu(fileName = "New Class", menuName = "Create New Class")]
public class classDataContainer : ScriptableObject
{
    public string className;
    public Sprite classArcaneSigil;
    public int classHealth;
    public int comboRequiredForPower;
    public classWeapon weapon;
    public AudioClip classAttack;
    public AudioClip classUlt;
    public animationFrame[] animFrames;
}
