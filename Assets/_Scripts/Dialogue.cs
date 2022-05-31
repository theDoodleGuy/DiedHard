using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public List<string> textList;
    [SerializeField] TMP_Text text;

    Animator anim;

    bool isTalking;
    int dialogueIndex =0;

    private void Start()
    {
        isTalking = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isTalking && Input.GetKeyDown(KeyCode.Return)) {
            StartDialogue();
            return;
        };
        if(isTalking && Input.GetKeyDown(KeyCode.Space)) NextDialogue();
    }

    public void StartDialogue() {
        text.text = textList[dialogueIndex];
        anim.SetBool("talking", true);
        anim.enabled = true;
        isTalking = true;
    }

    public void NextDialogue() {
        dialogueIndex++;
        if (dialogueIndex < textList.Count) {
            text.text = textList[dialogueIndex];
        } else
        {
            Reset();
        }
    }
    private void Reset()
    {
        anim.SetBool("talking", false);
        isTalking = false;
        dialogueIndex = 0;
    }
}
