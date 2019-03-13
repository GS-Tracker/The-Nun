using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAbbey : MonoBehaviour
{
    public MoveToAbbey()
    {
        // Node Start = new Node(new Vector3(260.0F, -2.031006F, 203.770691F));
        // Node Fork1 = new Node(new Vector3(-250.0F, -2.031006F, 203.770691F));
        // Node Fork2 = new Node(new Vector3(-1670.0F, -422.031006F, 203.770691F));
        // Node Goal1 = new Node(new Vector3(-1410.0F, 277.968994F, 203.770691F));
        // Node Goal2 = new Node(new Vector3(-2260.0F, -422.031006F, 203.770691F));
        // Node Goal3 = new Node(new Vector3(-2160.0F, -732.031006F, 203.770691F));
        //
        // Graph TerrainGraph = new Graph();
        // Heuristic Heuristic = new Heuristic(Goal2);
        // PathFinder AStarPathFinder = new PathFinder();
        // List<Connection> ActualPath = AStarPathFinder.PathFindAStar(TerrainGraph, Start, Goal2, Heuristic);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Node Start = new Node(new Vector3(260.0F, -2.031006F, 203.770691F));
            Node Fork1 = new Node(new Vector3(-250.0F, -2.031006F, 203.770691F));
            Node Fork2 = new Node(new Vector3(-1670.0F, -422.031006F, 203.770691F));
            Node Goal1 = new Node(new Vector3(-1410.0F, 277.968994F, 203.770691F));
            Node Goal2 = new Node(new Vector3(-2260.0F, -422.031006F, 203.770691F));
            Node Goal3 = new Node(new Vector3(-2160.0F, -732.031006F, 203.770691F));

            Graph TerrainGraph = new Graph();
            Heuristic Heuristic = new Heuristic(Goal2);
            PathFinder AStarPathFinder = new PathFinder();
            List<Connection> ActualPath = AStarPathFinder.PathFindAStar(TerrainGraph, Start, Goal2, Heuristic);
        }
    }
}

public class Node : IEquatable<Node>
{
    public Vector3 Location { get; set; }

    public Node()
    {
    }

    public Node(Vector3 location)
    {
        Location = location;
    }

    public bool Equals(Node n)
    {
        return this.Location.Equals(n.Location);
    }
}

public class Connection
{
    public Node FirstNode { get; set; }
    public Node SecondNode { get; set; }

    public Connection()
    {
    }

    public Connection(Node firstNode, Node secondNode)
    {
        FirstNode = firstNode;
        SecondNode = secondNode;
    }

    public Node GetFromNode()
    {
        return this.FirstNode;
    }

    public Node GetToNode()
    {
        return this.SecondNode;
    }

    public float GetCost()
    {
        return Math.Abs(SecondNode.Location.magnitude - FirstNode.Location.magnitude);
    }
}

public class NodeRecord
{
    public Connection Connection { get; set; }
    public Node Node { get; set; }
    public float CostSoFar { get; set; }
    public float Cost { get; set; }
    public float EstimatedTotalCost { get; set; }

    public NodeRecord()
    {
    }

}

public class Heuristic
{
    public Node Goal { get; set; }

    public Heuristic(Node goal)
    {
        Goal = goal;
    }

    public float Estimate(Node current)
    {
        return Math.Abs(Goal.Location.magnitude - current.Location.magnitude);
    }

    public bool IsGoalNode(Node current)
    {
        return current.Equals(Goal);
    }
}

// *** Graph Class with GUID *** //
//---------------------------------
//public class Graph
//{
//    public Dictionary<string, Tuple<Node, List<Connection>>> AdjacencyListGraph;

//    public void AddEdge(Node FirstNode, Node SecondNode)
//    {
//        Connection newConnection = new Connection(FirstNode, SecondNode);
//        foreach (var x in AdjacencyListGraph)
//        {
//            if (x.Value.Item1.Equals(FirstNode))
//            {
//                x.Value.Item2.Add(newConnection);
//                return;
//            }
//        }

//        List<Connection> l = new List<Connection> { newConnection };
//        Tuple<Node, List<Connection>> t = new Tuple<Node, List<Connection>>(FirstNode, l);
//        AdjacencyListGraph.Add(Guid.NewGuid().ToString(), t);
//    }

//}


// *** Graph Class without GUID *** //
//------------------------------------
public class Graph
{
    public Dictionary<Node, List<Connection>> AdjacencyListGraph;

    public void AddEdge(Node FirstNode, Node SecondNode)
    {
        Connection newConnection = new Connection(FirstNode, SecondNode);

        //compares using Node class implementation of IEquatable Equals method
        bool nodeExists = AdjacencyListGraph.ContainsKey(FirstNode);
        if (nodeExists)
        {
            AdjacencyListGraph[FirstNode].Add(newConnection);
        }
        else
        {
            List<Connection> l = new List<Connection> { newConnection };
            AdjacencyListGraph.Add(FirstNode, l);
        }
    }

    public List<Connection> GetConnections(Node current)
    {
        bool nodeExists = AdjacencyListGraph.ContainsKey(current);
        if (nodeExists)
        {
            return AdjacencyListGraph[current];
        }
        return null;
    }

    public int GetNumberOfForks()
    {
        return AdjacencyListGraph.Count;
    }
}

public class PathFindingList
{
    public List<NodeRecord> pathFindingList = new List<NodeRecord>();

    public void Add(NodeRecord NodeRecord)
    {
        pathFindingList.Add(NodeRecord);
        pathFindingList.Sort(new NodeRecordComparer());
    }

    public void Remove()
    {
        pathFindingList.RemoveAt(0);
    }

    public void Remove(NodeRecord nodeRecord)
    {
        pathFindingList.Remove(nodeRecord);
    }

    public void RemoveAt(int index)
    {
        pathFindingList.RemoveAt(index);
    }

    public NodeRecord SmallestElement() {
	    return pathFindingList[0];
    }

    public int NumberOfElements() {
	    return pathFindingList.Count;
    }

    public bool IsEmpty() {
	    return pathFindingList.Count == 0;
    }

    public bool Contains(Node n)
    {
        foreach(NodeRecord nodeRecord in pathFindingList)
        {
            if (nodeRecord.Node.Equals(n))
            {
                return true;
            }
        }
        return false;
    }

    public int Find(Node n)
    {
        for(int i = 0; i < pathFindingList.Count; i++)
        {
            if (pathFindingList[i].Node.Equals(n))
            {
                return i;
            }
        }
        return -1;
    }
}

public class NodeRecordComparer : IComparer<NodeRecord>
{
    public int Compare(NodeRecord first, NodeRecord second)
    {
        if (first == null)
        {
            if (second == null)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (second == null)
            {
                return 1;
            }
            else
            {
                return first.EstimatedTotalCost.CompareTo(second.EstimatedTotalCost);
            }
        }
    }
}

public class PathFinder
{
    public List<Connection> PathFindAStar(Graph graph, Node start, Node goal, Heuristic heuristic)
    {
        // Initialize the record for the start node
        NodeRecord StartRecord = new NodeRecord
        {
            Node = start,
            Connection = null,
            CostSoFar = 0.0F,
            EstimatedTotalCost = heuristic.Estimate(start) // we didn't add cost so far here cause of zero value.
        };

        // Initialize the open and closed lists
        PathFindingList openList = new PathFindingList();
        openList.Add(StartRecord);
        PathFindingList closedList = new PathFindingList();

        // Iterate through each node in the open list and start processing each one individually.
        NodeRecord current = new NodeRecord();
        while (!openList.IsEmpty())
        { // As long as the open list is not empty.

            // Find the smallest element in the open list (using the estimatedTotalCost)
            current = openList.SmallestElement();
            // If it is the goal node, then terminate
            List<Connection> foundConnections = new List<Connection>();
            // heuristic must know the distance between the current node and the goal node
            // also it must differentiate between whether the current node is the goal or not.
            // e.g: heuristic.isGoal(current)
            if (heuristic.IsGoalNode(current.Node))
            {
                break;
            }
            else
            {

                // Otherwise get its outgoing connections
                // FoundConnections = Graph.GetConnections(Current.Node);
            }

            // Loop through each connection in turn
            for (int i = 0; i < foundConnections.Count; i++)
            {
                // Get the cost estimate for the end nodes
                Node endNode = foundConnections[i].GetToNode();
                // expect to return endNode of Node type.
                // any connection should have the distance as its cost between two nodes and getCost();
                // getCost() -> subtraction between toNode location vector - fromNode location vector.
                float endNodeCostSoFar = current.CostSoFar + foundConnections[i].GetCost();
                NodeRecord endNodeRecord = new NodeRecord();
                float endNodeHeuristic;

                // If the node is closed we may have to skip, or remove it from the closed list.
                if (closedList.Contains(endNode))
                {
                    // Here we find the record in the closed list corresponding to the endNode.
                    int foundIndex = closedList.Find(endNode);
                    endNodeRecord = closedList.pathFindingList[foundIndex];
                    // If we didn�t find a shorter route, skip
                    if (endNodeRecord.CostSoFar <= endNodeCostSoFar)
                    {
                        continue;
                    }
                    else
                    {
                        // Otherwise remove it from the closed list
                        closedList.RemoveAt(foundIndex);
                    }

                    // We can use the node�s old cost values to calculate its heuristic without calling the possibly expensive heuristic function
                    // heuristic never changed because it's considered hardly estimated so do you know how to change your imagination ?
                    endNodeHeuristic = endNodeRecord.Cost;
                    // Skip if the node is open and we�ve not found a better route

                }
                else if (openList.Contains(endNode))
                {

                    // Here we find the record in the open list corresponding to the endNode.
                    int foundIndex = openList.Find(endNode);
                    // If our route is no better, then skip
                    if (endNodeRecord.CostSoFar <= endNodeCostSoFar)
                    {
                        continue;
                    }
                    // We can use the node�s old cost values to calculate its heuristic without calling the possibly expensive heuristic function
                    // heuristic never changed because it's considered hardly estimated so do you know how to change your imagination ?
                    endNodeHeuristic = endNodeRecord.Cost;

                }
                else
                {
                    // Otherwise we know we�ve got an unvisited node, so make a record for it
                    endNodeRecord.Node = endNode;
                    // We�ll need to calculate the heuristic value using the function, since we don�t have an existing record to use
                    endNodeHeuristic = heuristic.Estimate(endNode);
                }
                // We�re here if we need to update the node Update the cost, estimate and connection
                endNodeRecord.Cost = endNodeHeuristic;
                endNodeRecord.Connection = foundConnections[i];
                endNodeRecord.EstimatedTotalCost = endNodeCostSoFar + endNodeRecord.Cost;
                // And add it to the open list
                if (!openList.Contains(endNode))
                {
                    openList.Add(endNodeRecord);
                }
                // We�ve finished looking at the connections for the current node, so add it to the closed list and remove it from the open list
                openList.Remove(current);
                closedList.Add(current);
            }
        }

        // We�re here if we�ve either found the goal, or if we�ve no more nodes to search, find which.
        if (!heuristic.IsGoalNode(current.Node))
        {
            // We�ve run out of nodes without finding the goal, so there�s no solution
            List<Connection> emptyPath = new List<Connection>();
            return emptyPath;
        }
        else
        {
            // Compile the list of connections in the path
            List<Connection> path = new List<Connection>();
            // Work back along the path, accumulating connections
            while (!current.Node.Equals(start))
            {
                path.Add(current.Connection);
                current.Connection.GetFromNode();
            }

            // Reverse the path, and return it
            path.Reverse();
            return path;
        }
    }
}
