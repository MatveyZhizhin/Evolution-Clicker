using Evolution.Chains;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evolution
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRate;

        [SerializeField] private EvolutionChain _evolutionChain;

        private int _startLevel = 0;

        public void SpawnWithRandomPosition()
        {
            
        }
    }
}
