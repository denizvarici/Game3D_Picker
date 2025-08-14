using DG.Tweening;
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
            Debug.LogWarning("Stage'e çarptık bekleyelim...");
            DOVirtual.DelayedCall(3, () =>
            {
                Debug.LogWarning("3 saniye bekledik toplar yeterli sayı damı bakalım ?");
                var result = other.transform.parent.GetComponentInChildren<PoolController>().TakeResults(manager.StageValue);
                
                if (result)
                {
                    Debug.LogWarning("Toplar yeterli sayıda !");
                    CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                    InputSignals.Instance.onEnableInput?.Invoke();
                    Debug.LogWarning("Stage başarılı oldu, inputlar aktif edildi");
                }
                
                else CoreGameSignals.Instance.onLevelFailed?.Invoke();
                
            });
            return;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var transform1 = manager.transform;
        var position1 = transform1.position;
        Gizmos.DrawSphere(new Vector3(position1.x, position1.y-1, position1.z+.75f), 2f);
    }

    public void OnReset()
    {

    }
}