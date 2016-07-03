using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftMaterialDatabase : Singleton<CraftMaterialDatabase> {

    public Dictionary<int, CraftMaterial> CraftMaterials = new Dictionary<int, CraftMaterial>();

    // Use this for initialization
    protected override void Init () {
        base.Init();
        LoadFromJson();
	}

    void LoadFromJson() {

        object json = Resources.Load("Data/CraftMaterialsDatabase");

        JSONObject db = new JSONObject(json.ToString());
        for (int i = 0; i < db.list.Count; ++i) {
            CraftMaterial material = CraftMaterialFromJson(db.list[i]);
            if (CraftMaterials.ContainsKey(material.Id)) {
                Debug.LogError("CraftMaterialsDatabase conflict : two assets have the same ID " + material.Id);
                break;
            }
            CraftMaterials[material.Id] = material;
        }
    }

    CraftMaterial CraftMaterialFromJson(JSONObject obj) {
        CraftMaterial material = new CraftMaterial();

        material.Id = (int)obj.GetField("id").i;
        material.Name = obj.GetField("name").str;
        material.Description = obj.GetField("description").str;
        material.Ilvl = (int)obj.GetField("ilvl").i;
        material.Rarity = (ERarity)obj.GetField("rarity").i;
        material.BasePrice = (int)obj.GetField("basePrice").i;
        material.IconId = (int)obj.GetField("iconId").i;
        return (material);
    }
}
