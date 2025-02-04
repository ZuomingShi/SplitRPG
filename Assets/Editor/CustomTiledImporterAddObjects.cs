﻿using UnityEngine;
using System.Collections.Generic;
using Tiled2Unity;
using UnityEditor;
using System.Collections;

[Tiled2Unity.CustomTiledImporter(Order = 1)]
class CustomTiledImporterAddObjects : Tiled2Unity.ICustomTiledImporter{

	private string pathPrefix = PrefabMapper.PrefabLocation;
	//Need to change name to "layerToHeightMap"
	private Dictionary<string, string> prefabMap;
	private string mapName;

	public void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> props) {
		if(gameObject.transform.parent == null){
			if(props.ContainsKey("map")){
				prefabMap = PrefabMapper.maps[props["map"]];
				//Added backslash here so that we can use load the default map without inserting a conditional.
				mapName = props["map"] + "/";
			}else{
				Debug.LogWarning("Map does not contain requisite 'map' property, default used.");
				prefabMap = PrefabMapper.maps["default"];
				Utils.assert(prefabMap != null);
				mapName = "";
			}
		}
	}
	
	public void CustomizePrefab(GameObject prefab) {
		CreateObjectsInLayer (prefab, "Objects(Invisible)");
		CreateObjectsInLayer (prefab, "Pushblocks(Default)");

	}

	private void CreateObjectsInLayer(GameObject prefab, string s){
		Transform invisibleLayerTransform;
		GameObject invisibleLayer;
		if (invisibleLayerTransform = Utils.FindChildRecursive(prefab,s)) {
			invisibleLayer = invisibleLayerTransform.gameObject;
			
			foreach(Transform obj in invisibleLayer.transform)
			{
				string name = obj.gameObject.name.ToLower();
				
				//Debug.Log(name);
				//Debug.Log(mapName);
				//Debug.Log(prefabMap);
				if (!Utils.assert(prefabMap.ContainsKey(name))){Debug.LogError ("Prefab with name " + name + " cannot be found.");}
				
				GameObject item = AssetDatabase.LoadAssetAtPath(pathPrefix + mapName + prefabMap[name] + ".prefab", typeof(GameObject)) as GameObject;
				if(item != null){
					//Add Vector3.one to offset center differences.
					item = (GameObject)GameObject.Instantiate(item, obj.transform.position, obj.transform.rotation);
					
					_Mono itemMono = item.GetComponent<_Mono>();
					itemMono.xs /= Utils.TILED_TO_UNITY_SCALE;
					itemMono.ys /= Utils.TILED_TO_UNITY_SCALE;
					itemMono.x += itemMono.xs;
					itemMono.y -= itemMono.ys;
					//itemMono.x += itemMono.xs/2;
					//itemMono.y -= itemMono.ys/2;
					item.name = name;
					//itemMono.x = itemMono.x + Globals.PIXELS_PER_TILE/2;
					//itemMono.y = itemMono.y + Globals.PIXELS_PER_TILE/2;
					item.transform.parent = obj;
					obj.name = obj.name + "Parent";
					item.layer = invisibleLayer.layer;
					
					HeightScript hs = Utils.GetHeightScript(item);
					hs.height = 2;
				}else{
					Debug.LogWarning("Item name " + name + "not recognized");
				}
				//GameObject.Destroy(child);
			}
			
		}
	}

}