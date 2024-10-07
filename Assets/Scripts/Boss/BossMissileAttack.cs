using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BossMissileAttack : MonoBehaviour
{
    public Transform player;
    Rigidbody rb;

    public Transform initiator;
    public Transform attackPoint;
    public GameObject Grenade;

    public GameObject Projectile;

    public Animator animator;

    public float throwForce = 30f;
    public float throwUpwardForce = 5f;
    public int bullets;
    public float attackRange = 25;
    public float BossSpeed = 10;

    Vector3 playerdir;
    // Start is called before the first frame update
    void Start()
    {
        //funktioniert schon wieder nix leck mich
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Attack")) {
            StartCoroutine(MinigunFire());
        }

        if (animator.GetBool("Move"))
        {
            StartCoroutine(walkTowardsPlayer());
        }
    }

    IEnumerator MinigunFire()
    {
        playerdir = transform.position - player.position;

        bullets = 6;

        //feuert 6 Schüsse auf eine Stelle
        yield return new WaitForSeconds(0.5f);

        RaycastHit hit;

        Physics.Raycast(initiator.position, playerdir, out hit);

        if (hit.transform != null)
        {
            attackPoint.LookAt(hit.point);
        }
        else
        {
            attackPoint.LookAt(initiator.position + initiator.forward * 50);
        }
        while (bullets != 0)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(Projectile, attackPoint.position, attackPoint.rotation);
            bullets--;
        }
        


    }

    IEnumerator walkTowardsPlayer()
    {
        Vector3 target = new Vector3(player.position.x, initiator.position.y, player.position.z);
        //Vector3 subtract = target.normalized * 30;
        Vector3 newPos = Vector3.MoveTowards(initiator.position, target, BossSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        yield return new WaitForSeconds(0.5f);

        if (Vector3.Distance(player.position, rb.position) <= attackRange) 
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Move", false);
            //attackieren, auch so mit verschiedenen Attack-Möglichkeiten
        }
    }

    
}
