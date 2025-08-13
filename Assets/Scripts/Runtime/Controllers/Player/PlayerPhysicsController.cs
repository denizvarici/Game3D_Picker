using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Collider collider;
    [SerializeField] private Rigidbody rigidBody;

    #endregion

    #region Private Variables

    private readonly string _stageArea = "StageArea";
    private readonly string _finish = "FinishArea";
    private readonly string _miniGame = "MiniGameArea";

    #endregion
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_stageArea))
        {
            manager.ForceCommand.Execute();
            CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();

            //Stage Area Kontrol Süreci

        }

        if (other.CompareTag(_finish))
        {
            CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            return;
        }

        if (other.CompareTag(_miniGame))
        {
            //write the mini game mechanics

        }
    }

    public void OnReset()
    {
        
    }
}