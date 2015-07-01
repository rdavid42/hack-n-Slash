using UnityEngine;
using System.Collections;

public class textPop : MonoBehaviour
{
	public TextMesh			textMesh;
	public float			elapsedTime;
	public float			lifeTime;
	public Vector3			direction;
	public float			velocity;
	public bool				moving;

	// Use this for initialization
	void Start()
	{
		elapsedTime = 0.0f;
	}

	public void addConfig(string text)
	{
		textMesh.text = text;
	}

	public void addConfig(Color color, float characterSize, float lifeTime)
	{
		textMesh.color = color;
		textMesh.characterSize = characterSize;
		this.lifeTime = lifeTime;
	}

	public void addConfig(Vector3 direction, float velocity)
	{
		moving = true;
		this.direction = direction;
		this.velocity = velocity;
	}

	void Update()
	{
		if (elapsedTime > lifeTime)
			Destroy(gameObject);
		if (moving)
		{
			transform.position = new Vector3(transform.position.x + velocity * direction.x * Time.deltaTime,
			                                 transform.position.y + velocity * direction.y * Time.deltaTime,
			                                 transform.position.z + velocity * direction.z * Time.deltaTime);
			textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - (elapsedTime / lifeTime));
		}
		elapsedTime += Time.deltaTime;
	}
}
