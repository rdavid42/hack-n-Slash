using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {

	public GameObject[]		enemies;
	public float			spawnDelay;
	public GameObject		currentZomb;
	private enemy			currentZombScript;
	public bool				canSpawn;
	public player			player;
	public float			elapsedSpawnTime;
	public float			elapsedUpdateTime;
	public float			updateRate;
	public float			playerDist;
	public GameObject		lifePotion;

	// Use this for initialization
	void Start () {
		currentZomb = null;
		elapsedSpawnTime = spawnDelay;
		elapsedUpdateTime = 0.0f;
		canSpawn = true;
	}

	int getType()
	{
		return (Random.Range(0, enemies.Length - 1));
	}

	public void spawn()
	{
		int type = getType();
		currentZomb = (GameObject)Instantiate(enemies[type], transform.position, Quaternion.identity);
		currentZomb.SetActive(true);
		currentZombScript = currentZomb.GetComponent<enemy>();
		currentZombScript.st.level = player.st.level;
		currentZombScript.st.calculateEnemyStats();
		currentZombScript.lifePotion = lifePotion;
		currentZombScript.spawn = this;
		currentZombScript.player = player;
		canSpawn = false;
	}

	// Update is called once per frame
	void Update () {
		if (elapsedUpdateTime > updateRate)
		{
			playerDist = Vector3.Distance(player.transform.position, transform.position);
			if (playerDist < 50.0f)
			{
				if (canSpawn)
				{
					if (elapsedSpawnTime > spawnDelay)
					{
						spawn();
						elapsedSpawnTime = 0.0f;
					}
					elapsedSpawnTime += Time.deltaTime;
				}
			}
			elapsedUpdateTime = 0.0f;
		}
		elapsedUpdateTime += Time.deltaTime;
	}
}
