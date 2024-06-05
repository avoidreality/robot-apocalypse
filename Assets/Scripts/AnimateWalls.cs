using UnityEngine;

public class EndlessScrollWalls : MonoBehaviour
{
    public Transform leftWall;         // Reference to the left wall Transform
    public Transform rightWall;        // Reference to the right wall Transform
    public float scrollSpeed = 1f;     // Speed at which the walls scroll

    private float wallLength;          // Length of the wall
    private Vector3 leftStartPos;
    private Vector3 rightStartPos;

    void Start()
    {
        // Calculate the wall length based on the bounds of the left wall
        wallLength = GetWallLength(leftWall);
        Debug.Log("[+] walLength = " + wallLength);
        // Store the initial positions of the walls
        leftStartPos = leftWall.position;
        rightStartPos = rightWall.position;
    }

    void Update()
    {
        float delta = Time.deltaTime * scrollSpeed;

        // Move walls backward on the z-axis
        MoveWall(leftWall, delta);
        MoveWall(rightWall, delta);

        repositionWall();
        //makeWalls();
        DestroyWalls();

    }

    void repositionWall()
    {
        // Check if walls need to be repositioned
        if (leftWall.position.z < 12)
        {
            // can I instantiate a wall directly behind the wall moving off the screen? Instead of the jerking motion of the wall jumping up after a point is reached?
            leftWall.position = new Vector3(leftWall.position.x, leftWall.position.y, leftStartPos.z);
        }
        if (rightWall.position.z < 12)
        {
            rightWall.position = new Vector3(rightWall.position.x, rightWall.position.y, rightStartPos.z);
        }

    }

    void makeWalls()
    {
        if (leftWall.position.z < -1.5)
        {
            // Create a new wall
            leftWall = Instantiate(leftWall, leftStartPos, leftWall.rotation);
        }

        if (rightWall.position.z < -1.5)
        {
            // Create a new wall
            rightWall = Instantiate(rightWall, rightStartPos, rightWall.rotation);
        }
    }

    void MoveWall(Transform wall, float delta)
    {
        wall.Translate(0, 0, -delta, Space.World);
    }

    float GetWallLength(Transform wall)
    {
        Renderer renderer = wall.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.size.z;
        }
        else
        {
            Debug.LogWarning("Renderer not found on the wall object. Using default wall length.");
            return 300f; // Default length in case renderer is not found
        }
    }

    void DestroyWalls()
    {
        if (gameObject.transform.position.z < -25)
        {
            Destroy(gameObject);
        }
    }
}
