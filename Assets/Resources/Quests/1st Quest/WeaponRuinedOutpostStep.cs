using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WeaponRuinedOutpostStep : QuestStep
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

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onItemPickup += ItemCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onItemPickup -= ItemCollected;
    }

    

    private void ItemCollected()
    {
        trigger.TriggerDialogue();
        FinishQuestStep();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("RuinedOutpost2");
            //FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Sammle das Speichermodul ein.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
       
    }
}
