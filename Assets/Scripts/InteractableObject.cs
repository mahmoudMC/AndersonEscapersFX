using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject highlightObjectTemplate;
    private GameObject highlightObject;
    private bool wasInteracted = false;
    private void Awake() {
        PlayerInteraction.OnNewInteractableSet += OnNewInteractableSet;
        gameObject.layer = LayerMask.NameToLayer("Interactable");

        // Creating the highlight Object
        SpawningHightlightObject();
    }

    private void SpawningHightlightObject() {
        if (highlightObjectTemplate == null) {
            Debug.LogError($"Hightlight object is null at {gameObject.name}");
            return;
        }
        highlightObject = Instantiate(highlightObjectTemplate, transform);
        highlightObject.transform.localPosition = Vector3.zero;
        highlightObject.transform.localScale = Vector3.one * 1.15f;
        highlightObject.GetComponent<SpriteRenderer>().sprite =
            GetComponent<SpriteRenderer>().sprite;
        highlightObject.SetActive(false);
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
        print("I'm in range");
        InteractionIcon.Instance.ShowUp(transform, Vector3.up);
        if (highlightObject != null) highlightObject.SetActive(true);
    }
    protected virtual void ObjectOutsideRange() {
        print("I left the range");
        InteractionIcon.Instance.Hide();
        if (highlightObject != null) highlightObject.SetActive(false);
    }
    public virtual void Interact() { }
}