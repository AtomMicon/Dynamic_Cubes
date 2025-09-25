using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ResetColor(Cube cube)
    {
        cube.GetComponent<Renderer>().material.color = Color.white;
    }

    public void ChangeColor(Cube cube)
    {
        cube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }
}
