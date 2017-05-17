Shader "Custom/Sobel" {
	Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
	}

    SubShader {
    	Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Tags {
			"Queue"="Transparent"
			"RenderType"="Transparent"
		}

        Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;

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
	            float3 lum = float3(0.2125, 0.7154, 0.0721);//转化为luminance亮度值

	            //获取当前点的周围的点
	            //并与luminance点积，求出亮度值（黑白图）
	            float mc00 = dot (tex2D (_MainTex, i.texcoord - fixed2(_MainTex_TexelSize.x, _MainTex_TexelSize.y)).rgb, lum);
	            float mc10 = dot (tex2D (_MainTex, i.texcoord - fixed2(0, _MainTex_TexelSize.y)).rgb, lum);
	            float mc20 = dot (tex2D (_MainTex, i.texcoord - fixed2(-_MainTex_TexelSize.x, _MainTex_TexelSize.y)).rgb, lum);
	            float mc01 = dot (tex2D (_MainTex, i.texcoord - fixed2(_MainTex_TexelSize.x, 0)).rgb, lum);
	            float mc11mc = dot (tex2D (_MainTex, i.texcoord).rgb, lum);
	            float mc21 = dot (tex2D (_MainTex, i.texcoord - fixed2(-_MainTex_TexelSize.x, 0)).rgb, lum);
	            float mc02 = dot (tex2D (_MainTex, i.texcoord - fixed2(_MainTex_TexelSize.x, -_MainTex_TexelSize.y)).rgb, lum);
	            float mc12 = dot (tex2D (_MainTex, i.texcoord - fixed2(0, -_MainTex_TexelSize.y)).rgb, lum);
	            float mc22 = dot (tex2D (_MainTex, i.texcoord - fixed2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y)).rgb, lum);

	            //根据过滤器矩阵求出GX水平和GY垂直的灰度值
	            float GX = -1 * mc00 + mc20 + -2 * mc01 + 2 * mc21 - mc02 + mc22;
	            float GY = mc00 + 2 * mc10 + mc20 - mc02 - 2 * mc12 - mc22;

	            fixed4 color = length(float2(GX, GY));//length的内部算法就是灰度公式的算法，欧几里得长度

				return color;
			}
			ENDCG
        }
    }
}
