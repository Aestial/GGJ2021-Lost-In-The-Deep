using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTargetPull: MonoBehaviour
{   
    [SerializeField]
    float rayReach = 15.0f;
    [SerializeField]
    float shootDelay = 0.75f;
    [SerializeField]
    float reloadDelay = 1.0f;

    [Header("Events")]
    [SerializeField]
    private Vector3Event OnTargetShot = default;
    [SerializeField]
    private Vector3Event OnTargetAttached = default;
    [SerializeField]
    private Vector3Event OnTargetDeattached = default;
    [SerializeField]
    private Vector3Event OnKeepAttached = default;
    [SerializeField]
    private FloatEvent OnShootingProgress = default;
    [SerializeField]
    private FloatEvent OnReloadingProgress = default;
    [SerializeField]
    private StringEvent OnStateChanged = default;
    // [SerializeField]
    // private BoolEvent OnReachChanged = default;
    
    [Header("Debug")]
    [SerializeField]
    bool showDebug = false;
    [SerializeField]
    float rayLength = 10.0f;

    [Header("Debug Inspector")]
    public string state;
    public bool canReach = false;
    public bool isLoaded = true;
    public bool isAttached = false;
    public Vector3 anchor;

    RaycastHit hit;
    float shootTimer = 0.0f;
    float loadingTimer = 0.0f;
    
    public void OnFireInputStarted()
    {
        // Debug.Log($"Ray Shoot: fire started!");
        ShootAtTarget(hit);
    }

    public void OnFireInputPerformed()
    {
        // Debug.Log($"Ray Shoot: fire performed!");
    }

    public void OnFireInputCanceled()
    {
        // Debug.Log($"Ray Shoot: fire canceled!");
        DeattachTarget(transform.position);
    }
     
    private void ShootAtTarget(RaycastHit hit)
    {        
        if (canReach && isLoaded && !isAttached)
        {
            anchor = hit.point;     
            StartCoroutine(AttachTarget(anchor));            
            OnTargetShot.Invoke(anchor);          
            shootTimer = shootDelay;        
        }                
    }

    private void DeattachTarget(Vector3 target)
    {
        StopAllCoroutines();
        if (isAttached)
            loadingTimer = reloadDelay;        
        isAttached = false;
        OnTargetDeattached.Invoke(target);
    }

    private IEnumerator AttachTarget(Vector3 target) 
    {
        yield return new WaitForSeconds(shootDelay);
        isAttached = true;
        OnTargetAttached.Invoke(target);
    }    

    private void Update() 
    {
        // Update shoot reach timer
        if (shootTimer > 0.0f)
        {
            shootTimer -= Time.deltaTime;
            float progress = 1f - shootTimer / shootDelay;
            // Debug.Log($"Ray Shoot progress: {progress}");
            OnShootingProgress.Invoke(progress);
        }        
        // Update reload timer
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
        
    }

    private void FixedUpdate() 
    {
        // REACH CALCULATION
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8; // This would cast rays only against colliders in layer 8. (Level)
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        // layerMask = ~layerMask;
        // Does the ray intersect any objects in the 8 layer (Level)
        canReach = Physics.Raycast(transform.position, transform.right, out hit, rayReach, layerMask);
        // React with event
        // OnReachChanged.Invoke(canReach);
        
        // SET STATE
        if (canReach && isLoaded)
            state = "ready";
        else if (canReach)
            state = "loading";
        else if (isLoaded)
            state = "unreach";

        // React with event
        OnStateChanged.Invoke(state);
        // React with debug Rays
        if (showDebug)
            DrawDebugRays();
        
        // ACTUAL ATTACHMENT
        if (isAttached)
            OnKeepAttached.Invoke(anchor);
    }
    private void DrawDebugRays()
    {
        if (canReach) {
            Debug.Log("Did Hit");
            Color rayColor = isLoaded ? Color.green : Color.yellow;
            Debug.DrawRay(transform.position, transform.right * hit.distance, rayColor);
        } else {
            Debug.Log("Did Not Hit");
            Color rayColor = isLoaded ? Color.magenta : Color.red;
            Debug.DrawRay(transform.position, transform.right * rayLength, rayColor);
        }
    }
}
