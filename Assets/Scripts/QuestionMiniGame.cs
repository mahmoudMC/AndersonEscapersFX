using UnityEngine;
using UnityEngine.Events;

public class QuestionMiniGame : MonoBehaviour
{
    private int currentPage = 1;
    [SerializeField] private GameObject[] pages;
    [SerializeField] private int[] correctIndexs;
    [SerializeField] private UnityEvent OnComplete;
    private UIFeedback feedback;

    private void Awake() {
        feedback = GetComponent<UIFeedback>();
    }

    public void NextPage() {
        if (currentPage == pages.Length - 1) {
            gameObject.SetActive(false);
            OnComplete?.Invoke();
            return;
        }
        pages[currentPage].SetActive(false);
        currentPage++;
        pages[currentPage].SetActive(true);
    }
    public void OpenQuestion() {
        pages[0].SetActive(false);
        pages[currentPage].SetActive(true);
    }
    public void OpenDocument() {
        pages[currentPage].SetActive(false);
        pages[0].SetActive(true);
    }
    public void CheckAnswer(int index) {
        if (correctIndexs[currentPage] == index) {
            feedback.CorrectFeedback();
            Invoke(nameof(NextPage), 2.5f);
        } else {
            feedback.WrongFeedback();
        }
    }
}
