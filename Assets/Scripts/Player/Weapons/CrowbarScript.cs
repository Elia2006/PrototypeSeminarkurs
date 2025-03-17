using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class CrowbarScript : MonoBehaviour
{
    public bool unEquip = false;
    [SerializeField] PlayerMovement playerMovement;
    private HitTextureS hitTexture;
    private Animator anim;
    private float attackCooldown;
    public GameObject Player;
    public HUD hud;
    public Map map;

    private bool hasDamaged;
    private Collider coll;
    // Start is called before the first frame update
    void Awake()
    {
        hud = Player.GetComponent<HUD>();
        hitTexture = GameObject.Find("HitTexture").GetComponent<HitTextureS>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();

        coll.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused && !map.mapOpen && hud.playerHealth > 0)
        {
            if (Input.GetButtonDown("Fire1") && attackCooldown < Time.time)
            {
                hasDamaged = false;
                anim.SetTrigger("Attack");
                attackCooldown = Time.time + 1;
                if (playerMovement != null) // playerMovement ist jeden 2. Frame null, keine Ahnung wieso aber das geht so erstmal
                {
                    playerMovement.ReduceSpeed(2, 0.5f);
                }

            }
        }
        


        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(animStateInfo.normalizedTime >= 1 && animStateInfo.IsName("CrowbarHit"))
        {
            
        }
    }

    public void EnableCollider()
    {
        coll.enabled = true;
    }
    public void DisableCollider()
    {
        coll.enabled = false;
    }

    public void Unequip()
    {
        anim.SetTrigger("Equip");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && other.gameObject.TryGetComponent<CollisionScript>(out CollisionScript collisionScript) 
            && !hasDamaged)
        {
            collisionScript.TakeDamage(4, true);
            collisionScript.Knockback(0.1f);
            hitTexture.Hit();
            hasDamaged = true;
        }
    }
}
