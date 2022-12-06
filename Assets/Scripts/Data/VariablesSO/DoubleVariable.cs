using System;
using UnityEngine;


[CreateAssetMenu(fileName = "DoubleVariable", menuName = "Scriptable Objects/Variables/Double")]
public class DoubleVariable : NumberVariable<double>
{
    public void Add(double a)
    {
        Value += a;
    }

    public void Add(DoubleVariable a)
    {
        Value += a.Value;
    }

    public void Set(double a)
    {
        Value = a;
    }

    public void Set(DoubleVariable a)
    {
        Value = a.Value;
    }

    public bool Equals(double other)
    {
        return Value.Equals(other);
    }

    public bool Equals(DoubleVariable other)
    {
        return Value.Equals(other.Value);
    }
}