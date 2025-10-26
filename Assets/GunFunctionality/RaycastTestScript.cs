using UnityEngine;

public class RaycastTestScript : MonoBehaviour
{
    public Transform FirePoint;

    void Update()
    {
        Shooting();


    }

    public void Shooting()
    {
        RaycastHit hit;


        if (Physics.Raycast(FirePoint.position, transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
    }
}
