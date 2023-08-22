using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Stage : MonoBehaviour
{
    public Color hoverColor;
    public Color notMoneyColor;
    public Vector3 positionOffset;

    private Renderer rend;
    private Color startColor;

    [HideInInspector]
    public TowerBlueprint towerBlueprint;

    [HideInInspector]
    public GameObject tower; 

    BuildManager buildManager;
    Tower stopSupport;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void SellTower()
    {
        PlayerStats.Money += towerBlueprint.GetSellAmount();
        if (tower.tag == "Support")
        {
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Attack");
            foreach (GameObject tower in towers)
            {
                GameObject sTower = tower.gameObject;
                Tower stopSupport = sTower.GetComponent<Tower>();
                stopSupport.StopSupport();
            }

            
        }
        Destroy(tower);
       towerBlueprint = null;
    }

    void OnMouseDown()
    {  

        if (tower != null)
        {
            buildManager.SelectStage(this);
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }


        BuildTower(buildManager.GetTowerToBuild());
    }

    void BuildTower(TowerBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            return;
        }

        PlayerStats.Money -= blueprint.cost;


        GameObject _tower = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        towerBlueprint = blueprint;
    }

    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
