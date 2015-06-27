using UnityEngine;
using System.Collections;

public class inventory : MonoBehaviour
{
	public GameObject			grid;
	public int					size;
	public GameObject[]			items;
	public GameObject			rightHand;
	public GameObject			leftHand;

	// slots
	public GameObject			rightHandSlot;
	// Use this for initialization
	void Start()
	{
		int i;

		size = 18;
		items = new GameObject[size];
		for (i = 0; i < size; ++i)
		{
			items[i] = null;
		}
	}

	public void addItem(GameObject item)
	{
		int i;

		for (i = 0; i < size; i++)
		{
			if (items[i] != null)
			{
				Destroy(item.GetComponent<Rigidbody>());
				item.GetComponent<MeshCollider>().enabled = false;
				item.SetActive(false);
				items[i] = item;
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
