using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaschineGunTarget : MonoBehaviour
{
    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position, 0.05f);
    }
}
