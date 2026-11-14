using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string KEY_WALK = "walk";
    private const string KEY_UP = "up";
    private const string KEY_DOWN = "down";
    private const string KEY_SIDE = "side";
    private Animator animator;
    [SerializeField] private PlayerMovement pm;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        //animator.SetBool(KEY_WALK, pm.isWalking());
        //animator.SetBool(KEY_UP, pm.isUp());
        //animator.SetBool(KEY_DOWN, pm.isDown());
        //animator.SetBool(KEY_SIDE, pm.isSide());
        if (pm.isWalking()) {
            animator.SetBool(KEY_WALK, true);
            if (pm.isSide()) animator.Play(KEY_SIDE);
            else if (pm.isDown()) animator.Play(KEY_DOWN);
            else animator.Play(KEY_UP);
        } else animator.SetBool(KEY_WALK, false);
    }
}
