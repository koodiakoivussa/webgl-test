    precision mediump float;
    
    varying vec3 fragColor;
    uniform float uTime;
    uniform vec2 uResolution;

    void main()
    {
        // Normalize resolution and coordinates
        vec2 res = uResolution;
        res = res / res;        
        vec2 uv = (fragColor.xy - res.xy) / res.y;
        uv = fract(uv);

        uv *= 1.;
        uv = fract(uv);


        vec3 colors = vec3(uv, 0.1) + 0.25 + 0.25 * vec3(cos(uTime + uv.xyx + vec3(1,2,3)));


        gl_FragColor = vec4(colors, 1.);
    }
