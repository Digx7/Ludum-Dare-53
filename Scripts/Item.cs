using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    resource, key, speedBoots, inventorySpace, watch, gps
}

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/NewItem", order = 1)]
public class Item : ScriptableObject
{
   public Sprite sprite;

   public Color color = Color.white;
   public string id; 
   public ItemType type;
}
