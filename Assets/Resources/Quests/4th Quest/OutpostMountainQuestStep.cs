using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[RequireComponent(typeof(SphereCollider))]
public class OutpostMountainQuestStep : QuestStep
{
    public LocalizedString trueStatus;

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
        highlighter = Canvas.transform.Find("OutpostMHighlighter");
        highlighter.gameObject.SetActive(true);    
        
        UpdateState();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            highlighter.gameObject.SetActive(false);
            if (!si.IsThisAchievementUnlocked("ACH_OUTPOSTS"))
            {
                si.UnlockAchievements("ACH_OUTPOSTS");
            }
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
