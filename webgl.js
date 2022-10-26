/*
Small WebGL test based on Indigo Codes(Youtube) tutorial:
https://www.youtube.com/watch?v=kB0ZVUrI4Aw
*/

const vertexShaderText = 
[    
    'precision mediump float;',
    '',
    'attribute vec2 vertPosition;',
    'attribute vec3 vertColor;',
    'varying vec3 fragColor;',
    '',
    'void main()',
    '{',
    '   fragColor = vertColor;',
    '   gl_Position = vec4(vertPosition, 0.0, 1.0);',
    '}'
].join('\n');

const fragmentShaderText = 
[
    'precision mediump float;',
    '',
    'varying vec3 fragColor;',
    'uniform float uTime;',
    'void main()',
    '{',
    '   gl_FragColor = vec4(fragColor, 1.0) + (0.25 + 0.25 * vec4(cos(uTime + fragColor.xyx + vec3(0,3,6)), 0)) ;',
    '}'
].join('\n');

var InitDemo = function (){

console.log("This is working");

var canvas = document.querySelector('#glcanvas');
const gl = canvas.getContext('webgl');

if (!gl){
    alert('Your browser does not support webgl.');
}

gl.clearColor(0.0, 0.0, 1.0, 1.0);
gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

const vertexShader = gl.createShader(gl.VERTEX_SHADER);
const fragmentShader = gl.createShader(gl.FRAGMENT_SHADER);

gl.shaderSource(vertexShader, vertexShaderText);
gl.shaderSource(fragmentShader, fragmentShaderText);


gl.compileShader(vertexShader);
if (!gl.getShaderParameter(vertexShader, gl.COMPILE_STATUS)){
    console.error('ERROR compiling vertex shader', gl.getShaderInfoLog(vertexShader))
    return;
}
gl.compileShader(fragmentShader);
if (!gl.getShaderParameter(fragmentShader, gl.COMPILE_STATUS)){
    console.error('ERROR compiling fragment shader', gl.getShaderInfoLog(fragmentShader))
    return;
}

const program = gl.createProgram();
gl.attachShader(program, vertexShader);
gl.attachShader(program, fragmentShader);
gl.linkProgram(program);
if(!gl.getProgramParameter(program, gl.LINK_STATUS)){
    console.error('ERROR linkin program', gl.getProgramInfoLog(program));
    return;
}

gl.validateProgram(program);
if(!gl.getProgramParameter(program, gl.VALIDATE_STATUS)){
    console.error('ERROR validating program', gl.getProgramInfoLog(program));
    return;
}


const triangleVertices = 
[
    -1.0, 1.0,          1, 0, 0,
    -1.0, -1.0,         1, 1, 0,
    1.0, -1.0,          0, 1, 1,
    1.0, 1.0,           0, 0, 1    
];

const indices = [3, 2, 1, 3, 1, 0];

const triangleVertexBufferObject = gl.createBuffer();
gl.bindBuffer(gl.ARRAY_BUFFER, triangleVertexBufferObject);
gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(triangleVertices), gl.STATIC_DRAW);

const indexBufferObject = gl.createBuffer();
gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indices), gl.STATIC_DRAW);

const positionAttributeLocation = gl.getAttribLocation(program, 'vertPosition');
const colorAttributeLocation = gl.getAttribLocation(program, 'vertColor');
const timeAttributeLocation = gl.getUniformLocation(program, 'uTime');

gl.vertexAttribPointer(
    positionAttributeLocation,
    2,
    gl.FLOAT,
    gl.FALSE,
    5 * Float32Array.BYTES_PER_ELEMENT,
    0
);
gl.vertexAttribPointer(
    colorAttributeLocation,
    3,
    gl.FLOAT,
    gl.FALSE,
    5 * Float32Array.BYTES_PER_ELEMENT,
    2 * Float32Array.BYTES_PER_ELEMENT
);

gl.enableVertexAttribArray(positionAttributeLocation);
gl.enableVertexAttribArray(colorAttributeLocation);
gl.useProgram(program);

function render(time){
    gl.uniform1f(timeAttributeLocation, time / 1000);
    requestAnimationFrame(render);
    gl.drawElements(gl.TRIANGLES, indices.length, gl.UNSIGNED_SHORT, 0);
    }    
requestAnimationFrame(render);
};