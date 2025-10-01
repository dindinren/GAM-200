using UnityEngine;
using UnityEngine.EventSystems;
public class ConveyerBelt : MonoBehaviour
{
    public int BoxesToSpawn = 3;
    public int SpawnedBoxes;
    public Animator BoxAnimation;
    public GameObject RealBox;

    private bool isSpawning = false; // track if coroutine is running

    void Start()
    {
        SpawnedBoxes = 0;
        StartCoroutine(SpawnBoxesRoutine());
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !isSpawning && SpawnedBoxes < BoxesToSpawn)
        {
            StartCoroutine(SpawnBoxesRoutine());
        }
    }

    private System.Collections.IEnumerator SpawnBoxesRoutine()
    {
        isSpawning = true;
        BoxAnimation.Play("Warehouse",0);
        yield return null;

        yield return new WaitUntil(() =>
        {
            AnimatorStateInfo state = BoxAnimation.GetCurrentAnimatorStateInfo(0);
            return state.IsName("None");
        });

        // Spawn the box
        Instantiate(RealBox, new Vector2(3.82093f, 3.275727f), Quaternion.identity);
        SpawnedBoxes++;

        isSpawning = false;
    }
}
