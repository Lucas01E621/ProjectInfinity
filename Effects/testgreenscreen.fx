float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float4x4 upscale;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float2 uTargetPosition;

sampler _textureNoise = register(01);
sampler _texture = register(02);

struct VertexShaderInput
{
	float2 uv : TEXCOORD0;
	float4 Position : POSITION0;
	float4 Color : COLOR0;
};

struct VertexShaderOutput
{
	float2 uv : TEXCOORD0;
	float4 Position : POSITION0;
	float4 Color : COLOR0;
};

VertexShaderOutput VertexShader(VertexShaderInput i)
{
    VertexShaderOutput o;
    o.Color = i.Color;
    o.uv = i.uv;
    o.Position = mul(i.Position, upscale)
    return o;
}

float4 FragmentShader(VertexShaderOutput i) : COLOR0
{
    float2 textureNoise = tex2D(_textureNoise, clamp(i.uv * uTime));
    float2 texture = tex2D(_texture, i.uv)
    float2 greenScreen = lerp(float4(0,0,0,0), , textureNoise);
    return greenScreen;
}
    
technique Technique1
{
    pass testGreenscreenPass
    {
        PixelShader = compile ps_2_0 FragmentShader();
    }
}