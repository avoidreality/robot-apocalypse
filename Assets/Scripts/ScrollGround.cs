using UnityEngine;

public class ScrollGround : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Speed of the scrolling
    private Renderer groundRenderer;

    void Start()
    {
        groundRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Calculate the new offset based on time and speed
        float offset = Time.time * scrollSpeed;

        // Apply the offset to the material's texture
        Vector2 offsetVector = new Vector2(0, offset);
        groundRenderer.material.mainTextureOffset = offsetVector;
    }
}
