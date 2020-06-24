using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;
    private int randomNumber;
    public GameObject plane;
    private GameObject lastPlane;
    public GameObject[] obstacles;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        lastPlane = transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0,0,player.position.z) - offset;

        if (Input.touchCount > 0 && transform.position.z <= 2300)
        {
            if (timer <= 0)
            {
                randomNumber = Random.Range(0, 3);

                if (randomNumber == 0)
                {
                    lastPlane = Instantiate(plane, transform.position, plane.transform.rotation);
                    timer = 4f;
                }
                else if (randomNumber == 1)
                {
                    Instantiate(obstacles[0], transform.position + new Vector3(0, 1, 0), obstacles[0].transform.rotation);
                    timer = 4f;
                }
                else if (randomNumber == 2)
                {
                    Instantiate(obstacles[1], transform.position + new Vector3(0, 1, 0), obstacles[1].transform.rotation);
                    timer = 4f;
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

    }

    
}
