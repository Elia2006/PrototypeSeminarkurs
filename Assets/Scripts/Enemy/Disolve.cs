using System.Collections;
using UnityEngine;

public class Disolve : MonoBehaviour
{
    [SerializeField] Renderer[] renderers;


    // Update is called once per frame
    public IEnumerator StartDisolve()
    {
        yield return new WaitForSeconds(2);

        for(float i = 0; i < 1; i += 0.01f){
            foreach (Renderer renderer in renderers)
            {
                foreach(Material material in renderer.materials)
                {
                    material.SetFloat("_disolveAmount", i);
                }
                
                yield return new WaitForSeconds(0.01f);
            }
        }
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetFloat("_disolveAmount", 1);
        }

        yield return null;
    }
    
}
