<<<<<<< Updated upstream
using Evolution.Chains;
using UnityEngine;
=======
using UnityEngine;
using UnityEngine.UI;
>>>>>>> Stashed changes

namespace Evolution
{
    public class Mergable : MonoBehaviour
    {
        [SerializeField]
        private EvolutionChain _evolutionChain; 

        [SerializeField]
        private int _currentLevel = 0;

        private bool _isMerging = false;
        private Merger _merger;

        public bool IsAvailable => !_isMerging;
        public int CurrentLevel => _currentLevel;

        private void Awake()
        {
            _merger = FindObjectOfType<Merger>();
        }

        public void SetLevel(int level)
        {
            _currentLevel = level;
<<<<<<< Updated upstream
            gameObject.GetComponent<SpriteRenderer>().sprite = _evolutionChain.GetStep(level).Sprite;
=======
            var image = gameObject.GetComponentInChildren<Image>();
            image.sprite = _evolutionChain.GetStep(level).Sprite;
>>>>>>> Stashed changes
        }

        public void TryMerge()
        {
            if (_isMerging) return;

            Mergable partner = FindPartnerViaPhysics(_evolutionChain.MergeRadius);

            if (partner != null)
            {
                _merger.StartMerge(this, partner, _evolutionChain);
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

