using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animHandler : MonoBehaviour
{
    public Image animImage;
    public animationFrame[] frames;
    public bool loopAnim;

    public IEnumerator playAnim()
    {
        //Debug.Log("Starting Anim");
        for (int i = 0; i < frames.Length; i++)
        {
            animImage.sprite = frames[i].animSprite;
            for (int x = 0; x < frames[i].numberOfFramesTillNext; x++)
            {
                yield return null;
            }
        }

        if (loopAnim) StartCoroutine(playAnim());
    }
}

[System.Serializable]
public struct animationFrame
{
    public int numberOfFramesTillNext;
    public Sprite animSprite;
}
