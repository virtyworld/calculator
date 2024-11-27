public class ErrorWindowPresenter 
{
    private ErrorWindowModel model;

    public ErrorWindowPresenter(ErrorWindowModel model)
    {
        this.model = model;
        SetBasicState();
    }

    private void SetBasicState()
    {
        OnGotItButtonClicked();
    }
    public void OnGotItButtonClicked()
    {
        model.IsActive = false;
    }
}
