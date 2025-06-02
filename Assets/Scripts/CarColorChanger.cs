using UnityEngine;

public class CarColorChanger : MonoBehaviour
{
    public Renderer carBodyRenderer;
    public Renderer[] tireRenderers;

    public void ChangeColor(Color newColor)
    {
        if (carBodyRenderer != null)
        {
            carBodyRenderer.material.color = newColor;
        }

        foreach (Renderer tire in tireRenderers)
        {
            if (tire != null)
            {
                tire.material.color = newColor;
            }
        }
    }
}
