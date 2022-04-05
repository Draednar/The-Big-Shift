Shader "Custom/FirstShaderH"
{
	Properties{
		_Color("MainColor", Color) = (1,1,1,1)


	}

	Subshader
	{
        Tags{
            "RenderPipeline"="UniversalRenderPipeline" "Queue"="Geometry"
        }


		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

            #include "HLSLSupport.cginc" 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			

			uniform half4 _Color;
			struct vertexInput
			{
                float4 vertex : POSITION; //onj space

			};
			struct vertexOutput
			{
                float4 pos : SV_POSITION;

			};

			vertexOutput vert(vertexInput v)
			{
                vertexOutput o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;

			}

			half4 frag(vertexOutput i): COLOR
			{
                //i.pos - screenspace
                //return half4(1,0,0,1);
                return _Color;
			}

			ENDHLSL
		}
	}

	
}
