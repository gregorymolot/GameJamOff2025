using System.Collections;
using Unity.Cinemachine;
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
    Transform cutscenePosition;
    [SerializeField]
    GameObject player;
    CinemachinePanTilt tilt;
    [SerializeField]
    CutscenePopup cutscenePopup;

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

    void Start()
    {
        cutsceneDirector = GetComponent<PlayableDirector>();
        tilt = player.GetComponentInChildren<CinemachinePanTilt>();
    }
    public void StartCutscene()
    {
        ControllerManager.Instance.SwapCurrentController(ControllerType.None);
        //cutsceneDirector.Play();
        StartCoroutine(PrepareCutscene());
    }

    IEnumerator PrepareCutscene()
    {
        //Have the same thing for cinemachine pan tilt
        float initialTilt = tilt.TiltAxis.Value;
        float initialPan = tilt.PanAxis.Value;
        float timer = 0;
        while (cutscenePosition.position !=player.transform.position || tilt.PanAxis.Value != 0 || tilt.TiltAxis.Value != 0)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, cutscenePosition.position, Time.deltaTime/2f );
            tilt.PanAxis.Value = Mathf.Lerp(initialPan, 0, timer);
            tilt.TiltAxis.Value = Mathf.Lerp(initialTilt, 0, timer);
            timer+=Time.deltaTime;
            yield return null;
        }
        cutsceneDirector.Play();
    }

    public void OnCutsceneEnd()
    {
        cutscenePopup.gameObject.SetActive(true);
        cutscenePopup.Activate();
    }
}
