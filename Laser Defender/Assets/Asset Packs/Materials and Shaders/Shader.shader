Shader "Unlit/ScrollSprite"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _XSpeed ("_XSpeed", Float) = 1
        _YSpeed ("_YSpeed", Float) = 1
    }
 
    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
 
        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha
 
        Pass
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
			
                struct appdata
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 uv : TEXCOORD0;
                };
 
                struct v2f
		{
                    float4 vertex   : SV_POSITION;
                    fixed4 color    : COLOR;
                    float2 uv : TEXCOORD0;
		};
			
		fixed4 _Color;
		fixed _XSpeed;
		fixed _YSpeed;
 
                v2f vert(appdata IN)
		{
		    v2f OUT;
		    OUT.vertex = UnityObjectToClipPos(IN.vertex);
	            OUT.uv = IN.uv;
		    OUT.color = IN.color * _Color;
 
                    return OUT;
                }
 
                sampler2D _MainTex;
                sampler2D _AlphaTex;
 
                fixed4 TextureColor(float2 uv)
                {
                    // Scroll
		    fixed2 offset = fixed2(_XSpeed, _YSpeed) * _Time.y;
 
		    fixed2 newUV = uv;
		    newUV += offset;
 
		    fixed4 color = tex2D(_MainTex, newUV);
		    return color;
		}
 
                fixed4 frag(v2f IN) : SV_Target
                {
	            fixed4 c = TextureColor(IN.uv) * IN.color;
		    c.rgb *= c.a;
		    return c;
		}
	    ENDCG
	}
    }
}