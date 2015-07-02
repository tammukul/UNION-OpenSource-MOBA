Shader "MyShaders/TerrainTwoLayer" {


Properties
{
	_Color ("Main Color", Color) = (1,1,1)
	_MainTex ("Main Texture (RGB)", 2D) = ""
	_PathTex ("Path Texture (RGB)", 2D) = ""
	_PathMask ("Path Mask (A)", 2D) = ""
}

SubShader 
{
	Lighting On
	
	Material 
	{
		Ambient [_Color]
		Diffuse [_Color]
	}

	Pass
	{		
		SetTexture [_MainTex]
		
		SetTexture [_PathMask]
		{
			combine previous, texture
		}
				
		SetTexture [_PathTex]
		{
			combine texture lerp(previous) previous
		}
		
		SetTexture [_MainTex]
		{
			combine previous * primary	
		}
	}
}

SubShader 
{
	Lighting On
	
	Material 
	{
		Ambient [_Color]
		Diffuse [_Color]
	}

	Pass
	{	
		SetTexture [_MainTex]
		{
			combine texture * primary
		}		
	}
	
	Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha
				
		SetTexture [_PathTex]
		{
			combine texture * primary
		}
		
		SetTexture [_PathMask]
		{
			combine previous, texture
		}
	}
}

Fallback "Diffuse"

 
}