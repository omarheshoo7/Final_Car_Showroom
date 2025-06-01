using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public GameObject car1Prefab;
    public GameObject car2Prefab;

    public static GameObject selectedCar;

    public void SelectCar1() => selectedCar = car1Prefab;
    public void SelectCar2() => selectedCar = car2Prefab;
}
