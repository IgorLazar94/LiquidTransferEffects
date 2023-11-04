using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image closeValveButton, openValveButton;
    [SerializeField] private Image drainLeftButton, drainRightButton;
    [SerializeField] private Image liquidLeftButton, liquidRightButton;
    [SerializeField] private Image emergencyLeftImage, emergencyRightImage;
    [SerializeField] private Image emergencyImage;
    [SerializeField] private Pipe pipe;
    [SerializeField] private StorageTank storageTankLeft;
    [SerializeField] private StorageTank storageTankRight;

    private void Start()
    {
        SwitchValveButtonsColor(false);
    }

    private void OnEnable()
    {
        StorageTank.OnEmergencyStopWater += EmergencyDisableWaterSupply;
    }

    private void OnDisable()
    {
        StorageTank.OnEmergencyStopWater -= EmergencyDisableWaterSupply;
    }

    public void OpenPipe(bool isOpen)
    {
        AudioManager.instance.PlaySFX(AudioCollection.PressedButton, false);
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
        AudioManager.instance.PlaySFX(AudioCollection.PressedButton, false);
        AudioManager.instance.PlaySFX(AudioCollection.Valve, false);
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
        AudioManager.instance.PlaySFX(AudioCollection.PressedButton, false);
        AudioManager.instance.PlaySFX(AudioCollection.Valve, false);
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

    public void EnableWaterSupplyRight()
    {
        AudioManager.instance.PlaySFX(AudioCollection.PressedButton, false);
        if (!storageTankRight.IsActiveWaterSupply)
        {
            storageTankRight.ActivateWaterSupply(true);
            liquidRightButton.color = Color.green;
        }
        else
        {
            storageTankRight.ActivateWaterSupply(false);
            liquidRightButton.color = Color.white;
        }
    }

    public void EnableWaterSupplyLeft()
    {
        AudioManager.instance.PlaySFX(AudioCollection.PressedButton, false);
        if (!storageTankLeft.IsActiveWaterSupply)
        {
            storageTankLeft.ActivateWaterSupply(true);
            liquidLeftButton.color = Color.green;
        }
        else
        {
            storageTankLeft.ActivateWaterSupply(false);
            liquidLeftButton.color = Color.white;
        }
    }

    private void EmergencyDisableWaterSupply(StorageTank storageTank)
    {
        if (storageTankLeft == storageTank)
        {
            liquidLeftButton.color = Color.white;
            StartCoroutine(ShowEmergencyImage(emergencyLeftImage));
        }
        else if (storageTankRight == storageTank)
        {
            liquidRightButton.color = Color.white;
            StartCoroutine(ShowEmergencyImage(emergencyRightImage));
        }
    }

    private IEnumerator ShowEmergencyImage(Image emergencyImage)
    {
        AudioManager.instance.PlaySFX(AudioCollection.Attention, false);
        emergencyImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        emergencyImage.gameObject.SetActive(false);
    }
}
