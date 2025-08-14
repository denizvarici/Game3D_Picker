using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();
    [SerializeField] private TextMeshPro poolText;
    [SerializeField] private byte stageID;
    [SerializeField] private new Renderer renderer;

    #endregion

    #region Private Variables

    private PoolData _data;
    private byte _collectedCount;
    private readonly string _collectable = "Collectable";

    #endregion

    #endregion


    void Awake()
    {
        _data = GetPoolData();

    }

    void Start()
    {
        SetRequiredAmountText();
    }

    private void SetRequiredAmountText()
    {
        poolText.text = $"0 / {_data.RequiredObjectCount}";
    }

    void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
        CoreGameSignals.Instance.onStageAreaSuccessful += OnChangePoolColor;

    }

    private void OnChangePoolColor(byte stageValue)
    {
        if (stageValue != stageID) return;

        renderer.material.DOColor(Color.red, 1f).SetEase(Ease.Linear);
    }

    private void OnActivateTweens(byte stageValue)
    {
        if (stageValue != stageID) return;

        foreach (var tween in tweens)
        {
            tween.DOPlay();
        }
    }

    void OnDisable()
    {
        UnSubScribeEvents();
    }

    private void UnSubScribeEvents()
    {
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnActivateTweens;
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnChangePoolColor;
    }

    private PoolData GetPoolData()
    {
        return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].Pools[stageID];
    }

    public bool TakeResults(byte managerStageValue)
    {
        if (stageID == managerStageValue)
        {
            return _collectedCount >= _data.RequiredObjectCount;
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_collectable)) return;
        IncreaseCollectedAmount();
        SetCollectedAmountToPool();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_collectable)) return;
        DecreaseCollectedAmount();
        SetCollectedAmountToPool();
    }

    private void DecreaseCollectedAmount()
    {
        _collectedCount--;
    }

    private void SetCollectedAmountToPool()
    {
        poolText.text = $"{_collectedCount} / {_data.RequiredObjectCount}";
    }

    private void IncreaseCollectedAmount()
    {
        _collectedCount++;
    }
}