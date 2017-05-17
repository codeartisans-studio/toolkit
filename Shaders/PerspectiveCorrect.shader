Shader "Custom/Perspective Correct" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
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

			sampler2D _MainTex;

			struct vertexInput {
				float4 vertex : POSITION;
				float2 texcoord0 : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
			};

			vertexOutput vert(vertexInput input) {
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);

				output.uv0 = input.texcoord0;
				output.uv1  = input.texcoord1;

				return output;
			}

			fixed4 frag(vertexOutput input) : SV_Target {
				return tex2D(_MainTex, input.uv0 / input.uv1);
			}
			ENDCG
		}
    }
}
