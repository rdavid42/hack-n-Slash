using UnityEngine;
using System.Collections;

public class fireBlast : MonoBehaviour {

	public string			effect;
	public int				damage;
	public int				level;
	public float			cooldown;
	
	// Use this for initialization
	void Start () {
		effect = "This spell allow you to heal yourself.";
		level = 0;
		damage = 0;
		cooldown = 0;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
	