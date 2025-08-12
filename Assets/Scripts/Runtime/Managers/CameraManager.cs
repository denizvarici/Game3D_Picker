using System;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    #endregion

    private float3 _firstPosition;

    #endregion


    void Start()
    {
        Init();
    }

    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnReset()
    {
        transform.position = _firstPosition;
    }

    private void OnSetCameraTarget()
    {
        //var player = FindFirstObjectByType<PlayerManager>().transform;
        //virtualCamera.Follow = player;
        //virtualCamera.LookAt = player;
    }

    private void UnSubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void Init()
    {
        _firstPosition = transform.position;
    }
}
