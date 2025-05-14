using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBossQuestStep : QuestStep
{
    [SerializeField] DialogueTrigger trigger;

    GameObject SteamManager;
    SteamIntegration si;

    private void Start()
    {
        SteamManager = GameObject.Find("SteamManager");
        si = SteamManager.GetComponent<SteamIntegration>();

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
        if (!si.IsThisAchievementUnlocked("ACH_BOSS"))
        {
            si.UnlockAchievements("ACH_BOSS");
        }
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