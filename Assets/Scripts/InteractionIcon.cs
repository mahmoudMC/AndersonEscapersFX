using UnityEngine;

public class InteractionIcon : MonoBehaviour
{
    public static InteractionIcon Instance { get; private set; }
    private bool isRelocating = false;
    private void Awake() {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void ShowUp(Transform target, Vector3 offset) {
        if (gameObject.activeInHierarchy) Relocate();
        transform.position = target.position + offset;
        gameObject.SetActive(true);
    }
    private async void Relocate() {
        isRelocating = true;
        await Awaitable.NextFrameAsync();
        await Awaitable.NextFrameAsync();
        isRelocating = false;
    }
    public void Hide() {
        if (isRelocating) return;
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Instance = null;
    }
}
