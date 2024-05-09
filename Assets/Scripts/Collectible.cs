using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Reference to the target prefab that should be scaled
    public GameObject targetPrefab;

    // This method is called when the player interacts with the collectible object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("[+] Player collected Gun Power Up!");
            // Get the ScaleOnCollect script from the target prefab and call its Collect method
            var scaleScript = targetPrefab.GetComponent<ScaleOnCollect>();
            if (scaleScript != null)
            {
                scaleScript.Collect();
                Debug.Log("[+] Calling collect");
            }

            // Optionally, destroy the collectible object after it's been collected
            Destroy(gameObject);
        }
    }
}
