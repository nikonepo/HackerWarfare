using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _start_menu;
    [SerializeField] GameObject _settings_menu;
    public void Start()
    {
        _settings_menu.SetActive(false);
    }
    public void Exit()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void GoSettings()
    {
        _start_menu.SetActive(false);
        _settings_menu.SetActive(true);
    }

    public void GoStartMenu()
    {
        _settings_menu.SetActive(false);
        _start_menu.SetActive(true);
    }
}
