using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Filmgrain : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] Collider shield;
    private FilmGrain filmGrain;
    private float intensity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!shield.bounds.Contains(transform.position))
        {
            volume.profile.TryGet(out filmGrain);
            {
                filmGrain.intensity.value = intensity;
            }
            intensity += Time.deltaTime * 0.1f;
        }
        else if(intensity > 0)
        {
            volume.profile.TryGet(out filmGrain);
            {
                filmGrain.intensity.value = intensity;
            }
            intensity -= Time.deltaTime * 0.1f;
        }

    }
}
