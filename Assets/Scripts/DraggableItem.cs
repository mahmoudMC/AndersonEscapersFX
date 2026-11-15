using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IDragHandler, IEndDragHandler {

    private static int completedShaped = 0;

    [SerializeField] private UIFeedback feedback;
    [SerializeField] private Transform target;
    [SerializeField] private string objectName;
    [SerializeField] private TextMeshProUGUI objectText;
    [SerializeField] private UnityEvent OnComplete;
    private Vector3 originalPosition;
    private bool canDrag = true;

    private void OnEnable() {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData) {
        if (!canDrag) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!canDrag) return;
        if (InRage()) {
            canDrag = false;
            transform.position = target.position;
            objectText.SetText(objectName);
            feedback.CorrectFeedback();
            completedShaped++;
            if (completedShaped == 3) {
                Invoke(nameof(Completing), 2);
            }
        } else {
            transform.position = originalPosition;
            feedback.WrongFeedback();
        }
    }
    private void Completing() {
        OnComplete?.Invoke();
    }
    private bool InRage() {
        return Vector3.Distance(transform.position, target.position) <= 70;
    }

    private void OnDestroy() {
        completedShaped = 0;
    }
}
