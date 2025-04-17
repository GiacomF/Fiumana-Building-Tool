using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class Socket : Type
{
    private Vector3 CalculateOffset(GameObject obj)
    {
        Vector3 objScale = obj.transform.localScale;

        float yOffset = transform.position.y + objScale.x/2;

        return new Vector3(transform.position.x, yOffset, transform.position.z);
    }

    private void CheckForPlugs()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider other in hits)
        {
            Plug plug = other.GetComponent<Plug>();
            if (plug != null && CompareType(plug.PlugType))
            {
                other.transform.position = CalculateOffset(other.gameObject);
            }
        }
    }

    float radius;
    private void OnEnable()
    {
        radius = GetComponent<SphereCollider>().radius;
        EditorApplication.update += EditorUpdate;
    }

    private void EditorUpdate()
    {
        CheckForPlugs();   
    }

    private void OnDisable()
    {
        EditorApplication.update -= EditorUpdate;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
}