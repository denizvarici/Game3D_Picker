using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private new Collider collider;



    #endregion

    #region Private Variables

    private PlayerMovementData _data;
    private bool _isReadyToMove, _isReadyToPlay;
    private float _xValue;

    private float2 _clampValues;

    #endregion

    #endregion

    internal void SetData(PlayerMovementData data)
    {
        _data = data;
    }

    void FixedUpdate()
    {
        if (!_isReadyToPlay)
        {
            StopPlayer();
            return;
        }
        if (_isReadyToMove)
        {
            MovePlayer();
        }
        else
        {
            StopPlayerHorizontally();
        }
    }

    private void MovePlayer()
    {
        var velocity = rigidBody.linearVelocity;
        velocity = new Vector3(_xValue * _data.SidewaySpeed, velocity.y, _data.ForwardSpeed);
        rigidBody.linearVelocity = velocity;
        var position1 = rigidBody.position;
        Vector3 position;
        position = new Vector3(Mathf.Clamp(position1.x, _clampValues.x, _clampValues.y), (position = rigidBody.position).y, position.z);
        rigidBody.position = position;
    }

    private void StopPlayer()
    {
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    private void StopPlayerHorizontally()
    {
        rigidBody.linearVelocity = new Vector3(0, rigidBody.linearVelocity.y, _data.ForwardSpeed);
        rigidBody.angularVelocity = Vector3.zero;
    }


    internal void IsReadyToPlay(bool condition)
    {
        _isReadyToPlay = condition;
    }

    internal void IsReadyToMove(bool condition)
    {
        _isReadyToMove = condition;
    }

    internal void UpdateInputParams(HorizontalInputParams inputParams)
    {
        _xValue = inputParams.HorizontalValue;
        _clampValues = inputParams.ClampValues;
    }

    internal void OnReset()
    {
        StopPlayer();
        _isReadyToMove = false;
        _isReadyToPlay = false;
    }
}