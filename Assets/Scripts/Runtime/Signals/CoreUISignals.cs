using UnityEngine;
using UnityEngine.Events;

public class CoreUISignals : MonoBehaviour
{
    #region Singleton
    public static CoreUISignals Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    #endregion


    public UnityAction<UIPanelTypes, int> onOpenPanel = delegate { };
    public UnityAction<int> onClosePanel = delegate { };
    public UnityAction onCloseAllPanels = delegate { };
}