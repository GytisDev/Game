
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    private string resourcesFormat;
    public Text woodtext;
    public Text stonetext;
    public Text foodtext;

    public int woodKoef;
    public int stoneKoef;
    public int foodKoef;
    public int oneScorePointValue;
    private int currentScoreAcc;

    private ScoreManager scoreManager;

    public enum Resources {
        Wood,
        Food, 
        Stone,
        Storage
    }

    public int wood, food, stone, storage;

	// Use this for initialization
	void Start () {
        UpdateFormat();
        scoreManager = FindObjectOfType<ScoreManager>();
        currentScoreAcc = 0;
	}
	
	// Update is called once per frame
	void Update () {
        woodtext.text = wood.ToString() + "\n/" + storage.ToString();
        stonetext.text = stone.ToString() + "\n/" + storage.ToString();
        foodtext.text = food.ToString() + "\n/" + storage.ToString();

    }

    public void IncreaseResources(Resources resource, int amount) {
        AlterResources(resource, amount);
    }

    public void DecreaseResources(Resources resource, int amount) {
        AlterResources(resource, -amount);
    }

    public bool isEnough(int wood, int stone, int food) {
        return this.wood >= wood && this.stone >= stone && this.food >= food;
    }

    private void AlterResources(Resources resource, int amount) {
        switch (resource) {
            case Resources.Wood:
                wood += amount;
                if(amount > 0) currentScoreAcc += amount * woodKoef;
                break;
            case Resources.Food:
                food += amount;
                if (amount > 0) currentScoreAcc += amount * foodKoef;
                break;
            case Resources.Stone:
                stone += amount;
                if (amount > 0) currentScoreAcc += amount * stoneKoef;
                break;
            case Resources.Storage:
                storage += amount;
                break;
            default:
                return;
        }

        scoreManager.AddScore(currentScoreAcc / oneScorePointValue);
        currentScoreAcc %= oneScorePointValue;

        UpdateFormat();
    }
    
    private void UpdateFormat() {
        resourcesFormat = "Wood: " + wood + " Stone: " + stone + " Food: " + food;

    }
    /*
    private void OnGUI() {
        GUI.skin.box.fontSize = 18;
        GUI.Box(new Rect(10, 10, 250, 25), resourcesFormat);
    }*/
}
