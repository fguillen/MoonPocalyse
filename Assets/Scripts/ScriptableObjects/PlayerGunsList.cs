using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;

// [FilePath("Assets/Data/Player/PlayerGunsList.asset", FilePathAttribute.Location.PreferencesFolder)]
[CreateAssetMenu(fileName = "New PlayerGunsList", menuName = "PlayerGunsList")]
public class PlayerGunsList : SingletonScriptableObject<PlayerGunsList>
{
    public List<GunController> guns = new List<GunController>();
    public UnityEvent onListChanged = new UnityEvent();

    public void Add(GunController gunController)
    {
        guns.Add(gunController);
        onListChanged.Invoke();
    }

    public void Remove(GunController gunController)
    {
        guns.Remove(gunController);
        onListChanged.Invoke();
    }

    public GunController ByName(string name)
    {
        return guns.Find( gunController => gunController.gunData.name == name );
    }

    public List<GunController> NoUpgradable()
    {
        return guns.Where( gunController => gunController.level >= gunController.gunData.levels.Count()).ToList();
    }

    public List<GunController> Upgradable()
    {
        return guns.Where( gunController => gunController.level < gunController.gunData.levels.Count()).ToList();
    }

    public void UpgradeGun(GunController gunController)
    {
        gunController.Upgrade();
    }

    void OnEnable()
    {
        guns.Clear();
    }
}
