using Evolution.Chains;
using UnityEngine;

namespace Evolution
{
    public class Mergable : MonoBehaviour
    {
        [SerializeField]
        private EvolutionChain _evolutionChain; 

        [SerializeField]
        private int _currentLevel = 0;

        private bool _isMerging = false;

        public bool IsAvailable => !_isMerging;
        public int CurrentLevel => _currentLevel;

        private void Start()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _evolutionChain.GetStep(_currentLevel);
        }

        public void TryMerge()
        {
            if (_isMerging) return;

            Mergable partner = FindPartnerViaPhysics(_evolutionChain.MergeRadius);

            if (partner != null)
            {
                Merger.StartMerge(this, partner, _evolutionChain);
            }
        }

        public void IncreaseLevel() => _currentLevel++;

        private Mergable FindPartnerViaPhysics(float radius)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (var hit in hits)
            {
                Mergable otherMergable = hit.GetComponent<Mergable>();

                if (otherMergable != null &&
                    otherMergable != this &&                  
                    otherMergable.CurrentLevel == _currentLevel && 
                    otherMergable.IsAvailable)                
                {
                    return otherMergable;
                }
            }

            return null;
        }

        public void SetLocked(bool locked)
        {
            _isMerging = locked;
        }
    }
}

