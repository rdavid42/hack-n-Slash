using UnityEngine;
using System.Collections;

public class iceNova : MonoBehaviour {
	public string			effect;
	public int				damage;
	public int				level;
	public float			cooldown;
	
	// Use this for initialization
	void Start () {
		effect = "Ice Nova assaults surrounding enemies with a shivering blast of pure ice";
		level = 0;
		damage = 5;
		cooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
