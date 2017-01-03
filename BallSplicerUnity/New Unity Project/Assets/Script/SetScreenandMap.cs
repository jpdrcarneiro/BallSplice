using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenandMap : MonoBehaviour {

    public GameObject floorTile;
    float centerX;
    float centerY;
    float centerZ;

    // Use this for initialization
    void Start () {

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();


        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

        setMapMobile();

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

        


        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
		{
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}


    void setMapMobile()
    {
        CreateGrid();
        float windowWidth = (float)Screen.width,
    windowHeight = (float)Screen.height;

        Debug.Log("width = " + windowWidth + "; height = " + windowHeight);

       Vector2 tileSize = new Vector2(floorTile.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, floorTile.GetComponent<MeshFilter>().sharedMesh.bounds.size.y);
      //  tileSize[0] = floorTile.rect.width;
        //tileSize[1] = floorTile.rect.height;
//          tileSize =  floorTile.GetComponent<MeshFilter>().mesh.bounds.size;
        Debug.Log(tileSize);

        float scaleX = (windowWidth/20),
                scaleY = windowHeight/40;

        float numberOfColumns = windowWidth / scaleX,
            numberOfRows = windowHeight / scaleY;

        

    }

    void CreateGrid()
    {
        float gridX = 15f;
        float gridY = 30f;
        float spacing = 0.5f;

        Vector3 intialPos = new Vector3(3, 7.5f, 0);



        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 orgPos = new Vector3(x, y, 0) * spacing;
                Vector3 pos = intialPos - orgPos;
                Instantiate(floorTile, pos, Quaternion.identity);
            }
        }

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Floor");
        float[] centerPointsX = new float[tiles.Length];
        float[] centerPointsY = new float[tiles.Length];
        float[] centerPointsZ = new float[tiles.Length];



        for (int z = 0; z < tiles.Length; z++)
        {
            centerPointsX[z] = tiles[z].transform.position.x;
            centerPointsY[z] = tiles[z].transform.position.y;
            centerPointsZ[z] = tiles[z].transform.position.z;
        }
        for (int z = 0; z < tiles.Length; z++)
        {
            centerX += centerPointsX[z];
            centerY += centerPointsY[z];
            centerZ += centerPointsZ[z];
        }
        centerX = centerX / (tiles.Length + 1);
        centerY = centerY / (tiles.Length + 1);
        centerZ = centerZ / (tiles.Length + 1);


        Vector3 bouncerUpCenter = new Vector3(centerX, 0.5f+centerY+(0.5f*gridY * (tiles[14].transform.position.y - tiles[15].transform.position.y)), 0f);
        Vector3 bouncerUpSize = new Vector3((gridX * (tiles[3].transform.position.x - tiles[2].transform.position.x)), 1f, 10f);
        createDivision(bouncerUpCenter, bouncerUpSize);

        Vector3 bouncerDownCenter = new Vector3(centerX, centerY - 0.5f - (0.5f * gridY * (tiles[14].transform.position.y - tiles[15].transform.position.y)), 0f);
        Vector3 bouncerDownSize = new Vector3((gridX * (tiles[3].transform.position.x - tiles[2].transform.position.x)), 1f, 10f);
        createDivision(bouncerDownCenter, bouncerDownSize);

        Vector3 bouncerRightCenter = new Vector3(centerX-0.5f-(0.5f*gridX*(tiles[2].transform.position.x - tiles[3].transform.position.x)), centerY, 0f);
        Vector3 bouncerRightSize = new Vector3(1f, gridY* (tiles[14].transform.position.y - tiles[15].transform.position.y), 10f);
        createDivision(bouncerRightCenter, bouncerRightSize);

        Vector3 bouncerLeftCenter = new Vector3(centerX + 0.5f + (0.5f * gridX * (tiles[2].transform.position.x - tiles[3].transform.position.x)), centerY, 0f);
        Vector3 bouncerLeftSize = new Vector3(1f, gridY * (tiles[14].transform.position.y - tiles[15].transform.position.y), 10f);
        createDivision(bouncerLeftCenter, bouncerLeftSize);

        Vector3 centroid = new Vector3(centerX, centerY, centerZ);
       // Debug.Log(centroid);

       

    }
    void createDivision (Vector3 bouncerCenter, Vector3 bouncerSize)
    {
        var bouncer = new GameObject();
        bouncer.tag = "bouncer";
        bouncer.AddComponent<BoxCollider>();
        BoxCollider bouncerCollider;
        bouncerCollider = bouncer.GetComponent<BoxCollider>();
        bouncerCollider.size = bouncerSize;
        bouncerCollider.center = bouncerCenter;
        PhysicMaterial wallMat = new PhysicMaterial();
        wallMat.dynamicFriction = 0.0f;
        wallMat.staticFriction = 0.0f;
        wallMat.bounciness = 1.0f;
        wallMat.bounceCombine = PhysicMaterialCombine.Maximum;
        bouncerCollider.material = wallMat;
    }
    void cleanBouncers()
    {
        GameObject[] bouncers;
        bouncers = GameObject.FindGameObjectsWithTag("bouncer");
        foreach (GameObject bounce in bouncers)
        {
            Destroy(bounce);
        }
       
    }
}

