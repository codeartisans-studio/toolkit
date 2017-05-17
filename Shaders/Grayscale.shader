Shader "Custom/Grayscale" {
    Properties {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_RampTex ("Ramp Texture", 2D) = "grayscaleRamp" {}
		_RampOffset ("Ramp Offset", Range (-1, 1)) = 0
    }

    SubShader {
    	Cull Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Tags {
			"Queue"="Transparent"
			"RenderType"="Transparent"
		}
		
        pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _RampTex;
			half _RampOffset;

			struct appdata {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata v) {
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target {
//	            float3 lum = float3(0.2125, 0.7154, 0.0721);//转化为luminance亮度值
//
//				fixed4 original = tex2D(_MainTex, i.texcoord);
//				fixed4 output = dot(original.rgb, lum);
//				output.a = original.a;
//
//				return output;

				fixed4 original = tex2D(_MainTex, i.texcoord);
				fixed grayscale = Luminance(original.rgb);
				half2 remap = half2 (grayscale + _RampOffset, .5);
				fixed4 output = tex2D(_RampTex, remap);
				output.a = original.a;

				return output;
			}
			ENDCG
		}
    }
}
