using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class chordControlShower : MonoBehaviour
{
    public GameObject[] chordControlObj;
    public chordAssetList chordListReference;
    public int indexOffset;
    public Image[] notes;
    private void Awake()
    {
        for (int i = 0; i < chordControlObj.Length; i++)
        {
            chordControlObj[i].GetComponentInChildren<TMP_Text>().text = chordListReference.possibleChords[i + indexOffset].chordElement.ToString().ToUpper() + " CHORD: ";

            notes = chordControlObj[i].GetComponentsInChildren<Image>();
            for (int x = 0; x < notes.Length; x++)
            {
                notes[x].sprite = chordListReference.possibleChords[i + indexOffset].notesForChord[x].noteSprite;
            }
        }
    }
}
