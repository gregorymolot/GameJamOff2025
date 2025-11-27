using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndgamePopup : MonoBehaviour
{
    [SerializeField]
    Material dissolveMaterial;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Button endButton;

    [SerializeField]
    List<Ending> endings;

    [SerializeField]
    List<TextMeshProUGUI> texts;

    void Start()
    {
        dissolveMaterial.SetFloat("_DissolveAmount", -2);
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
        dissolveMaterial.SetFloat("_DissolveAmount", -2);
    }

    public void Activate(Name name)
    {
        Ending correctEnding = null;
        foreach(Ending ending in endings)
        {
            if (name == ending.name)
            {
                correctEnding = ending;
                break;
            }
        }
        if (correctEnding == null)
        {
            Debug.Log("Not an option!!!");
            return;
        }
        for(int i=0; i<texts.Count; i++)
        {
            texts[i].text = correctEnding.endingLines[i];
        }
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro()
    {
        float timer = 0;
        while (timer < 1f)
        {
            dissolveMaterial.SetFloat("_DissolveAmount", Mathf.Lerp(-2, 2, timer));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        animator.SetTrigger("End");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Ending"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        endButton.enabled = true;
    }
}

[System.Serializable]
public class Ending
{
    public Name name;
    [TextArea(3,10)]
    public string[] endingLines;
}