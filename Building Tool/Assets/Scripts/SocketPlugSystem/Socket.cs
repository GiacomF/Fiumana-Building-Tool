using UnityEngine;

public class Socket : Type
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected");

        //if(other.GetComponent<Plug>() != null)
        //{
            other.transform.position = transform.position;        
        //}
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