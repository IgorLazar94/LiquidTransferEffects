using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class StorageTank : MonoBehaviour
{
    public static System.Action<StorageTank> OnEmergencyStopWater;
    public bool DrainIsOpen { get; private set; }
    public bool IsActiveWaterSupply { get; private set; }

    [SerializeField] private Transform leftDrain;
    [SerializeField] private Transform rightDrain;
    [SerializeField] private GameObject liquidSpawner;
    [SerializeField] private GameObject liquidContainer;
    [SerializeField] private LiquidElement liquidElementPrefab;
    private List<LiquidElement> liquidInTank = new List<LiquidElement>();
    private float timeToOpenDrain = 0.5f;
    private float openDrainPos = 5f;
    private float closeDrainPos = 1.5f;
    private int waterCounter;
    private int maxLiquidSize = 700;

    private void Start()
    {
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


    }

    private void FixedUpdate()
    {
        if (IsActiveWaterSupply)
        {
            AddNewWater();
        }
    }

    private void AddNewWater()
    {
        if (liquidInTank.Count >= maxLiquidSize)
        {
            IsActiveWaterSupply = false;
            OnEmergencyStopWater?.Invoke(this);
        }
        var liquid =Instantiate(liquidElementPrefab, liquidSpawner.transform.position, Quaternion.identity, liquidContainer.transform); // ObjectPool!
        liquidInTank.Add(liquid);
        liquid.SetTank(this);
    }

    public void RemoveLiquid(LiquidElement liquidElement)
    {
        liquidInTank.Remove(liquidElement);
    }

    public void ActivateWaterSupply(bool isActivate)
    {
        IsActiveWaterSupply = isActivate;
    }
}
