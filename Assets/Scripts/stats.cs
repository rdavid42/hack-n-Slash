using UnityEngine;
using System.Collections;

public class stats : MonoBehaviour
{
	public player		player = null;
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
	
	public int dmgPerSecond()
	{
		return ((int)((((float)minDamage + (float)maxDamage) / 2.0f)));
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
		int armor;

		if (target.player != null)
		{
			Debug.Log("Je calcule l'armure");
			armor = target.armor;
			foreach (GameObject item in target.player.inv.equipedItems)
			{
				if (item != null)
					armor += item.GetComponent<itemStats>().finalArmor;
			}
		}
		else
		{
			armor = target.armor;
		}
		if (armor > 200)
			armor = 200;
		if (armor < 0)
			armor = 0;
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
