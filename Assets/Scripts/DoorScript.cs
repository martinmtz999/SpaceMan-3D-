using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public GameObject uiPrompt;
    public string sceneToLoad = "Space";
    private bool isPlayerNear = false;

    void Start()
    {
        uiPrompt.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPrompt.SetActive(true);
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPrompt.SetActive(false);
            isPlayerNear = false;
        }
    }
}
