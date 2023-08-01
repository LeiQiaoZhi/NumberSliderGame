using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGridGenerator : MonoBehaviour
{
    public GridSystem gridSystem;
    
    void Start()
    {
        gridSystem.CreateDefaultCells();
    }
}
