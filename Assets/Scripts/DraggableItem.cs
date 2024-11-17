using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData itemData;
    private Vector3 screenPoint;
    private Vector3 offset;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    [SerializeField] private Material originalMaterial;
    private const float hoverHeight = 0.5f;
    private Renderer itemRenderer;

    [Header("Grid Settings")]
    public GridLayout gridLayout;
    public bool snapToGrid = true;

    private bool isInBag;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        itemRenderer = GetComponentInChildren<Renderer>();
        originalMaterial = itemRenderer.material;
    }

    private void OnMouseDown()
    {
        if (!GameManager.Instance.isGameActive) { return; }
        //print("OnMouseDown");
        isDragging = true;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        // disable physics while dragging
        rb.isKinematic = true;

        // visual feedback
       // itemRenderer.material.color = Color.green;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) { return; }
        //print("OnMouseDrag");
        Vector3 curScreenPoint = new(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        // keep item at consistent height while dragging
        curPosition.y = originalPosition.y + hoverHeight;

        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        if (!isDragging) { return; }
        //print("OnMouseUp");
        isDragging = false;
        rb.isKinematic = false;

        // reset material
        itemRenderer.material = originalMaterial;

        // check if item is over the bag
        //Ray ray = new(transform.position, Vector3.down);

        //if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        //if (hit.collider.TryGetComponent<Bag>(out var bag))
        if (isInBag)
        {
            print("BAG");
            //  let gravity handle the drop
            GameManager.Instance.OnItemDropped(this);
        }
        else if (snapToGrid && gridLayout != null)
        {
            // nearest grid position
            SnapToNearestGridPosition();
        }
        else
        {
            // return to original position if not over bag
            ReturnToStart();
        }
    }

    private void SnapToNearestGridPosition()
    {
        Vector3 currentPos = transform.position;

        // nearest grid cell
        float cellSize = gridLayout.cellSize;
        Vector3 gridOrigin = gridLayout.transform.position;

        // local position relative to grid
        Vector3 localPos = currentPos - gridOrigin;

        // round to nearest cell
        int xCell = Mathf.RoundToInt(localPos.x / cellSize);
        int zCell = Mathf.RoundToInt(localPos.z / cellSize);

        // new position
        Vector3 newPos = gridOrigin + new Vector3(
            xCell * cellSize,
            gridLayout.itemHeight,
            zCell * cellSize
        );

        // if position is within grid bounds
        if (gridLayout.IsValidGridPosition(newPos))
        {
            transform.position = newPos;
            originalPosition = newPos; 
        }
        else
        {
            ReturnToStart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bag")
            isInBag = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Bag")
            isInBag = false;
    }

    public void ReturnToStart()
    {
        transform.SetPositionAndRotation(originalPosition, originalRotation);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        print("begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        print("drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        print("end");
    }
}
