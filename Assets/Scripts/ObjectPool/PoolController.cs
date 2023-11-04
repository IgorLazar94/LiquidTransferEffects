using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] private int poolCount;
    [SerializeField] private bool autoExpand;
    [SerializeField] private LiquidElement liquidElementPrefab;

    private LiquidPool<LiquidElement> pool;

    private void Start()
    {
        //GetSettingsValues();
        this.pool = new LiquidPool<LiquidElement>(liquidElementPrefab, poolCount, this.transform);
        this.pool.autoExpand = this.autoExpand;
    }
    //private void GetSettingsValues()
    //{
    //    poolCount = GameSettings.Instance.GetPoolCount();
    //    autoExpand = GameSettings.Instance.GetPoolAutoExpand();
    //    playerBulletPrefab = GameSettings.Instance.GetPlayerBulletPrefab();
    //}

    public LiquidElement CreateLiquidElement()
    {
        var bullet = this.pool.GetFreeElement();
        return bullet;
    }




}
