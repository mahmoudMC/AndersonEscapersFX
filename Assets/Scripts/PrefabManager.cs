using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance { get; private set; }

    [SerializeField] private GameObject highlightObject;
    [SerializeField] private GameObject miniMapIcon;
    [SerializeField] private GameObject electricityText;
    public GameObject HighlightObject => highlightObject;
    public GameObject MiniMapIcon => miniMapIcon;

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnDestroy() {
        Instance = null;
    }
    public async void ShowElectricityError() {
        electricityText.SetActive(true);
        await Awaitable.WaitForSecondsAsync(3);
        electricityText.SetActive(false);
    }
}
