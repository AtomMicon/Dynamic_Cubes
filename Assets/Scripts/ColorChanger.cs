using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ResetColor(Renderer renderer)
    {
        renderer.material.color = Color.white;
    }

    public void ChangeColor(Renderer renderer)
    {
        renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
