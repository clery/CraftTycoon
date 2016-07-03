using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarketManager : MonoSingleton<MarketManager> {

    [SerializeField]
    private UIMaterialBuyBox _materialBuyBoxPrefab;
    [SerializeField]
    private Transform _materialContainer;

    public Dictionary<int, int> Multipliers = new Dictionary<int, int>();

	// Use this for initialization
	protected override void Init () {
        base.Init();
        for (int i = _materialContainer.childCount - 1; i >= 0; --i)
            Destroy(_materialContainer.GetChild(i).gameObject);
        
        foreach (KeyValuePair<int, CraftMaterial> pair in CraftMaterialDatabase.Instance.CraftMaterials) {
            UIMaterialBuyBox box = Instantiate(_materialBuyBoxPrefab);
            box.transform.SetParent(_materialContainer, false);
            box.Load(pair.Value);
            Multipliers[pair.Value.Id] = 1;
        }
	}

    public void ChangeMultiplier(int id) {
        Multipliers[id] *= 10;
        if (Multipliers[id] > 100)
            Multipliers[id] = 1;
    }

    public bool Buy(int id) {
        CraftMaterial mat = CraftMaterialDatabase.Instance.CraftMaterials[id];
        Debug.Log("Bought " + Multipliers[id] + " " + mat.Name);
        if (InventoryManager.Instance.UseGold((uint)(mat.BasePrice * Multipliers[id]))) {
            InventoryManager.Instance.CraftMaterials[id] += (uint)Multipliers[id];
        }
        return (true);
    }
}
