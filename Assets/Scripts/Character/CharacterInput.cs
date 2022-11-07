using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    [SerializeField]
    private InputDataBaseSO _inputDataBaseSO = null;

    private void Start() => GetComponent<CharacterEvent>().RegistrationToDictionary(_inputDataBaseSO);

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    string actionName = _inputDataBaseSO.GetInputData(keyCode);
                    GetComponent<CharacterEvent>().EventTrigger(actionName);
                }
            }
        }
    }
}
