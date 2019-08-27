using UnityEngine;

namespace Game
{
    public class PrefabFactory : MonoBehaviour
    {
        [SerializeField] private GameObject pipePairPrefab;

        public GameObject CreatePipePair()
        {
            return CreatePipePair(Vector3.zero, Quaternion.identity,null);
        }

        public GameObject CreatePipePair(
            Vector3 position,
            Quaternion rotation,
            GameObject parent)
        {
            var gameObject_MyVariAble = Instantiate(pipePairPrefab, position, rotation);
            if (parent) gameObject_MyVariAble.transform.parent = parent.transform;
            return gameObject_MyVariAble;
        }
    }
}