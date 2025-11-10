using UnityEngine;
using UnityEngine.UI;


public class Sensitivity : MonoBehaviour
{
    private Slider sensitivitySlider;

    private void Awake()
    {
        sensitivitySlider = GetComponent<Slider>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 1f);
    }

    public void OnSliderValueChanged()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
    }
}
