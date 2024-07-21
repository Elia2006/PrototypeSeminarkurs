using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UIElements;

public class CrowbarScript : MonoBehaviour
{
    private GameObject HitTexture;
    private Animator anim;
    float hitTextureCooldown;
    bool isAttacking = false;
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
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
            isAttacking = true;
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

    }
}
