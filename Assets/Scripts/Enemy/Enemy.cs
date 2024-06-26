using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    protected GameObject Player;
    protected NavMeshAgent agent;
    public LayerMask groundLayer;
    protected int speed;
    protected float speedMultiplier;
    
    protected int patrollingRange = 20;
    protected float waitTime = 0;
    protected float sightDistance = 30;
    protected float allertDistance = 40;
    protected float attackCooldown = 0;




    protected int health = 50;
    protected Vector3 newPos;
    protected int prevState = 0;

    protected Vector3 knockback;
    protected Vector3 knockbackOldPos;
    protected Vector3 knockbackNewPos;
    protected float knockbackLerp = 1;

    [SerializeField] ParticleSystem AllertEffect;
    [SerializeField] Transform PatrollPoint;

    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected Vector3 FindPosOnNavMesh(int distance, Vector3 direction, NavMeshAgent agent)
    {
        NavMeshHit hit;
        Vector3 randomDirection;

        randomDirection = direction * distance;

        NavMesh.SamplePosition(randomDirection + transform.position, out hit, distance, NavMesh.AllAreas);

        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(hit.position, path);

        if(path.status == NavMeshPathStatus.PathComplete)
        {
            return hit.position;
        }
        else{
            return new Vector3(0, 0, 0);
        }

        
    }

    protected bool IsPlayerInRange(Transform PlayerTrans, LayerMask groundLayer, float sightDistance, float allertRadius)
    {
        RaycastHit hit;
        
        Physics.Linecast(transform.position, PlayerTrans.position, out hit, groundLayer);
        if(hit.transform == null && Vector3.Distance(transform.position, PlayerTrans.position) < sightDistance)
        {
            //Allert(allertRadius, PlayerTrans);
            return true;
        }else
        {
            return false;
        }
    }

    public void Knockback()
    {
        //knockback = Quaternion.LookRotation(transform.position - Player.transform.position, Vector3.up).eulerAngles.normalized * 0.2f;
        knockback = Player.transform.forward * 3;
        knockbackOldPos = transform.position;
        knockbackLerp = 0;
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + knockback, out hit, 10, NavMesh.AllAreas);
        knockbackNewPos = hit.position;
    }

    protected void Patroll()
    {
        
    }

    private void Allert(float allertRadius, Transform PlayerTrans)
    {
        RaycastHit[] hits;
        if(prevState == 0)
        {
            //AllertEffect.Play();
            hits = Physics.SphereCastAll(transform.position, allertRadius, Vector3.up, 0);
            foreach(RaycastHit i in hits)
            {
                if(i.transform.GetComponent<Enemy>() != null){
                    i.transform.GetComponent<Enemy>().newPos = PlayerTrans.position;
                }
            }
        }
    }
}
