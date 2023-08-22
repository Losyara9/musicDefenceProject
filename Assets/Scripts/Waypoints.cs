using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    private void Awake ()
    {
        points = new Transform[transform.childCount]; // заполняем массив waypoint-ами исходя из их кол-ва в родительском объекте
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
