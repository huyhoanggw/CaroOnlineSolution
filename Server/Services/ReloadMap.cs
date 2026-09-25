using Server.Entities;

namespace Server.Services
{
    public static class ReloadMap
    {
        public static List<Cell> Map(List<Cell> Maps)
        {
            for (int i = 0; i < 625; i++)
            {
                Maps.Add(new Cell() { Value = Enum.Status.None });
            }
            return Maps;
        }
    }
}
