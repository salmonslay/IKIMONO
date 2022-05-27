using IKIMONO.Minigame.Jump;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pausGamePanel;
    [SerializeField] private Text _pausGameText;
    [SerializeField] private JumpManager _jumpManager;


    public void PausGame()
    {
        Time.timeScale = 0;

        Screen.sleepTimeout = SleepTimeout.SystemSetting;

        _pausGamePanel.SetActive(true);
        const string color = "#C08C2B";
        _pausGameText.text = $"You have travelled <color={color}>{Mathf.RoundToInt(_jumpManager.HighestJump)}</color> meters so far, collected <color={color}>{_jumpManager.CoinsCollected}</color> coin{(_jumpManager.CoinsCollected == 1 ? "" : "s")} and jumped <color={color}>{_jumpManager.JumpCount}</color> times!";

    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        _pausGamePanel.SetActive(false);
    }


}
