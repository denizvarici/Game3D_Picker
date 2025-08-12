using UnityEngine;
using UnityEngine.Events;

public class CameraSignals : MonoBehaviour
{
    #region Singleton

    public static CameraSignals Instance;

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

    public UnityAction onSetCameraTarget = delegate { };
}
