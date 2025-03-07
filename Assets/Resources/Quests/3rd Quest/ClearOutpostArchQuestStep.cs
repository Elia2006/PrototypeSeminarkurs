using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ClearOutpostArchQuestStep : QuestStep
{
    private void Start()
    {
        UpdateState();
    }

    private void Update()
    {
        UpdateState();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Clear the enemies and collect the Toolbox";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
