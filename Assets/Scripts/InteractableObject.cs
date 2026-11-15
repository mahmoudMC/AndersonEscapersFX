using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject mission;
    [SerializeField] private Vector3 iconOffset = Vector3.up;
    private GameObject highlightObject;
    private GameObject icon;
    private bool wasInteracted = false;
    protected bool canInteract = true;
    private void Start() {
        PlayerInteraction.OnNewInteractableSet += OnNewInteractableSet;
        gameObject.layer = LayerMask.NameToLayer("Interactable");

        // Creating the highlight Object
        SpawningHightlightObject();
        SpawnMinimapIcon();
    }

    private void SpawningHightlightObject() {
        if (PrefabManager.Instance == null) {
            Debug.LogError($"Hightlight object is null at {gameObject.name}");
            return;
        }
        highlightObject = Instantiate(PrefabManager.Instance.HighlightObject, transform);
        highlightObject.transform.localPosition = Vector3.zero;
        highlightObject.transform.localScale = Vector3.one * 1.15f;
        highlightObject.GetComponent<SpriteRenderer>().sprite =
            GetComponent<SpriteRenderer>().sprite;
        highlightObject.SetActive(false);
    }
    private void SpawnMinimapIcon() {
        icon = Instantiate(PrefabManager.Instance.MiniMapIcon);
        icon.transform.parent = transform;
        icon.transform.localPosition = Vector3.zero;
    }

    private void OnDestroy() {
        PlayerInteraction.OnNewInteractableSet -= OnNewInteractableSet;
    }

    private void OnNewInteractableSet(IInteractable obj) {
        if (obj == (IInteractable)this) {
            wasInteracted = true;
            ObjectInRange();
        } else if (wasInteracted) {
            wasInteracted = false;
            ObjectOutsideRange();
        }
    }
    protected virtual void ObjectInRange() {
        //print("I'm in range");
        InteractionIcon.Instance.ShowUp(transform, iconOffset);
        if (highlightObject != null) highlightObject.SetActive(true);
    }
    protected virtual void ObjectOutsideRange() {
        //print("I left the range");
        InteractionIcon.Instance.Hide();
        if (highlightObject != null) highlightObject.SetActive(false);
    }

    public void MissionDone() {
        canInteract = false;
        mission.SetActive(false);
        GameStateManager.Instance.GameStart();
        Destroy(icon);
    }
    public virtual void Interact() {
        if (mission == null) return;
        if (GameStateManager.Instance.HasElectricity) {
            mission.SetActive(true);
            GameStateManager.Instance.PauseGame();
        } else {
            PrefabManager.Instance.ShowElectricityError();
        }
    }
    public bool CanInteract() {
        return canInteract;
    }
}