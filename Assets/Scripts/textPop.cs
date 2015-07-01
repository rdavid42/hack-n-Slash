using UnityEngine;
using System.Collections;

public class textPop : MonoBehaviour
{
	public TextMesh			text;
	public float			elapsedTime;
	public float			lifeTime;
	public Vector3			direction;
	public float			velocity;
	public float			velocityOverLifetime;

	// Use this for initialization
	void Start()
	{
		text = gameObject.GetComponent<TextMesh>();
		text.color = new Color(1.0f, 1.0f, 1.0f);
		text.characterSize = 0.2f;
		transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
		elapsedTime = 0.0f;
	}
	
	public void config(Vector3 position, Color color, float characterSize, float lifetime)
	{
		text.color = color;
		text.characterSize = characterSize;
		this.lifeTime = lifeTime;
		this.direction = direction;
		transform.position = position;
	}

	public void config(Vector3 direction, float velocity, Vector3 eulerAngles)
	{
		transform.eulerAngles = eulerAngles;
		this.direction = direction;
		this.velocity = velocity;
	}

	void Update()
	{
		if (elapsedTime > lifeTime)
			Destroy(gameObject);
		elapsedTime += Time.deltaTime;
	}
}
