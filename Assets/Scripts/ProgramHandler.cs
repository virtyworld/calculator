using System;
using UnityEngine;

public class ProgramHandler : MonoBehaviour
{
   [SerializeField] private CalculatorView calculatorView;
   [SerializeField] private FormWindowView formWindowView;
   [SerializeField] private ErrorWindowView errorWindowView;

   private CalculatorModel calculatorModel;
   private CalculatorPresenter calculatorPresenter;
   private ErrorWindowModel errorWindowModel;
   private ErrorWindowPresenter errorWindowPresenter;
   private FormWindowModel formWindowModel;
   private FormWindowPresenter formWindowPresenter;
   private Action<string,bool> onClickResultButtonAction;
   private Action <string> onCalculatedAction;
   private Action  onGotItButtonClickedAction;
   
   private void Awake()
   {
      onClickResultButtonAction += ClickResultButtonAction;
      onCalculatedAction += WasCalculated;
      onGotItButtonClickedAction += GotItButtonClickedAction;
      
      InitCalculator();
      InitErrorWindow();
      InitFormWindow();
   }

   private void InitCalculator()
   {
      calculatorModel = new CalculatorModel();
      calculatorModel.Init(calculatorView);
      calculatorPresenter = new CalculatorPresenter(calculatorModel);
      calculatorView.Init(calculatorPresenter,onCalculatedAction);
   }

   private void InitErrorWindow()
   {
       errorWindowModel = new ErrorWindowModel();
       errorWindowPresenter = new ErrorWindowPresenter(errorWindowModel);
       errorWindowView.Init(errorWindowPresenter,onGotItButtonClickedAction);
   }

   private void InitFormWindow()
   {
      formWindowModel = new FormWindowModel(formWindowView);
      formWindowPresenter = new FormWindowPresenter(formWindowModel,formWindowView);
      formWindowView.Init(formWindowPresenter,onClickResultButtonAction);
   }

   private void ClickResultButtonAction(string value, bool isValid)
   {
      if (isValid) calculatorView.Calculate(value);
      else
      {
         formWindowView.HideFormPanel();
         errorWindowView.ShowErrorPanel();
      }
   }

   private void WasCalculated(string value)
   {
      formWindowView.InitNewResultObject(value);
      formWindowView.SetResultFromCalculator(value);
    
      if (value.Contains("error"))
      {
         formWindowView.HideFormPanel();
         errorWindowView.ShowErrorPanel();
      }
      else
      {
         formWindowView.ShowFormPanel();
         errorWindowView.HideErrorPanel();
      }
   }

   private void GotItButtonClickedAction()
   {
      formWindowView.ShowFormPanel();
      errorWindowView.HideErrorPanel();
   }
}
