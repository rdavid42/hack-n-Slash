using UnityEngine;
using System.Collections;

public class itemStats : MonoBehaviour
{
	// editor
	public int				type;
	public int				typeBaseMinDmg;
	public int				typeBaseMaxDmg;
	public float			speed;
	public string			name;
	// generator
	public int				ilvl;
	// here
	public int				qualitybonusDmg;
	public int				finalMinDmg;
	public int				finalMaxDmg;
	public int				quality;
	public string			qualityPrefix;
	public string			finalName;

	// Use this for initialization
	void Start()
	{
	
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
		else if (quality >= 80 && quality < 100)
		{
			qualitybonusDmg = 3;
			qualityPrefix = "Enchanted";
		}
		else if (quality == 100)
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
		finalMinDmg = typeBaseMinDmg + (ilvl * (int)((float)typeBaseMinDmg * 15.0f / 100.0f)) + qualitybonusDmg;
		finalMaxDmg = typeBaseMaxDmg + (ilvl * (int)((float)typeBaseMaxDmg * 15.0f / 100.0f)) + qualitybonusDmg;
		generateFinalName();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
