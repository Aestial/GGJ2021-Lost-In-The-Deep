using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCheckpointReached : MonoBehaviour
{
	public PlayerSpawn playerSpawn;

	/// <summary>
	/// When a collision is triggered, the PlayerSpawn is notifyed
	/// </summary>
	/// <param name="collider">The object that collides with the checkpoint.</param>
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.transform.tag == "Player")
		{
			collider.enabled = true;			
			playerSpawn.CheckpointReached();
		}
	}
}
