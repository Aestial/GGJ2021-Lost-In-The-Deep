using UnityEngine;

public class RigidbodyAddForce : MonoBehaviour 
{
    [SerializeField]
    new private Rigidbody rigidbody = default;
    [SerializeField]
    private float thrust = 50.0f;
    
    public void AddForceTowardsPosition(Vector3 position) 
    {
        Vector3 targetDirection = position - transform.position;
        targetDirection.Normalize();
        rigidbody.AddForceAtPosition(targetDirection * thrust, transform.position);        
    }    
}