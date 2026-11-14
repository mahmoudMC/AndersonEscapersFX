using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string KEY_WALK = "walk";
    private Animator animator;
    [SerializeField] private PlayerMovement pm;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetInteger(KEY_WALK, pm.isWalking() ? 1 : 0);
    }
}
