using UnityEngine;
using System.Collections;

public class BlockCreator : MonoBehaviour {

	private static int H_COUNT = 8;
	private static int V_COUNT = 12;
	private static float EXTRA_CHANCES = 0.15f;
	private static float BIGGER_CHANCES = 0.15f;

	public GameObject blockGroup;

	private float roomHeight;
	private float roomWidth;
	private float cellHeight;
	private float cellWidth;
	public GameObject block;




	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(){
		roomHeight = Camera.main.orthographicSize  * 2;
		roomWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
		cellHeight = (roomHeight / V_COUNT) * 0.4f;
		cellWidth = (roomWidth / H_COUNT) * 0.8f;
		initBlockSize ();
	}

	private void initBlockSize(){
		MeshFilter mf = block.GetComponent<MeshFilter>();
		if (mf == null) 
			return;
		Mesh mesh = mf.sharedMesh;
		Bounds bounds = mesh.bounds;
		

		float scaleX = cellWidth /  bounds.size.x;
		float scaleY = cellHeight /  bounds.size.z;
		
		Vector3[] verts = mesh.vertices;
		
		for (int i = 0; i < verts.Length; i++) {
			verts[i] = new Vector3( verts[i].x * scaleX,verts[i].y ,verts[i].z * scaleY);    
		}
		
		mesh.vertices = verts;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		BoxCollider b = block.GetComponent<Collider>() as BoxCollider;
		b.size = new Vector3(cellWidth, b.size.y, cellHeight);
	}

	public void create(){
		GameObject group = Instantiate(blockGroup) as GameObject;
		for(int i=0;i<H_COUNT;i++){
			for(int j=0;j<V_COUNT;j++){
				createOneBlock(group,i,j);
			}
		}
	}

	private void createOneBlock(GameObject group,int i,int j){
		GameObject newObj = Instantiate(block) as GameObject;
		float x = i * cellWidth;
		float y = j * cellHeight;
		float shiftY = (roomHeight -(cellHeight * V_COUNT) ) * 0.2f;
		float shiftX = ( (cellWidth*H_COUNT)/2) - (cellWidth/2)  ;
		newObj.transform.position = new Vector3 (x - shiftX ,0,y - shiftY);
		
		Block b = newObj.GetComponent<Block> ();
		b.setupHp ();
		newObj.transform.parent = group.transform;
	}



	private Vector3 getBoxSize(GameObject go){
		BoxCollider box = go.GetComponent<BoxCollider> ();
		return box.size;
	}
}
