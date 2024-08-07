using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverButtonLisenerAdd : MonoBehaviour
{
    public Button Restart;
    public Button Menu;
    [HideInInspector]
    public botones butonLiseners;
    private void Awake()
    {
        butonLiseners = FindObjectOfType<botones>();
        Restart.onClick.AddListener(butonLiseners.RestartLevel);
        Menu.onClick.AddListener(butonLiseners.LoadMenuScene);
    }
}
