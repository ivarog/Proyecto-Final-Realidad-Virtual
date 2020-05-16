  using UnityEngine;
  using UnityEngine.AI;
 
 public class RandomTree : MonoBehaviour {
 
     public GameObject tree;
     public float minTreeSize;
     public float maxTreeSize;
     public Texture2D noiseImage;
     public float forestSize;
     public float treeDensity;
     public NavMeshSurface navMeshSurface;
     public float yPos;
     public string tagObject;
     public float areaAroundBonfire;
 
     private float baseDensity = 5.0f;
 
     // Use this for initialization
     void Start () 
     {
        Generate();
        navMeshSurface.BuildNavMesh();
     }
 
     public void Generate() {
 
        for(int y = 0; y < forestSize; y++) {
 
            for(int x = 0; x < forestSize; x++) {
 
                float chance = noiseImage.GetPixel(x, y).r / (baseDensity/treeDensity);

                float posX = x + transform.position.x - (forestSize / 2);
                float posZ = y + transform.position.z - (forestSize / 2);


                if (ShouldPlaceTree(chance) && !IsAroundBonfire(posX, posZ)) 
                {
                    float size = Random.Range(minTreeSize, maxTreeSize);

                    GameObject newTree = Instantiate(tree, transform);
                    newTree.transform.localScale = Vector3.one * size;
                    newTree.transform.position = new Vector3(posX, -yPos, posZ);
                    newTree.transform.parent = transform;
                    Vector3 euler = transform.eulerAngles;
                    euler.y = Random.Range(0f, 360f);
                    newTree.transform.eulerAngles = euler;
                }
            }
        }
    }

     public void Clean() 
     {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tagObject);
        foreach(GameObject obj in allObjects) 
        {
            DestroyImmediate(obj);
        }
    }
        
 
     //Returns true or false given some chance from 0 to 1
    public bool ShouldPlaceTree(float chance) {
         if (Random.Range(0.0f, 1.0f) <= chance) {
             return true;
         }
         return false;
     }

    bool IsAroundBonfire(float x, float z)
    {
        if(x <= areaAroundBonfire && x >= -areaAroundBonfire && z <= areaAroundBonfire && z >= -areaAroundBonfire)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
 }