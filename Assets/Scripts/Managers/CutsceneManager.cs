using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No Controller");
            }
            return _instance;
        }
    }
    private static CutsceneManager _instance;
    PlayableDirector cutsceneDirector;
    [SerializeField]
    Vector3 cutsceneStartPosition;
    [SerializeField]
    Vector3 cutsceneStartRotation;
    [SerializeField]
    GameObject player;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartCutscene()
    {
        ControllerManager.Instance.SwapCurrentController(ControllerType.None);
        StartCoroutine(PrepareCutscene());
    }

    IEnumerator PrepareCutscene()
    {
        //Have the same thing for cinemachine pan tilt
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
