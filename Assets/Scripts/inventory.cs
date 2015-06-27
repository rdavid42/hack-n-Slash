using UnityEngine;
using System.Collections;

public class inventory : MonoBehaviour
{
	public GameObject			grid;
	public static int			width = 9;
	public static int			height = 5;
	public static int			cellSize = 32;
	public GameObject[][]		items;

	// slots
	public GameObject			rightHandSlot;
	// Use this for initialization
	void Start()
	{
		int i;

		items = new GameObject[height][];
		for (i = 0; i < height; i++)
			items[i] = new GameObject[width];
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
