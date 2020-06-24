using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform cameraPosition;
    public Transform player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = GetComponent<Transform>();
        offset = cameraPosition.position - player.transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition.position = player.transform.position + offset;
    }
}
