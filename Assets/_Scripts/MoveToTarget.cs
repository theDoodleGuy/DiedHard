using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target;

    [SerializeField] float moveSpeed;
    [SerializeField] bool turn;
    [SerializeField] float delayMove;

    Animator anim;
    public bool shouldMove;
    Vector2 scale;
    float timeRemaining;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        scale = transform.localScale;
        timeRemaining = delayMove;
    }

    private void Update()
    {
        if (target != null && shouldMove)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining > 0) return;
            transform.localScale = turn ? new Vector2(-scale.x, scale.y) : scale;
            anim.SetBool("isWalking", true);
            StartMoving();
        }
        
    }

    void StartMoving()
    {
        if (transform.position == target.transform.position) shouldMove = false;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }
}
