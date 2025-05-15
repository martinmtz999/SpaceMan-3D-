using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public GameObject uiPrompt;
    public string sceneToLoad = "Space";
    private bool isPlayerNear = false;

    void Start()
    {
        if (uiPrompt != null)
            uiPrompt.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiPrompt != null)
                uiPrompt.SetActive(true);

            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiPrompt != null)
                uiPrompt.SetActive(false);

            isPlayerNear = false;
        }
    }
}
