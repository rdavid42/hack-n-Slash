using UnityEngine;
using System.Collections;

public class passivSpell : MonoBehaviour {


	public int		armor;
	// Use this for initialization
	void Start () {
		armor = 2;
		GameObject.FindGameObjectWithTag("player").GetComponent<player>().st.armor += 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
