using System;
using UnityEngine;
using UnityEngine.UI;

public class ErrorWindowView : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Button gotItButton;

    private ErrorWindowPresenter errorWindowPresenter;
    private Action onGotItButtonClickedAction;

    public void Init(ErrorWindowPresenter errorWindowPresenter, Action onGotItButtonClickedAction)
    {
        this.errorWindowPresenter = errorWindowPresenter;
        this.onGotItButtonClickedAction = onGotItButtonClickedAction;
    }

    private void Start()
    {
        canvas.SetActive(false);
        gotItButton.onClick.AddListener(OnGotItButtonClicked);
    }

    public void ShowErrorPanel()
    {
        canvas.SetActive(true);
    }

    public void HideErrorPanel()
    {
        canvas.SetActive(false);
    }

    private void OnGotItButtonClicked()
    {
        canvas.SetActive(false);
        errorWindowPresenter.OnGotItButtonClicked();
        onGotItButtonClickedAction?.Invoke();
    }
}