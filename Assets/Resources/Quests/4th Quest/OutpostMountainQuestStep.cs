using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class OutpostMountainQuestStep : QuestStep
{
    private void Start()
    {
        UpdateState();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Begebe dich zu einem der Aussenposten";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}
