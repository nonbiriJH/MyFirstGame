using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{
    [Header("Scene Variables")]
    public string sceneToLoad;
    public float sceneChangeWaitTime;


    [Header("Camera Local Variables")]
    public Vector2 newScenePosition;

    [Header("Camera Global Variables")]
    public vectorValue globalPosition;

    [Header("Animation")]
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;

    [Header("Utility")]
    public GameObject nextRoomVirtualCamera;
    public GlobalVariables globalVariables;//register next scene name

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            StartCoroutine(SceneChangeCo());
            globalPosition.runtimeValue = newScenePosition;
            if (nextRoomVirtualCamera != null)
            {
                nextRoomVirtualCamera.SetActive(true);
            }
            
        }
    }

    private IEnumerator SceneChangeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(sceneChangeWaitTime);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        globalVariables.currentScene = sceneToLoad;
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }


}
