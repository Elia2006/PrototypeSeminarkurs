using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ReachDoorQuestStep : QuestStep
{
    [SerializeField] DialogueTrigger trigger;
    private void Start()
    {
        trigger.TriggerDialogue();
        UpdateState();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventsManager.instance.miscEvents.DoorOpened();
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Gehe zur großen Tür";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
