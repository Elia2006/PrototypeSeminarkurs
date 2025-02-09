using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    void Start (){

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            TriggerDialogue();
            Debug.Log("hello");
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
