using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ClearOutpostArchQuestStep : QuestStep
{
    public GameObject Map;
    public Transform Canvas;
    public Transform Disabler;
    [SerializeField] DialogueTrigger trigger;
    private void Start()
    {
        Map = GameObject.Find("Map");
        Canvas = Map.transform.Find("Canvas");
        Disabler = Canvas.transform.Find("Disabler1");
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
            Disabler.gameObject.SetActive(true);
            
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Beseitige die Gegner und sammle den Werkzeugkoffer ein.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
