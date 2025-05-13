using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDialogue : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public float messageDuration = 3f;
    public float fadeDuration = 0.5f;

    private Coroutine currentMessage;

    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "House")
        {
            ShowMessage("Where... am I? Did I just come out of a toilet? I need to find the exit");
        }
        else if (scene == "Space")
        {
            ShowMessage("I think my ship is at the top, I need to climb up to it!");
        }
    }

    public void ShowMessage(string message)
    {
        if (currentMessage != null)
            StopCoroutine(currentMessage);

        currentMessage = StartCoroutine(FadeMessage(message));
    }

    IEnumerator FadeMessage(string message)
    {
        storyText.text = message;

        Color c = storyText.color;
        c.a = 0f;
        storyText.color = c;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / fadeDuration);
            storyText.color = c;
            yield return null;
        }


        yield return new WaitForSeconds(messageDuration);

        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, t / fadeDuration);
            storyText.color = c;
            yield return null;
        }

        storyText.text = "";
    }
}
