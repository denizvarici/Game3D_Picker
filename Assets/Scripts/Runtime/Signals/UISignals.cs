using UnityEngine;
using UnityEngine.Events;

public class UISignals : MonoBehaviour
{
    #region Singleton

    public static UISignals Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    #endregion


    public UnityAction<byte> onSetStageColor = delegate { };
    public UnityAction<byte> onSetLevelValue = delegate { };
    public UnityAction onPlay = delegate { };
}