using UnityEngine;

public class Door2 : MonoBehaviour
{
    private Animator anim;
    [SerializeField] AudioSource sound;
    [SerializeField] SphereCollider sphereCollider;

    [SerializeField] Boss_new Boss;

    // Start is called before the first frame update
    //
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlaySound()
    {
        sound.Play();
    }

    void Update()
    {
        //Debug.Log(Boss.isActivated);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (Boss.isActivated == false)
            {
                anim.SetBool("IsOpen", true);
            }
            else
            {
                anim.SetBool("IsOpen", false);
            }
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
