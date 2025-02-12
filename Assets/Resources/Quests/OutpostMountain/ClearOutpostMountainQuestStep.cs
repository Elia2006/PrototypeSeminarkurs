using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ClearOutpostMountainQuestStep : QuestStep
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}
