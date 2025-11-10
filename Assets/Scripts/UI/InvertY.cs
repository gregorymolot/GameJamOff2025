using UnityEngine;
using UnityEngine.UI;

public class InvertY : MonoBehaviour
{
    Toggle invertToggle;

    void Awake()
    {
        invertToggle = GetComponent<Toggle>();
    }

    void OnEnable()
    {
        invertToggle.isOn = PlayerPrefs.GetInt("Invert", 0) == 1;
    }

    public void OnValueChanged()
    {
        PlayerPrefs.SetInt("Invert", invertToggle.isOn ? 1 : 0);
    }
}
