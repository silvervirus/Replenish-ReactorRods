using UnityEngine;
using System.Collections.Generic;
using UWE;

public class ReactorRodCrafter : MonoBehaviour
{
    public TechType outputTechType = TechType.ReactorRod; // The crafted Reactor Rod
    public TechType depletedRodTechType = TechType.DepletedReactorRod;
    public TechType uraniniteCrystalTechType = TechType.UraniniteCrystal;
    public Transform spawnPoint; // The point where crafted items will appear

    private Dictionary<TechType, int> requiredItems = new Dictionary<TechType, int>();
    public StorageContainer lockerStorage;

    void Start()
    {
        // Define the required items and their quantities
        requiredItems[depletedRodTechType] = 1;
        requiredItems[uraniniteCrystalTechType] = 2;

        // Get the StorageContainer component from the locker
        lockerStorage = GetComponent<StorageContainer>();
        if (lockerStorage == null)
        {
            Debug.LogError("No StorageContainer component found on this locker.");
        }
    }

    void Update()
    {
        if (lockerStorage != null && CheckRequiredItems())
        {
            CraftReactorRod();
        }
    }

    private bool CheckRequiredItems()
    {
        foreach (var item in requiredItems)
        {
            if (lockerStorage.container.GetCount(item.Key) < item.Value)
            {
                return false;
            }
        }

        return true;
    }

    private void CraftReactorRod()
    {
        // Remove the required items from the storage
        foreach (var item in requiredItems)
        {
            for (int i = 0; i < item.Value; i++)
            {
                if (!lockerStorage.container.DestroyItem(item.Key))
                {
                    Debug.LogError($"Failed to destroy required item: {item.Key}");
                    return; // Exit if any item removal fails
                }
            }
        }

        // Add the Reactor Rod to the inventory
        CraftData.AddToInventory(outputTechType);
        Debug.Log("Reactor Rod crafted and added to inventory!");
    }
    
}
