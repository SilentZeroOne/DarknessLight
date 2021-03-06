﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
	//目标位置
	private Vector3 mTarget;
	//屏幕坐标
	private Vector3 mScreen;
	//伤害数值
	public float Value;

	//文本宽度
	public float ContentWidth = 100;
	//文本高度
	public float ContentHeight = 50;

	//GUI坐标
	private Vector2 mPoint;

	//销毁时间
	public float FreeTime = 1f;

	void Start()
	{
		//获取目标位置
		mTarget = transform.position;
		//获取屏幕坐标
		mScreen = Camera.main.WorldToScreenPoint(mTarget);
		//将屏幕坐标转化为GUI坐标
		mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);
		//开启自动销毁线程
		StartCoroutine("Free");
	}

	void Update()
	{
		//使文本在垂直方向山产生一个偏移
		transform.Translate(Vector3.up * 1 * Time.deltaTime);
		//重新计算坐标
		mTarget = transform.position;
		//获取屏幕坐标
		mScreen = Camera.main.WorldToScreenPoint(mTarget);
		//将屏幕坐标转化为GUI坐标
		mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);
	}

	void OnGUI()
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.alignment = TextAnchor.MiddleLeft;
		style.normal.textColor = Color.white;
		//保证目标在摄像机前方
		if (mScreen.z > 0)
		{
			//Debug.Log(GameObject.FindGameObjectWithTag(TagsManager.enemy).GetComponent<WolfBaby>().isMiss);
			if (Value==0)
			{
				GUI.Label(new Rect(mPoint.x, mPoint.y, ContentWidth, ContentHeight), "Miss", style);
			}
			else { 
				GUI.Label(new Rect(mPoint.x, mPoint.y, ContentWidth, ContentHeight), Value.ToString(), style); 
			}
		}
	}

	IEnumerator Free()
	{
		yield return new WaitForSeconds(FreeTime);
		Destroy(gameObject);
	}


}
