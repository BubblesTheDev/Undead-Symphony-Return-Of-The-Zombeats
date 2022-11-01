using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class musicPlayer : MonoBehaviour
{

    public List<note> recentNotes = new List<note>();
    public int currentNoteCombo, maxNoteCombo;
    public chordAssetList chordList;

    private void Update()
    {
        playNote();
        compareChord();
    }

    void compareChord()
    {
        if(Input.GetKeyUp(KeyCode.G) && recentNotes.Count >= 3)
        {
            Debug.Log("Compare Chords");
            foreach (chord possibleChord in chordList.possibleChords)
            {
                if(possibleChord.notesForChord[0].buttonColour == recentNotes[0].buttonColour && possibleChord.notesForChord[1].buttonColour == recentNotes[1].buttonColour && possibleChord.notesForChord[2].buttonColour == recentNotes[2].buttonColour)
                {
                    //Damage Enemy With Element
                    recentNotes.Clear();
                    Debug.Log("Damaged Enemy With " + possibleChord.chordElement.ToString());
                }
            }
        }
    }

    void playNote()
    {
        if (recentNotes.Count >= 3) return;
        if (Input.GetKeyUp(KeyCode.A)) recentNotes.Add(new note(buttonColour.red));
        if (Input.GetKeyUp(KeyCode.S)) recentNotes.Add(new note(buttonColour.green));
        if (Input.GetKeyUp(KeyCode.D)) recentNotes.Add(new note(buttonColour.blue));
        if (Input.GetKeyUp(KeyCode.F)) recentNotes.Add(new note(buttonColour.yellow));
    }

}

public enum elements
{
    Metal,
    Electricity,
    Light,
    Fire
}

public enum buttonColour
{
    red,
    blue,
    yellow,
    green
}

[System.Serializable]
public class note
{
    public note(buttonColour pressedButtonColor)
    {
        buttonColour = pressedButtonColor;
    }

    public buttonColour buttonColour;
}

[System.Serializable]
public class chord
{
    public elements chordElement;
    public note[] notesForChord;
}
