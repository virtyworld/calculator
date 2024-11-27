using System;
using UnityEngine;

public class CalculatorView : MonoBehaviour
{
    private CalculatorPresenter presenter;
    private Action<string> onCalculatedAction;

    public void Init(CalculatorPresenter presenter, Action<string> onCalculatedAction)
    {
        this.presenter = presenter;
        this.onCalculatedAction = onCalculatedAction;
    }

    public void Calculate(string value)
    {
        presenter.Calculate(value, onCalculatedAction);
    }
}