using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[RequireComponent(typeof(BoxCollider))]
public class OpenDoorQuestStep : QuestStep
{
    public LocalizedString trueStatus;
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
