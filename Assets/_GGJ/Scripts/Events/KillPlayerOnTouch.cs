using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayerOnTouch : MonoBehaviour
{
	public PlayerSpawn playerSpawn;

    /// <summary>
    /// When a collision is triggered, check if the thing colliding is actually the player. If yes, kill it.
    /// </summary>
    /// <param name="collider">The object that collides with the KillPlayerOnTouch object.</param>
    private void OnTriggerEnter(Collider collider)
	{
		if (collider.transform.tag == "Player") 
		{
			collider.enabled = false;
			playerSpawn.RespawnPlayer();
		}
	}
}
