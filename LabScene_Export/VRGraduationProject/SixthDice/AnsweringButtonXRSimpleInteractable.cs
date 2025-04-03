using UnityEngine.XR.Interaction.Toolkit;

public class AnsweringButtonXRSimpleInteractable : XRSimpleInteractable
{
    private void Start()
    {
        ButtonForIdetificationPurpleCabbage.Instance.OnClickButton += Handle_AnswerButtonActive;

        GetComponent<XRSimpleInteractable>().enabled = false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        WallQuiz.Instance.Answering(args.interactableObject.transform.name);

        base.OnSelectEntered(args);
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        SixthDicePokeButtonText.Instance.DisplayPokeButtonText(args.interactableObject.transform.name);

        base.OnHoverEntered(args);
    }

    private void Handle_AnswerButtonActive()
    {
        GetComponent<XRSimpleInteractable>().enabled = true;
    }
}
