using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint guitarTower;
    public TowerBlueprint drummerTower;
    public TowerBlueprint keyboardTower;
    public TowerBlueprint djTower;


    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectGuitarTower()
    {
        buildManager.SelectTowerToBuild(guitarTower);
    }
    
    public void SelectDrummerTower()
    {
        buildManager.SelectTowerToBuild(drummerTower);
    }

    public void SelectKeyboardTower()
    {
        buildManager.SelectTowerToBuild(keyboardTower);
    }

    public void SelectDjTower()
    {
        buildManager.SelectTowerToBuild(djTower);
    }
}
