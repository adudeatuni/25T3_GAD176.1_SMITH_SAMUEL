using UnityEngine;


public class TargetDummyScript : MonoBehaviour
{
    [SerializeField] public float objectHealth = 100;


    public void BeenHit(float incomingDamage)
    {
        objectHealth = objectHealth - incomingDamage;
        if (objectHealth <= 0)
        {
            Debug.Log("Ow I died!");
            GameObject.Destroy(gameObject);
        }
    }

}



