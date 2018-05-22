
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
    public int koefEnergyHouse = 1;
    public int koefEnergyFortress = 2;
    private int currentScoreAcc;

    private float nightTimer = 0;

    private ScoreManager scoreManager;
    public GameObject moon;

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
        if (woodKoef == 0) woodKoef = 1;
        if (stoneKoef == 0) stoneKoef = 1;
        if (foodKoef == 0) foodKoef = 1;
        if (oneScorePointValue == 0) oneScorePointValue = 1;
    }
	
	// Update is called once per frame
	void Update () {
        woodtext.text = wood.ToString() + "\n/" + storage.ToString();
        stonetext.text = stone.ToString() + "\n/" + storage.ToString();
        foodtext.text = food.ToString() + "\n/" + storage.ToString();

        if(moon.transform.position.y > 0) {

            if(nightTimer > 10) {
                DecreaseResources(Resources.Wood, koefEnergyHouse * StatisticsManager.CivilianBuildingCount + koefEnergyFortress * StatisticsManager.FortressCount);
                nightTimer = 0;
            }

            nightTimer += Time.deltaTime;
        } else {
            nightTimer = 0;
        }
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
                if (wood < 0) wood = 0;
                if(amount > 0) currentScoreAcc += amount * woodKoef;
                break;
            case Resources.Food:
                food += amount;
                if (amount > 0) currentScoreAcc += amount * foodKoef;
                if (food < 0) food = 0;
                break;
            case Resources.Stone:
                stone += amount;
                if (amount > 0) currentScoreAcc += amount * stoneKoef;
                if (stone < 0) stone = 0;
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
