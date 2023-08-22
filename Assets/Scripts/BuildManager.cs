using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private TowerBlueprint towerToBuild;
    private Stage selectedStage;

    public StageUI stageUI;

    public bool CanBuild 
    {
        get
        { 
            return towerToBuild != null; 
        }
    }

    public bool HasMoney
    {
        get
        {
            return PlayerStats.Money >= towerToBuild.cost;
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void SelectStage(Stage stage)
    {
        if (selectedStage == stage)
        {
            DeselectStage();
            return;
        }

        selectedStage = stage;
        towerToBuild = null;

        stageUI.SetTarget(stage);
    }

    public void DeselectStage()
    {
        selectedStage = null;
        stageUI.Hide();
    }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;

        DeselectStage();
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return towerToBuild;
    }
}
