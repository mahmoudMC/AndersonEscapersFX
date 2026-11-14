using DG.Tweening;
using UnityEngine;

public class TweeningAnimation : MonoBehaviour
{
    private enum AnimationType {
        None,
        Floating
    }

    [SerializeField] private AnimationType animationType;
    [SerializeField] private float animationTime = 0.25f;
    [Header("Floating")]
    [SerializeField] private float floatingYValue = 1;
    private Tween currentTween;

    private void OnEnable() {
        switch (animationType) {
            case AnimationType.Floating: Float(); break;
        }
    }
    private void Float() {
        currentTween = transform.DOMoveY(transform.position.y + floatingYValue, animationTime)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable() {
        if (currentTween != null) currentTween.Kill();
    }
}
