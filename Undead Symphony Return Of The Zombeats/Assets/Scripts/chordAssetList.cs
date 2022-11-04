using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Chord List", menuName = "Create Chord List")]
public class chordAssetList : ScriptableObject
{
    public chordWeight chordWeights;
    public chord[] possibleChords;
    
}


