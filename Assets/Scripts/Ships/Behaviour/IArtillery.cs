using UnityEngine;
using System.Collections;

public interface IArtillery
{
    int Level { get;}
    int Strenght { get; }
    float StrenghtMultiplier {get; }

    void Fire();
}
