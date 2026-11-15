using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIFeedback : MonoBehaviour
{
    [SerializeField] private Image[] images;

    public void CorrectFeedback() {
        FlashingColor(Color.green);
    }
    public void WrongFeedback() {
        FlashingColor(Color.red);
    }
    private void FlashingColor(Color color) {
        if (images == null) return;
        foreach (Image img in images) {
            img.DOColor(color, 1).SetEase(Ease.InOutFlash, 6);
        }
    }
}
