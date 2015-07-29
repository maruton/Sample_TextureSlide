/*
 *	@file	TexAnimate_Test.cs
 *	@note		なし 
 *	@attention	
 *				[TexAnimate_Test.cs]
 *				Copyright (c) [2015] [Maruton]
 *				This software is released under the MIT License.
 *				http://opensource.org/licenses/mit-license.php
 */
using UnityEngine;
using System.Collections;

public class TexAnimate_Test : MonoBehaviour {
	void Awake(){}
	void Start (){
		Init();
	}
	void OnApplicationFocus(){}
	void OnApplicationQuit(){}
	void OnApplicationPause(bool pauseStatus) {
		if(pauseStatus){ //Suspend (back to OS)
		}
		else{ //Resume (From OS)
		}
	}
	void OnGUI(){
		DrawGUI(); // 操作ボタン、2D文字描画 
	}
	void Update() {
		//Debug.Log ("Update:");
		if(Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape)){//戻るボタン対応 
			Application.Quit();
		}
		AnimateTexture(); // テクスチャスライド
	}
	//============================================================

	public float tex_offsetX = 0.0f; //!< テクスチャオフセット X。※Editorから操作も可 
	public float tex_offsetY = 0.0f; //!< テクスチャオフセット Y。※Editorから操作も可 

	public float C_tex_Add = 0.001f; //!< テクスチャ スライド分解能 
	float tex_Add;
	void Init(){
		tex_Add = -C_tex_Add;
	}
	void AnimateTexture(){
		tex_offsetX += tex_Add;
		if(tex_offsetX<=0.0f){
			tex_offsetX = 1.0f - C_tex_Add;
		}
		if(tex_offsetX>=1.0f){
			tex_offsetX = 0.0f;
		}
		//	See detail: https://docs.unity3d.com/Documentation/ScriptReference/Material.SetTextureOffset.html
		//renderer.material.SetTextureOffset("_BumpMap", new Vector2(tex_offsetX, tex_offsetY));// Mobile/Bumpoed Diffuseでは不要
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(tex_offsetX, tex_offsetY));// Mobile/Bumpoed Diffuse ではNormal Mapも同時に動く
	}

	void DrawGUI(){
		//if(true)return;
		//Debug.Log ("OnGUI:");
		GUIStyle labelStyle;	//!< GUIフォント表示用スタイル
		int fontRetio = 18;
		
		labelStyle = new GUIStyle();
		labelStyle.fontSize = Screen.width / fontRetio; // Font size
		labelStyle.normal.textColor = Color.grey;
		labelStyle.wordWrap = true;
		GUI.Label(new Rect(0, 0, Screen.width-100, Screen.height-100), "Offset X="+tex_offsetX+"\nOffset Y="+tex_offsetY, labelStyle);

		float ScrnHor = (float)Screen.width;
		float ScrnVert = (float)Screen.height;
		Rect rectBtn = new Rect(ScrnHor*0.12f,ScrnVert*0.10f,ScrnHor*0.76f,ScrnVert*0.05f);
		if(GUI.Button(rectBtn, "[SLOW FWD]")) {
			tex_Add = -C_tex_Add;
		}
		rectBtn = new Rect(ScrnHor*0.12f,ScrnVert*0.15f,ScrnHor*0.76f,ScrnVert*0.05f);
		if(GUI.Button(rectBtn, "[SLOW BWD]")) {
			tex_Add = C_tex_Add;
		}
		rectBtn = new Rect(ScrnHor*0.12f,ScrnVert*0.20f,ScrnHor*0.76f,ScrnVert*0.05f);
		if(GUI.Button(rectBtn, "[STOP]")) {
			tex_Add = 0.0f;
		}
		rectBtn = new Rect(ScrnHor*0.12f,ScrnVert*0.25f,ScrnHor*0.76f,ScrnVert*0.05f);
		if(GUI.Button(rectBtn, "[FAST FWD]")) {
			tex_Add = -C_tex_Add * 10.0f;
		}
		rectBtn = new Rect(ScrnHor*0.12f,ScrnVert*0.30f,ScrnHor*0.76f,ScrnVert*0.05f);
		if(GUI.Button(rectBtn, "[FAST BWS]")) {
			tex_Add = C_tex_Add * 10.0f;
		}
	}
}
