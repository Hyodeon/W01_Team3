using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLoader : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject parent;
    public GameObject boundPrefab; // Bound 프리팹을 인스펙터에서 할당
    public TextAsset csvFile;      // 인스펙터에서 할당할 수 있도록 TextAsset 타입으로 선언

    void Start()
    {
        LoadTilesFromCSV();
        AddBounds();
    }

    void LoadTilesFromCSV()
    {
        if (csvFile == null)
        {
            Debug.LogError("CSV 파일이 할당되지 않았습니다.");
            return;
        }

        // CSV 파일 읽기
        string[] lines = csvFile.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        // 중앙 좌표 계산
        int rows = lines.Length;
        int cols = lines[0].Split(',').Length;
        float startX = -cols / 2.0f + 0.5f;
        float startY = rows / 2.0f - 0.5f;

        // CSV 데이터를 파싱하여 타일 배치
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
                }
            }
        }
    }

    void AddBounds()
    {
        if (boundPrefab == null)
        {
            Debug.LogError("Bound 프리팹이 할당되지 않았습니다.");
            return;
        }

        // CSV 파일 읽기
        string[] lines = csvFile.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        // 중앙 좌표 계산
        int rows = lines.Length;
        int cols = lines[0].Split(',').Length;
        float startX = -cols / 2.0f + 0.5f;
        float startY = rows / 2.0f - 0.5f;

        // 테두리 추가
        for (int x = -1; x <= cols; x++)
        {
            // 상단 테두리
            Vector2 topPosition = new Vector2(startX + x, startY + 1);
            Instantiate(boundPrefab, topPosition, Quaternion.identity, parent.transform);

            // 하단 테두리
            Vector2 bottomPosition = new Vector2(startX + x, startY - rows);
            Instantiate(boundPrefab, bottomPosition, Quaternion.identity, parent.transform);
        }

        for (int y = 0; y < rows; y++)
        {
            // 좌측 테두리
            Vector2 leftPosition = new Vector2(startX - 1, startY - y);
            Instantiate(boundPrefab, leftPosition, Quaternion.identity, parent.transform);

            // 우측 테두리
            Vector2 rightPosition = new Vector2(startX + cols, startY - y);
            Instantiate(boundPrefab, rightPosition, Quaternion.identity, parent.transform);
        }
    }
}
