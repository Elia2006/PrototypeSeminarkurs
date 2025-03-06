using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsQuestStep : QuestStep
{

    private int plantscollected = 0;
    private int plantstocollect = 3;

    

    private void Start()
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
            FinishQuestStep();
        }
    }
    private void UpdateState()
    {
        string state = "";
        string status = "Collected " + plantscollected + " / " + plantstocollect + "herbs";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.plantscollected = System.Int32.Parse(state);
        UpdateState();
    }
}
