using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    #region Variables
    private float _directDistanceToEnd = 0f;
    public float DirectDistanceToEnd { get { return _directDistanceToEnd; } }
    //Total cost of shortest path to this node
    private float _pathWeight = int.MaxValue;
    public float PathWeight
    {
        get { return _pathWeight + _directDistanceToEnd; }
        set { _pathWeight = value; }
    }
    //Following the shortest path, _previousNode if the previous step on that path
    private Node _previousNode = null;
    public Node PreviousNode
    {
        get { return _previousNode; }
        set { _previousNode = value; }
    }

    [SerializeField] private List<Node> _neighbourNodes;
    public List<Node> NeighbourNodes { get { return _neighbourNodes; } }
    
    #endregion
    private void Start()
    {
        ResetNode();
        ValidateNeighbours();
    }

    public void ResetNode()
    {
        _pathWeight = int.MaxValue;
        _previousNode = null;
        _directDistanceToEnd = 0f;
    }
    public void AddNeighbourNode(Node node)
    {
        _neighbourNodes.Add(node);
    }
    private void OnDrawGizmos()
    {
        foreach (Node node in _neighbourNodes)
        {
            if (node == null) continue;
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
    private void OnValidate()
    {
        ValidateNeighbours();
    }
    private void ValidateNeighbours()
    {
        foreach (Node node in _neighbourNodes)
        {
            if (node == null) continue;
            if (!node._neighbourNodes.Contains(this))
            {
                node._neighbourNodes.Add(this);
            }
        }
    }
    public void SetDirectDistanceToEnd (Vector3 endPosition)
    {
        _directDistanceToEnd = Vector3.Distance(transform.position, endPosition);
    }
}
