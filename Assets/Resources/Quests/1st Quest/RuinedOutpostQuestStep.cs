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

    private void Update()
    {
        UpdateState();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("RuinedOutpost1");
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Begebe dich zum zerstörten Aussenposten.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}
