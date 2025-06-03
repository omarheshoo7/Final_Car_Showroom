using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarSpawner : MonoBehaviour
{
    public GameObject car1Prefab; // Drag Car1 prefab
    public GameObject car2Prefab; // Drag Car2 prefab
    private int selectedCarIndex = 0;

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedCars = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
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
        foreach (var trackedImage in args.added)
        {
            SpawnOrUpdate(trackedImage);
        }

        foreach (var trackedImage in args.updated)
        {
            SpawnOrUpdate(trackedImage);
        }

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

        if (!spawnedCars.ContainsKey(imageName))
        {
            GameObject selectedPrefab = selectedCarIndex == 0 ? car1Prefab : car2Prefab;

            GameObject car = Instantiate(selectedPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
            car.transform.parent = trackedImage.transform;
            spawnedCars[imageName] = car;
        }
        else
        {
            GameObject car = spawnedCars[imageName];
            car.transform.position = trackedImage.transform.position;
            car.transform.rotation = trackedImage.transform.rotation;
            car.SetActive(true);
        }
    }

    // Call this from UI buttons to change model
    public void SetCarModel(int index)
    {
        selectedCarIndex = index;
    }
}
