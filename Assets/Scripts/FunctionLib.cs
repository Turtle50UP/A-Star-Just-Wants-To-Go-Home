using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionLib{

	public float Sigmoid(float x, float shift, float stretch)
	{
		return (1.0f / (1.0f + Mathf.Exp(-(stretch * (x + shift)))));
	}

	public float TwoWaySigmoid(float x, float stretch){
		return (2.0f / (1.0f + Mathf.Exp(-(stretch * x)))) - 1.0f;
	}
}
