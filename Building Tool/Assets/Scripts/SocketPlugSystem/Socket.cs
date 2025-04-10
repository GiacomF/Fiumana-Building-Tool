using UnityEngine;

public class Socket : Type
{
    private void OnTriggerStay(Collider other)
    {

        if(other.GetComponent<Plug>() != null)
        {
            if(CompareType(other.GetComponent<Plug>().type))
            {
                Debug.Log("Socket and Plug match!");
                other.transform.position = transform.position;        
            }
        }
    }

    float radius;
    private void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
}