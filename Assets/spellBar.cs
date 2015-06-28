using UnityEngine;
using System.Collections;

public class spellBar : MonoBehaviour {

	public spellEffect		spell;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (spell.canBeUse == false)
			gameObject.GetComponent<CanvasRenderer>().SetColor(Color.grey);
		else
			gameObject.GetComponent<CanvasRenderer>().SetColor(Color.white);

	}
}
