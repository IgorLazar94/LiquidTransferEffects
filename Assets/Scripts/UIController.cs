using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Pipe pipe;
    [SerializeField] private Image closeValveButton, openValveButton;

    private void Start()
    {
        SwitchValveButtonsColor(false);
    }

    public void OpenPipe(bool isOpen)
    {
        pipe.ActivateValve(isOpen);
        SwitchValveButtonsColor(isOpen);
    }

    private void SwitchValveButtonsColor(bool isOpen)
    {
        if (isOpen)
        {
            openValveButton.color = Color.green;
            closeValveButton.color = Color.white;
        }
        else
        {
            openValveButton.color = Color.white;
            closeValveButton.color = Color.green;
        }
    }
}
