using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class zombeatManager : MonoBehaviour
{
    public int difficultyNumber = 1;
    public float timeBetweenSpawns = 3;
    public int maxNumZombies = 3;
    public List<GameObject> zombeats;
    public int points;
    public GameObject spawnPoint;
    public GameObject zombeatPrefab;
    public float maxOffsetDistance = 1;
    public bool canSpawn;
    [SerializeField]
    GameObject currentZombeat;
    public TMP_Text pointsText;
    public GameObject worldSpaceImages;
    
    private void Update()
    {
        difficultyChanger();

        //regenerateTempZombeat();

        if (zombeats.Count < maxNumZombies && canSpawn)
        {
            StartCoroutine(spawnZombeat());
        }

        pointsText.text = " POINTS: " + points.ToString();
    }

    void difficultyChanger()
    {
        if (difficultyNumber < 1) difficultyNumber = 1;
        if (timeBetweenSpawns > .75f) timeBetweenSpawns = 5 - (difficultyNumber * .35f);
        maxNumZombies = 3 * (1 + Mathf.RoundToInt(difficultyNumber * .15f));
    }

    IEnumerator spawnZombeat()
    {
        canSpawn = false;

        Vector3 offset = new Vector3(Random.Range(-maxOffsetDistance, maxOffsetDistance),0,0);

        currentZombeat = Instantiate(zombeatPrefab, spawnPoint.transform.position + offset, Quaternion.identity, worldSpaceImages.transform);
        currentZombeat.name = "Zombeat (" + zombeats.Count + ")";
        yield return new WaitForEndOfFrame();
        zombeats.Add(currentZombeat);
        
        yield return new WaitForSeconds(timeBetweenSpawns);
        canSpawn = true;
    }

    /*void regenerateTempZombeat()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            canSpawn = false;

            zombeats.Clear();
            Destroy(currentZombeat);

            currentZombeat = Instantiate(zombeatPrefab, spawnPoint.transform.position + new Vector3(Random.Range(-maxOffsetDistance, maxOffsetDistance), 0, 0), Quaternion.identity, GameObject.Find("WorldSpaceImages").transform);
            currentZombeat.GetComponent<zombeatAI>().createZombeat(difficultyNumber);
            
            zombeats.Add(currentZombeat);
        }
    }*/

}
