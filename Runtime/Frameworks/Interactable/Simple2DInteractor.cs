using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ninito.UsualSuspects.Volumes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ninito.UsualSuspects.Interactable
{
    /// <summary>
    ///     A simple class that provides basic interaction functionality
    /// </summary>
    public class Simple2DInteractor : MonoBehaviour, IInteractor
    {
        #region Private Fields

        [SerializeField]
        [Header("Interaction Volume")]
        private ContainerArea interactionArea;

        [FormerlySerializedAs("interactionTooltipComponent")]
        [SerializeField]
        private TMP_Text interactionTooltipText;

        [SerializeField]
        private float periodicTooltipUpdateInterval = 1.5f;

        private Coroutine _periodicTooltipUpdateRoutine;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            interactionArea.ColliderEnteredVolume += UpdateInteractionTooltip;
            interactionArea.ColliderLeftVolume += UpdateInteractionTooltip;

            _periodicTooltipUpdateRoutine = StartCoroutine(PeriodicTooltipUpdate());
        }

        #endregion

        #region Public Methods

        public void OnInteract()
        {
            IInteractable firstOrDefault = GetInteractablesInVolume().FirstOrDefault();
            firstOrDefault?.InteractWithAs(this);
        }

        #endregion

        #region Protected Methods

        protected virtual void UpdateInteractionTooltip()
        {
            IInteractable interactable = GetInteractableInVolume();

            interactionTooltipText.text =
                interactable == null ? String.Empty : interactable.InteractionToolTip;
        }
        
        protected IInteractable GetInteractableInVolume()
        {
            return GetInteractablesInVolume().FirstOrDefault();
        }
        
        protected IEnumerable<IInteractable> GetInteractablesInVolume()
        {
            return interactionArea.GetComponentsInVolume<IInteractable>();
        }

        #endregion

        #region Private Methods

        private IEnumerator PeriodicTooltipUpdate()
        {
            WaitForSeconds wait = new WaitForSeconds(periodicTooltipUpdateInterval);
            
            // Profile to see if this is a memory leak. I think it might be a very, very slow albeit existing one.
            while (true)
            {
                UpdateInteractionTooltip();
                yield return wait;
            }
        }

        #endregion

        #region IInteractor Implementation

        public GameObject GameObject => gameObject;

        #endregion
    }
}