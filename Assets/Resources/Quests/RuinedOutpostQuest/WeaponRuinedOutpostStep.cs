using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WeaponRuinedOutpostStep : QuestStep
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponPickupQuest1"))
        {
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
       
    }
}
