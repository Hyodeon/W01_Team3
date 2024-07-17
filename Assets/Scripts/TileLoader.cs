using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TileLoader : MonoBehaviour
{
    public GameObject[] tilePrefab;
    public GameObject parent;
    public GameObject boundPrefab; // Bound �������� �ν����Ϳ��� �Ҵ�
    public TextAsset csvFile;      // �ν����Ϳ��� �Ҵ��� �� �ֵ��� TextAsset Ÿ������ ����
    public int boundDistance = 3;

    public Camera mainCamera;
    public float size = 5.0f; // �⺻ ��

    public GameObject player;
    public GameObject goal;

    public TextDisplay TD;


    // �������� ��ư�� �����ϱ� ���� ����Ʈ
    private Dictionary<int, List<GameObject>> laserList = new Dictionary<int, List<GameObject>>();
    private Dictionary<int, GameObject> buttonList = new Dictionary<int, GameObject>();

    void Start()
    {
        LoadTilesFromCSV();
        TD.StageUpdate();
        AddBounds();
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = size;
            mainCamera.transform.position = new Vector3(size - 4f, 0, -10);
        }
    }

    void LoadTilesFromCSV()
    {
        buttonList.Clear();
        laserList.Clear();
        for (int i = 0; i < 10; i++)
        {
            laserList.Add(i, new List<GameObject>());
        }

        csvFile = MenuManager.MapFile;
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
                string tileType = values[x];
                Vector2 position = new Vector2(startX + x, startY - y);

                GenerateTile(tileType, position);
            }
        }
        Debug.Log("1");
        ConnectLaser();
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

    void GenerateTile(string tileType, Vector2 pos)
    {
        int typeNumber = int.Parse(tileType[0].ToString()) - 1;

        switch (tileType[0])
        {
            case '1':
            case '5':
                // [1] ���� [5] �ð� ���� �Ұ��� ����
                Instantiate(tilePrefab[typeNumber], pos, Quaternion.identity, parent.transform);
                break;

                // [2] ��ֹ� - ��
            case '2':
                GameObject temp1 = Instantiate(tilePrefab[typeNumber], pos, Quaternion.identity, parent.transform);
                temp1.GetComponent<ColorPlatform>().type = tileType[1] switch
                {
                    'R' => "red",
                    'G' => "green",
                    'B' => "blue",
                    _ => "none"
                };
                break;

            case '3':
                // ������ ��ư
                GameObject temp2 = Instantiate(tilePrefab[typeNumber], pos, Quaternion.identity, parent.transform);
                int id1 = int.Parse(tileType[1].ToString());

                temp2.GetOrAddComponent<Button>().id = id1;
                buttonList.Add(id1, temp2);

                break;

            case '4':
                // ������ �����
                GameObject temp4 = Instantiate(tilePrefab[typeNumber], pos, Quaternion.identity, parent.transform);
                temp4.GetOrAddComponent<Laser>().type = tileType[1] switch
                {
                    '1' => "down",
                    '2' => "up",
                    '3' => "right",
                    '4' => "left",
                    _ => "none"
                };
                int id2 = int.Parse(tileType[3].ToString());

                temp4.GetOrAddComponent<Laser>().id = id2;


                temp4.GetOrAddComponent<Laser>().State = tileType[2] == 'P';


                laserList[id2].Add(temp4);

                break;

            case '6':
                int detailType = int.Parse(tileType[1].ToString());
                Instantiate(tilePrefab[typeNumber + detailType - 1], pos, Quaternion.identity, parent.transform);
                break;

            case '7':
                GameObject temp3 = Instantiate(tilePrefab[typeNumber + 1], pos, Quaternion.identity, parent.transform);
                temp3.GetComponent<ColorZone>().type = tileType[1] switch
                {
                    'R' => "red",
                    'G' => "green",
                    'B' => "blue",
                    _ => "none"
                };

                temp3.GetComponent<ColorZone>().Initialize();

                break;
            case '8':
                player.transform.position = pos;
                player.transform.position += new Vector3(0, 0, -2);
                break;
            case '9':
                goal.transform.position = pos;
                goal.transform.position += new Vector3(0, 0, -2);
                break;
        }
    }

    void ConnectLaser()
    {

        foreach (var btn in buttonList)
        {

            int id = btn.Key;

            btn.Value.GetComponent<Button>().RegisterLaser(laserList[id]);

            foreach (GameObject go in laserList[id])
            {
                go.GetComponent<Laser>().Initialize();
            }

            Debug.Log($"��ư id : {id} ���� {laserList[id].Count}���� �������� ���Ե˴ϴ�.");
        }
    }
}
