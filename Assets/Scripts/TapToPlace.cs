using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    public GameObject carPrefab; // Assign in inspector
    private GameObject spawnedCar;

    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        if (arRaycastManager == null)
        {
            Debug.LogError("ARRaycastManager not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (spawnedCar == null)
            {
                spawnedCar = Instantiate(carPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedCar.transform.position = hitPose.position;
                spawnedCar.transform.rotation = hitPose.rotation;
            }
        }
    }

    // Return the spawned car GameObject for other scripts to use
    public GameObject GetSpawnedCar()
    {
        return spawnedCar;
    }

    // Call this to toggle the engine sound on the spawned car
    public void ToggleCarEngine()
    {
        if (spawnedCar == null)
        {
            Debug.LogWarning("No spawned car to toggle engine.");
            return;
        }

        EngineController engineController = spawnedCar.GetComponent<EngineController>();
        if (engineController == null)
        {
            Debug.LogWarning("No EngineController component found on spawned car.");
            return;
        }

        engineController.ToggleEngine();
    }
}
