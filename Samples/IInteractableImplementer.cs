using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Examples
{
    public class IInteractableImplementer : MonoBehaviour, IInteractable
    {
        public string InteractionToolTip { get; }
        
        public void InteractWithAs(IInteractor interactor)
        {
            throw new System.NotImplementedException();
        }
    }
}