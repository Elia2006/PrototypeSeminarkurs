using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
public class RepairShipQuestStep : QuestStep
{
    public LocalizedString trueStatus;
    GameObject SteamManager;
    SteamIntegration si;

    public GameObject Map;
    public GameObject Canvas;
    public Transform PressEStart;

    [SerializeField] DialogueTrigger trigger;
    private void Start()
    {
        SteamManager = GameObject.Find("SteamManager");
        si = SteamManager.GetComponent<SteamIntegration>();

        Canvas = GameObject.Find("Canvas");
        PressEStart = Canvas.transform.Find("PressEStart");

        trigger.TriggerDialogue();
        UpdateState();
    }

    

    private void Update()
    {
        UpdateState();
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressEStart.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (!si.IsThisAchievementUnlocked("ACH_GAME_BEATEN"))
                {
                    si.UnlockAchievements("ACH_GAME_BEATEN");
                }

                SceneManager.LoadScene("Outro");
                //FinishQuestStep();
                
            }
        }
        else
        {
            PressEStart.gameObject.SetActive(false);
        }
    }

    


    

    private void UpdateState()
    {
        string state = "";
        string status = "Starte das Raumschiff";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {

    }

}
