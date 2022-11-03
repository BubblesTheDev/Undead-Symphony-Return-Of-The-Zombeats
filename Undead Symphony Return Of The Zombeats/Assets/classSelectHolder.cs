using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class classSelectHolder : MonoBehaviour
{
    public classDataContainer selectedClass;

    public void selectClass(classDataContainer container) {
        selectedClass = container;
    }
}
