using UnityEngine;
using System.Collections;

public class inventory : MonoBehaviour
{
	public GameObject			grid;
	public int					size;
	public GameObject[]			slots;
	public GameObject			rightHand;
	public GameObject			leftHand;

	// slots
	public GameObject			rightHandSlot;
	// Use this for initialization
	void Start()
	{
		int i;

		size = 18;
		slots = new GameObject[size];
		i = 0;
		foreach (Transform child in grid)
		{
			slots[i] = child.gameObject;
			i++;
		}
	}

	public void addItem(GameObject item)
	{
		int i;

		for (i = 0; i < size; i++)
		{
			if (slots[i] != null)
			{
				Destroy(item.GetComponent<Rigidbody>());
				item.GetComponent<MeshCollider>().enabled = false;
				item.transform.position = new Vector3(10.0f, -10.0f, 0.0f);
				item.transform.eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
				item.transform.localScale = new Vector3(40.0f, 40.0f, 40.0f);
				item.transform.parent = slots[i].transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
