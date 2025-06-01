using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab; // Drag your car prefab here
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
            GameObject car = Instantiate(carPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
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
}
