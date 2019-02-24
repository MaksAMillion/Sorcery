using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AnimationRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
    [SerializeField] private GameObject enemyPrefab2;
    private GameObject _enemy2;
    [SerializeField] private GameObject enemyPrefab3;
    private GameObject _enemy3;
    [SerializeField] private GameObject enemyPrefab4;
    private GameObject _enemy4;

    public int currNumEnemies = 0;
    private const int MAX_NUM_ENEMIES = 15;

    public float PlaceX;
    public float PlaceZ;
    public float PlaceX2;
    public float PlaceZ2;
    public float PlaceX3;
    public float PlaceZ3;
    public float PlaceX4;
    public float PlaceZ4;

    /*void Awake()
    {
        InvokeRepeating("SpawnEnemy", 50.0f, 50.0f);
        InvokeRepeating("SpawnEnemy2", 50.0f, 50.0f);
        InvokeRepeating("SpawnEnemy3", 50.0f, 50.0f);
        InvokeRepeating("SpawnEnemy4", 50.0f, 50.0f);
    }*/

    void Start()
    {
        PlaceX = Random.Range(-4, -33);
        PlaceZ = Random.Range(4, -7);

        PlaceX2 = Random.Range(18, 1);
        PlaceZ2 = Random.Range(-25, -29);

        PlaceX3 = Random.Range(4, -31);
        PlaceZ3 = Random.Range(-54, -61);

        PlaceX4 = Random.Range(14, -7);
        PlaceZ4 = Random.Range(-66, -76);

        SpawnEnemy();
        SpawnEnemy2();
        SpawnEnemy3();
        SpawnEnemy4();
    }

    private void SpawnEnemy()
    {
        int enemyType1 = 0;
        
            if (enemyType1 == 0)
            {
                _enemy = Instantiate(enemyPrefab) as GameObject;
                _enemy.transform.position = new Vector3(PlaceX, 0, PlaceZ);
                currNumEnemies++;
            }
    }

    private void SpawnEnemy2()
    {
        int enemyType2 = 1;

        if (enemyType2 == 1)
        {
            _enemy2 = Instantiate(enemyPrefab2) as GameObject;
            _enemy2.transform.position = new Vector3(PlaceX2, 0, PlaceZ2);
            currNumEnemies++;
        }
    }

    private void SpawnEnemy3()
    {
        int enemyType3 = 2;

        if (enemyType3 == 2)
        {
            _enemy3 = Instantiate(enemyPrefab3) as GameObject;
            _enemy3.transform.position = new Vector3(PlaceX3, 0, PlaceZ3);
            currNumEnemies++;
        }
    }

    private void SpawnEnemy4()
    {
        int enemyType4 = 3;

        if (enemyType4 == 3)
        {
            _enemy4 = Instantiate(enemyPrefab4) as GameObject;
            _enemy4.transform.position = new Vector3(PlaceX4, 0, PlaceZ4);
            currNumEnemies++;
        }
    }
}
