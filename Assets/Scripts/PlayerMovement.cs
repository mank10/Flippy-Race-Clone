using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public int sideForce;
    private Touch touch;
    public bool isGrounded;
    public float upwardTorque;
    public float flipForce;
    public GameObject rankManager;
    public GameObject youWin;
    public GameObject youLose;
    public GameObject tapStart;
    public Text flipCountText;
    private RankManager scriptRankManager;

    private Vector2 startTouchPosition;
    private Vector2 direction;
    private bool touchBegan, touchMoved;
    private int flipCount;
    private float startAngleFlip;
    private bool startNewFlip;

    void Start()
    {
        scriptRankManager = rankManager.GetComponent<RankManager>();
        playerRb = GetComponent<Rigidbody>();
        startAngleFlip = transform.eulerAngles.z;
    }

    void FixedUpdate()
    {
        if(transform.position.y < -5)
        {
            Death();
        }

        if (Vector3.Dot(transform.up, Vector3.down) > 0 && isGrounded == true)
        {
            playerRb.velocity = Vector3.zero;
            Invoke("Death", 1f);
        }

        if (Input.touchCount > 0)
        {
            if(tapStart.activeInHierarchy == true)
            {
                tapStart.SetActive(false);
            }

            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    touchBegan = true;
                    break;
                case TouchPhase.Moved:
                    touchMoved = true;
                    break;

                case TouchPhase.Ended:
                    touchBegan = false;
                    break;
            }

            if (isGrounded)
            {
                playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

                if(transform.position.z >= 2350)
                {
                    playerRb.velocity = Vector3.zero;
                    if(scriptRankManager.playerRank.text == "1")
                    {
                        youWin.SetActive(true);
                        SceneManager.LoadScene("Level 2");
                    }
                    else
                    {
                        youLose.SetActive(true);
                        SceneManager.LoadScene("Level 1");
                    }
                }

                if(flipCount > 0)
                {
                    playerRb.AddForce(Vector3.forward * flipForce * Time.fixedDeltaTime * 100, ForceMode.Force);
                }

                flipCount = 0;

                if (touchBegan)
                {
                    if (playerRb.velocity.z <= 45)
                    {
                        playerRb.AddForce(Vector3.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
                    }
                    if (touchMoved)
                    {
                        direction = touch.position - startTouchPosition;
                        if (direction.x < -20)
                        {
                            playerRb.AddForce(Vector3.left * sideForce * Time.fixedDeltaTime * 100, ForceMode.Force);
                        }
                        else if (direction.x > 20)
                        {
                            playerRb.AddForce(Vector3.right * sideForce * Time.fixedDeltaTime * 100, ForceMode.Force);
                        }
                    }
                }
                else
                {
                    Input.ResetInputAxes();
                    playerRb.ResetInertiaTensor();
                }
            }
            else
            {
                playerRb.constraints = RigidbodyConstraints.None;
                playerRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
                playerRb.AddTorque(Vector3.left * upwardTorque * Time.fixedDeltaTime * 100, ForceMode.Acceleration);

                
                if(transform.eulerAngles.z - startAngleFlip < 5 && startNewFlip)
                {
                    flipCount++;
                    flipCountText.text = flipCount.ToString();
                    startNewFlip = false;
                }
                if(transform.eulerAngles.z - startAngleFlip >= 350  && startNewFlip == false)
                {
                    startNewFlip = true;
                }
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
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

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
