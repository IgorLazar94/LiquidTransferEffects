using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StorageTank : MonoBehaviour
{
    public static System.Action<StorageTank> OnEmergencyStopWater;
    public bool DrainIsOpen { get; private set; }
    public bool IsActiveWaterSupply { get; private set; }

    [SerializeField] private Transform leftDrain, rightDrain;
    [SerializeField] private GameObject liquidSpawner;
    [SerializeField] private GameObject liquidContainer;
    [SerializeField] private LiquidElement liquidElementPrefab;
    [SerializeField] private TextMeshProUGUI capacityText;
    [SerializeField] private PoolController liquidPoolController;
    private List<LiquidElement> liquidInTank = new List<LiquidElement>();
    private float timeToOpenDrain = 0.5f;
    private float openDrainPos = 5f;
    private float closeDrainPos = 1.5f;
    private int maxLiquidSize = 700;
    private bool cardIsActive;
    private int calculatedValueCapacity;
    [SerializeField] private float intensity = 1.0f;  // 0.01 - 0.1  (0.05)

    private void Start()
    {
        cardIsActive = false;
        DrainIsOpen = false;
        IsActiveWaterSupply = false;
    }

    private void OpenDrain()
    {
        rightDrain.DOLocalMoveX(openDrainPos, timeToOpenDrain);
        leftDrain.DOLocalMoveX(-openDrainPos, timeToOpenDrain);
    }

    private void CloseDrain()
    {
        rightDrain.DOLocalMoveX(closeDrainPos, timeToOpenDrain);
        leftDrain.DOLocalMoveX(-closeDrainPos, timeToOpenDrain);
    }

    public void SwitchDrainState()
    {
        if (!DrainIsOpen)
        {
            OpenDrain();
            DrainIsOpen = true;
        }
        else
        {
            CloseDrain();
            DrainIsOpen = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenDrain();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CloseDrain();
        }
        if (cardIsActive)
        {
            UpdateCapacityText();
        }
    }

    private IEnumerator SpawnWaterPeriodically()
    {
        while (IsActiveWaterSupply)
        {
            AudioManager.instance.PlaySFX(AudioCollection.Waterfall, true);
            AddNewWater();
            yield return new WaitForSeconds(intensity);
        }
    }

    private void AddNewWater()
    {
        WaterLimit();

        float[] spawnOffsets = { -0.5f, 0f, 0.5f };

        foreach (float offset in spawnOffsets)
        {
            Vector3 spawnPosition = new Vector3(liquidSpawner.transform.position.x + offset, liquidSpawner.transform.position.y, liquidSpawner.transform.position.z);
            //var liquid = Instantiate(liquidElementPrefab, spawnPosition, Quaternion.identity, liquidContainer.transform); // ObjectPool!
            var liquid = liquidPoolController.CreateLiquidElement();
            liquid.transform.position = spawnPosition;
            AddLiquidToList(liquid);
            liquid.SetTank(this);
        }
    }

    public bool WaterLimit()
    {
        if (liquidInTank.Count >= maxLiquidSize)
        {
            IsActiveWaterSupply = false;
            OnEmergencyStopWater?.Invoke(this);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActivateWaterSupply(bool isActivate)
    {
        IsActiveWaterSupply = isActivate;
        if (isActivate)
        {
            StartCoroutine(SpawnWaterPeriodically());
        }
        else
        {
            AudioManager.instance.StopSFX(AudioCollection.Waterfall);
        }
    }

    private void OnMouseDown()
    {
        cardIsActive = !cardIsActive;
        ShowCapacityText();
    }

    private void ShowCapacityText()
    {
        if (cardIsActive)
        {
            capacityText.transform.DOScale(Vector3.one, 0.5f);
            UpdateCapacityText();
        }
        else
        {
            capacityText.transform.DOScale(Vector3.zero, 0.5f);
        }
    }

    private void UpdateCapacityText()
    {
        calculatedValueCapacity = Mathf.FloorToInt(((float)liquidInTank.Count / maxLiquidSize) * 100);
        string message = "Capacity:\n" + calculatedValueCapacity.ToString() + "%";
        capacityText.text = message;
    }

    public void AddLiquidToList(LiquidElement liquidElement)
    {
        liquidInTank.Add(liquidElement);
    }

    public void RemoveLiquidFromList(LiquidElement liquidElement)
    {
        liquidInTank.Remove(liquidElement);
    }
}
