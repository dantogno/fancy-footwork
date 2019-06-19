using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private float fadeTime = 3.0f;

    private MeshCollider triggerCollider;
    private bool hasBeenTriggered = false;

    private void Awake()
    {
        triggerCollider = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBeenTriggered)
        {
            hasBeenTriggered = true;
            StartCoroutine(FadeThenExit());
        }
    }

    IEnumerator FadeThenExit()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += 1 * Time.deltaTime;
            yield return null;
        }

        SceneManagement.Win();
    }
}
