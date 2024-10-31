using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UIElements;

public class CrowbarScript : MonoBehaviour
{
    public bool unEquip = false;
    [SerializeField] PlayerMovement playerMovement;
    private GameObject HitTexture;
    private Animator anim;
    float hitTextureCooldown;
    bool isAttacking = false;
    private float attackCooldown;
    List<Collider> alreadyDamaged = new List<Collider>();
    // Start is called before the first frame update
    void Awake()
    {
        HitTexture = GameObject.Find("HitTexture");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && attackCooldown < Time.time)
        {
            anim.SetTrigger("Attack");
            isAttacking = true;
            attackCooldown = Time.time + 1;
            if(playerMovement != null) // playerMovement ist jeden 2. Frame null, keine Ahnung wieso aber das geht so erstmal
            {
                playerMovement.ReduceSpeed(1, 1);
            }
        }


        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo (0);
        if(animStateInfo.normalizedTime >= 1 && animStateInfo.IsName("CrowbarHit"))
        {
            isAttacking = false;
            alreadyDamaged.Clear();
            transform.position = new Vector3(0.3f, -0.415f, 0.6f);
            transform.rotation = Quaternion.Euler(-76.5f, 90, -90);
        }

        if(hitTextureCooldown > Time.time)
        {
            HitTexture.SetActive(true);
        }else{
            HitTexture.SetActive(false);
        }
    }

    public void Unequip()
    {
        anim.SetTrigger("Equip");
    }

    private void OnTriggerStay(Collider other)
    {
        

        if(other.transform.CompareTag("Enemy") && isAttacking && !alreadyDamaged.Contains(other))
        {
            alreadyDamaged.Add(other);
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(20);
            enemy.KnockbackStart();
            hitTextureCooldown = Time.time + 0.1f;
        }
        /*
        if (other.transform.CompareTag("Boss") && isAttacking && !alreadyDamaged.Contains(other))
        {
            alreadyDamaged.Add(other);
            Boss boss = other.GetComponent<Boss>();
            boss.BossTakeDamage(20);
            
            hitTextureCooldown = Time.time + 0.1f;
        }*/

    }
}
