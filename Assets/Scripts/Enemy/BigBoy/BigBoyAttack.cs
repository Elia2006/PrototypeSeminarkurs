using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class BigBoyAttack : MonoBehaviour
{
    [SerializeField] Transform BigBoy;
    [SerializeField] LineRenderer laser;
    [SerializeField] LayerMask groundLayer;
    
    private GameObject Player;

    private Vector3 nextPosition;
    private Vector3 lastPosition;
    private float lerp;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(lastPosition, nextPosition, lerp);
        lerp += Time.deltaTime;

        if(lerp >= 1)
        {
            lastPosition = nextPosition;
            nextPosition = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
            lerp = 0;
        }   
    

        RaycastHit hit;

        if(Physics.Raycast(transform.position + Vector3.up * 4, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            transform.position = hit.point;
        }
        
        laser.SetPosition(0, BigBoy.position + BigBoy.transform.up * 6);
        laser.SetPosition(1, transform.position);

        Collision();
    }

    private void Collision()
    {
        RaycastHit hit;
        if(Physics.Linecast(transform.position, BigBoy.position + BigBoy.transform.up * 6, out hit) && hit.transform.CompareTag("Player"))
        {
            Player.GetComponent<HUD>().TakeDamage((int)(200 * Time.deltaTime), 1, hit.point, 0.1f);
        }
    }


}
