using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class musicPlayer : MonoBehaviour {

    public List<note> recentNotes = new List<note>();
    public List<chord> damageChords = new List<chord>();
    public int currentDamageStoredOnZombeat;
    public int currentChordCombo;
    public chordAssetList chordList;
    playerManager manager;

    private void Awake() {
        manager = GetComponent<playerManager>();
    }

    private void Update() {
        playNote();
        compareChord();

        //Find Player class manager and get class and targeted zombeat
        if (Input.GetKeyUp(KeyCode.G)) {
            damageZombeat(manager.zombeatTargeted);
            damageChords.Clear();
            currentDamageStoredOnZombeat = 0;
        }
    }

    void damageZombeat(GameObject zombeat) {
        foreach (chord damageChord in damageChords) {
            if (zombeat.GetComponent<zombeatAI>().zombieWeaknessElement == damageChord.chordElement) currentDamageStoredOnZombeat++;
        }
        if (currentDamageStoredOnZombeat <= 0) return;
        zombeat.GetComponent<zombeatAI>().takeDamage(currentDamageStoredOnZombeat);
        Debug.Log("Attacked Zombeat " + zombeat.name + " with " + currentDamageStoredOnZombeat + " damage");
    }

    void compareChord() {
        if (recentNotes.Count >= 3) {
            //Debug.Log("Compare Chords");
            foreach (chord possibleChord in chordList.possibleChords) {
                if (possibleChord.notesForChord[0].buttonColour == recentNotes[0].buttonColour
                    && possibleChord.notesForChord[1].buttonColour == recentNotes[1].buttonColour
                    && possibleChord.notesForChord[2].buttonColour == recentNotes[2].buttonColour) {
                    //Store Newly made chord from player
                    damageChords.Add(new chord(recentNotes[0], recentNotes[1], recentNotes[2], possibleChord.chordElement));


                }
            }
            recentNotes.Clear();
        }

    }

    void playNote() {
        if (recentNotes.Count >= 3) return;
        if (Input.GetKeyUp(KeyCode.A)) recentNotes.Add(new note(buttonColour.red));
        if (Input.GetKeyUp(KeyCode.S)) recentNotes.Add(new note(buttonColour.green));
        if (Input.GetKeyUp(KeyCode.D)) recentNotes.Add(new note(buttonColour.blue));
        if (Input.GetKeyUp(KeyCode.F)) recentNotes.Add(new note(buttonColour.yellow));
    }

}

public enum elements {
    Metal,
    Electricity,
    Light,
    Fire
}

public enum buttonColour {
    red,
    blue,
    yellow,
    green
}

[System.Serializable]
public class note {

    public note(buttonColour pressedButtonColor) {
        buttonColour = pressedButtonColor;
    }
    public buttonColour buttonColour;

}

[System.Serializable]
public class chord {
    public elements chordElement;
    public note[] notesForChord;

    public chord(note note1, note note2, note note3, elements damageElement) {
        chordElement = damageElement;
        notesForChord = new note[3] { note1, note2, note3 };
    }


}
