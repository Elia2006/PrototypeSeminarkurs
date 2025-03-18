using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    [SerializeField] AudioSource sound;
    [SerializeField] SphereCollider sphereCollider;

    // Start is called before the first frame update
    //
    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onDoorOpen += ActivateCollider;
    }

    void ActivateCollider()
    {
        sphereCollider.enabled = true;
    }
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        anim = GetComponent<Animator>();
    }

    public void PlaySound()
    {
        sound.Play();
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            anim.SetBool("IsOpen", true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            anim.SetBool("IsOpen", false);
        }
    }
}
