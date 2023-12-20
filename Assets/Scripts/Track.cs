using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{

    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;
    public GameObject coin;
    public Vector2 numberOfcoins;


    public List<GameObject> newObstacles;
    public List<GameObject> newCoins;

    // Start is called before the first frame update
    void Start()
    {
        int newNumberOfObstacles =(int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);
        int newNumberOfCoins = (int)Random.Range(numberOfcoins.x, numberOfcoins.y);
        for (int i = 0; i < newNumberOfObstacles; i++)
        { 
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
        }
        for (int i = 0; i < newNumberOfCoins; i++)
        {
            newCoins.Add(Instantiate(coin, transform));
            newCoins[i].SetActive(false);
        }
        PositionateObstacles();
        PositionateCoins();
    }

    void PositionateObstacles()
    {
        for (int i = 0; i < newObstacles.Count; i++)
        {
            float posZmin = (297f / newObstacles.Count) + (297f / newObstacles.Count) * i;
            float posZmax = (297f / newObstacles.Count) + (297f / newObstacles.Count) * i + 1;
            newObstacles[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZmax, posZmin));
            newObstacles[i].SetActive(true);
        }
    }
    void PositionateCoins()
    {
        float minZpos = 10f;
        for (int i = 0; i < newCoins.Count; i++)
        {
            float maxPosZ = minZpos + 5f;
            float randomZpos = Random.Range(minZpos, maxPosZ);
            newCoins[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y, randomZpos);
            newCoins[i].SetActive(true);
            minZpos = randomZpos + 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector3(0, 0, transform.position.z + 297 * 2);
            PositionateObstacles();
            PositionateCoins();

        }
    }
}
