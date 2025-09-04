using UnityEngine;

/// <summary>
/// This method for implementing objects will be further expanded and detailed in the future.
/// This is just a crude implementation to get things started.
/// </summary>
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public string description = "Item Description";
    public ItemType itemType;
    public Sprite icon = null;
    
}
