using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private GameObject[] _nodes;

    public Node start;
    public Node end;

    private void Start()
    {
        List<Node> shortestPath = FindShortestPath(start, end);

        Node prevNode = null; ;
        foreach (Node node in shortestPath)
        {
            if (prevNode != null)
            {
                Debug.DrawLine(node.transform.position + Vector3.up, prevNode.transform.position + Vector3.up, Color.blue, 10f);
            }
            Debug.Log(node.gameObject.name);
            prevNode = node;
        }
    }

    public List<Node> FindShortestPath(Node start, Node end)
    {
        _nodes = GameObject.FindGameObjectsWithTag("Node");

        foreach(GameObject node in _nodes)
        {

        }
                
        if (AStarAlgorithm(start, end))
        {
            List<Node> result = new List<Node>();
            Node node = end;
            do
            {
                result.Insert(0, node);
                node = node.PreviousNode;
            } while (node != null);

            return result;
        }
        return null;
    }
    private bool AStarAlgorithm(Node start, Node end)
    {
        List<Node> unexplored = new List<Node>();

        foreach(GameObject obj in _nodes)
        {
            Node n = obj.GetComponent<Node>();
            if (n == null) continue;
            n.ResetNode();
            n.SetDirectDistanceToEnd(end.transform.position);
            unexplored.Add(n);
        }
        if (!unexplored.Contains(start) && !unexplored.Contains(end))
        {
            return false;
        }
        start.PathWeight = 0;
        while (unexplored.Count > 0)
        {
            //Order based on path
            unexplored.Sort((x,y) => x.PathWeight.CompareTo(y.PathWeight));
            //Current is the current shortest path possibility
            Node current = unexplored[0];

            if (current == end)
            {
                break;
            }

            unexplored.RemoveAt(0);

            foreach(Node neighbourNode in current.NeighbourNodes)
            {
                if (current.PreviousNode == neighbourNode) continue;
                if (!unexplored.Contains(neighbourNode)) continue;
                float distance = Vector3.Distance(neighbourNode.transform.position, current.transform.position);
                distance += current.PathWeight;

                if (distance < neighbourNode.PathWeight)
                {
                    neighbourNode.PathWeight = distance;
                    neighbourNode.PreviousNode = current;
                }
                
                
            }
        }

        return true;
    }
}
