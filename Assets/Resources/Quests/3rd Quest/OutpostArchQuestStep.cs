using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class OutpostArchQuestStep : QuestStep
{
    [SerializeField] DialogueTrigger trigger;

    public GameObject Map;
    public Transform Canvas;
    public Transform highlighter;






    private void Start()
    {
        Map = GameObject.Find("Map");
        Canvas = Map.transform.Find("Canvas");
        highlighter = Canvas.transform.Find("OutpostAHighlighter");
        highlighter.gameObject.SetActive(true);
       
        trigger.TriggerDialogue();
        UpdateState();
    }


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            highlighter.gameObject.SetActive(false);    
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Begebe dich zu einem der Aussenposten";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
