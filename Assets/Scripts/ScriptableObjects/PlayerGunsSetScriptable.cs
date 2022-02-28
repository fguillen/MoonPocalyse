using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;

[CreateAssetMenu]
public class PlayerGunsSetScriptable : RuntimeSetScriptable<GunController>
{
    public GunController ByName(string name)
    {
        return Items.Find( gunController => gunController.gunData.name == name );
    }

    public List<GunController> NoUpgradable()
    {
        return Items.Where( gunController => gunController.level >= gunController.gunData.levels.Count()).ToList();
    }

    public List<GunController> Upgradable()
    {
        return Items.Where( gunController => gunController.level < gunController.gunData.levels.Count()).ToList();
    }

    public void UpgradeGun(GunController gunController)
    {
        gunController.Upgrade();
    }

    void OnEnable()
    {
        Items.Clear();
    }
}
