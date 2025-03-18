using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OpenDoorQuestStep : QuestStep
{
    [SerializeField] DialogueTrigger trigger;
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
            trigger.TriggerDialogue();
            //Debug.Log("RuinedOutpost2");
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Erkunde die Fabrik";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
