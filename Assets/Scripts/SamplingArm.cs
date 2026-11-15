using DG.Tweening;
using HeyHeyThere.PixelUISciFiFree;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SamplingArm : MonoBehaviour
{
    [SerializeField] private UnityEvent OnMissionDone;
    [SerializeField] private Slider slider;
    [SerializeField] private PixelBar pixelbar;
    private UIFeedback feedback;
    private int maxSliderValue = 100;
    private int minRange = 35;
    private int maxRange = 65;
    private int Speed = 80;
    private int direction = 1;
    private float currentValue = 0;
    private const byte task = 3;
    private byte done = 0;
    private bool isMoving = true;

    private void Awake() {
        feedback = GetComponent<UIFeedback>();
    }
    private void OnEnable() {
        currentValue = 0;
        done = 0;
        maxSliderValue = (int)slider.maxValue;
    }
    private void Update() {
        if (!isMoving) return;
        currentValue += Time.deltaTime * Speed * direction;
        if (currentValue >= maxSliderValue) {
            direction = -1;
            currentValue = maxSliderValue - (currentValue - maxSliderValue);
        }
        if (currentValue <= 0) {
            direction = 1;
            currentValue *= -1;
        }
        slider.value = currentValue;
    }

    public void Clicked() {
        if (currentValue >= minRange && currentValue <= maxRange) {
            done++;
            SetOxygenValue(Mathf.Clamp01((float)done / task));
            feedback.CorrectFeedback();
            if (done == task) {
                isMoving = false;
                Invoke(nameof(MissionDonw), 2);
            }
        } else {
            feedback.WrongFeedback();
        }
    }
    private void MissionDonw() {
        OnMissionDone?.Invoke();
    }

    public void SetOxygenValue(float value) {
        DOVirtual.Float(pixelbar.value, value, 0.4f, val => {
            pixelbar.value = val;
        });
    }
}
