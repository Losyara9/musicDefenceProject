using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    private void Awake ()
    {
        points = new Transform[transform.childCount]; // ��������� ������ waypoint-��� ������ �� �� ���-�� � ������������ �������
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
