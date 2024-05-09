using UnityEngine;

public class ScaleOnCollect : MonoBehaviour
{
    // Adjust this value to determine how much to scale the object
    public float scaleFactor = 1.5f;

    // This method is called when the player collects the object
    public void Collect()
    {
        // Scale the object
        transform.localScale *= scaleFactor;
    }
}
