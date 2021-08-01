using UnityEngine;

namespace Ninito.UsualSuspects.Interactable
{
    /// <summary>
    ///     An interface that defines an object that can interact with interactables
    /// </summary>
    public interface IInteractor
    {
        public GameObject GameObject { get; }
    }
}