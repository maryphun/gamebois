Shader "ShockWave"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_TableTex("DistortionTable", 2D) = "white" {}
	}
		SubShader
	{
		Pass
		{
			Cull Off ZWrite Off ZTest Always
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#pragma glsl
#include "UnityCG.cginc"
			uniform sampler2D _MainTex;
			uniform sampler2D _TableTex;
			uniform float     _TimeOffset;
			uniform float     _PosX;
			uniform float     _PosY;
			uniform float     _ShineMag;
			uniform float     _DistortionMag;
			uniform float     _WidthRev;
			uniform float     _AspectRatio;
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			struct v2f
			{
				float2 texcoord  : TEXCOORD0;
				float4 vertex   : SV_POSITION;
				float4 color    : COLOR;
			};
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				return OUT;
			}
			half4 _MainTex_ST;
			float4 frag(v2f i) : COLOR
			{
				float2 org_uv = UnityStereoScreenSpaceUVAdjust(i.texcoord, _MainTex_ST);
				float2 adjusted_uv = org_uv.xy;//距離を算出するため、アスペクト比を考慮したuv
				float2 wave_center = float2(_PosX, _PosY);

				//アスペクト比の考慮
				adjusted_uv.x *= _AspectRatio;
				wave_center.x *= _AspectRatio;

				float2 distortion_dir = adjusted_uv - wave_center;//歪み方向。より外側のテクスチャをpickする
				float dist = distance(adjusted_uv, wave_center);
				distortion_dir /= dist;//正規化
				float offset = dist * _WidthRev - _TimeOffset;

				float distortion = tex2D(_TableTex, float2(offset, 0.5)).r;
				org_uv += distortion_dir * distortion * _DistortionMag;//歪ませる
				float4 color = tex2D(_MainTex, org_uv.xy) + _ShineMag * float4(distortion, distortion, distortion, 0.0);//歪んだ元画像に光をプラス

				return color;
			}
			ENDCG
		}
	}
}