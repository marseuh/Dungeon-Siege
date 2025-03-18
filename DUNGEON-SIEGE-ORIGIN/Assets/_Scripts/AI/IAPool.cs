using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPool : MonoBehaviour
{
    public static IAPool Instance;
    public GameObject IAPrefab;
    public int PoolSize = 15;

    private List<GameObject> _iaPools;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _iaPools = new List<GameObject>();

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject ia = Instantiate(IAPrefab, transform);
            ia.SetActive(false);
            _iaPools.Add(ia);
        }
    }

    public GameObject GetIA()
    {
        for (int i = 0; i < _iaPools.Count; i++)
        {
            if (!_iaPools[i].activeInHierarchy)
            {
                _iaPools[i].SetActive(true);
                return _iaPools[i];
            }
        }

        GameObject newIA = Instantiate(IAPrefab, transform);
        _iaPools.Add(newIA);
        newIA.SetActive(true);
        return newIA;
    }

    public void ClearOneIA(GameObject ia)
    {
        ia.SetActive(false);
    }

    public void ClearAllIA()
    {
        foreach (GameObject ia in _iaPools)
            Destroy(ia);

        _iaPools.Clear();
    }
}
