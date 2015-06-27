using UnityEngine;
using System.Collections;

public class stats : MonoBehaviour
{
	public int			xp;
	public int			level;
	public int			money;

	public int			strength;
	public int			agility;
	public int			constitution;
	
	public int			armor;
	
	[HideInInspector]
	public int			maxHp;
	public int			hp;
	[HideInInspector]
	public int			xpNext;
	[HideInInspector]
	public int			minDamage;
	[HideInInspector]
	public int			maxDamage;

	// Use this for initialization
	void Start()
	{
		calculateStats();
		xpNext = getNextXp();
	}

	public void calculateStats()
	{
		hp = 5 * constitution;
		maxHp = hp;
		minDamage = strength / 2;
		maxDamage = minDamage + 4;
	}

	public void calculateEnemyStats()
	{
		strength = (int)((float)strength * (100.0f + (float)(level - 1) * 15.0f) / 100.0f);
		agility = (int)((float)agility * (100.0f + (float)(level - 1) * 15.0f) / 100.0f);
		constitution = (int)((float)constitution * (100.0f + (float)(level - 1) * 15.0f) / 100.0f);
	}

	public bool hitChance(stats target)
	{
		int hit = 75 + agility - target.agility;
		return (Random.Range(0, 100) <= hit);
	}

	public int baseDamage()
	{
		return (Random.Range(minDamage, maxDamage));
	}

	public int finalDamage(stats target)
	{
		return (baseDamage() * (1 - target.armor / 200));
	}

	public int getNextXp()
	{
		return (level * 1000 + level * 500);
	}

	// Update is called once per frame
	void Update()
	{
	
	}
}
