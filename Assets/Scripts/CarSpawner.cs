using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarSpawner : MonoBehaviour
{
    public GameObject car1Prefab; // Drag Car1 prefab in Inspector
    public GameObject car2Prefab; // Drag Car2 prefab in Inspector
    private int selectedCarIndex = 0;

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedCars = new Dictionary<string, GameObject>();

    private GameObject currentSpawnedCar; // Last spawned car reference

    // ─── We’ll store how far below each prefab's pivot the wheels are ───
    private float bottomOffsetCar1;
    private float bottomOffsetCar2;

    void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        // Compute bottomOffsetCar1
        {
            GameObject temp = Instantiate(car1Prefab, Vector3.zero, Quaternion.identity);
            float lowestY = float.MaxValue;
            foreach (var mr in temp.GetComponentsInChildren<MeshRenderer>())
            {
                lowestY = Mathf.Min(lowestY, mr.bounds.min.y);
            }
            Destroy(temp);
            bottomOffsetCar1 = -lowestY;
        }

        // Compute bottomOffsetCar2
        {
            GameObject temp = Instantiate(car2Prefab, Vector3.zero, Quaternion.identity);
            float lowestY = float.MaxValue;
            foreach (var mr in temp.GetComponentsInChildren<MeshRenderer>())
            {
                lowestY = Mathf.Min(lowestY, mr.bounds.min.y);
            }
            Destroy(temp);
            bottomOffsetCar2 = -lowestY;
        }
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        // For each newly detected image, spawn or update the car.
        foreach (var trackedImage in args.added)
        {
            SpawnOrUpdate(trackedImage);
        }

        // If an existing image’s pose changed (moved/rotated), update its car’s transform.
        foreach (var trackedImage in args.updated)
        {
            SpawnOrUpdate(trackedImage);
        }

        // If an image is removed, destroy its associated car.
        foreach (var trackedImage in args.removed)
        {
            if (spawnedCars.ContainsKey(trackedImage.referenceImage.name))
            {
                Destroy(spawnedCars[trackedImage.referenceImage.name]);
                spawnedCars.Remove(trackedImage.referenceImage.name);
            }
        }
    }

    void SpawnOrUpdate(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        // Decide which prefab and offset to use
        GameObject prefabToUse    = (selectedCarIndex == 0) ? car1Prefab : car2Prefab;
        float      bottomOffset   = (selectedCarIndex == 0) ? bottomOffsetCar1 : bottomOffsetCar2;

        if (!spawnedCars.ContainsKey(imageName))
        {
            // ─── Instantiate under the trackedImage, then lift by bottomOffset ───
            GameObject car = Instantiate(prefabToUse, trackedImage.transform);
            // Inherit parent's rotation so that the car faces the same way as the marker:
            car.transform.localRotation = Quaternion.identity;
            // Place it so that its wheels are exactly on the marker’s plane:
            car.transform.localPosition = new Vector3(0f, bottomOffset, 0f);

            spawnedCars[imageName] = car;
            currentSpawnedCar      = car;
        }
        else
        {
            // Update an already‐spawned car’s pose:
            GameObject car = spawnedCars[imageName];
            // Make sure it remains parented so its localPosition stays at (0, bottomOffset, 0):
            car.transform.SetParent(trackedImage.transform, worldPositionStays: false);
            car.transform.localRotation = Quaternion.identity;
            car.transform.localPosition = new Vector3(0f, bottomOffset, 0f);
            car.SetActive(true);
            currentSpawnedCar = car;
        }
    }

    // Called by your “Use Car 1” / “Use Car 2” UI buttons:
    //   pass 0 for Car1, 1 for Car2
    public void SetCarModel(int index)
    {
        selectedCarIndex = index;

        // If a marker is already detected and a car is showing, swap it immediately:
        if (currentSpawnedCar != null)
        {
            // Get the tracked-image name from the current car’s parent:
            string imageName = currentSpawnedCar.transform.parent.name;

            // Destroy existing GameObject, then let OnTrackedImagesChanged re‐spawn next frame:
            Destroy(currentSpawnedCar);
            spawnedCars.Remove(imageName);
        }
    }

    // Toggle turntable on the currently spawned car
    public void ToggleTurntable()
    {
        if (currentSpawnedCar != null)
        {
            CarRotator rotator = currentSpawnedCar.GetComponent<CarRotator>();
            if (rotator != null)
            {
                rotator.ToggleRotation();
            }
        }
    }
}
