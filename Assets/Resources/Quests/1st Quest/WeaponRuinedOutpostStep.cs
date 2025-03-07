using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WeaponRuinedOutpostStep : QuestStep
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
            //Debug.Log("RuinedOutpost2");
            FinishQuestStep();
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
