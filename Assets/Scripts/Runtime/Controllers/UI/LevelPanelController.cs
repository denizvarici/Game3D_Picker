using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<Image> stageImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> levelTexts = new List<TextMeshProUGUI>();

    #endregion

    #endregion



    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        UISignals.Instance.onSetLevelValue += OnSetLevelValue;
        UISignals.Instance.onSetStageColor += OnSetStageColor;
        
    }

    [Button]
    private void OnSetStageColor(byte stageValue)
    {
        stageImages[stageValue].DOColor(Color.cyan, 2f);
    }

    private void OnSetLevelValue(byte levelValue)
    {
        var additionalLevel = ++levelValue;
        levelTexts[0].text = additionalLevel.ToString();
        additionalLevel++;
        levelTexts[1].text = additionalLevel.ToString();
    }

    private void UnSubscribeEvents()
    {
        UISignals.Instance.onSetLevelValue -= OnSetLevelValue;
        UISignals.Instance.onSetStageColor -= OnSetStageColor;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
