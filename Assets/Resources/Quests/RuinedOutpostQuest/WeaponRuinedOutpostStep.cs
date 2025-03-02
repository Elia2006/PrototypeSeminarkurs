using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WeaponRuinedOutpostStep : QuestStep
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("RuinedOutpost2");
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
       
    }
}
