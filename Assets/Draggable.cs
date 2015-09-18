using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	private Transform originalParent = null;
	private int originalIndex;
	private bool returnToOriginal = false;

//	private Transform placeholderParent = null;
	static GameObject placeholder = null;

	void Start()
	{
		if (placeholder == null) {
			placeholder = GameObject.FindGameObjectWithTag("DropIndicator");
			placeholder.SetActive(false);
		}
	}

	public void OnBeginDrag(PointerEventData eventData) {
		originalParent = this.transform.parent;
		originalIndex = this.transform.GetSiblingIndex ();

		// same location as the dragable
		placeholder.SetActive (true);
		placeholder.transform.SetParent( originalParent );
		placeholder.transform.SetSiblingIndex( originalIndex );

//		placeholderParent = originalParent;

		Canvas canvas = (Canvas)GameObject.FindObjectOfType ( typeof(Canvas));

        this.transform.SetParent( canvas.transform );
		this.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	
	public void OnDrag(PointerEventData eventData) {

		this.transform.position = eventData.position;
		var parent = placeholder.transform.parent;
		if( parent != null && !returnToOriginal)
		{
			int newSiblingIndex = parent.childCount;

			for(int i=0; i < parent.childCount; i++) {
				if(this.transform.position.x < parent.GetChild(i).position.x) {

					newSiblingIndex = i;

					if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
						newSiblingIndex--;

					break;
				}
			}

			placeholder.transform.SetSiblingIndex(newSiblingIndex);
		}
	}
	
	public void OnEndDrag(PointerEventData eventData) {
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		this.transform.SetParent( placeholder.transform.parent );
		this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );

		Canvas canvas = (Canvas)GameObject.FindObjectOfType ( typeof(Canvas));
		placeholder.transform.SetParent (canvas.transform);
		placeholder.SetActive (false);
	}

	public void EnteringDropZone( Transform dropZone)
	{
//		Debug.Log ("entering drop zone " + dropZone.name);
		returnToOriginal = false;
		placeholder.transform.SetParent (dropZone);
		placeholder.transform.SetSiblingIndex (0);
	}

	public void LeavingDropZone( Transform dropZone)
	{
//		Debug.Log ("Leaving drop zone " + dropZone.name);
		placeholder.transform.SetParent(originalParent);
//		Debug.Log("Set new sibling index " + originalIndex);
		placeholder.transform.SetSiblingIndex( originalIndex);
		returnToOriginal = true;
	}
}