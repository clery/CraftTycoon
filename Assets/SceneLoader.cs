using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    public class Scenes {
        public const string Market = "Market";
        public const string Inventory = "Inventory";
        public const string Foundry = "Foundry";
    }

    public void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadMarket() {
        LoadScene(Scenes.Market);
    }

    public void LoadInventory() {
        LoadScene(Scenes.Inventory);
    }

    public void LoadFoundry() {
        LoadScene(Scenes.Foundry);
    }
}
