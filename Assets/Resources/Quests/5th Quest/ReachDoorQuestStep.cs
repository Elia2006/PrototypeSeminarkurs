using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[RequireComponent(typeof(SphereCollider))]
public class ReachDoorQuestStep : QuestStep
{
    public LocalizedString trueStatus;
    [SerializeField] DialogueTrigger trigger;

    public GameObject Map;
    public Transform Canvas;
    public Transform highlighter;


    GameObject SteamManager;
    SteamIntegration si;



    private void Start()
    {
        SteamManager = GameObject.Find("SteamManager");
        si = SteamManager.GetComponent<SteamIntegration>();

        Map = GameObject.Find("Map");
        Canvas = Map.transform.Find("Canvas");
        highlighter = Canvas.transform.Find("DoorHighlighter");
        highlighter.gameObject.SetActive(true);
        
        trigger.TriggerDialogue();
        UpdateState();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventsManager.instance.miscEvents.DoorOpened();
            highlighter.gameObject.SetActive(false);
            if (!si.IsThisAchievementUnlocked("ACH_FACILITY"))
            {
                si.UnlockAchievements("ACH_FACILITY");
            }
            FinishQuestStep();
        }
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
