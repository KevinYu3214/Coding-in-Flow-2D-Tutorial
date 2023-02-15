

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // strength of the push
    public float jumpStrength = 15f;

    [Header("Ground setup")]
    //if the object collides with another object tagged as this, it can jump again
    public string groundTag = "Ground";


    //this determines if the script has to check for when the player touches the ground to enable him to jump again
    //if not, the player can jump even while in the air
    public bool checkGround = true;

    public float downTime, startTime = 0;
    public float countDown = 2.0f;
    public bool release = false;

    public bool canJump = true;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)  && release == false && canJump)
        {
            if (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow))
            {
                startTime = Time.time;
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) && canJump)
        {
            release = true;
        }

        if (release == true && canJump && Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKeyUp(KeyCode.LeftArrow))
        {

            // Calculate the force for the jump
            jumpForce();

            // Apply an instantaneous upwards force
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpStrength * downTime, ForceMode2D.Impulse);
            canJump = !checkGround;
        }

        if (release == true && canJump && Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {

            // Calculate the force for the jump
            jumpForce();

            // Apply an instantaneous upwards force
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpStrength * downTime, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().AddForce((Vector2.right * jumpStrength * downTime)/3, ForceMode2D.Impulse);
            canJump = !checkGround;
        }

        if (release == true && canJump && Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            // Calculate the force for the jump
            jumpForce();

            // Apply an instantaneous upwards force
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpStrength * downTime, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().AddForce((Vector2.left * jumpStrength * downTime)/3, ForceMode2D.Impulse);
            canJump = !checkGround;
        }

        // Prevents user from charging the jump midair
        if (!canJump)
        {
            startTime = Time.time;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collisionData)
    {
        if (checkGround
            && collisionData.gameObject.CompareTag(groundTag))
        {
            canJump = true;
        }
    }



    private void jumpForce()
    {
        // sets max charge strength at 2 (1.5 second total charge time)
        downTime = (float).7 + (Time.time - startTime);
        release = false;
        if (downTime > (float)1.5)
        {
            downTime = (float)1.5;
        }

        downTime = downTime * downTime;

    }


}
