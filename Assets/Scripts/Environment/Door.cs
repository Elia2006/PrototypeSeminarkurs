using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    [SerializeField] AudioSource sound;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlaySound()
    {
        sound.Play();
    }


    public void OnTriggerEnter(Collider other)
    {
        if(!isOpen && other.transform.CompareTag("Player"))
        {
            anim.SetTrigger("isOpen");
            isOpen = true;
        }
    }
}
