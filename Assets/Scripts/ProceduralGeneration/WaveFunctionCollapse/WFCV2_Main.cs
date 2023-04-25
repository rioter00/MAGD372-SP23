using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WFCV2_Main : MonoBehaviour
{
    public List<GameObject> baseAndWellPrefabs;
    public List<GameObject> easyIslands;
    public List<GameObject> mediumIslands;
    public List<GameObject> hardIslands;
    public List<GameObject> selectableIslands;
    public List<GameObject> mirroredIslands;
    public Vector3 grid;
    public float cellSize;
    public float cellSpacing;
    public float spawnEvery;

    private Vector3 boundingUnit;
    private Dictionary<string, List<Vector3>> sockets = new Dictionary<string, List<Vector3>>();
    private List<WFCV2_SingleState> superPosition = new List<WFCV2_SingleState>();
    private List<WFCV2_CellInfo> allCells = new List<WFCV2_CellInfo>();
    private List<WFCV2_CellInfo> cellToProcess = new List<WFCV2_CellInfo>();
    [SerializeField] private WFC_Spawned_Data_List allSpawnedPrefab = new WFC_Spawned_Data_List();
    private int collapsed;
    private float timer;
    private bool islandsHaveBeenPlaced = false;

    private List<Vector3> basePositions = new List<Vector3>();
    private List<int> baseIndices = new List<int>();
    private List<Vector3> wellPositions = new List<Vector3>();
    private List<int> wellIndices = new List<int>();
    Vector3 tempCord = new Vector3();
    Vector3 adjustedCord = new Vector3();
    [SerializeField] private Transform IslandHolder;
    private GameObject mirroredIsland;

    private int totalIslandSpaces = 36;
    [SerializeField] private int easyIslandAmount;
    [SerializeField] private int mediumIslandAmount;
    [SerializeField] private int hardIslandAmount;

    // Start is called before the first frame update
    void Start()
    {
        selectableIslands.AddRange(baseAndWellPrefabs);
        selectableIslands.AddRange(easyIslands);
        selectableIslands.AddRange(mediumIslands);
        selectableIslands.AddRange(hardIslands);
        collapsed = 0;
        boundingUnit = new Vector3(cellSize / 2, 0, cellSize / 2);
        processPrefab();
        CreateBaseAndWellPositions();

        allocatePossiblitySpace();

        //triggerCollapse();
        //propogateCollapseNew();

        //collapsed++;
        timer = spawnEvery;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && collapsed < allCells.Count / 2)
        {
            timer = spawnEvery;
            getLowestEntropyCellAndSpawn();
            propogateCollapseNew();
        }
        else if(collapsed >= allCells.Count / 2 && islandsHaveBeenPlaced == false)
        {
            islandsHaveBeenPlaced = true;
            Vector3 wellPlacement = new Vector3(grid.x * 30, 0, grid.z * 30);
            Instantiate(baseAndWellPrefabs[2], wellPlacement, Quaternion.identity, IslandHolder);
            CreateBases();
            //Debug.Log(totalIslandSpaces);
        }
    }

    private void CreateBaseAndWellPositions()
    {
        basePositions.Add(new Vector3(0, 0, 0));
        basePositions.Add(new Vector3(0, 0, grid.z));
        basePositions.Add(new Vector3(grid.x, 0, 0));
        basePositions.Add(new Vector3(grid.x, 0, grid.z));
        wellPositions.Add(new Vector3(grid.x / 2 - 1, 0, grid.z / 2 - 1));
        wellPositions.Add(new Vector3(grid.x / 2 - 1, 0, grid.z / 2 + 1));
    }

    private void CreateBases()
    {
        GameObject obj;

        obj = Instantiate(baseAndWellPrefabs[3], basePositions[0], Quaternion.identity, IslandHolder);
        obj.transform.Rotate(0, 180, 0);

        obj = Instantiate(baseAndWellPrefabs[4], new Vector3(0, 0, grid.z * 50), Quaternion.identity, IslandHolder);
        obj.transform.Rotate(0, 270, 0);

        obj = Instantiate (baseAndWellPrefabs[5], new Vector3(grid.x * 50, 0, 0), Quaternion.identity, IslandHolder);
        obj.transform.Rotate(0, 90, 0);

        obj = Instantiate(baseAndWellPrefabs[6], new Vector3(grid.x * 50, 0, grid.z * 50), Quaternion.identity, IslandHolder);
        obj.transform.Rotate(0, 0, 0);
    }

    public bool CheckIfBasePosition(Vector3 pos)
    {
        for (int i = 0; i < basePositions.Count; i++)
        {
            if (pos == basePositions[i])
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckIfWellPosition(Vector3 pos)
    {
        for (int i = 0; i < wellPositions.Count; i++)
        {
            if (pos == wellPositions[i])
            {
                return true;
            }
        }
        return false;
    }

    private void OnApplicationQuit()
    {
        //string json = JsonUtility.ToJson(allSpawnedPrefab, true);
        //File.WriteAllText(Application.dataPath + "/CustomAssets/WFC/Prototype/Spawned.json", json);
    }

    private void processPrefab()
    {
        for (int i = 0; i < selectableIslands.Count; i++)
        {
            GameObject g = selectableIslands[i];
            g.transform.position = Vector3.zero;
            g.transform.rotation = Quaternion.Euler(g.transform.localEulerAngles.x, g.transform.localEulerAngles.y + 0, g.transform.localEulerAngles.z);
            processMesh(g);
            g.transform.rotation = Quaternion.Euler(g.transform.localEulerAngles.x, g.transform.localEulerAngles.y + 90, g.transform.localEulerAngles.z);
            processMesh(g);
            g.transform.rotation = Quaternion.Euler(g.transform.localEulerAngles.x, g.transform.localEulerAngles.y + 90, g.transform.localEulerAngles.z);
            processMesh(g);
            g.transform.rotation = Quaternion.Euler(g.transform.localEulerAngles.x, g.transform.localEulerAngles.y + 90, g.transform.localEulerAngles.z);
            processMesh(g);
        }
    }

    private void processMesh(GameObject prefab)
    {
        examineMesh(prefab, (int)prefab.transform.localEulerAngles.y);
    }

    private void examineMesh(GameObject prefab, int rotationIndex)
    {
        MeshFilter m = prefab.GetComponentInChildren<MeshFilter>();
        List<Vector3> allPositionsx_posbnd = new List<Vector3>();
        List<Vector3> allPositionsx_negbnd = new List<Vector3>();
        List<Vector3> allPositionsz_posbnd = new List<Vector3>();
        List<Vector3> allPositionsz_negbnd = new List<Vector3>();
        Vector3 roundedVertexPosition = new Vector3();

        for (int i = 0; i < m.sharedMesh.vertices.Length; i++)
        {
            roundedVertexPosition.x = (float)System.Math.Round(prefab.transform.TransformPoint(m.sharedMesh.vertices[i]).x, 1);
            roundedVertexPosition.y = (float)System.Math.Round(prefab.transform.TransformPoint(m.sharedMesh.vertices[i]).y, 1);
            roundedVertexPosition.z = (float)System.Math.Round(prefab.transform.TransformPoint(m.sharedMesh.vertices[i]).z, 1);

            if (!allPositionsx_posbnd.Contains(roundedVertexPosition) && roundedVertexPosition.x == boundingUnit.x)
            {
                allPositionsx_posbnd.Add(roundedVertexPosition);
            }

            if (!allPositionsx_negbnd.Contains(roundedVertexPosition) && roundedVertexPosition.x == -boundingUnit.x)
            {
                allPositionsx_negbnd.Add(roundedVertexPosition);
            }

            if (!allPositionsz_posbnd.Contains(roundedVertexPosition) && roundedVertexPosition.z == boundingUnit.z)
            {
                allPositionsz_posbnd.Add(roundedVertexPosition);
            }

            if (!allPositionsz_negbnd.Contains(roundedVertexPosition) && roundedVertexPosition.z == -boundingUnit.z)
            {
                allPositionsz_negbnd.Add(roundedVertexPosition);
            }
        }

        WFCV2_SingleState ss = new WFCV2_SingleState();
        ss.prefab = prefab;
        ss.rotationIndex = rotationIndex;
        ss.right_SocketCode = getOrCreateSocketCode(allPositionsx_posbnd);
        ss.left_SocketCode = getOrCreateSocketCode(allPositionsx_negbnd);
        ss.back_SocketCode = getOrCreateSocketCode(allPositionsz_negbnd);
        ss.front_SocketCode = getOrCreateSocketCode(allPositionsz_posbnd);
        superPosition.Add(ss);
    }

    private string getOrCreateSocketCode(List<Vector3> source)
    {
        string socketCode = null;
        float hashSocket = 0;

        for (int i = 0; i < source.Count - 1; i++)
        {
            hashSocket += hashVertexPos(new Vector3(Mathf.Abs(source[i].x), Mathf.Abs(source[i].y), Mathf.Abs(source[i].z)));
        }
        socketCode = System.Math.Round(hashSocket, 0).ToString();
        if (socketCode != null)
        {
            return socketCode;
        }
        else
        {
            return "-1";
        }
    }

    private float hashVertexPos(Vector3 pos)
    {
        return ((pos.x * 47) + (pos.y * 53) + (pos.z * 59));
    }

    private void allocatePossiblitySpace()
    {
        for (int i = 0; i <= grid.x; i += (int)cellSize)
        {
            for (int j = 0; j <= grid.z; j += (int)cellSize)
            {
                WFCV2_CellInfo ci = new WFCV2_CellInfo();
                ci.cellCoordinate = new Vector3(i, 0, j);
                ci.isCollapsed = false;
                ci.superPosition.AddRange(superPosition);
                allCells.Add(ci);
                if (CheckIfBasePosition(ci.cellCoordinate))
                {
                    baseIndices.Add(allCells.Count - 1);
                }
                if (CheckIfWellPosition(ci.cellCoordinate))
                {
                    wellIndices.Add(allCells.Count - 1);
                }

            }
        }
    }

    private void triggerCollapse()
    {
        int randomCellIndex = Random.Range(0, allCells.Count / 2);
        /*for (int i=0; i<allCells.Count; i++)
        {
            if(allCells[i].cellCoordinate == Vector3.zero)
            {
                randomCellIndex = i;
                break;
            }
        }*/
        Vector3 randomCell = allCells[randomCellIndex].cellCoordinate;
        int randomStateIndex = Random.Range(0, allCells[randomCellIndex].superPosition.Count);
        WFCV2_SingleState ss = allCells[randomCellIndex].superPosition[randomStateIndex];
        tempCord = GetReflectedPosition(randomCell);
        if (ss.prefab.name == "Base" || ss.prefab.name == "Well")
        {
            while (ss.prefab.name == "Base" || ss.prefab.name == "Well")
            {
                ss = FindLowestEntropyAgain();
            }
        }
        spawn(ss.prefab, randomCell, ss.rotationIndex);
        Instantiate(ss.prefab, tempCord, Quaternion.identity, IslandHolder);//flip rotation?
        allCells[randomCellIndex].isCollapsed = true;
        foreach (WFCV2_SingleState wss in allCells[randomCellIndex].superPosition.ToArray())
        {
            if (wss != ss)
            {
                allCells[randomCellIndex].superPosition.Remove(wss);
            }
        }
        cellToProcess.Add(allCells[randomCellIndex]);
    }

    private void propogateCollapseNew()
    {
        Vector3 frontCellDelta = new Vector3(0, 0, cellSize);
        Vector3 backCellDelta = new Vector3(0, 0, -cellSize);
        Vector3 leftCellDelta = new Vector3(-cellSize, 0, 0);
        Vector3 rightCellDelta = new Vector3(cellSize, 0, 0);

        List<string> toProcessFrontSockets = new List<string>();
        List<string> toProcessBackSockets = new List<string>();
        List<string> toProcessLeftSockets = new List<string>();
        List<string> toProcessRightSockets = new List<string>();

        foreach (WFCV2_CellInfo toProceess in cellToProcess.ToArray())
        {
            foreach (WFCV2_SingleState tps in toProceess.superPosition)
            {
                if (!toProcessFrontSockets.Contains(tps.front_SocketCode))
                {
                    toProcessFrontSockets.Add(tps.front_SocketCode);
                }
                if (!toProcessBackSockets.Contains(tps.back_SocketCode))
                {
                    toProcessBackSockets.Add(tps.back_SocketCode);
                }
                if (!toProcessLeftSockets.Contains(tps.left_SocketCode))
                {
                    toProcessLeftSockets.Add(tps.left_SocketCode);
                }
                if (!toProcessRightSockets.Contains(tps.right_SocketCode))
                {
                    toProcessRightSockets.Add(tps.right_SocketCode);
                }
            }

            foreach (WFCV2_CellInfo cellInfo in allCells.ToArray())
            {
                if (cellInfo.cellCoordinate == (toProceess.cellCoordinate + frontCellDelta) && !cellInfo.isCollapsed)
                {
                    foreach (WFCV2_SingleState adjCellSS in cellInfo.superPosition.ToArray())
                    {
                        if (!toProcessFrontSockets.Contains(adjCellSS.back_SocketCode))
                        {
                            cellInfo.superPosition.Remove(adjCellSS);
                            cellToProcess.Add(cellInfo);
                        }
                    }
                }

                if (cellInfo.cellCoordinate == (toProceess.cellCoordinate + leftCellDelta) && !cellInfo.isCollapsed)
                {
                    foreach (WFCV2_SingleState adjCellSS in cellInfo.superPosition.ToArray())
                    {
                        if (!toProcessLeftSockets.Contains(adjCellSS.right_SocketCode))
                        {
                            cellInfo.superPosition.Remove(adjCellSS);
                            cellToProcess.Add(cellInfo);
                        }
                    }
                }

                if (cellInfo.cellCoordinate == (toProceess.cellCoordinate + backCellDelta) && !cellInfo.isCollapsed)
                {
                    foreach (WFCV2_SingleState adjCellSS in cellInfo.superPosition.ToArray())
                    {
                        if (!toProcessBackSockets.Contains(adjCellSS.front_SocketCode))
                        {
                            cellInfo.superPosition.Remove(adjCellSS);
                            cellToProcess.Add(cellInfo);
                        }
                    }
                }

                if (cellInfo.cellCoordinate == (toProceess.cellCoordinate + rightCellDelta) && !cellInfo.isCollapsed)
                {
                    foreach (WFCV2_SingleState adjCellSS in cellInfo.superPosition.ToArray())
                    {
                        if (!toProcessRightSockets.Contains(adjCellSS.left_SocketCode))
                        {
                            cellInfo.superPosition.Remove(adjCellSS);
                            cellToProcess.Add(cellInfo);
                        }
                    }
                }
            }
            cellToProcess.Remove(toProceess);
        }
    }

    private void getLowestEntropyCellAndSpawn()
    {
        int lowestCount = selectableIslands.Count * 400;
        WFCV2_CellInfo lowestEntropyCellInfo = new WFCV2_CellInfo();
        WFCV2_SingleState ss = new WFCV2_SingleState();
        foreach (WFCV2_CellInfo ci in allCells)
        {
            if (!ci.isCollapsed && lowestCount > ci.superPosition.Count)
            {
                lowestCount = ci.superPosition.Count;
                lowestEntropyCellInfo = ci;
            }
        }
        if (lowestCount > 1)
        {
            lowestCount = Random.Range(0, lowestCount);
        }
        ss = lowestEntropyCellInfo.superPosition[lowestCount];
        string diff = GetIslandDifficulty();
        while(ss.prefab.name.Contains(diff) == false)
        {
            ss = FindLowestEntropyAgain();
        }

        adjustedCord = lowestEntropyCellInfo.cellCoordinate;
        adjustedCord = new Vector3(adjustedCord.x * cellSpacing, 0, adjustedCord.z * cellSpacing);
        tempCord = GetReflectedPosition(adjustedCord);
        if (lowestEntropyCellInfo != null)
        {
            if (CheckIfBasePosition(lowestEntropyCellInfo.cellCoordinate))
            {
                spawn(selectableIslands[0], adjustedCord, ss.rotationIndex);
                spawn(selectableIslands[0], tempCord, ss.rotationIndex);
            }
            else if (CheckIfWellPosition(lowestEntropyCellInfo.cellCoordinate))
            {
                spawn(selectableIslands[1], adjustedCord, ss.rotationIndex);
                spawn(selectableIslands[1], tempCord, ss.rotationIndex);
            }
            else
            {
                while (ss.prefab.name.Contains("Base") || ss.prefab.name.Contains("Well") || CheckAdjacency(ss.prefab.name, lowestEntropyCellInfo.cellCoordinate) == false)
                {
                    ss = FindLowestEntropyAgain();
                }
                spawn(ss.prefab, adjustedCord, ss.rotationIndex);
                CheckIfMirrored(ss.prefab);
                if (CheckIfMirrored(ss.prefab))
                {
                    if (ss.prefab.name.Contains("PlanksOnSand"))
                    {
                        if(ss.rotationIndex == 180 || ss.rotationIndex == 0)
                            spawn(mirroredIsland, tempCord, ss.rotationIndex);
                        else
                            spawn(mirroredIsland, tempCord, ss.rotationIndex + 180);
                    }
                    else if(ss.prefab.name.Contains("TilesOnSand"))
                    {
                        if(ss.rotationIndex == 90 || ss.rotationIndex == 270)
                            spawn(mirroredIsland, tempCord, ss.rotationIndex + 180);
                        else
                            spawn(mirroredIsland, tempCord, ss.rotationIndex);
                    }
                    else if(ss.prefab.name.ToLower().Contains("quicksand"))
                    {
                        if (ss.rotationIndex == 90 || ss.rotationIndex == 270)
                            spawn(mirroredIsland, tempCord, ss.rotationIndex + 180);
                        else
                            spawn(mirroredIsland, tempCord, ss.rotationIndex);
                    }
                    else if(ss.prefab.name.Contains("Spring") || ss.prefab.name.Contains("Weeds"))
                    {
                        if(ss.rotationIndex == 90 || ss.rotationIndex == 270)
                            spawn(mirroredIsland, tempCord, ss.rotationIndex + 180);
                        else
                            spawn(mirroredIsland, tempCord, ss.rotationIndex);
                    }
                    else
                    {
                        spawn(mirroredIsland, tempCord, ss.rotationIndex);
                    }
                }
                else
                {
                    spawn(ss.prefab, tempCord, ss.rotationIndex);
                }  

            }

            collapsed++;
        }
        foreach (WFCV2_SingleState wss in lowestEntropyCellInfo.superPosition.ToArray())
        {
            if (wss != ss)
            {
                lowestEntropyCellInfo.superPosition.Remove(wss);
            }
        }
        lowestEntropyCellInfo.isCollapsed = true;
        cellToProcess.Add(lowestEntropyCellInfo);
    }

    private void spawn(GameObject prefab, Vector3 position, int rotationIndex)
    {
        GameObject pf = GameObject.Instantiate(prefab, position, Quaternion.identity, IslandHolder);
        pf.transform.Rotate(0, rotationIndex, 0);
        WFC_Spawned_Data wsd = new WFC_Spawned_Data();
        wsd.prefabName = prefab.name;
        wsd.rotation = pf.transform.rotation;
        wsd.position = position;
        allSpawnedPrefab.wsdList.Add(wsd);
        totalIslandSpaces--;
    }

    private WFCV2_SingleState FindLowestEntropyAgain()
    {
        int lowestCount = selectableIslands.Count * 400;
        WFCV2_CellInfo lowestEntropyCellInfo = new WFCV2_CellInfo();
        foreach (WFCV2_CellInfo ci in allCells)
        {
            if (!ci.isCollapsed && lowestCount > ci.superPosition.Count)
            {
                lowestCount = ci.superPosition.Count;
                lowestEntropyCellInfo = ci;
            }
        }
        if (lowestCount > 1)
        {
            lowestCount = Random.Range(0, lowestCount);
        }

        return lowestEntropyCellInfo.superPosition[lowestCount];
    }

    private Vector3 GetReflectedPosition(Vector3 pos)
    {
        return new Vector3(grid.x * cellSpacing - pos.x, 0, pos.z);
    }

    private bool CheckAdjacency(string name, Vector3 position)
    {
        Vector3 left = new Vector3(position.x - cellSize, 0, position.z);
        Vector3 up = new Vector3(position.x, 0, position.z - cellSize);
        Vector3 diagonalUpLeft = new Vector3(position.x - cellSize, 0, position.z - cellSize);
        Vector3 diagonalDownLeft = new Vector3(position.x - cellSize, 0, position.z + cellSize);

        WFCV2_CellInfo ciLeft = allCells.Find(x => x.cellCoordinate == left);
        WFCV2_CellInfo ciUp = allCells.Find(x => x.cellCoordinate == up);
        WFCV2_CellInfo ciDiagonalUpLeft = allCells.Find(x => x.cellCoordinate == diagonalUpLeft);
        WFCV2_CellInfo ciDiagonalDownLeft = allCells.Find(x => x.cellCoordinate == diagonalDownLeft);
        if (ciLeft != null)
        {
            if (ciLeft.superPosition[0].prefab.name == name)
            {
                return false;
            }

        }
        if (ciUp != null)
        {
            if (ciUp.superPosition[0].prefab.name == name)
            {
                return false;
            }
        }
        if(ciDiagonalUpLeft != null)
        {
            if(ciDiagonalUpLeft.superPosition[0].prefab.name == name)
            {
                return false;
            }
        }
        if (ciDiagonalDownLeft != null)
        {
            if (ciDiagonalDownLeft.superPosition[0].prefab.name == name)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckIfMirrored(GameObject island)
    {
        if (mirroredIslands.Find(x => x.name.ToLower().Contains(island.name.ToLower())))
        {
            mirroredIsland = mirroredIslands.Find(x => x.name.ToLower().Contains(island.name.ToLower()));
            return true;
        }
        return false;
    }

    private string GetIslandDifficulty()
    {
        string str = "";
        int realAmountSpacesLeft = totalIslandSpaces / 2;

        int rand = Random.Range(1, realAmountSpacesLeft + 1);
        Debug.Log("Random number is " + rand);
        if (rand <= hardIslandAmount)
        {
            hardIslandAmount--;
            Debug.Log(hardIslandAmount + " hard islands left");
            str = "Hard";
        }
        else if (rand  <= mediumIslandAmount)
        {
            mediumIslandAmount--;
            Debug.Log(mediumIslandAmount + " medium islands left");
            str = "Medium";
        }
        else if (rand <= easyIslandAmount)
        {
            easyIslandAmount--;
            Debug.Log(easyIslandAmount + " easy islands left");
            str = "Easy";
        }

        return str;

    }
}
