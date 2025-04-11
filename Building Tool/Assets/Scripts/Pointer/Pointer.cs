using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private LayerMask validLayer;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, validLayer))
        {
            transform.position = hit.point;
        }   
    }
}