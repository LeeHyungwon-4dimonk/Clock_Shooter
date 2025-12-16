using System.Collections.Generic;

public class MonsterPositionManager
{
    private readonly Dictionary<int, Dictionary<int, MonsterController>> _slots = new();

    private readonly HashSet<MonsterController> _monsters = new();

    public void Register(MonsterController monster)
    {
        _monsters.Add(monster);

        if (!_slots.ContainsKey(monster.DirectionIndex))
            _slots[monster.DirectionIndex] = new Dictionary<int, MonsterController>();

        _slots[monster.DirectionIndex][monster.DistanceStep] = monster;
    }

    public void Unregister(MonsterController monster)
    {
        _monsters.Remove(monster);

        if (_slots.TryGetValue(monster.DirectionIndex, out var dirSlots))
            dirSlots.Remove(monster.DistanceStep);
    }

    public void ResolveTurnMove(int delta)
    {
        if (delta == 0) return;

        foreach (var dirPair in _slots)
        {
            ResolveDirectionMove(dirPair.Key, delta);
        }
    }

    private void ResolveDirectionMove(int dir, int delta)
    {
        if (!_slots.ContainsKey(dir)) return;

        var dirSlots = _slots[dir];

        List<int> steps = new List<int>(dirSlots.Keys);

        if (delta < 0) steps.Sort();
        else steps.Sort((a, b) => b - a);

        HashSet<int> moved = new HashSet<int>();

        foreach (int step in steps)
        {
            if (!dirSlots.ContainsKey(step)) continue;

            MonsterController monster = dirSlots[step];
            int target = monster.GetTargetStep(delta);

            if (target == step) continue;

            // 이동하려는 슬롯이 비어있거나, 빌 예정일 때
            if (!dirSlots.ContainsKey(target) || moved.Contains(target))
            {
                dirSlots.Remove(step);
                monster.ApplyMove(target);
                dirSlots[target] = monster;

                moved.Add(target);
            }
        }
    }
}