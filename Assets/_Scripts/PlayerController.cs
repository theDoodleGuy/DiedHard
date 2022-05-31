using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;

    float move;
    Vector2 currentVelocity;
    Rigidbody2D rb;
    Animator anim;

    Vector2 scale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirInput();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }
    void GetDirInput()
    {
        move = Input.GetAxis("Horizontal");

        if (move > 0)
        {
            //look right
            transform.localScale = new Vector2(scale.x, scale.y);
        }
        else if (move < 0)
        {
            //look left
            transform.localScale = new Vector2(-scale.x, scale.y);
        }

        if (move == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        currentVelocity = rb.velocity;
    }
    void PlayerMovement()
    {
        if (move != 0)
        {
            rb.velocity = new Vector2(move * movementSpeed, currentVelocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
