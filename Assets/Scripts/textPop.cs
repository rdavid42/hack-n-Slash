using UnityEngine;
using System.Collections;

public class textPop : MonoBehaviour {
	public TextMesh			text;
	public float			elapsedTime;
	public float			lifeTime;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<TextMesh>();
		text.color = new Color(1.0f, 1.0f, 1.0f);
		text.characterSize = 0.2;
		text.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		text.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
		elapsedTime = 0.0f;
	}

	void config(Vector3 position, Color color, float characterSize, Vector3 orientation, Vector3 velocity, float lifetime)
	{
		text.color = color;
		text.characterSize = characterSize;
	}

	// Update is called once per frame
	void Update () {
		if (elapsedTime > lifeTime)
			Destroy(gameObject);
		elapsedTime += Time.deltaTime;
	}
}
