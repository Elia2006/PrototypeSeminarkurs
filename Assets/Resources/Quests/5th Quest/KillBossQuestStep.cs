using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class KillBossQuestStep : QuestStep
{
    public LocalizedString trueStatus;
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
        string status = trueStatus.GetLocalizedString();
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}