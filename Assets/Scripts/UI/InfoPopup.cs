using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopup : MonoBehaviour
{
    [SerializeField]
    Material dissolveMaterial;

    [SerializeField]
    TextMeshProUGUI title;

    [SerializeField]
    TextMeshProUGUI description;

    [SerializeField]
    TextMeshProUGUI question;

    [SerializeField]
    MeshFilter displayMesh;

    Animator animator;

    bool isActivated;

    void Start()
    {
        dissolveMaterial.SetFloat("_DissolveAmount", -2);
        animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        dissolveMaterial.SetFloat("_DissolveAmount", -2);
    }

    public void Activate(InteractableItem item)
    {
        if (!isActivated)
        {
            ControllerManager.Instance.SwapCurrentController(ControllerType.Interactable);
            isActivated = true;
            displayMesh.mesh = item.GetComponent<MeshFilter>().mesh;
            description.text = item.Description;
            title.text = item.ItemName;
            question.text = "You can now ask " + item.ItemOwner + " about " + item.ItemName;

        StartCoroutine(Activation(item));
        }
    }

    IEnumerator Activation(InteractableItem item)
    {
        float timer = 0;
        while (timer < 1f)
        {
            dissolveMaterial.SetFloat("_DissolveAmount", Mathf.Lerp(-1, 2, timer));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        animator.SetTrigger("In");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("InfoPopupAnimationIn"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        item.Returnable = true;
    }

    public void Deactivate()
    {
        StartCoroutine(Deactivation());
    }
    
    IEnumerator Deactivation()
    {
        animator.SetTrigger("Out");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("InfoPopupAnimationOut"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        animator.SetTrigger("Wait");

        float timer = 0;
        while (timer < 1f)
        {
            dissolveMaterial.SetFloat("_DissolveAmount", Mathf.Lerp(2, -2, timer));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        isActivated = false;

        UIManager.Instance.TurnOffCanvas();

    }
}
