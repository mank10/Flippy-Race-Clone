using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public Text playerRank;
    public GameObject playerBoat;
    public Ranks[] boats;
    private GameObject tempObject;

    void Start()
    {
        SortRank();
        for (int i = 0; i < boats.Length; i++)
        {
            if (boats[i].boat.CompareTag("Player"))
            {
                playerRank.text = boats[i].rank.ToString();
                playerBoat = boats[i].boat;
            }
        }
    }

    void Update()
    {
        for(int i = 0; i< boats.Length; i++)
        {
            if (boats[i].boat.CompareTag("Player"))
            {
                playerRank.text = boats[i].rank.ToString();
                playerBoat = boats[i].boat;
            }
        }

        SortRank();
    }

    void SortRank()
    {
        for(int i = 0; i < boats.Length - 1; i++)
        {
            for(int j = 0; j < boats.Length - 1; j++)
            {
                if(boats[j].boat.transform.position.z < boats[j + 1].boat.transform.position.z)
                {
                    tempObject = boats[j].boat;
                    boats[j].boat = boats[j + 1].boat;
                    boats[j + 1].boat = tempObject;
                }
            }
        }

        for (int i = 0; i < boats.Length; i++)
            boats[i].rank = i + 1;
    }
}
