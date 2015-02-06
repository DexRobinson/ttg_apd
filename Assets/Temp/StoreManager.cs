using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour {
	public static StoreManager instance;
	public Transform[] itemSlots;
	
	public GameObject[] row1Items;
	public GameObject[] row2Items;
	public GameObject[] row3Items;
	public GameObject[] row4Items;
	public GameObject[] row5Items;
	
	private int itemIndex = 0;
	
	void Awake(){
		instance = this;
		ReplaceItems(0);
	}
	
	void ReplaceItems(int index){
		if(index == 0){
			for(int i = 0; i < row1Items.Length; i++){
				row1Items[i].transform.position = itemSlots[i].position;
			}
			for(int i = 0; i < row2Items.Length; i++){
				row2Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row3Items.Length; i++){
				row3Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row4Items.Length; i++){
				row4Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row5Items.Length; i++){
				row5Items[i].transform.position = new Vector3(100, 100, 0);
			}
		}
		else if(index == 1){
			for(int i = 0; i < row1Items.Length; i++){
				row1Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row2Items.Length; i++){
				row2Items[i].transform.position = itemSlots[i].position;
			}
			for(int i = 0; i < row3Items.Length; i++){
				row3Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row4Items.Length; i++){
				row4Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row5Items.Length; i++){
				row5Items[i].transform.position = new Vector3(100, 100, 0);
			}
		}
		else if(index == 2){
			for(int i = 0; i < row1Items.Length; i++){
				row1Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row2Items.Length; i++){
				row2Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row3Items.Length; i++){
				row3Items[i].transform.position = itemSlots[i].position;
			}
			for(int i = 0; i < row4Items.Length; i++){
				row4Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row5Items.Length; i++){
				row5Items[i].transform.position = new Vector3(100, 100, 0);
			}
		}
		else if(index == 3){
			for(int i = 0; i < row1Items.Length; i++){
				row1Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row2Items.Length; i++){
				row2Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row3Items.Length; i++){
				row3Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row4Items.Length; i++){
				row4Items[i].transform.position = itemSlots[i].position;
			}
			for(int i = 0; i < row5Items.Length; i++){
				row5Items[i].transform.position = new Vector3(100, 100, 0);
			}
		}
		else if(index == 4){
			for(int i = 0; i < row1Items.Length; i++){
				row1Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row2Items.Length; i++){
				row2Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row3Items.Length; i++){
				row3Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row4Items.Length; i++){
				row4Items[i].transform.position = new Vector3(100, 100, 0);
			}
			for(int i = 0; i < row5Items.Length; i++){
				row5Items[i].transform.position = itemSlots[i].position;
			}
		}
	}
	
	public void UpdateItemsRight(){
		itemIndex++;
		
		if(itemIndex > 4)
			itemIndex = 0;
		
		ReplaceItems(itemIndex);
	}
	
	public void UpdateItemsLeft(){
		itemIndex--;
		
		if(itemIndex < 0)
			itemIndex = 4;
		
		ReplaceItems(itemIndex);
	}
}
