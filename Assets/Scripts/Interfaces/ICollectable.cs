using System;

public interface ICollectable
{
    void Collect();
    CollectableData GetData();
}

[Serializable]
public struct CollectableData
{
        public float HealData;
        public int AmmoData;
}