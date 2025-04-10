using UnityEngine;

public class Socket : Type
{
    private Vector3 CalculateOffset(GameObject obj)
    {
        Vector3 objScale = obj.transform.localScale;

        float yOffset = transform.position.y + objScale.x/2;

        return new Vector3(transform.position.x, yOffset, transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Plug>() != null)
        {
            if(CompareType(other.GetComponent<Plug>().PlugType))
            {
                //Debug.Log("Socket and Plug match!");
                other.transform.position = CalculateOffset(other.gameObject);
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