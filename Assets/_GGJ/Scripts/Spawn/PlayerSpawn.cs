using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
	public GameObject player;

	public List<Transform> spawnPoints;

	public int spawnIndex;

	public CapsuleCollider playerCollider;

	public bool playerRespawning;

	public float respawnSpeed;

	public float deltaTime;

	private MeshRenderer[] playerRenderers;

	public int playerLives;

	public string sceneName;

	private void Start()
    {
		playerCollider = player.GetComponentInChildren<CapsuleCollider>();
		playerRenderers = new MeshRenderer[] { };
		playerRenderers = player.GetComponentsInChildren<MeshRenderer>();
	}

    private void Update()
    {
		if (playerRespawning == true) 
		{
			SetPlayerPosition();
		}
    }

    public void SetPlayerPosition()
    {
		if (player == null)
		{
			Debug.LogError("Player object missing.");
			return;
		}

		if (spawnPoints.Count == 0)
		{
			Debug.LogError("Spawn points missing.");
			return;
		}

		// we re-enable its collider
		player.transform.position = Vector3.Lerp(player.transform.position, spawnPoints[spawnIndex].position, respawnSpeed);
		player.transform.rotation = spawnPoints[spawnIndex].rotation;
		playerCollider.enabled = true;
	}

	public void RespawnPlayer() 
	{
		if (playerLives == 0) 
		{
			SceneManager.LoadScene(sceneName);
		}

		playerLives--;

		playerRespawning = true;
		deltaTime = Time.time;

		for (int i = 0; i < playerRenderers.Length; i++) 
		{
			playerRenderers[i].enabled = false;
		}
	}

	public void CheckpointReached() 
	{
		playerRespawning = false;

		for (int i = 0; i < playerRenderers.Length; i++)
		{
			playerRenderers[i].enabled = true;
		}
	}

	

}
