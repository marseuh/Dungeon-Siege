using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] WaveSpawner spawner;
    [SerializeField] int spawnInterval = 3;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            //spawner.SpawnEnemy();
        }
    }

    /*    [SerializeField] List<GameObject> aiList = new List<GameObject>();
        [SerializeField] List<GameObject> prefabList = new List<GameObject>();

        [SerializeField] float spawningInterval = 10;
        [SerializeField] float spawningDuration = 300;
        [SerializeField] int pack = 5;

        public bool PartyOn;

        // Start is called before the first frame update
        void Start()
        {
            PartyOn = true;

            StartCoroutine(EndOfGame());
            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            yield return new WaitForSeconds(spawningInterval);
            SpawnIA();
        }

        IEnumerator EndOfGame()
        {
            yield return new WaitForSeconds(spawningDuration);
            PartyOn = false;
        }

        private void SpawnIA()
        {
            if (spawnerList != null)
            {
                for (int i = 0; i < aiList.Count; i++)
                {
                    int var = Random.Range(0, 1);

                    if (var == 0)
                        aiList[i] = prefabList[0];
                    else
                        aiList[i] = prefabList[1];
                }
            }
        }
    */
}
