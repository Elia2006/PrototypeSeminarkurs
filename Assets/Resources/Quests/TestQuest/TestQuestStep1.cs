using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestStep1 : QuestStep
{
    private int metalCollected = 0;
    private int metalToComplete = 3;

    private void OnEnable()
    {
        //Hier bei dem Event subscriben
        //GameEventsManager.instance.miscEvents.onMetalCollected += metalCollected;
    }

    private void OnDisable()
    {
        //Hier bei dem Event unsubscriben
        //GameEventsManager.instance.miscEvents.onMetalCollected -= metalCollected;
    }

    private void MetalCollected()
    {
        if (metalCollected < metalToComplete) 
        {
            metalCollected++;
            UpdateState();
        }

        if (metalCollected >= metalToComplete)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = metalCollected.ToString();
        string status = "Collected " + metalCollected + " / " + metalToComplete + " coins.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.metalCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
