using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evolution;
using Money;
using System;
using Movement;
using Random = UnityEngine.Random;
using Timer = UI.Timer;
using Tutorial;

namespace Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int _startLevel = 0;
        [SerializeField] private float _spawnRate;
        [SerializeField] private int _maxObjects;
        [SerializeField] private Canvas _canvas;


        [SerializeField] private EvolutionChain _evolutionChain;
        [SerializeField] private Timer _timer;
        private Balance _balance;
        private TutorialManager _tutorialManager;

        public float SpawnRate { get => _spawnRate; set => _spawnRate = value; }
        public int StartLevel { get => _startLevel; set => _startLevel = value; }

        private List<GameObject> _spawnedObjects = new();

        public event Action ObjectChanged;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
            _tutorialManager = FindObjectOfType<TutorialManager>();
        }

        public GameObject[] GetObjects()
        {
            return _spawnedObjects.ToArray();
        }

        private void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                if (_spawnedObjects.Count == 0)
                {
                    SpawnObjectWithRandomPosition(Random.Range(0, _startLevel));
                }             


                yield return new WaitUntil(_tutorialManager.GetTutorialCompletion);
                yield return new WaitUntil(HasSpaceToSpawn);
                _timer.StartTimer(_spawnRate);
                yield return new WaitForSeconds(_spawnRate);
            }
        }

        public bool HasSpaceToSpawn() => _spawnedObjects.Count < _maxObjects;

        public void RemoveObject(GameObject obj)
        {
            if (_spawnedObjects.Contains(obj))
            {
                _spawnedObjects.Remove(obj);
                Destroy(obj);
                ObjectChanged?.Invoke();
            }
        }

        public void SpawnObjectWithRandomPosition(int level)
        {
            SpawnObject(level, ScreenUtils.GetRandomVisiblePosition(Camera.main, 0.5f));
        }

        public void SpawnObject(int level, Vector3 position)
        {
            if (!HasSpaceToSpawn())
                return;


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
            _tutorialManager.OnObjectSpawn();
        }

        public void SpawnObjectWithRandomPosition(int level, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (!HasSpaceToSpawn())
                    return;

                SpawnObjectWithRandomPosition(level);
            }
        }
    }
}
