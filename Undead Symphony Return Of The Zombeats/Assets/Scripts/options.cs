using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class options : MonoBehaviour
{


    public static void changeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
