using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance { get; private set; }

    [SerializeField] private GameObject highlightObject;
    [SerializeField] private GameObject miniMapIcon;
    public GameObject HighlightObject => highlightObject;
    public GameObject MiniMapIcon => miniMapIcon;

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnDestroy() {
        Instance = null;
    }
}
