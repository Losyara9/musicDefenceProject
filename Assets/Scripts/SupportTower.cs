using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SupportTower : MonoBehaviour
{
    public int percentOfGain;
    private bool isSupport = false;

    public string towerTag = "Attack";

    public float range;
    public float gainPercent = 0.05f;

    public void GetInfo(float _range)
    {
        range = _range;
    }


    void Update()
    {
        if (gameObject.tag == "Support")
        {
            GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
            foreach (GameObject tower in towers)
            {
                float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
                if (distanceToTower <= range && tower.tag == towerTag && !isSupport)
                {
                    GameObject sTower = tower.gameObject;
                    Tower supportedTower = sTower.GetComponent<Tower>();
                    supportedTower.Support();
                    isSupport = true;
                }
            }
        }
    }
}
