using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private LayerMask validLayer;

    public void AdjustPosition(Ray ray)
    {
        Debug.Log(ray.origin);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.position = hit.point;
        }   
    }
}