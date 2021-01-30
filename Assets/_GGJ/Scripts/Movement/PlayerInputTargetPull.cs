using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PositionEvent : UnityEvent<Vector3> {}
[Serializable]
public class StringEvent : UnityEvent<string> {}
[Serializable]
public class FloatEvent : UnityEvent<float> {}
[Serializable]
public class BoolEvent : UnityEvent<bool> {}

public class PlayerInputTargetPull: MonoBehaviour
{   
    [SerializeField]
    float rayReach = 15.0f;
    [SerializeField]
    float shootDelay = 0.75f;
    [SerializeField]
    float reloadDelay = 1.0f;

    [SerializeField]
    private PositionEvent OnTargetShot = default;
    [SerializeField]
    private PositionEvent OnTargetAttached = default;
    [SerializeField]
    private PositionEvent OnTargetDeattached = default;
    [SerializeField]
    private FloatEvent OnShootingProgress = default;
    [SerializeField]
    private FloatEvent OnReloadingProgress = default;
    [SerializeField]
    private BoolEvent OnReachChanged = default;
    [SerializeField]
    private StringEvent OnStateChanged = default;

    [SerializeField]
    private Rigidbody rigidbody = default;
    [SerializeField]
    private float thrust = 50.0f;
    
    // [Header("Debug")]
    // [SerializeField]
    // float failRayLength = 10.0f;

    public string state;

    public bool canReach = false;
    public float shootTimer = 0.0f;
    public bool isAnchored = false;
    public Vector3 anchor;

    RaycastHit hit;

    public bool isLoaded = true;
    float loadingTimer = 0.0f;

    public void OnFireInputStarted()
    {
        Debug.Log($"Ray Shoot: fire started!");
        if (!isAnchored)
        {
            ShootAtTarget(hit);
        }            
    }
    public void OnFireInputPerformed()
    {
        // Debug.Log($"Ray Shoot: fire performed!");
    }
    public void OnFireInputCanceled()
    {
        Debug.Log($"Ray Shoot: fire canceled!");
        StopAllCoroutines();
        DeattachTarget(transform.position);
    }
     
    private void ShootAtTarget(RaycastHit hit)
    {        
        if (canReach && isLoaded)
        {
            anchor = hit.point;
            OnTargetShot.Invoke(anchor); 
            shootTimer = shootDelay;
            StartCoroutine(AttachTarget(anchor));
        }                
    }

    IEnumerator AttachTarget(Vector3 target) 
    {        
        yield return new WaitForSeconds(shootDelay);
        isAnchored = true;
        OnTargetAttached.Invoke(target);
    }

    public void DeattachTarget(Vector3 target)
    {
        if (isAnchored)
            loadingTimer = reloadDelay;
        isAnchored = false;
        
        OnTargetDeattached.Invoke(target);
    }

    private void Update() 
    {
        if (loadingTimer > 0.0f)
        {
            loadingTimer -= Time.deltaTime;
            float progress = 1f - loadingTimer / reloadDelay;
            // Debug.Log($"Ray Loading progress: {progress}");
            OnReloadingProgress.Invoke(progress);
            isLoaded = false;
        } else {
            isLoaded = true;
        }

        if (shootTimer > 0.0f)
        {
            shootTimer -= Time.deltaTime;
            float progress = 1f - shootTimer / shootDelay;
            // Debug.Log($"Ray Shoot progress: {progress}");
            OnShootingProgress.Invoke(progress);
        }
    }

    private void FixedUpdate() 
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        // layerMask = ~layerMask;

        // Does the ray intersect any objects excluding the player layer
        canReach = Physics.Raycast(transform.position, transform.right, out hit, rayReach, layerMask);
        // React with event
        OnReachChanged.Invoke(canReach);
        // React with debug Rays
        // if (canReach) {
        //     Debug.Log("Did Hit");
        //     Color rayColor = isLoaded ? Color.green : Color.yellow;
        //     Debug.DrawRay(transform.position, transform.right * hit.distance, rayColor);
        // } else {
        //     Debug.Log("Did Not Hit");
        //     Color rayColor = isLoaded ? Color.magenta : Color.red;
        //     Debug.DrawRay(transform.position, transform.right * failRayLength, rayColor);
        // }
        if (canReach && isLoaded)
            state = "ready";
        else if (canReach)
            state = "loading";
        else if (isLoaded)
            state = "unreach";
        // else 
        //     state = "";

        OnStateChanged.Invoke(state);
        // React with force
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
