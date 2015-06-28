using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public player			player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 7.0f, player.transform.position.z - 10.0f);
		transform.eulerAngles = new Vector3(30.0f, 0.0f, 0.0f);
	}
}
