using UnityEngine;
using System.Collections;

public class NoFog : MonoBehaviour
{
    void OnEnable()
    {
        RenderSettings.fog = false;
    }

    void OnDisable()
    {
        RenderSettings.fog = true;
    }
}
