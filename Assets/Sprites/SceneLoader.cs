using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class SceneLoader : MonoBehaviour
{
    // The name of the scene you want to load
    [SerializeField] private string sceneToLoad;

    // This method is called automatically when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
            print("working");
        }
    }
}
