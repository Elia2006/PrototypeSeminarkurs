using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OpenDoorQuestStep : QuestStep
{
    [SerializeField] DialogueTrigger trigger;
    private void Start()
    {
        trigger.TriggerDialogue();
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
            //Debug.Log("RuinedOutpost2");
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Öffne die Tür";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
