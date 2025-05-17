using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[RequireComponent(typeof(BoxCollider))]
public class ClearOutpostMountainQuestStep : QuestStep
{
    public LocalizedString trueStatus;
    public GameObject Map;
    public Transform Canvas;
    public Transform Disabler;
    [SerializeField] DialogueTrigger trigger;
    [SerializeField] DialogueTrigger trigger2;
    private void Start()
    {
        Map = GameObject.Find("Map");
        Canvas = Map.transform.Find("Canvas");
        Disabler = Canvas.transform.Find("Disabler2");
        trigger.TriggerDialogue();
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

    private void Update()
    {
        UpdateState();
    }

    private void ItemCollected()
    {
        Disabler.gameObject.SetActive(true);
        trigger2.TriggerDialogue();
        FinishQuestStep();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Beseitige die Gegner und sammle das Hitzeschild ein.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
