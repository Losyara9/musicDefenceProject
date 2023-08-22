using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    private Stage target;

    public GameObject ui;

    public Text SellAmount;

    public void SetTarget(Stage _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        SellAmount.text = target.towerBlueprint.GetSellAmount().ToString() + "♪";

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Sell()
    {
        target.SellTower();
        BuildManager.instance.DeselectStage();
    }
}
