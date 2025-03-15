using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsQuestStep : QuestStep
{
    [SerializeField] DialogueTrigger trigger;

    private int plantscollected = 0;
    private int plantstocollect = 3;

    public GameObject Map;
    public Transform Canvas;
    public Transform highlighter;
    
    
        



    private void Start()
    {
        Map = GameObject.Find("Map");
        Canvas = Map.transform.Find("Canvas");
        highlighter = Canvas.transform.Find("PlantHighlighter");
        trigger.TriggerDialogue();
        UpdateState();
        highlighter.gameObject.SetActive(true);
    }

    private void Update()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected += HerbCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected -= HerbCollected;
    }
    //hier weitermachen noch

    private void HerbCollected()
    {
        if (plantscollected < plantstocollect)
        {
            plantscollected++;
            UpdateState();
        }

        if (plantscollected >= plantstocollect)
        {
            highlighter.gameObject.SetActive(false);
            FinishQuestStep();
        }
    }
    private void UpdateState()
    {
        string state = "";
        string status = plantscollected + " von " + plantstocollect + " benötigten Pflanzen eingesammelt.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.plantscollected = System.Int32.Parse(state);
        UpdateState();
    }
}
