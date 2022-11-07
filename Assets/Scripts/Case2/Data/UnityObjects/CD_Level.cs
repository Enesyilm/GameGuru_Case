using Case2.Data.ValueObjects;
using UnityEngine;

namespace Case2.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "GameGuruChallenge", menuName = "GameGuruChallenge/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public LevelData LevelData;
    }
}