using System.Collections;
using TMPro;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator uiAnimator;
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private float waitTime = 0.04f;

    [SerializeField] private Animator bagAnimator;
    public void ShowPrompt(string text)
    {
        uiAnimator.SetBool("showPrompt", true);
        StartCoroutine(Typewriter());
        IEnumerator Typewriter()
        {
            promptText.text = "";
            foreach (var c in text)
            {
                promptText.text += c;
                yield return  new WaitForSeconds(waitTime);
            }
        }
    }

    public void CloseBag(bool close)
    {
        bagAnimator.SetBool("close", close);
    }
}
