using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
public class RequireTagAttribute : Attribute {

	private readonly string tag;

	public RequireTagAttribute(string tag){
		Debug.Log("TAG: " + tag);
		this.tag = tag;
	}
}
