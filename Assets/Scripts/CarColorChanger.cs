using UnityEngine;

public class CarColorChanger : MonoBehaviour
{
    public Renderer carBodyRenderer;
    public Renderer[] tireRenderers;

    private Color originalColor;

    void Start()
    {
        if (carBodyRenderer != null)
        {
            originalColor = carBodyRenderer.material.color;
        }
    }

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

    // ✅ Make sure this method is public and has no parameters
    public void ResetColor()
    {
        ChangeColor(originalColor);
    }
}
