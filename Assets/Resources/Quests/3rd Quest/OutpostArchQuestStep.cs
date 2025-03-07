using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class OutpostArchQuestStep : QuestStep
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
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Reach the outpost on the arch";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
