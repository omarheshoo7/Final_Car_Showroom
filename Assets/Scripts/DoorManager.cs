using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public DoorController[] doors;  // Drag all door scripts here

    public void ToggleAllDoors()
    {
        foreach (DoorController door in doors)
        {
            if (door != null)
            {
                door.ToggleDoor();
            }
        }
    }
}
