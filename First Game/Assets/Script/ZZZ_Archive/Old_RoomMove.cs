using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Old_RoomMove : MonoBehaviour
{
    public Vector2 cameraMove;
    public Vector3 playerMove;
    [Header("Global Camera Min Max")]
    public vectorValue globalMaxPosition;
    public vectorValue globalMinPosition;
    [Header("Room Move Text")]
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            globalMinPosition.runtimeValue += cameraMove;
            globalMaxPosition.runtimeValue += cameraMove;
            other.transform.position += playerMove;
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
