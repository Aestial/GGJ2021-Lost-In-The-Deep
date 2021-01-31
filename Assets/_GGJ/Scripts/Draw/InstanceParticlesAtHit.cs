using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceParticlesAtHit : MonoBehaviour
{
    [SerializeField]
    GameObject particles = default;

    public void InstantiateAtHit(RaycastHit hit)
    {
        Quaternion rotation = Quaternion.LookRotation(hit.normal);
        Instantiate(particles, hit.point, rotation);
    }

    public void InstantiateAtCollision(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.LookRotation(contact.normal);
        Instantiate(particles, contact.point, rotation);
    }

    void Start()
    {
        
    }
}
