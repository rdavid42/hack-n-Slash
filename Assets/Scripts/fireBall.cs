using UnityEngine;
using System.Collections;

public class fireBall : MonoBehaviour {

	public string			effect;
	public int				damage;
	public int				level;
	public float			cooldown;

	// Use this for initialization
	void Start () {
		effect = "Throw a fireball at your oponents. Damaging them if it hit";
		level = 0;
		damage = 10;
		cooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
