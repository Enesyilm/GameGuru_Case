using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace UnityTemplateProjects.Datas.UnityObjects
{
    [CreateAssetMenu(fileName = "Game", menuName = "Game/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> LevelDataList;
    }
}