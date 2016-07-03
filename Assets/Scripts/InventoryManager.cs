using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnGoldChangedEvent : GameEvent {}
public class OnInventoryCraftMaterialsChangedEvent : GameEvent {}

public class InventoryManager : Singleton<InventoryManager> {

    private uint _golds = 100;
    public uint Golds {
        get { return (_golds); }
        private set {
            _golds = value;
            Events.Instance.Raise(new OnGoldChangedEvent());
            Save();
        }
    }

    public Dictionary<int, uint> CraftMaterials = new Dictionary<int, uint>();

    protected override void Init() {
        base.Init();
        LoadSavedDatas();
    }

    void LoadSavedDatas() {
        foreach (KeyValuePair<int, CraftMaterial> pair in CraftMaterialDatabase.Instance.CraftMaterials) {
            if (PlayerPrefs.HasKey("Smithing_CraftMat_" + pair.Value.Id) && PlayerPrefs.GetInt("Smithing_CraftMat_" + pair.Value.Id) >= 0) {
                CraftMaterials[pair.Value.Id] = (uint)PlayerPrefs.GetInt("Smithing_CraftMat_" + pair.Value.Id);
            } else {
                CraftMaterials[pair.Value.Id] = 0;
            }
        }
        Events.Instance.Raise(new OnInventoryCraftMaterialsChangedEvent());
        if (PlayerPrefs.HasKey("Smithing_Golds"))
            Golds = (uint)PlayerPrefs.GetInt("Smithing_Golds");
        Events.Instance.Raise(new OnGoldChangedEvent());
    }

    void Save() {
        foreach (KeyValuePair<int, CraftMaterial> pair in CraftMaterialDatabase.Instance.CraftMaterials) {
            if (CraftMaterials.ContainsKey(pair.Value.Id)) {
                PlayerPrefs.SetInt("Smithing_CraftMat_" + pair.Value.Id, (int)CraftMaterials[pair.Value.Id]);
            }
        }
        PlayerPrefs.SetInt("Smithing_Golds", (int)Golds);
    }

    public void AddCraftMaterials(int materialId, uint amount) {
        CraftMaterials[materialId] += amount;
        Events.Instance.Raise(new OnInventoryCraftMaterialsChangedEvent());
        Save();
    }

    public bool UseCraftMaterials(int materialId, uint amount) {
        if (CraftMaterials[materialId] >= amount) {
            CraftMaterials[materialId] -= amount;
            Events.Instance.Raise(new OnInventoryCraftMaterialsChangedEvent());
            return (true);
        }
        return (false);
    }

    public void AddGold(uint amount) {
        Golds += amount;
    }

    public bool UseGold(uint amount) {
        if (Golds >= amount) {
            Golds -= amount;
            return (true);
        }
        return (false);
    }
}
