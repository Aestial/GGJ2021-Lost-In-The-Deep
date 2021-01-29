using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PositionEvent : UnityEvent<Vector3> {}

public class PlayerInputTargetPull: MonoBehaviour
{   
    [SerializeField]
    float rayReach = 15.0f;
    [SerializeField]
    float attachDelay = 0.75f;
    [SerializeField]
    float reloadDelay = 1.0f;

    [SerializeField]
    private PositionEvent OnTargetShot = default;
    [SerializeField]
    private PositionEvent OnTargetAttached = default;
    [SerializeField]
    private PositionEvent OnTargetDeattached = default;

    [SerializeField]
    private Rigidbody rigidbody = default;
    [SerializeField]
    private float thrust = 50.0f;
    
    [Header("Debug")]
    [SerializeField]
    float failRayLength = 10.0f;

    public bool hasReach = false;
    public bool isAnchored = false;
    public Vector3 anchor;

    RaycastHit hit;

    public bool isLoaded = true;
    float loadingTimer = 0.0f;

    public void OnFireInputPerformed()
    {
        Debug.Log($"Ray Shoot: fire performed!");
        if (!isAnchored)
        {
            ShootAtTarget(hit);
        }            
    }

    public void OnFireInputCanceled()
    {
        Debug.Log($"Ray Shoot: fire canceled!");
        if (isAnchored)
        {
            DeattachTarget(transform.position);
        }            
    }
     
    private void ShootAtTarget(RaycastHit hit)
    {        
        if (hasReach && isLoaded)
        {
            anchor = hit.point;
            OnTargetShot.Invoke(anchor);            
            StartCoroutine(AttachTarget(anchor));
        }                
    }

    IEnumerator AttachTarget(Vector3 target) 
    {
        yield return new WaitForSeconds(attachDelay);
        isAnchored = true;                    
        OnTargetAttached.Invoke(target);
    }

    public void DeattachTarget(Vector3 target)
    {
        isAnchored = false;
        loadingTimer = reloadDelay;
        OnTargetDeattached.Invoke(target);
    }

    private void Update() 
    {
        if (loadingTimer > 0.0f)
        {
            loadingTimer -= Time.deltaTime;
            isLoaded = false;
        } else {
            isLoaded = true;
        }
    }

    private void FixedUpdate() 
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.right, out hit, rayReach))
        {
            // Debug.Log("Did Hit");
            Color rayColor = isLoaded ? Color.green : Color.yellow;
            Debug.DrawRay(transform.position, transform.right * hit.distance, rayColor);
            hasReach = true;
        }
        else
        {
            Color rayColor = isLoaded ? Color.magenta : Color.red;
            Debug.DrawRay(transform.position, transform.right * failRayLength, rayColor);
            hasReach = false;
        }
        if (isAnchored)
        {
            MoveWithForce(anchor);
        }
    }
    public void MoveWithForce(Vector3 anchor) 
    {
        Vector3 targetDirection = anchor - transform.position;
        targetDirection.Normalize();
        rigidbody.AddForce(targetDirection * thrust);        
    }

}
