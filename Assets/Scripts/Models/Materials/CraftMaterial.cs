using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ERarity {
    Junk,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Artifact,
}

public class CraftMaterial {

    public static readonly Dictionary<ERarity, Color> RarityColor = new Dictionary<ERarity, Color>() {
        { ERarity.Junk, Color.grey },
        { ERarity.Common, Color.white },
        { ERarity.Uncommon, Color.green },
        { ERarity.Rare, new Color(0f, 0.5f, 1f) },
        { ERarity.Epic, new Color(0.5f, 0, 1) },
        { ERarity.Legendary, new Color(1, 0.5f, 0) },
        { ERarity.Artifact, Color.red },
    };

    public static readonly Dictionary<ERarity, string> RarityName = new Dictionary<ERarity, string>() {
        { ERarity.Junk, "Junk" },
        { ERarity.Common, "Common" },
        { ERarity.Uncommon, "Uncommon" },
        { ERarity.Rare, "Rare" },
        { ERarity.Epic, "Epic" },
        { ERarity.Legendary, "Legendary" },
        { ERarity.Artifact, "Artifact" },
    };

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Ilvl { get; set; }
    public ERarity Rarity { get; set; }
    public int BasePrice { get; set; }
    public int IconId { get; set; }

    public CraftMaterial Clone() {
        return (Clone(this));
    }

    public static CraftMaterial Clone(CraftMaterial material) {
        CraftMaterial clone = new CraftMaterial();

        clone.Id = material.Id;
        clone.Name = material.Name;
        clone.Description = material.Description;
        clone.Ilvl = material.Ilvl;
        clone.Rarity = material.Rarity;
        clone.BasePrice = material.BasePrice;
        clone.IconId = material.IconId;

        return (clone);
    }
}