using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
	public List<GameObject> keys;

    public void AddKeyToInventory(GameObject key)
	{
		keys.Add(key);
	}
}
