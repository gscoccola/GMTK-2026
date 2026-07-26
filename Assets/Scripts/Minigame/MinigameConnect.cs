using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameConnect : Minigame
{
    [SerializeField] private float timeLimit = 8f;
    [SerializeField] private int requiredMatches = 3;
    [SerializeField] private LineRenderer linePrefab;

    private Camera mainCamera;
    private float timer;
    private bool shuffled;
    private int matchesFound;

    private ConnectNode[] allNodes;
    private List<ConnectNode> leftNodes = new();
    private List<ConnectNode> rightNodes = new();
    private ConnectNode dragStartNode;
    private LineRenderer activeLine;

    private readonly List<LockedConnection> lockedConnections = new();

    private class LockedConnection
    {
        public ConnectNode start;
        public ConnectNode end;
        public LineRenderer line;
    }

    public override void StartMinigame()
    {
        base.StartMinigame();
        mainCamera = Camera.main;
        timer = timeLimit;
        shuffled = false;
        matchesFound = 0;
        ClearConnections();
        InitializeNodes();

        foreach (var node in leftNodes)
            node.Initialize(this);

        foreach (var node in rightNodes)
            node.Initialize(this);
    }

    public override void OnUpdate(float deltaTime)
    {
        HandleTimer(deltaTime);
        HandleShuffle();
        RefreshLockedLines();
        RefreshActiveLine();
    }

    private void HandleTimer(float deltaTime)
    {
        // Daniel: I think the timer should be elsewhere to apply for all the mini-games.
        timer -= Time.unscaledDeltaTime;

        if (timer <= 0f)
        {
            timer = 0;
            Finish(false);
        }
    }

    private void HandleShuffle()
    {
        if (!shuffled && timer <= timeLimit / 2f)
        {
            CancelCurrentDrag();
            ShufflePositions(leftNodes);
            ShufflePositions(rightNodes);
            shuffled = true;
        }
    }

    public void BeginDrag(ConnectNode node, PointerEventData eventData)
    {
        if (node == null || !node.IsLeftNode || node.IsConnected)
            return;

        CancelCurrentDrag();
        dragStartNode = node;
        activeLine = Instantiate(linePrefab, transform);
        activeLine.positionCount = 2;
        activeLine.SetPosition(0, dragStartNode.transform.position);
        activeLine.SetPosition(1, ScreenToWorld(eventData.position));
    }

    public void ContinueDrag(ConnectNode node, PointerEventData eventData)
    {
        if (node != dragStartNode || activeLine == null)
            return;

        activeLine.SetPosition(0, dragStartNode.transform.position);
        activeLine.SetPosition(1, ScreenToWorld(eventData.position));
    }

    public void EndDrag(ConnectNode node, PointerEventData eventData)
    {
        if (node != dragStartNode || dragStartNode == null || activeLine == null)
            return;

        ConnectNode targetNode = GetNodeUnderPointer(eventData);

        if (IsValidMatch(dragStartNode, targetNode))
        {
            dragStartNode.IsConnected = true;
            targetNode.IsConnected = true;

            activeLine.SetPosition(0, dragStartNode.transform.position);
            activeLine.SetPosition(1, targetNode.transform.position);

            lockedConnections.Add(new LockedConnection
            {
                start = dragStartNode,
                end = targetNode,
                line = activeLine
            });

            matchesFound++;

            if (matchesFound >= requiredMatches)
            {
                Finish(true);
            }
        }

        else
            Destroy(activeLine.gameObject);

        dragStartNode = null;
        activeLine = null;
    }

    private bool IsValidMatch(ConnectNode left, ConnectNode right)
    {
        if (left == null || right == null)
            return false;

        if (!left.IsLeftNode || right.IsLeftNode)
            return false;

        if (left.IsConnected || right.IsConnected)
            return false;

        return left.PairId == right.PairId;
    }

    private ConnectNode GetNodeUnderPointer(PointerEventData eventData)
    {
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            ConnectNode node = result.gameObject.GetComponentInParent<ConnectNode>();
            
            if (node != null)
                return node;
        }

        return null;
    }

    private Vector3 ScreenToWorld(Vector2 screenPos)
    {
        Vector3 world = mainCamera.ScreenToWorldPoint(screenPos);
        world.z = 0f;
        return world;
    }

    private void InitializeNodes()
    {
        allNodes = FindObjectsByType<ConnectNode>();

        foreach (ConnectNode node in allNodes)
        {
            if (node.IsLeftNode)
                leftNodes.Add(node);

            else
                rightNodes.Add(node);
        }
    }

    private void RefreshActiveLine()
    {
        if (dragStartNode == null || activeLine == null)
            return;

        activeLine.SetPosition(0, dragStartNode.transform.position);
    }

    private void RefreshLockedLines()
    {
        foreach (var connection in lockedConnections)
        {
            if (connection.line == null)
                continue;

            connection.line.SetPosition(0, connection.start.transform.position);
            connection.line.SetPosition(1, connection.end.transform.position);
        }
    }

    private void ShufflePositions(List<ConnectNode> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            int randomIndex = Random.Range(i, nodes.Count);
            (nodes[randomIndex].transform.position, nodes[i].transform.position) = (nodes[i].transform.position, nodes[randomIndex].transform.position);
        }
    }

    private void CancelCurrentDrag()
    {
        if (activeLine != null)
            Destroy(activeLine.gameObject);

        activeLine = null;
        dragStartNode = null;
    }

    private void ClearConnections()
    {
        CancelCurrentDrag();

        foreach (var connection in lockedConnections)
        {
            if (connection.line != null)
                Destroy(connection.line.gameObject);
        }

        lockedConnections.Clear();

        foreach (var node in leftNodes)
            node.IsConnected = false;

        foreach (var node in rightNodes)
            node.IsConnected = false;
    }
}