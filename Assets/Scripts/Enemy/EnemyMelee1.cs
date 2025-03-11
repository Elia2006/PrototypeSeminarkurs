using UnityEngine;
using UnityEngine.AI;
public class EnemyMelee1 : Enemy
{
    //Attack
    private float lerp;

    //Attack
    [SerializeField] GameObject Hit;


    //ANIMATION

    //get direction
    private Vector3 lastPos;
    private Vector3 currentDirection;
    [SerializeField] Transform RotationFix;

    private float attackChargeTimer;
    private bool attack = false;

    [SerializeField] bool isStationary;

    //Audio
    [SerializeField] AudioSource attackSound;



    void Start()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        speed = 2;
        patrollingRange = 20;
        health = 70;

        sightDistance = 30;
        allertDistance = 60;

        agent.updateUpAxis = false;
    }
    private void Awake()
    {
        newPos = transform.position;
    }

    void Update()
    {
        if(isActivated){
            if(!KnockbackUpdate())
            {

                agent.isStopped = false;
                if(IsPlayerInRange(Player.transform.position, sightDistance) || lerp > 0)
                {
                    Attack();
                }else
                {     
                    Patroll();
                }
                
            }

            //Rotation();

            if(continueCharge)
            {
                Debug.Log("hello");
                newPos = Player.transform.position;
                agent.destination = newPos;
                continueCharge = false;
            }


            agent.speed = speed * speedMultiplier;
        }
    }

    public void Attack()
    {
        speedMultiplier = 2;

        float distance = Vector3.Distance(transform.position, Player.transform.position);

        //Rotate towards Player
        agent.updateRotation = false;
        var lookRotation = Quaternion.LookRotation(Player.transform.position + Player.GetComponent<PlayerMovement>().direction * Vector3.Distance(transform.position, Player.transform.position) * 3 - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.05f);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));

        


        if(distance < 2 || attack)
        {
            agent.isStopped = true;
        }else
        {
            agent.isStopped = false;
            newPos = Player.transform.position;
            agent.destination = newPos;
        }

        RaycastHit hit;

        Physics.Raycast(transform.position, transform.forward, out hit, 4);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 4);



        if(attackCooldown < Time.time && !attack && hit.transform != null && hit.transform.gameObject.tag == "Player")
        {
            attack = true;
            attackChargeTimer = Time.time + 0.4f;
        }

        if(attackChargeTimer < Time.time && attack)
        {
            Physics.Raycast(transform.position + Vector3.up * 2 + transform.forward * 2, Vector3.down, out hit, Mathf.Infinity, groundLayer);

            GameObject hitAttack = Instantiate(Hit, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            hitAttack.GetComponent<ImpactScript>().SetEnemy(transform);

            attackCooldown = Time.time + 2;
            attack = false;

            attackSound.Play();
        }
    }
    private void Patroll()
    {
        agent.updateRotation = true;

        speedMultiplier = 1;
        if(isStationary && Vector3.Distance(transform.position, newPos) < 2)
        {
            newPos = patrollPoint.position;
            agent.destination = newPos;
        }else
        {
            if(Vector3.Distance(transform.position, newPos) < 2 && waitTime < Time.time)
            {
                waitTime = Time.time + 3;

                var checkNewPos = FindPosOnNavMesh(patrollingRange, Random.insideUnitSphere, agent, patrollPoint.position);
                if(checkNewPos != new Vector3(0, 0, 0))
                {
                    newPos = checkNewPos;
                }
            }
            else if(waitTime < Time.time)
            {         
                agent.destination = newPos;
            }
        }
    }

    private void Rotation()
    {
        currentDirection = transform.position - lastPos;

        lastPos = transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(currentDirection, Vector3.up);

        targetRotation = Quaternion.Lerp(targetRotation, Quaternion.Euler(Vector3.up), 0.7f);

        RotationFix.rotation = targetRotation;

        Debug.DrawLine(transform.position, transform.position + currentDirection * 1000);

        //RotationFix.rotation = Quaternion.Euler(currentDirection.z * 1000, 90, -currentDirection.x * 1000);

    }

}
