using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 5;
    private Vector3 moveDirection = Vector3.zero;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector2 moveDireNormalized = PlayerInput();
        moveDirection = new Vector3(moveDireNormalized.x, moveDireNormalized.y, 0);
        moveDirection *= speed;
    }

    private void FixedUpdate() {
        rb.linearVelocity = moveDirection;
    }

    private Vector2 PlayerInput() {
        Vector2 moveDir = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) moveDir.y = 1;
        if (Input.GetKey(KeyCode.S)) moveDir.y = -1;
        if (Input.GetKey(KeyCode.D)) moveDir.x = 1;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1;
        changeDirection(moveDir.x);
        return moveDir.normalized;
    }
    private void changeDirection(float xValue) {
        if (xValue == 0) return;
        if (xValue == 1) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);
    }
    public bool isSide() {
        return moveDirection.x != 0;
    }
    public bool isUp() {
        return moveDirection.y > 0;
    }
    public bool isDown() {
        return moveDirection.y < 0;
    }

    public bool isWalking() {
        return moveDirection != Vector3.zero;
    }
}
