using TMPro;
using UnityEngine;

public class DialougeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private float TypingSpeedPerSecond = 15;
    private bool skipping = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            skipping = true;
        }
    }

    public async void StartTyping(string sentence) {
        skipping = false;
        nameText.SetText("Anderson");
        sentenceText.SetText(sentence);
        for (int i = 0; i < sentence.Length; i++) {
            if (skipping) break;
            sentenceText.maxVisibleCharacters = i;
            await Awaitable.WaitForSecondsAsync(1f / TypingSpeedPerSecond);
        }
        sentenceText.maxVisibleCharacters = sentence.Length;
    }
}
