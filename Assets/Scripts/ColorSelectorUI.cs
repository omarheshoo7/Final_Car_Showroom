using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelectorUI : MonoBehaviour
{
    public CarColorChanger carColorChanger;

    public void SetRed()    => carColorChanger.ChangeColor(Color.red);
    public void SetBlue()   => carColorChanger.ChangeColor(Color.blue);
    public void SetGreen()  => carColorChanger.ChangeColor(Color.green);
    public void SetPurple() => carColorChanger.ChangeColor(new Color(0.5f, 0f, 0.5f));
}
