using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject[] carPrefabs; // Assign your car prefabs here in Inspector (e.g., Car1, Car2)
    
    private GameObject currentCar;
    private int currentCarIndex = 0;

    // Spawn a car prefab at the origin (or any position)
    public void SpawnCar(int index)
    {
        if (index < 0 || index >= carPrefabs.Length) return;

        if (currentCar != null)
            Destroy(currentCar);

        currentCar = Instantiate(carPrefabs[index], Vector3.zero, Quaternion.identity);
        currentCarIndex = index;
    }

    // Spawn a car prefab at a specific position and rotation
    public void SpawnCarAtPosition(int index, Vector3 position, Quaternion rotation)
    {
        if (index < 0 || index >= carPrefabs.Length) return;

        if (currentCar != null)
            Destroy(currentCar);

        currentCar = Instantiate(carPrefabs[index], position, rotation);
        currentCarIndex = index;
    }

    // Get the current car instance
    public GameObject GetCurrentCar()
    {
        return currentCar;
    }

    // Get the index of the current car prefab
    public int GetCurrentCarIndex()
    {
        return currentCarIndex;
    }
}
