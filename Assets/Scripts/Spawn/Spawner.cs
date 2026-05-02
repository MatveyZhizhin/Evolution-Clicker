using Evolution.Chains;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evolution;
using Money;

namespace Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int _startLevel = 0;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private int _maxObjects;

        [SerializeField] private EvolutionChain _evolutionChain;
        private Balance _balance;

        private List<GameObject> _spawnedObjects = new();

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
        }

        private void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                SpawnObject(_startLevel, ScreenUtils.GetRandomVisiblePosition(Camera.main, 0.5f));
                yield return new WaitForSeconds(_spawnDelay);           
            }
        }

        private bool HasSpaceToSpawn() => _spawnedObjects.Count < _maxObjects;

        public void RemoveObject(GameObject obj)
        {
            if (_spawnedObjects.Contains(obj))
            {
                _spawnedObjects.Remove(obj);
                Destroy(obj);
            }
        }

        public void SpawnObject(int level, Vector3 position)
        {
            if (!HasSpaceToSpawn())
                return;

            GameObject newObj = Instantiate(_evolutionChain.Prefab, position, Quaternion.identity);
            _spawnedObjects.Add(newObj);

            Mergable mergable = newObj.GetComponent<Mergable>();  
            ObjectEconomy objectEconomy = newObj.GetComponent<ObjectEconomy>();

            mergable.SetLevel(level);
            objectEconomy.Initialize(_balance, _evolutionChain);
        }
    }
}
