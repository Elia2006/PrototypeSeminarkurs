using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEnergyCoreScript : QuestStep
{
    public GameObject BossSpawner;
    public Transform Boss;


    [SerializeField] DialogueTrigger trigger;
    private void Start()
    {
        BossSpawner = GameObject.Find("BossSpawner");
        Boss = BossSpawner.transform.Find("boss als 1 objekt");
        transform.position = Boss.position + Vector3.up;


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
        string status = "Sammle den Energiekern ein.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
