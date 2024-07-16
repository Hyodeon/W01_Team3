using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLoader : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject parent;
    public GameObject boundPrefab; // Bound �������� �ν����Ϳ��� �Ҵ�
    public TextAsset csvFile;      // �ν����Ϳ��� �Ҵ��� �� �ֵ��� TextAsset Ÿ������ ����
    public int boundDistance = 3;

    public Camera mainCamera;
    public float size = 5.0f; // �⺻ ��

    public GameObject player;
    public GameObject goal;

    void Start()
    {
        LoadTilesFromCSV();
        AddBounds();
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = size;
        }
    }

    void LoadTilesFromCSV()
    {
        if (csvFile == null)
        {
            Debug.LogError("CSV ������ �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // CSV ���� �б�
        string[] lines = csvFile.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        // �߾� ��ǥ ���
        int rows = lines.Length;
        int cols = lines[0].Split(',').Length;
        size = lines.Length / 2 + 2;
        float startX = -cols / 2.0f + 0.5f;
        float startY = rows / 2.0f - 0.5f;

        // CSV �����͸� �Ľ��Ͽ� Ÿ�� ��ġ
        for (int y = 0; y < rows; y++)
        {
            string[] values = lines[y].Split(',');

            for (int x = 0; x < values.Length; x++)
            {
                int tileType = int.Parse(values[x]);
                Vector2 position = new Vector2(startX + x, startY - y);

                switch (tileType)
                {
                    case 1:
                        Instantiate(prefab1, position, Quaternion.identity, parent.transform);
                        break;
                    case 2:
                        Instantiate(prefab2, position, Quaternion.identity, parent.transform);
                        break;
                    case 8:
                        player.transform.position = position;
                        break;
                    case 9:
                        goal.transform.position = position;
                        break;
                }
            }
        }
    }

    void AddBounds()
    {
        if (boundPrefab == null)
        {
            Debug.LogError("Bound �������� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // CSV ���� �б�
        string[] lines = csvFile.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        // �߾� ��ǥ ���
        int rows = lines.Length;
        int cols = lines[0].Split(',').Length;
        float startX = -cols / 2.0f + 0.5f;
        float startY = rows / 2.0f - 0.5f;

        // �׵θ� �߰�
        for (int x = -boundDistance; x <= cols + boundDistance - 1; x++)
        {
            // ��� �׵θ�
            Vector2 topPosition = new Vector2(startX + x, startY + boundDistance);
            Instantiate(boundPrefab, topPosition, Quaternion.identity, parent.transform);

            // �ϴ� �׵θ�
            Vector2 bottomPosition = new Vector2(startX + x, startY - rows - boundDistance + 1);
            Instantiate(boundPrefab, bottomPosition, Quaternion.identity, parent.transform);
        }

        for (int y = -boundDistance; y < rows + boundDistance; y++)
        {
            // ���� �׵θ�
            Vector2 leftPosition = new Vector2(startX - boundDistance, startY - y);
            Instantiate(boundPrefab, leftPosition, Quaternion.identity, parent.transform);

            // ���� �׵θ�
            Vector2 rightPosition = new Vector2(startX + cols + boundDistance - 1, startY - y);
            Instantiate(boundPrefab, rightPosition, Quaternion.identity, parent.transform);
        }
    }
}
