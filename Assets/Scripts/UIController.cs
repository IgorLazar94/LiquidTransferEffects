using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image closeValveButton, openValveButton;
    [SerializeField] private Image drainLeftButton, drainRightButton;
    [SerializeField] private Pipe pipe;
    [SerializeField] private StorageTank storageTankLeft;
    [SerializeField] private StorageTank storageTankRight;

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

    public void OpenLeftDrain()
    {
        Debug.Log(storageTankLeft.DrainIsOpen + " is open");
        storageTankLeft.SwitchDrainState();
        if (!storageTankLeft.DrainIsOpen)
        {
            drainLeftButton.color = Color.white;
        }
        else
        {
            drainLeftButton.color = Color.red;
        }
    }

    public void OpenRightDrain()
    {
        storageTankRight.SwitchDrainState();
        if (!storageTankRight.DrainIsOpen)
        {
            drainRightButton.color = Color.white;
        }
        else
        {
            drainRightButton.color = Color.red;
        }
    }
}
