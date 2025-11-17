using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    PlayableDirector cutsceneDirector;
    [SerializeField]
    Vector3 cutsceneStartPosition;
    [SerializeField]
    Vector3 cutsceneStartRotation;
    [SerializeField]
    GameObject player;
    public void StartCutscene()
    {
        ControllerManager.Instance.SwapCurrentController(ControllerType.None);
        StartCoroutine(PrepareCutscene());
    }

    IEnumerator PrepareCutscene()
    {
        float speed = 5f;
        while (cutsceneStartPosition !=player.transform.position && cutsceneStartRotation != player.transform.rotation.eulerAngles)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, cutsceneStartPosition, Time.deltaTime * speed );
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Quaternion.Euler(cutsceneStartRotation), Time.deltaTime * speed);
            yield return null;
        }
        cutsceneDirector.Play();
    }

    public void OnCutsceneEnd()
    {
        ControllerManager.Instance.SwapCurrentController(ControllerType.Cutscene);
    }
}
