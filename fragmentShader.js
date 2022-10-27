precision mediump float;
    
    varying vec3 fragColor;
    uniform float uTime;

    void main()
    {
       gl_FragColor = vec4(fragColor, 1.0) + (0.25 + 0.25 * vec4(cos(uTime + fragColor.xyx + vec3(0,3,6)), 0));
    }
