using JetBrains.Annotations;
using Ninito.TheUsualSuspects.Attributes;
using Ninito.UsualSuspects;
using Ninito.UsualSuspects.Attributes;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Ninito.TheUsualSuspects.Samples
{
    public sealed class AttributeExamples : MonoBehaviour
    {
        [Header("Show If Examples")]
        [SerializeField]
        private bool showNumber;

        [SerializeField]
        [ShowIf(nameof(showNumber))]
        [UsedImplicitly]
#pragma warning disable CS0414
        private int number = 10;
#pragma warning restore

        [Header("Require Interface Examples")]
        [SerializeField]
        [RequireInterface(typeof(IInteractable))]
        private Object interfaceImplementer;

        // ReSharper disable once SuspiciousTypeConversion.Global
        [UsedImplicitly]
        private IInteractable Interface => (IInteractable)interfaceImplementer;

#pragma warning disable CS0414
        [Header("Readonly Examples")]
        [SerializeField]
        [ReadOnlyField]
        private int readonlyNumber = 10;
#pragma warning restore


        [Header("Require Examples")]
        [SerializeField]
        [RequireField]
        private GameObject errorIfNotFilled;

        [SerializeField]
        [RequireField(RequireFieldAttribute.RequiredFieldType.Warning)]
        private GameObject warningIfNotFilled;

        [SerializeField]
        [RequireField(RequireFieldAttribute.RequiredFieldType.Exception)]
        private GameObject exceptionIfNotFilled;

        [Header("Serialized Dictionary Examples")]
        [SerializeField]
        private SerializedDictionary<string, GameObject> serializedDictionary =
            new SerializedDictionary<string, GameObject>();

        [Header("Min Max Slider Examples")]
        [SerializeField]
        [MinMaxSlider(0f, 100f)]
        private Vector2 slider;
    }
}