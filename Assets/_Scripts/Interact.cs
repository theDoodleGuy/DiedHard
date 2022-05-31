using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] GameObject uiHelper;
    [SerializeField] GameObject affectedHostile;
    [SerializeField] GameObject landedItem;
    [SerializeField] float delay = 2f;
    [SerializeField] float moveSpeed = 5f;

    public bool canRepeat;
    public bool canInteract;
    public bool willAlertHostile;
    public bool stopInteractions;

    GameObject player;
    Animator anim;
    Animator uiAnim;
    Animator hostileAnim;

    Vector3 startPos;
    Vector3 targetPos;
    Vector3 currentTargetPos;

    bool movingTowards;

    private void Start()
    {
        anim = GetComponent<Animator>();
        uiAnim = uiHelper.GetComponent<Animator>();
        hostileAnim = affectedHostile.GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>().gameObject;

        targetPos = landedItem.transform.position;
        startPos = affectedHostile.transform.position;
    }

    private void Update()
    {
        if (movingTowards)
        {
            MoveTowards(currentTargetPos);
        };
        if (!canInteract) return;
        if (stopInteractions) return;

        if (canInteract && Input.GetKeyDown(KeyCode.Space))
        {
            Interaction();
            if (!canRepeat) stopInteractions = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stopInteractions) return;
        if (!collision.CompareTag("Player")) return;
        uiAnim.SetBool("showUI", true);
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        canInteract = false;
        uiAnim.SetBool("showUI", false);
    }

    void Interaction()
    {
        landedItem.SetActive(true);
        canInteract = false;
        if (willAlertHostile)
        {
            currentTargetPos = targetPos;
            movingTowards = true;
            affectedHostile.transform.localScale = new Vector2(1, 1);
        }
    }

    void MoveTowards(Vector3 target)
    {
        hostileAnim.SetBool("isRunning", true);
        if (affectedHostile.transform.position == target)
        {
            StartCoroutine(WaitAndReturn());
        } else
        {
            affectedHostile.transform.position = Vector2.MoveTowards(affectedHostile.transform.position, target, moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator WaitAndReturn()
    {
        movingTowards = false;
        hostileAnim.SetBool("isRunning", false);

        yield return new WaitForSeconds(delay);
        affectedHostile.transform.localScale = new Vector2(-1, 1);
        currentTargetPos = startPos;
        movingTowards = true;
        hostileAnim.SetBool("isRunning", true);
    }
    public void FallOver()
    {
        movingTowards = false;
        stopInteractions = true;
        affectedHostile.GetComponent<Sleep>().isAsleep = true;
        hostileAnim.SetTrigger("Sleep");
    }
}
