using Abstract;

namespace Datas.ValueObject
{
    public class LevelData: SaveableEntity
    {   
        public static string LevelKey = "Level";

        public int LevelId;
        
        public LevelData() { }
        
        public LevelData(int _levelId)
        {
            LevelId = _levelId;
        }
        public override string GetKey()
        {
            return LevelKey;
        }
    }
}