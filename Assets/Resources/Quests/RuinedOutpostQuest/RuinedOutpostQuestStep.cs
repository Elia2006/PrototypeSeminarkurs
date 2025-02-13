using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RuinedOutpostQuestStep : QuestStep
{
    private void Start()
    {
        UpdateState();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("RuinedOUtpost1");
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Move your tin ass over here and hurry please";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}
