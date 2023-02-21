using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinerState
{
    void Enter();
    void LogicalUpdate();
    void PhysicalUpdate();
    void Exit();
}