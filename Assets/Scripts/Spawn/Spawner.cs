<<<<<<< Updated upstream
using Evolution.Chains;
=======
>>>>>>> Stashed changes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evolution;
using Money;
<<<<<<< Updated upstream
=======
using System;
using Movement;
using Random = UnityEngine.Random;
>>>>>>> Stashed changes

namespace Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int _startLevel = 0;
<<<<<<< Updated upstream
        [SerializeField] private float _spawnDelay;
        [SerializeField] private int _maxObjects;
=======
        [SerializeField] private float _spawnRate;
        [SerializeField] private int _maxObjects;
        [SerializeField] private Canvas _canvas;

>>>>>>> Stashed changes

        [SerializeField] private EvolutionChain _evolutionChain;
        private Balance _balance;

<<<<<<< Updated upstream
        private List<GameObject> _spawnedObjects = new();

=======
        public float SpawnRate { get => _spawnRate; set => _spawnRate = value; }
        public int StartLevel { get => _startLevel; set => _startLevel = value; }

        private List<GameObject> _spawnedObjects = new();

        public event Action ObjectChanged;

>>>>>>> Stashed changes
        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
        }

<<<<<<< Updated upstream
=======
        public GameObject[] GetObjects()
        {
            return _spawnedObjects.ToArray();
        }

>>>>>>> Stashed changes
        private void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
<<<<<<< Updated upstream
                SpawnObject(_startLevel, ScreenUtils.GetRandomVisiblePosition(Camera.main, 0.5f));
                yield return new WaitForSeconds(_spawnDelay);           
=======
                SpawnObjectWithRandomPosition(Random.Range(0, _startLevel));
        
                yield return new WaitForSeconds(_spawnRate);           
>>>>>>> Stashed changes
            }
        }

        private bool HasSpaceToSpawn() => _spawnedObjects.Count < _maxObjects;

        public void RemoveObject(GameObject obj)
        {
            if (_spawnedObjects.Contains(obj))
            {
                _spawnedObjects.Remove(obj);
                Destroy(obj);
<<<<<<< Updated upstream
            }
        }

=======
                ObjectChanged?.Invoke();
            }
        }

        public void SpawnObjectWithRandomPosition(int level)
        {
            SpawnObject(level, ScreenUtils.GetRandomVisiblePosition(Camera.main, 0.5f));
        }

>>>>>>> Stashed changes
        public void SpawnObject(int level, Vector3 position)
        {
            if (!HasSpaceToSpawn())
                return;

<<<<<<< Updated upstream
            GameObject newObj = Instantiate(_evolutionChain.Prefab, position, Quaternion.identity);
            _spawnedObjects.Add(newObj);

            Mergable mergable = newObj.GetComponent<Mergable>();  
            ObjectEconomy objectEconomy = newObj.GetComponent<ObjectEconomy>();

            mergable.SetLevel(level);
            objectEconomy.Initialize(_balance, _evolutionChain);
=======

            GameObject newObj = Instantiate(_evolutionChain.Prefab, position, Quaternion.identity);
            _spawnedObjects.Add(newObj);
            newObj.transform.SetParent(_canvas.transform, true);
            newObj.transform.SetSiblingIndex(1);


            Mergable mergable = newObj.GetComponent<Mergable>();  
            ObjectEconomy objectEconomy = newObj.GetComponent<ObjectEconomy>();
            RandomMover randomMover = newObj.GetComponent<RandomMover>();
            Draggable draggable = newObj.GetComponent<Draggable>();

            mergable.SetLevel(level);
            objectEconomy.Initialize(_balance, _evolutionChain);

            ObjectChanged?.Invoke();
        }

        public void SpawnObjectWithRandomPosition(int level, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (!HasSpaceToSpawn())
                    return;

                SpawnObjectWithRandomPosition(level);
            }
>>>>>>> Stashed changes
        }
    }
}
