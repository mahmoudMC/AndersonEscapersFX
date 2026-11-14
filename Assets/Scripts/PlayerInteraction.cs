using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public static event Action<IInteractable> OnNewInteractableSet;

    private float scanRadius = 2.5f;
    [SerializeField] private LayerMask interactableLayer;

    private IInteractable currentClosestInteractable;

    private void Update() {
        FindAndProcessClosestInteractable();

        if (Input.GetKeyDown(KeyCode.E) && currentClosestInteractable != null) {
            currentClosestInteractable.Interact();
        }
    }

    private void FindAndProcessClosestInteractable() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, scanRadius, interactableLayer);

        IInteractable closestInteractable = null;
        float minSqrDistance = float.MaxValue;
        Vector3 currentPosition = transform.position;

        foreach (var hit in hits) {
            // Check if the hit object implements IInteractable
            if (hit.TryGetComponent<IInteractable>(out IInteractable interactable)) {
                float sqrDistance = (hit.transform.position - currentPosition).sqrMagnitude;

                // Track the closest valid interactable
                if (sqrDistance < minSqrDistance) {
                    minSqrDistance = sqrDistance;
                    closestInteractable = interactable;
                }
            }
        }

        SetNewInteractable(closestInteractable);
    }

    private void SetNewInteractable(IInteractable interactable) {
        if (currentClosestInteractable == interactable) return;
        currentClosestInteractable = interactable;
        OnNewInteractableSet?.Invoke(interactable);
    }

    private void OnDestroy() {
        OnNewInteractableSet = null;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}

public interface IInteractable {
    public void Interact();
}