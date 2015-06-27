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
		for (i = 0; i < size; ++i)
		{
			slots[i] = null;
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
				item.transform.parent = slots[i].transform;
				item.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
