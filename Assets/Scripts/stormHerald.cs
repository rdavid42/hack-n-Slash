using UnityEngine;
using System.Collections;

public class stormHerald : MonoBehaviour {

	public string			effect;
	public int				damage;
	public int				level;
	public float			cooldown;
	
	// Use this for initialization
	void Start () {
		effect = "Engulfs you in magical storm that rapidly shock nearby enemies.";
		level = 0;
		damage = 2;
		cooldown = 0;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
