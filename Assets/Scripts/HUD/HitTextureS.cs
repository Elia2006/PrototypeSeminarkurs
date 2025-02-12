using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitTextureS : MonoBehaviour
{
    private float hitTextureCooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitTextureCooldown > Time.time)
        {
            gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
        }
        else
        {
            gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
        }
    }

    public void Hit()
    {
        hitTextureCooldown = Time.time + 0.1f;
    }

    public void SetColor(Color color)
    {
        gameObject.GetComponent<Image>().color = color;
    }
}
