using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBoatAI : MonoBehaviour
{
    public float boatSpeed;
    private Rigidbody rbBoat;
    private bool gameStarted;
    public Transform forcePosition;
    private bool isGrounded;
    private Quaternion initialRotation;

    void Start()
    {
        rbBoat = GetComponent<Rigidbody>();
        gameStarted = false;
        initialRotation = transform.rotation;
    }


    void FixedUpdate()
    {
        if(Input.touchCount > 0 && gameStarted == false)
        {
            gameStarted = true;
        }
        if(gameStarted)
        {
            if(Vector3.Dot(transform.up,Vector3.down) > 0 && isGrounded == true)
            {
                Respawn();
            }
            if (rbBoat.velocity.z <= 40)
            {
                rbBoat.AddForceAtPosition(Vector3.forward * boatSpeed * Time.fixedDeltaTime * 100f, forcePosition.position, ForceMode.Force);
            }
        }

        if(transform.position.z >= 2350)
        {
            rbBoat.velocity = Vector3.zero;
        }

    }

    void Respawn()
    {
        transform.rotation = initialRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Plane")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;

        }
        if (collision.gameObject.tag == "Plane")
        {
            isGrounded = false;

        }
    }
}
