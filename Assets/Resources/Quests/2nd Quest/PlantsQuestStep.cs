using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class PlantsQuestStep : QuestStep
{
    public LocalizedString trueStatus;
    [SerializeField] DialogueTrigger trigger;

    private int plantscollected = 0;
    private int plantstocollect = 3;

    public GameObject Map;
    public Transform Canvas;
    public Transform highlighter;

    public GameObject Plants;
    public Transform PlansEnabler;

    GameObject SteamManager;
    SteamIntegration si;
    
        



    private void Start()
    {
        
        SteamManager = GameObject.Find("SteamManager");
        si = SteamManager.GetComponent<SteamIntegration>();
        if (!si.IsThisAchievementUnlocked("ACH_DATASHARD"))
        {
            si.UnlockAchievements("ACH_DATASHARD");
        }
        

        Map = GameObject.Find("Map");
        Canvas = Map.transform.Find("Canvas");
        highlighter = Canvas.transform.Find("PlantHighlighter");

        Plants = GameObject.Find("PflanzenHead");
        PlansEnabler = Plants.transform.Find("Pflanzen");
        trigger.TriggerDialogue();
        UpdateState();
        highlighter.gameObject.SetActive(true);
        PlansEnabler.gameObject.SetActive(true);
    }

    private void Update()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected += HerbCollected;
        trueStatus. Arguments = new object[] {plantscollected, plantstocollect};
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
        trueStatus.Arguments[0] = plantscollected;
        trueStatus.Arguments[1] = plantstocollect;
        string state = "";
        string status = trueStatus.GetLocalizedString();
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.plantscollected = System.Int32.Parse(state);
        UpdateState();
    }
}
