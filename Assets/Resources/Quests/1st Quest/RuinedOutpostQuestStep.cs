using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[RequireComponent(typeof(SphereCollider))]
public class RuinedOutpostQuestStep : QuestStep
{
    public LocalizedString trueStatus;
    
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
        string status = trueStatus.GetLocalizedString();
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}
