using System.Collections.Generic;
using UnityEngine;

public enum EdgeType
{
    Empty, Connected, Green, Gray
}

[CreateAssetMenu(fileName = "WFC Item Edges", menuName = "WFCItem/Edges")]
[System.Serializable]
public class WfcItemEdges : WfcItem
{
    [Header("Rotation")] 
    public bool rotate90;
    public bool rotate180;
    public bool rotate270;
    [Header("Edges")] 
    public List<EdgeType> topEdges;
    public List<EdgeType> downEdges;
    public List<EdgeType> leftEdges;
    public List<EdgeType> rightEdges;
}