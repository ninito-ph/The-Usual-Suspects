namespace Ninito.UsualSuspects.Interactable
{
    /// <summary>
    ///     An interface that defines an object that can be interacted with
    /// </summary>
    public interface IInteractable
    {
        public string InteractionToolTip { get; }
        public void InteractWithAs(IInteractor interactor);
    }
}