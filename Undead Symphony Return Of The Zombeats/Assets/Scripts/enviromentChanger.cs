using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enviromentChanger : MonoBehaviour
{
    public GameObject moonObj;
    public GameObject skyPlane;
    public Sprite[] possibleMoons;
    public Sprite[] skyColors;
    public float rotateBounceAngle;
    public float rotateSpeed;

    public GameObject enviromentObj;
    public GameObject[] possibleEnviroments;

    private void Awake()
    {
        int randomInt = Random.Range(0, possibleMoons.Length);
        
        moonObj.GetComponent<Image>().sprite = possibleMoons[randomInt];
        skyPlane.GetComponent<Image>().sprite = skyColors[randomInt];

        /*randomInt = Random.Range(0, possibleEnviroments.Length);
        for (int i = 0; i < 25; i++)
        {
            Instantiate(possibleEnviroments[randomInt], enviromentObj.transform.position + -transform.forward, Quaternion.identity, GameObject.Find("Enviroment").transform);
        }*/
    }

    private void Update()
    {
        float rotAngle = Mathf.SmoothStep(-rotateBounceAngle, rotateBounceAngle, Mathf.PingPong(Time.time * rotateSpeed, 1));
        moonObj.transform.rotation = Quaternion.Euler(-35, 0, rotAngle);
    }
}
