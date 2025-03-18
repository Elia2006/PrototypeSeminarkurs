using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RepairShipQuestStep : QuestStep
{
    public GameObject Map;
    public GameObject Canvas;
    public Transform PressEStart;

    [SerializeField] DialogueTrigger trigger;
    private void Start()
    {
        
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
