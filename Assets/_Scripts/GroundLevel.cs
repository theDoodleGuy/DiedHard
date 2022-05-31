using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLevel : MonoBehaviour
{
    [SerializeField] GameObject hostileTrigger;
    [SerializeField] GameObject door;

    Proximity proximity;
    Bump bump;

    bool levelComplete;

    private void Start()
    {
        proximity = hostileTrigger.GetComponent<Proximity>();
        bump = door.GetComponent<Bump>();
    }

    private void Update()
    {
        if (levelComplete) return;
        if (!proximity.seenByPlayer) return;
        if (!bump.bumped) return;

        hostileTrigger.GetComponentInParent<MoveToTarget>().shouldMove = true;
        levelComplete = true;
    }
}
