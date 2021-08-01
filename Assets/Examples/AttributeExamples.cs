using Ninito.UsualSuspects;
using Ninito.UsualSuspects.Editor;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

public class AttributeExamples : MonoBehaviour
{
    [Header("Show If Examples"), SerializeField]
    private bool showNumber;

    [SerializeField, ShowIf(nameof(showNumber))]
    private int number = 10;

    [Header("Require Interface Examples"), SerializeField, RequireInterface(typeof(IInteractable))]
    private Object interfaceImplementer;
    
    // ReSharper disable once SuspiciousTypeConversion.Global
    private IInteractable Interface => (IInteractable) interfaceImplementer;

    [Header("Readonly Examples"), SerializeField, ReadOnlyField]
    private int readonlyNumber = 10;

    [Header("Require Examples"), SerializeField, RequireField]
    private GameObject errorIfNotFilled;
    [SerializeField, RequireField(RequireFieldAttribute.RequiredFieldType.Warning)]
    private GameObject warningIfNotFilled;
    [SerializeField, RequireField(RequireFieldAttribute.RequiredFieldType.Exception)]
    private GameObject exceptionIfNotFilled;

    [Header("Serialized Dictionary Examples"), SerializeField]
    private SerializedDictionary<string, GameObject> serializedDictionary =
        new SerializedDictionary<string, GameObject>();
}