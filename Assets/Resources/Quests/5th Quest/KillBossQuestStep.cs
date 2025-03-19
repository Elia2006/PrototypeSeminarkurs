using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBossQuestStep : QuestStep
{
    [SerializeField] DialogueTrigger trigger;
    private void Start()
    {
        
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onBossDeath += BossKilled;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onBossDeath -= BossKilled;
    }


    void BossKilled()
    {
        trigger.TriggerDialogue();
        FinishQuestStep();
    }

    private void UpdateState()
    {
        string state = "";
        string status = "Erledige den 5AND-1N3L";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}