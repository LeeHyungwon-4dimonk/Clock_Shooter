using System.Collections.Generic;

public class MonsterPositionManager
{
    private readonly Dictionary<int, Dictionary<int, MonsterController>> _slots = new();

    public bool IsOccupied(int dir, int step)
    {
        return _slots.ContainsKey(dir) && _slots[dir].ContainsKey(step);
    }

    public void Register(MonsterController monster, int dir, int step)
    {
        if(!_slots.ContainsKey(dir)) _slots[dir] = new Dictionary<int, MonsterController>();

        _slots[dir][step] = monster;
    }

    public void Unregister(int dir, int step)
    {
        if(_slots.ContainsKey(dir)) _slots[dir].Remove(step);
    }

    public bool TryMove(MonsterController monster, int targetStep)
    {
        int dir = monster.DirectionIndex;

        if (IsOccupied(dir, targetStep)) return false;

        if(targetStep < 0) return false;

        Unregister(dir, monster.DistanceStep);
        Register(monster, dir, targetStep);

        return true;
    }
}
