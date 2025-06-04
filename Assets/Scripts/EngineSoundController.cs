using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundController : MonoBehaviour
{
    public TapToPlace tapToPlace; // Reference your spawner script
    private EngineController currentCarEngine;

    public void ToggleEngineSound()
    {
        if (tapToPlace == null) return;

        GameObject spawnedCar = tapToPlace.GetSpawnedCar();
        if (spawnedCar == null) return;

        // Get EngineController component on spawned car
        currentCarEngine = spawnedCar.GetComponent<EngineController>();
        if (currentCarEngine != null)
        {
            currentCarEngine.ToggleEngine();
        }
    }
}
