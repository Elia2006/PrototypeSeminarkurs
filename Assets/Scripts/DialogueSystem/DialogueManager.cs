using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] Animator anim;

    private Queue<string> sentences;

    private bool isTyping = false;
    private bool skip;

    //Audio
    [SerializeField] AudioSource click;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            DisplayNextSentence();
        }else if(Input.GetKeyDown(KeyCode.E))
        {
            skip = true;
        }
        if(!isTyping)
        {
            skip = false;
        }
    }

    // Update is called once per frame
    public void StartDialogue(Dialogue dialogue)
    {
        

        anim.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialouge();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;

        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            click.Play();

            yield return new WaitForSeconds(0.02f);

            if(skip)
            {
                dialogueText.text = sentence;
                break;
            }
        }

        isTyping = false;
    }

    void EndDialouge()
    {
        anim.SetBool("IsOpen", false);
    }
}
