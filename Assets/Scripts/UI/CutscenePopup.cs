using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutscenePopup : MonoBehaviour
{
    [SerializeField]
    Material dissolveMaterial;

    [SerializeField]
    Animator animator;

    void Start()
    {
        dissolveMaterial.SetFloat("_DissolveAmount", -2);
    }

    void OnEnable()
    {
        EventManager.Game.EndCutscene += Deactivate;
    }

    void OnDisable()
    {
        EventManager.Game.EndCutscene -= Deactivate;
        dissolveMaterial.SetFloat("_DissolveAmount", -2);
    }

    public void Activate()
    {
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
        EventManager.Game.BeginGame?.Invoke();
        animator.SetTrigger("Start");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("CutsceneIn"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        ControllerManager.Instance.SwapCurrentController(ControllerType.Cutscene);
    }

    public void Deactivate()
    {
        StartCoroutine(Deactivation());
    }
    
    IEnumerator Deactivation()
    {
        animator.SetTrigger("End");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("CutsceneOut"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        float timer = 0;
        while (timer < 1f)
        {
            dissolveMaterial.SetFloat("_DissolveAmount", Mathf.Lerp(2, -2, timer));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
        gameObject.SetActive(false);
    }
}
