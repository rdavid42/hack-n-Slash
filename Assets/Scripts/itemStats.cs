using UnityEngine;
using System.Collections;

public class itemStats : MonoBehaviour
{
	// editor
	public int				type;
	public int				typeBaseMinDmg;
	public int				typeBaseMaxDmg;
	public float			speed;
	public int				armor;

	public string			name;
	// generator
	public int				ilvl;
	public int				size;
	// here
	public int				qualitybonusDmg;
	public int				finalMinDmg;
	public int				finalMaxDmg;
	public int				quality;
	public int				estimatedDps;
	public int				finalArmor;
	public string			qualityPrefix;
	public string			finalName;
	public string			description;

	// Use this for initialization
	void Start()
	{
	
	}
	
	public int baseDamage()
	{
		return (Random.Range(finalMinDmg, finalMaxDmg));
	}
	
	public int finalDamage(stats target)
	{
		return (baseDamage() * (1 - target.armor / 200));
	}

	public int dmgPerSecond()
	{
		return ((int)((((float)finalMinDmg + (float)finalMaxDmg) / 2.0f) * speed));
	}

	private void generateQuality()
	{
		quality = Random.Range(0, 100);
		if (quality >= 0 && quality < 50)
		{
			qualitybonusDmg = 0;
			qualityPrefix = "Common";
		}
		else if (quality >= 50 && quality < 80)
		{
			qualitybonusDmg = 1;
			qualityPrefix = "Uncommon";
		}
		else if (quality >= 80 && quality < 99)
		{
			qualitybonusDmg = 3;
			qualityPrefix = "Enchanted";
		}
		else if (quality == 99)
		{
			qualitybonusDmg = 8;
			qualityPrefix = "Rare";
		}
	}

	private void generateFinalName()
	{
		finalName = qualityPrefix + " " + name;
	}

	public void generate(int ilvl)
	{
		this.ilvl = ilvl;
		generateQuality();
		if (armor != 0)
			finalArmor = armor + (ilvl * (int)((float)armor * 10.0f / 100.0f)) + (int)((float)qualitybonusDmg / 2.0f);
		else
			finalArmor = 0;
		finalMinDmg = typeBaseMinDmg + (ilvl * (int)((float)typeBaseMinDmg * 15.0f / 100.0f)) + qualitybonusDmg;
		finalMaxDmg = typeBaseMaxDmg + (ilvl * (int)((float)typeBaseMaxDmg * 15.0f / 100.0f)) + qualitybonusDmg;
		estimatedDps = dmgPerSecond();
		generateFinalName();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
