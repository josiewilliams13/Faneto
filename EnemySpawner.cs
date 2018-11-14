using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;

    public int numberOfEnemies;

    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {

            var spawnPosition = new Vector3(
                Random.Range(-17f, 17f),
                1.0f,
                -17f);

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);


            if (i % 3 == 0)
            {
                var spawnPosition2 = new Vector3(
                Random.Range(-17f, 17f),
                1.0f,
                17f);
                var enemy2 = (GameObject)Instantiate(enemyPrefab2, spawnPosition2, spawnRotation);
                NetworkServer.Spawn(enemy2);
            }
            var enemy = (GameObject)Instantiate(enemyPrefab1, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }

    void Update()
    {

        if (Time.frameCount % 600 == 0)
        {
            var spawnPosition = new Vector3(
            Random.Range(-17f, 17f),
            1.0f,
            Random.Range(-17, 17f));

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab1, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }

        if (Time.frameCount % 1200 == 0)
        {
            var spawnPosition = new Vector3(
Random.Range(-17f, 17f),
1.0f,
Random.Range(-17f, 17f));

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab2, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }


    }
}
