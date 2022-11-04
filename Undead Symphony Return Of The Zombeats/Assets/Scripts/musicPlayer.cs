using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicPlayer : MonoBehaviour
{

    public List<note> recentNotes = new List<note>();
    public List<chord> damageChords = new List<chord>();

    public int currentDamageStoredOnZombeat;
    public int currentChordCombo;

    public chordAssetList chordListLight;
    public chordAssetList chordListMed;
    public chordAssetList chordListHeavy;
    playerManager manager;

    public Image[] noteImages;
    public Sprite[] noteSprites;
    public Sprite transparent;
    public ImageSpreader spreader;

    public Image comboMeter;
    public Sprite[] powerMeterImages;

    private void OnEnable() {
        manager = GetComponent<playerManager>();
        spreader = GetComponent<ImageSpreader>();
    }

    private void Awake()
    {
        manager = GetComponent<playerManager>();
        spreader = GetComponent<ImageSpreader>();
    }

    private void Update()
    {
        playNote();
        compareChord();

        //Find Player class manager and get class and targeted zombeat
        if (Input.GetKeyUp(KeyCode.F))
        {
            damageZombeat(manager.zombeatTargeted);
            damageChords.Clear();
            currentDamageStoredOnZombeat = 0;
        }

        handlePower();
    }

    void damageZombeat(GameObject zombeat)
    {
        foreach (chord damageChord in damageChords)
        {
            if (zombeat.GetComponent<zombeatAI>().zombieWeaknessElement == damageChord.chordElement && zombeat.GetComponent<zombeatAI>().tier == damageChord.weight) currentDamageStoredOnZombeat++;
        }
        if (currentDamageStoredOnZombeat <= 0) return;


        StartCoroutine(manager.playerAnim.playAnim());
        manager.playerAttack.Play();
        zombeat.GetComponent<zombeatAI>().takeDamage(currentDamageStoredOnZombeat);
        currentDamageStoredOnZombeat = 0;
        //Debug.Log("Attacked Zombeat " + zombeat.name + " with " + currentDamageStoredOnZombeat + " damage");
    }

    void compareChord()
    {
        if (recentNotes.Count >= 3)
        {
            //Debug.Log("Compare Chords");
            if(manager.zombeatTargeted.GetComponent<zombeatAI>().tier == chordWeight.light) foreach (chord possibleChord in chordListLight.possibleChords)
            {
                if (possibleChord.notesForChord[0].Colour == recentNotes[0].Colour
                    && possibleChord.notesForChord[1].Colour == recentNotes[1].Colour
                    && possibleChord.notesForChord[2].Colour == recentNotes[2].Colour)
                {
                    //Store Newly made chord from player
                    damageChords.Add(new chord(recentNotes[0], recentNotes[1], recentNotes[2], possibleChord.chordElement, chordWeight.light));
                    switch (possibleChord.chordElement)
                    {
                        case elements.Metal:
                            spreader.addAnotherNote(0);
                            currentDamageStoredOnZombeat++;
                            manager.zombeatTargeted.GetComponent<zombeatAI>().text.text = "DMG: " + (currentDamageStoredOnZombeat + 1);
                            break;
                        case elements.Fire:
                            spreader.addAnotherNote(1);
                            currentDamageStoredOnZombeat++;

                            manager.zombeatTargeted.GetComponent<zombeatAI>().text.text = "DMG: " + (currentDamageStoredOnZombeat + 1);

                            break;
                        case elements.Electricity:
                            spreader.addAnotherNote(2);
                            currentDamageStoredOnZombeat++;
                            manager.zombeatTargeted.GetComponent<zombeatAI>().text.text = "DMG: " + (currentDamageStoredOnZombeat + 1);
                            break;
                    }
                    currentChordCombo++;
                } else manager.wrongChord.Play();
            }
            else if (manager.zombeatTargeted.GetComponent<zombeatAI>().tier == chordWeight.medium) foreach (chord possibleChord in chordListMed.possibleChords)
            {
                if (possibleChord.notesForChord[0].Colour == recentNotes[0].Colour
                    && possibleChord.notesForChord[1].Colour == recentNotes[1].Colour
                    && possibleChord.notesForChord[2].Colour == recentNotes[2].Colour)
                {
                    //Store Newly made chord from player
                    damageChords.Add(new chord(recentNotes[0], recentNotes[1], recentNotes[2], possibleChord.chordElement, chordWeight.medium));
                    switch (possibleChord.chordElement)
                    {
                        case elements.Metal:
                            spreader.addAnotherNote(0+3);
                            currentDamageStoredOnZombeat++;
                            manager.zombeatTargeted.GetComponent<zombeatAI>().text.text = "DMG: " + (currentDamageStoredOnZombeat + 1);
                            break;
                        case elements.Fire:
                            spreader.addAnotherNote(1 + 3);
                            currentDamageStoredOnZombeat++;
                            manager.zombeatTargeted.GetComponent<zombeatAI>().text.text = "DMG: " + (currentDamageStoredOnZombeat + 1);
                            break;
                        case elements.Electricity:
                            spreader.addAnotherNote(2 + 3);
                            currentDamageStoredOnZombeat++;
                            manager.zombeatTargeted.GetComponent<zombeatAI>().text.text = "DMG: " + (currentDamageStoredOnZombeat + 1);
                            break;
                    }
                    currentChordCombo++;
                }
                else manager.wrongChord.Play();
            }
            else if (manager.zombeatTargeted.GetComponent<zombeatAI>().tier == chordWeight.heavy) foreach (chord possibleChord in chordListHeavy.possibleChords)
            {
                if (possibleChord.notesForChord[0].Colour == recentNotes[0].Colour
                    && possibleChord.notesForChord[1].Colour == recentNotes[1].Colour
                    && possibleChord.notesForChord[2].Colour == recentNotes[2].Colour)
                {
                    //Store Newly made chord from player
                    damageChords.Add(new chord(recentNotes[0], recentNotes[1], recentNotes[2], possibleChord.chordElement, chordWeight.heavy));
                    switch (possibleChord.chordElement)
                    {
                        case elements.Metal:
                            spreader.addAnotherNote(0+6);
                            currentDamageStoredOnZombeat++;
                            manager.zombeatTargeted.GetComponent<zombeatAI>().text.text = "DMG: " + (currentDamageStoredOnZombeat + 1);
                            break;
                        case elements.Fire:
                            spreader.addAnotherNote(1 + 6);
                            currentDamageStoredOnZombeat++;
                            manager.zombeatTargeted.GetComponent<zombeatAI>().text.text = "DMG: " + (currentDamageStoredOnZombeat + 1);
                            break;
                        case elements.Electricity:
                            spreader.addAnotherNote(2 + 6);
                            currentDamageStoredOnZombeat++;
                            manager.zombeatTargeted.GetComponent<zombeatAI>().text.text = "DMG: " + (currentDamageStoredOnZombeat + 1);
                            break;
                    }
                    currentChordCombo++;
                }
                else manager.wrongChord.Play();
            }
            recentNotes.Clear();
            for (int i = 0; i < noteImages.Length; i++)
            {
                noteImages[i].sprite = transparent;
            }
        }

    }

    void playNote()
    {
        if (recentNotes.Count >= 3) return;
        if (Input.GetKeyUp(KeyCode.A))
        {
            recentNotes.Add(new note(buttonColour.green));
            switch (recentNotes.Count-1) {
                case 0:
                recentNotes[0].noteSprite = noteSprites[0];
                break;
                case 1:
                recentNotes[1].noteSprite = noteSprites[1];
                break;
                case 2:
                recentNotes[2].noteSprite = noteSprites[2];
                break;
            }
            if (noteImages[recentNotes.Count-1].sprite == transparent) noteImages[recentNotes.Count-1].sprite = noteSprites[0];
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            recentNotes.Add(new note(buttonColour.red));
            switch (recentNotes.Count-1) {
                case 0:
                recentNotes[0].noteSprite = noteSprites[0];
                break;
                case 1:
                recentNotes[1].noteSprite = noteSprites[1];
                break;
                case 2:
                recentNotes[2].noteSprite = noteSprites[2];
                break;
            }
            if (noteImages[recentNotes.Count-1].sprite == transparent) noteImages[recentNotes.Count-1].sprite = noteSprites[1];
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            recentNotes.Add(new note(buttonColour.blue));
            switch (recentNotes.Count-1) {
                case 0:
                recentNotes[0].noteSprite = noteSprites[0];
                break;
                case 1:
                recentNotes[1].noteSprite = noteSprites[1];
                break;
                case 2:
                recentNotes[2].noteSprite = noteSprites[2];
                break;
            }
            if (noteImages[recentNotes.Count-1].sprite == transparent) noteImages[recentNotes.Count-1].sprite = noteSprites[2];
        }
    }

    void handlePower()
    {
        comboMeter.fillAmount = (float)currentChordCombo / manager.classSelectHolder.GetComponent<classSelectHolder>().selectedClass.comboRequiredForPower;
        //Debug.Log((float)currentChordCombo / manager.selectedClass.comboRequiredForPower);
        if (currentChordCombo < manager.classSelectHolder.GetComponent<classSelectHolder>().selectedClass.comboRequiredForPower) comboMeter.sprite = powerMeterImages[0];
        else comboMeter.sprite = powerMeterImages[1];

        if(currentChordCombo >= manager.classSelectHolder.GetComponent<classSelectHolder>().selectedClass.comboRequiredForPower && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.F))
        {
            Debug.Log("Actiave Power");
            currentChordCombo = 0;
        }
    }

}

public enum elements
{
    Metal,
    Fire,
    Electricity
}

public enum buttonColour
{
    green,
    red,
    blue
}

public enum chordWeight
{
    light,
    medium,
    heavy
}

[System.Serializable]
public class note
{
    public note(buttonColour pressedButtonColor)
    {
        Colour = pressedButtonColor;
    }
    public buttonColour Colour;
    public Sprite noteSprite;

}

[System.Serializable]
public class chord
{
    public elements chordElement;
    public chordWeight weight;
    public note[] notesForChord;

    public chord(note note1, note note2, note note3, elements damageElement, chordWeight givenWeight)
    {
        chordElement = damageElement;
        weight = givenWeight;
        notesForChord = new note[3] { note1, note2, note3 };
    }


}
