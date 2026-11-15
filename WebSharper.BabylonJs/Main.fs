#nowarn "20"   // `Class |+> ...` definitions are intentionally statements
#nowarn "3886" // single-element option-field lists are intentional

namespace WebSharper.BabylonJs.Extension

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =

    // ==================================================================
    // Helpers
    // ==================================================================

    /// A class exported by a Babylon.js module: `import { Name } from path`.
    let private Imported (path: string) (c: CodeModel.Class) =
        Import c.Name path c

    /// A class of static numeric constants (Babylon uses `const enum`-like
    /// objects for its enums in the ESM build).
    let private IntEnum (name: string) (values: (string * int) list) =
        Class name
        |+> Static [
            for (n, v) in values ->
                Getter n T<int> |> WithGetterInline (string v) :> _
        ]

    // ==================================================================
    // Enums / constant tables
    // ==================================================================

    let PowerPreference =
        Pattern.EnumStrings "PowerPreference" [
            "default"
            "high-performance"
            "low-power"
        ]

    let Space =
        IntEnum "Space" [
            "Local", 0
            "World", 1
            "Bone", 2
        ]

    let PointerEventTypes =
        IntEnum "PointerEventTypes" [
            "PointerDown", 1
            "PointerUp", 2
            "PointerMove", 4
            "PointerWheel", 8
            "PointerPick", 16
            "PointerTap", 32
            "PointerDoubleTap", 64
        ]

    let TextureSamplingMode =
        IntEnum "TextureSamplingMode" [
            "ExplicitMode", 0
            "NearestSamplingMode", 1
            "BilinearSamplingMode", 2
            "TrilinearSamplingMode", 3
        ]

    let AddressMode =
        IntEnum "AddressMode" [
            "ClampAddressMode", 0
            "WrapAddressMode", 1
            "MirrorAddressMode", 2
        ]

    // ==================================================================
    // Class shells (declared first so the members below can refer to them)
    // ==================================================================

    let Vector2 =
        Class "Vector2"
        |> Imported "@babylonjs/core/Maths/math.vector.js"

    let Vector3 =
        Class "Vector3"
        |> Imported "@babylonjs/core/Maths/math.vector.js"

    let Vector4 =
        Class "Vector4"
        |> Imported "@babylonjs/core/Maths/math.vector.js"

    let Quaternion =
        Class "Quaternion"
        |> Imported "@babylonjs/core/Maths/math.vector.js"

    let Matrix =
        Class "Matrix"
        |> Imported "@babylonjs/core/Maths/math.vector.js"

    let Color3 =
        Class "Color3"
        |> Imported "@babylonjs/core/Maths/math.color.js"

    let Color4 =
        Class "Color4"
        |> Imported "@babylonjs/core/Maths/math.color.js"

    let Node =
        Class "Node"
        |> Imported "@babylonjs/core/node.js"

    let TransformNode =
        Class "TransformNode"
        |> Imported "@babylonjs/core/Meshes/transformNode.js"
        |=> Inherits Node

    let AbstractMesh =
        Class "AbstractMesh"
        |> Imported "@babylonjs/core/Meshes/abstractMesh.js"
        |=> Inherits TransformNode

    let Mesh =
        Class "Mesh"
        |> Imported "@babylonjs/core/Meshes/mesh.js"
        |=> Inherits AbstractMesh

    let GroundMesh =
        Class "GroundMesh"
        |> Imported "@babylonjs/core/Meshes/groundMesh.js"
        |=> Inherits Mesh

    let LinesMesh =
        Class "LinesMesh"
        |> Imported "@babylonjs/core/Meshes/linesMesh.js"
        |=> Inherits Mesh

    let Camera =
        Class "Camera"
        |> Imported "@babylonjs/core/Cameras/camera.js"
        |=> Inherits Node

    let TargetCamera =
        Class "TargetCamera"
        |> Imported "@babylonjs/core/Cameras/targetCamera.js"
        |=> Inherits Camera

    let ArcRotateCamera =
        Class "ArcRotateCamera"
        |> Imported "@babylonjs/core/Cameras/arcRotateCamera.js"
        |=> Inherits TargetCamera

    let FreeCamera =
        Class "FreeCamera"
        |> Imported "@babylonjs/core/Cameras/freeCamera.js"
        |=> Inherits TargetCamera

    let Light =
        Class "Light"
        |> Imported "@babylonjs/core/Lights/light.js"
        |=> Inherits Node

    let ShadowLight =
        Class "ShadowLight"
        |=> Inherits Light

    let HemisphericLight =
        Class "HemisphericLight"
        |> Imported "@babylonjs/core/Lights/hemisphericLight.js"
        |=> Inherits Light

    let DirectionalLight =
        Class "DirectionalLight"
        |> Imported "@babylonjs/core/Lights/directionalLight.js"
        |=> Inherits ShadowLight

    let PointLight =
        Class "PointLight"
        |> Imported "@babylonjs/core/Lights/pointLight.js"
        |=> Inherits ShadowLight

    let SpotLight =
        Class "SpotLight"
        |> Imported "@babylonjs/core/Lights/spotLight.js"
        |=> Inherits ShadowLight

    let Material =
        Class "Material"
        |> Imported "@babylonjs/core/Materials/material.js"

    let StandardMaterial =
        Class "StandardMaterial"
        |> Imported "@babylonjs/core/Materials/standardMaterial.js"
        |=> Inherits Material

    let BaseTexture =
        Class "BaseTexture"
        |> Imported "@babylonjs/core/Materials/Textures/baseTexture.js"

    let Texture =
        Class "Texture"
        |> Imported "@babylonjs/core/Materials/Textures/texture.js"
        |=> Inherits BaseTexture

    let DynamicTexture =
        Class "DynamicTexture"
        |> Imported "@babylonjs/core/Materials/Textures/dynamicTexture.js"
        |=> Inherits Texture

    let ShadowGenerator =
        Class "ShadowGenerator"
        |> Imported "@babylonjs/core/Lights/Shadows/shadowGenerator.js"

    let GlowLayer =
        Class "GlowLayer"
        |> Imported "@babylonjs/core/Layers/glowLayer.js"

    let PickingInfo =
        Class "PickingInfo"
        |> Imported "@babylonjs/core/Collisions/pickingInfo.js"

    let Ray =
        Class "Ray"
        |> Imported "@babylonjs/core/Culling/ray.js"

    let PointerInfo =
        Class "PointerInfo"
        |> Imported "@babylonjs/core/Events/pointerEvents.js"

    let AbstractEngine =
        Class "AbstractEngine"

    let ThinEngine =
        Class "ThinEngine"
        |=> Inherits AbstractEngine

    let Engine =
        Class "Engine"
        |> Imported "@babylonjs/core/Engines/engine.js"
        |=> Inherits ThinEngine

    let Scene =
        Class "Scene"
        |> Imported "@babylonjs/core/scene.js"

    let MeshBuilder =
        Class "MeshBuilder"
        |> Imported "@babylonjs/core/Meshes/meshBuilder.js"

    // ==================================================================
    // Options / configuration shapes
    // ==================================================================

    let EngineOptions =
        Pattern.Config "EngineOptions" {
            Required = []
            Optional = [
                "preserveDrawingBuffer", T<bool>
                "stencil", T<bool>
                "alpha", T<bool>
                "premultipliedAlpha", T<bool>
                "antialias", T<bool>
                "powerPreference", PowerPreference.Type
                "disableWebGL2Support", T<bool>
                "xrCompatible", T<bool>
                "failIfMajorPerformanceCaveat", T<bool>
                "useHighPrecisionFloats", T<bool>
                "limitDeviceRatio", T<float>
                "adaptToDeviceRatio", T<bool>
            ]
        }

    let IPointerEvent =
        Pattern.Config "IPointerEvent" {
            Required = [
                "clientX", T<float>
                "clientY", T<float>
                "pointerId", T<int>
                "pointerType", T<string>
                "button", T<int>
                "buttons", T<int>
                "altKey", T<bool>
                "ctrlKey", T<bool>
                "shiftKey", T<bool>
                "metaKey", T<bool>
                "type", T<string>
            ]
            Optional = []
        }

    let MinMaxVectors =
        Pattern.Config "MinMaxVectors" {
            Required = [
                "min", Vector3.Type
                "max", Vector3.Type
            ]
            Optional = []
        }

    let IGlowLayerOptions =
        Pattern.Config "IGlowLayerOptions" {
            Required = []
            Optional = [
                "mainTextureRatio", T<float>
                "mainTextureFixedSize", T<int>
                "blurKernelSize", T<int>
                "mainTextureSamples", T<int>
                "generateStencilBuffer", T<bool>
            ]
        }

    // MeshBuilder option objects -----------------------------------------

    let BoxOptions =
        Pattern.Config "BoxOptions" {
            Required = []
            Optional = [
                "size", T<float>
                "width", T<float>
                "height", T<float>
                "depth", T<float>
                "faceUV", !| Vector4
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "wrap", T<bool>
                "updatable", T<bool>
            ]
        }

    let SphereOptions =
        Pattern.Config "SphereOptions" {
            Required = []
            Optional = [
                "segments", T<int>
                "diameter", T<float>
                "diameterX", T<float>
                "diameterY", T<float>
                "diameterZ", T<float>
                "arc", T<float>
                "slice", T<float>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "updatable", T<bool>
            ]
        }

    let GroundOptions =
        Pattern.Config "GroundOptions" {
            Required = []
            Optional = [
                "width", T<float>
                "height", T<float>
                "subdivisions", T<int>
                "subdivisionsX", T<int>
                "subdivisionsY", T<int>
                "updatable", T<bool>
            ]
        }

    let CylinderOptions =
        Pattern.Config "CylinderOptions" {
            Required = []
            Optional = [
                "height", T<float>
                "diameterTop", T<float>
                "diameterBottom", T<float>
                "diameter", T<float>
                "tessellation", T<int>
                "subdivisions", T<int>
                "arc", T<float>
                "faceColors", !| Color4
                "faceUV", !| Vector4
                "cap", T<int>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "updatable", T<bool>
            ]
        }

    let TorusOptions =
        Pattern.Config "TorusOptions" {
            Required = []
            Optional = [
                "diameter", T<float>
                "thickness", T<float>
                "tessellation", T<int>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "updatable", T<bool>
            ]
        }

    let TorusKnotOptions =
        Pattern.Config "TorusKnotOptions" {
            Required = []
            Optional = [
                "radius", T<float>
                "tube", T<float>
                "radialSegments", T<int>
                "tubularSegments", T<int>
                "p", T<float>
                "q", T<float>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "updatable", T<bool>
            ]
        }

    let PlaneOptions =
        Pattern.Config "PlaneOptions" {
            Required = []
            Optional = [
                "size", T<float>
                "width", T<float>
                "height", T<float>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "updatable", T<bool>
            ]
        }

    let DiscOptions =
        Pattern.Config "DiscOptions" {
            Required = []
            Optional = [
                "radius", T<float>
                "tessellation", T<int>
                "arc", T<float>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "updatable", T<bool>
            ]
        }

    let LinesOptions =
        Pattern.Config "LinesOptions" {
            Required = [
                "points", !| Vector3
            ]
            Optional = [
                "updatable", T<bool>
                "colors", !| Color4
                "useVertexAlpha", T<bool>
                "material", Material.Type
            ]
        }

    let CapsuleOptions =
        Pattern.Config "CapsuleOptions" {
            Required = []
            Optional = [
                "orientation", Vector3.Type
                "subdivisions", T<int>
                "tessellation", T<int>
                "height", T<float>
                "radius", T<float>
                "capSubdivisions", T<int>
                "radiusTop", T<float>
                "radiusBottom", T<float>
                "topCapSubdivisions", T<int>
                "bottomCapSubdivisions", T<int>
                "updatable", T<bool>
            ]
        }

    let IcoSphereOptions =
        Pattern.Config "IcoSphereOptions" {
            Required = []
            Optional = [
                "radius", T<float>
                "radiusX", T<float>
                "radiusY", T<float>
                "radiusZ", T<float>
                "flat", T<bool>
                "subdivisions", T<int>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "updatable", T<bool>
            ]
        }

    let PolyhedronOptions =
        Pattern.Config "PolyhedronOptions" {
            Required = []
            Optional = [
                "type", T<int>
                "size", T<float>
                "sizeX", T<float>
                "sizeY", T<float>
                "sizeZ", T<float>
                "flat", T<bool>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "updatable", T<bool>
            ]
        }

    let TubeOptions =
        Pattern.Config "TubeOptions" {
            Required = [
                "path", !| Vector3
            ]
            Optional = [
                "radius", T<float>
                "tessellation", T<int>
                "cap", T<int>
                "arc", T<float>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "invertUV", T<bool>
                "updatable", T<bool>
            ]
        }

    let RibbonOptions =
        Pattern.Config "RibbonOptions" {
            Required = [
                "pathArray", !| (!| Vector3)
            ]
            Optional = [
                "closeArray", T<bool>
                "closePath", T<bool>
                "offset", T<int>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "uvs", !| Vector2
                "colors", !| Color4
                "invertUV", T<bool>
                "updatable", T<bool>
            ]
        }

    let LatheOptions =
        Pattern.Config "LatheOptions" {
            Required = [
                "shape", !| Vector3
            ]
            Optional = [
                "radius", T<float>
                "tessellation", T<int>
                "clip", T<float>
                "arc", T<float>
                "closed", T<bool>
                "cap", T<int>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "invertUV", T<bool>
                "updatable", T<bool>
            ]
        }

    let ExtrudeShapeOptions =
        Pattern.Config "ExtrudeShapeOptions" {
            Required = [
                "shape", !| Vector3
                "path", !| Vector3
            ]
            Optional = [
                "scale", T<float>
                "rotation", T<float>
                "closeShape", T<bool>
                "closePath", T<bool>
                "cap", T<int>
                "sideOrientation", T<int>
                "frontUVs", Vector4.Type
                "backUVs", Vector4.Type
                "invertUV", T<bool>
                "updatable", T<bool>
            ]
        }

    // ==================================================================
    // Observable<'T> / Observer<'T>
    // ==================================================================

    let Observer =
        Generic - fun (a: CodeModel.TypeParameter) ->
        Class "Observer"
        |+> Instance [
            "callback" =@ (a ^-> T<unit>)
            "mask" =@ T<int>
            "unregisterOnNextCall" =@ T<bool>
            "remove" => !?T<bool>?defer ^-> T<unit>
        ]

    let Observable =
        Generic - fun (a: CodeModel.TypeParameter) ->
        Class "Observable"
        |+> Instance [
            "add" => (a ^-> T<unit>)?callback ^-> Observer.[a]
            "addOnce" => (a ^-> T<unit>)?callback ^-> Observer.[a]
            "remove" => Observer.[a]?observer ^-> T<bool>
            "removeCallback" => (a ^-> T<unit>)?callback ^-> T<bool>
            "notifyObservers" => a?eventData ^-> T<bool>
            "hasObservers" => T<unit> ^-> T<bool>
            "clear" => T<unit> ^-> T<unit>
            "clone" => T<unit> ^-> TSelf.[a]
        ]

    // ==================================================================
    // Math
    // ==================================================================

    Vector2
    |+> Static [
        Constructor (T<float>?x * T<float>?y)
        "Zero" => T<unit> ^-> Vector2
        "One" => T<unit> ^-> Vector2
        "FromArray" => (!|T<float>)?array * !?T<int>?offset ^-> Vector2
        "Random" => !?T<float>?min * !?T<float>?max ^-> Vector2
        "Distance" => Vector2?value1 * Vector2?value2 ^-> T<float>
        "Lerp" => Vector2?start * Vector2?endValue * T<float>?amount ^-> Vector2
        "Dot" => Vector2?left * Vector2?right ^-> T<float>
        "CatmullRom" =>
            Vector2?value1 * Vector2?value2 * Vector2?value3 * Vector2?value4
            * T<float>?amount ^-> Vector2
        "Clamp" => Vector2?value * Vector2?min * Vector2?max ^-> Vector2
    ]
    |+> Instance [
        "x" =@ T<float>
        "y" =@ T<float>
        "toString" => T<unit> ^-> T<string>
        "getClassName" => T<unit> ^-> T<string>
        "getHashCode" => T<unit> ^-> T<int>
        "asArray" => T<unit> ^-> !|T<float>
        "set" => T<float>?x * T<float>?y ^-> TSelf
        "setAll" => T<float>?v ^-> TSelf
        "copyFrom" => Vector2?source ^-> TSelf
        "copyFromFloats" => T<float>?x * T<float>?y ^-> TSelf
        "add" => Vector2?otherVector ^-> Vector2
        "addInPlace" => Vector2?otherVector ^-> TSelf
        "subtract" => Vector2?otherVector ^-> Vector2
        "subtractInPlace" => Vector2?otherVector ^-> TSelf
        "multiply" => Vector2?otherVector ^-> Vector2
        "multiplyInPlace" => Vector2?otherVector ^-> TSelf
        "scale" => T<float>?scale ^-> Vector2
        "scaleInPlace" => T<float>?scale ^-> TSelf
        "divide" => Vector2?otherVector ^-> Vector2
        "negate" => T<unit> ^-> Vector2
        "length" => T<unit> ^-> T<float>
        "lengthSquared" => T<unit> ^-> T<float>
        "normalize" => T<unit> ^-> TSelf
        "normalizeToNew" => T<unit> ^-> Vector2
        "equals" => Vector2?otherVector ^-> T<bool>
        "equalsWithEpsilon" => Vector2?otherVector * !?T<float>?epsilon ^-> T<bool>
        "equalsToFloats" => T<float>?x * T<float>?y ^-> T<bool>
        "clone" => T<unit> ^-> Vector2
    ]

    Vector3
    |+> Static [
        Constructor (T<float>?x * T<float>?y * T<float>?z)
        "Zero" => T<unit> ^-> Vector3
        "One" => T<unit> ^-> Vector3
        "Up" => T<unit> ^-> Vector3
        "Down" => T<unit> ^-> Vector3
        "Right" => T<unit> ^-> Vector3
        "Left" => T<unit> ^-> Vector3
        "Forward" => !?T<bool>?rightHandedSystem ^-> Vector3
        "Backward" => !?T<bool>?rightHandedSystem ^-> Vector3
        "Random" => !?T<float>?min * !?T<float>?max ^-> Vector3
        "FromArray" => (!|T<float>)?array * !?T<int>?offset ^-> Vector3
        "FromFloatsToRef" => T<float>?x * T<float>?y * T<float>?z * Vector3?result ^-> Vector3
        "TransformCoordinates" => Vector3?vector * Matrix?transformation ^-> Vector3
        "TransformNormal" => Vector3?vector * Matrix?transformation ^-> Vector3
        "Lerp" => Vector3?start * Vector3?endValue * T<float>?amount ^-> Vector3
        "Dot" => Vector3?left * Vector3?right ^-> T<float>
        "Cross" => Vector3?left * Vector3?right ^-> Vector3
        "Normalize" => Vector3?vector ^-> Vector3
        "Clamp" => Vector3?value * Vector3?min * Vector3?max ^-> Vector3
        "Distance" => Vector3?value1 * Vector3?value2 ^-> T<float>
        "DistanceSquared" => Vector3?value1 * Vector3?value2 ^-> T<float>
        "Minimize" => Vector3?left * Vector3?right ^-> Vector3
        "Maximize" => Vector3?left * Vector3?right ^-> Vector3
        "Center" => Vector3?value1 * Vector3?value2 ^-> Vector3
        "GetAngleBetweenVectors" =>
            Vector3?vector0 * Vector3?vector1 * Vector3?normal ^-> T<float>
    ]
    |+> Instance [
        "x" =@ T<float>
        "y" =@ T<float>
        "z" =@ T<float>
        "toString" => T<unit> ^-> T<string>
        "getClassName" => T<unit> ^-> T<string>
        "getHashCode" => T<unit> ^-> T<int>
        "asArray" => T<unit> ^-> !|T<float>
        "set" => T<float>?x * T<float>?y * T<float>?z ^-> TSelf
        "setAll" => T<float>?v ^-> TSelf
        "copyFrom" => Vector3?source ^-> TSelf
        "copyFromFloats" => T<float>?x * T<float>?y * T<float>?z ^-> TSelf
        "add" => Vector3?otherVector ^-> Vector3
        "addInPlace" => Vector3?otherVector ^-> TSelf
        "addInPlaceFromFloats" => T<float>?x * T<float>?y * T<float>?z ^-> TSelf
        "subtract" => Vector3?otherVector ^-> Vector3
        "subtractInPlace" => Vector3?otherVector ^-> TSelf
        "subtractFromFloats" => T<float>?x * T<float>?y * T<float>?z ^-> Vector3
        "multiply" => Vector3?otherVector ^-> Vector3
        "multiplyInPlace" => Vector3?otherVector ^-> TSelf
        "multiplyByFloats" => T<float>?x * T<float>?y * T<float>?z ^-> Vector3
        "divide" => Vector3?otherVector ^-> Vector3
        "divideInPlace" => Vector3?otherVector ^-> TSelf
        "negate" => T<unit> ^-> Vector3
        "negateInPlace" => T<unit> ^-> TSelf
        "scale" => T<float>?scale ^-> Vector3
        "scaleInPlace" => T<float>?scale ^-> TSelf
        "length" => T<unit> ^-> T<float>
        "lengthSquared" => T<unit> ^-> T<float>
        "normalize" => T<unit> ^-> TSelf
        "normalizeToNew" => T<unit> ^-> Vector3
        "cross" => Vector3?other ^-> Vector3
        "dot" => Vector3?otherVector ^-> T<float>
        "equals" => Vector3?otherVector ^-> T<bool>
        "equalsWithEpsilon" => Vector3?otherVector * !?T<float>?epsilon ^-> T<bool>
        "equalsToFloats" => T<float>?x * T<float>?y * T<float>?z ^-> T<bool>
        "clone" => T<unit> ^-> Vector3
        "floor" => T<unit> ^-> Vector3
        "fract" => T<unit> ^-> Vector3
        "toQuaternion" => T<unit> ^-> Quaternion
        "rotateByQuaternionToRef" => Quaternion?quaternion * Vector3?result ^-> Vector3
        "minimizeInPlace" => Vector3?other ^-> TSelf
        "maximizeInPlace" => Vector3?other ^-> TSelf
    ]

    Vector4
    |+> Static [
        Constructor (T<float>?x * T<float>?y * T<float>?z * T<float>?w)
        "Zero" => T<unit> ^-> Vector4
        "One" => T<unit> ^-> Vector4
        "FromArray" => (!|T<float>)?array * !?T<int>?offset ^-> Vector4
        "FromFloatsToRef" =>
            T<float>?x * T<float>?y * T<float>?z * T<float>?w * Vector4?result
            ^-> Vector4
        "Lerp" => Vector4?start * Vector4?endValue * T<float>?amount ^-> Vector4
        "Dot" => Vector4?left * Vector4?right ^-> T<float>
        "Distance" => Vector4?value1 * Vector4?value2 ^-> T<float>
    ]
    |+> Instance [
        "x" =@ T<float>
        "y" =@ T<float>
        "z" =@ T<float>
        "w" =@ T<float>
        "toString" => T<unit> ^-> T<string>
        "asArray" => T<unit> ^-> !|T<float>
        "set" => T<float>?x * T<float>?y * T<float>?z * T<float>?w ^-> TSelf
        "setAll" => T<float>?v ^-> TSelf
        "add" => Vector4?otherVector ^-> Vector4
        "addInPlace" => Vector4?otherVector ^-> TSelf
        "subtract" => Vector4?otherVector ^-> Vector4
        "subtractInPlace" => Vector4?otherVector ^-> TSelf
        "multiply" => Vector4?otherVector ^-> Vector4
        "multiplyInPlace" => Vector4?otherVector ^-> TSelf
        "scale" => T<float>?scale ^-> Vector4
        "scaleInPlace" => T<float>?scale ^-> TSelf
        "negate" => T<unit> ^-> Vector4
        "length" => T<unit> ^-> T<float>
        "normalize" => T<unit> ^-> TSelf
        "equals" => Vector4?otherVector ^-> T<bool>
        "clone" => T<unit> ^-> Vector4
    ]

    Quaternion
    |+> Static [
        Constructor (T<float>?x * T<float>?y * T<float>?z * T<float>?w)
        "Zero" => T<unit> ^-> Quaternion
        "Identity" => T<unit> ^-> Quaternion
        "RotationAxis" => Vector3?axis * T<float>?angle ^-> Quaternion
        "FromEulerAngles" => T<float>?x * T<float>?y * T<float>?z ^-> Quaternion
        "FromRotationMatrix" => Matrix?matrix ^-> Quaternion
        "FromArray" => (!|T<float>)?array * !?T<int>?offset ^-> Quaternion
        "Slerp" => Quaternion?left * Quaternion?rightValue * T<float>?amount ^-> Quaternion
        "Lerp" => Quaternion?left * Quaternion?rightValue * T<float>?amount ^-> Quaternion
        "Dot" => Quaternion?left * Quaternion?rightValue ^-> T<float>
        "Inverse" => Quaternion?q ^-> Quaternion
        "AreClose" =>
            Quaternion?quat0 * Quaternion?quat1 * !?T<float>?epsilon ^-> T<bool>
    ]
    |+> Instance [
        "x" =@ T<float>
        "y" =@ T<float>
        "z" =@ T<float>
        "w" =@ T<float>
        "toString" => T<unit> ^-> T<string>
        "asArray" => T<unit> ^-> !|T<float>
        "set" => T<float>?x * T<float>?y * T<float>?z * T<float>?w ^-> TSelf
        "copyFrom" => Quaternion?other ^-> TSelf
        "copyFromFloats" => T<float>?x * T<float>?y * T<float>?z * T<float>?w ^-> TSelf
        "add" => Quaternion?other ^-> Quaternion
        "subtract" => Quaternion?other ^-> Quaternion
        "scale" => T<float>?value ^-> Quaternion
        "multiply" => Quaternion?q1 ^-> Quaternion
        "multiplyInPlace" => Quaternion?other ^-> TSelf
        "conjugate" => T<unit> ^-> Quaternion
        "invert" => T<unit> ^-> Quaternion
        "length" => T<unit> ^-> T<float>
        "lengthSquared" => T<unit> ^-> T<float>
        "normalize" => T<unit> ^-> TSelf
        "toEulerAngles" => T<unit> ^-> Vector3
        "equals" => Quaternion?otherQuaternion ^-> T<bool>
        "clone" => T<unit> ^-> Quaternion
    ]

    Matrix
    |+> Static [
        Constructor (T<unit>)
        "Identity" => T<unit> ^-> Matrix
        "Zero" => T<unit> ^-> Matrix
        "FromValues" =>
            T<float>?m11 * T<float>?m12 * T<float>?m13 * T<float>?m14
            * T<float>?m21 * T<float>?m22 * T<float>?m23 * T<float>?m24
            * T<float>?m31 * T<float>?m32 * T<float>?m33 * T<float>?m34
            * T<float>?m41 * T<float>?m42 * T<float>?m43 * T<float>?m44
            ^-> Matrix
        "FromArray" => (!|T<float>)?array * !?T<int>?offset ^-> Matrix
        "Compose" =>
            Vector3?scale * Quaternion?rotation * Vector3?translation ^-> Matrix
        "RotationX" => T<float>?angle ^-> Matrix
        "RotationY" => T<float>?angle ^-> Matrix
        "RotationZ" => T<float>?angle ^-> Matrix
        "RotationAxis" => Vector3?axis * T<float>?angle ^-> Matrix
        "RotationYawPitchRoll" =>
            T<float>?yaw * T<float>?pitch * T<float>?roll ^-> Matrix
        "Scaling" => T<float>?x * T<float>?y * T<float>?z ^-> Matrix
        "Translation" => T<float>?x * T<float>?y * T<float>?z ^-> Matrix
        "LookAtLH" => Vector3?eye * Vector3?target * Vector3?up ^-> Matrix
        "LookAtRH" => Vector3?eye * Vector3?target * Vector3?up ^-> Matrix
        "PerspectiveFovLH" =>
            T<float>?fov * T<float>?aspect * T<float>?near * T<float>?far ^-> Matrix
        "PerspectiveFovRH" =>
            T<float>?fov * T<float>?aspect * T<float>?near * T<float>?far ^-> Matrix
        "OrthoLH" =>
            T<float>?width * T<float>?height * T<float>?znear * T<float>?zfar ^-> Matrix
        "OrthoRH" =>
            T<float>?width * T<float>?height * T<float>?znear * T<float>?zfar ^-> Matrix
        "Invert" => Matrix?source ^-> Matrix
    ]
    |+> Instance [
        "isIdentity" => T<unit> ^-> T<bool>
        "determinant" => T<unit> ^-> T<float>
        "toString" => T<unit> ^-> T<string>
        "toArray" => T<unit> ^-> !|T<float>
        "asArray" => T<unit> ^-> !|T<float>
        "set" =>
            T<float>?m11 * T<float>?m12 * T<float>?m13 * T<float>?m14
            * T<float>?m21 * T<float>?m22 * T<float>?m23 * T<float>?m24
            * T<float>?m31 * T<float>?m32 * T<float>?m33 * T<float>?m34
            * T<float>?m41 * T<float>?m42 * T<float>?m43 * T<float>?m44
            ^-> TSelf
        "setAll" => T<float>?value ^-> TSelf
        "reset" => T<unit> ^-> TSelf
        "invert" => T<unit> ^-> TSelf
        "multiply" => Matrix?other ^-> Matrix
        "multiplyInPlace" => Matrix?other ^-> TSelf
        "transpose" => T<unit> ^-> Matrix
        "clone" => T<unit> ^-> Matrix
        "getRow" => T<int>?index ^-> Vector4
        "setRow" => T<int>?index * Vector4?row ^-> TSelf
        "setTranslation" => Vector3?vector3 ^-> TSelf
        "setTranslationFromFloats" => T<float>?x * T<float>?y * T<float>?z ^-> TSelf
        "getTranslation" => T<unit> ^-> Vector3
        "getRotationMatrix" => T<unit> ^-> Matrix
        "decompose" =>
            !?Vector3?scale * !?Quaternion?rotation * !?Vector3?translation
            ^-> T<bool>
        "equals" => Matrix?value ^-> T<bool>
    ]

    Color3
    |+> Static [
        Constructor (T<float>?r * T<float>?g * T<float>?b)
        "FromHexString" => T<string>?hex ^-> Color3
        "FromInts" => T<int>?r * T<int>?g * T<int>?b ^-> Color3
        "FromArray" => (!|T<float>)?array * !?T<int>?offset ^-> Color3
        "FromHSV" => T<float>?hue * T<float>?saturation * T<float>?value ^-> Color3
        "Lerp" => Color3?start * Color3?endValue * T<float>?amount ^-> Color3
        "Red" => T<unit> ^-> Color3
        "Green" => T<unit> ^-> Color3
        "Blue" => T<unit> ^-> Color3
        "White" => T<unit> ^-> Color3
        "Black" => T<unit> ^-> Color3
        "Yellow" => T<unit> ^-> Color3
        "Purple" => T<unit> ^-> Color3
        "Magenta" => T<unit> ^-> Color3
        "Gray" => T<unit> ^-> Color3
        "Teal" => T<unit> ^-> Color3
    ]
    |+> Instance [
        "r" =@ T<float>
        "g" =@ T<float>
        "b" =@ T<float>
        "toString" => T<unit> ^-> T<string>
        "toString" => T<string>?space ^-> T<string>
        "asArray" => T<unit> ^-> !|T<float>
        "toColor4" => !?T<float>?alpha ^-> Color4
        "toHexString" => T<unit> ^-> T<string>
        "toHSV" => T<unit> ^-> Color3
        "toLinearSpace" => !?T<bool>?exact ^-> Color3
        "toGammaSpace" => !?T<bool>?exact ^-> Color3
        "toLuminance" => T<unit> ^-> T<float>
        "set" => T<float>?r * T<float>?g * T<float>?b ^-> TSelf
        "setAll" => T<float>?v ^-> TSelf
        "copyFrom" => Color3?source ^-> TSelf
        "add" => Color3?otherColor ^-> Color3
        "addInPlace" => Color3?otherColor ^-> TSelf
        "subtract" => Color3?otherColor ^-> Color3
        "subtractInPlace" => Color3?otherColor ^-> TSelf
        "multiply" => Color3?otherColor ^-> Color3
        "multiplyInPlace" => Color3?otherColor ^-> TSelf
        "scale" => T<float>?scale ^-> Color3
        "scaleInPlace" => T<float>?scale ^-> TSelf
        "equals" => Color3?otherColor ^-> T<bool>
        "equalsWithEpsilon" => Color3?otherColor * !?T<float>?epsilon ^-> T<bool>
        "clone" => T<unit> ^-> Color3
    ]

    Color4
    |+> Static [
        Constructor (T<float>?r * T<float>?g * T<float>?b * T<float>?a)
        "FromColor3" => Color3?color3 * !?T<float>?alpha ^-> Color4
        "FromArray" => (!|T<float>)?array * !?T<int>?offset ^-> Color4
        "FromHexString" => T<string>?hex ^-> Color4
        "Lerp" => Color4?start * Color4?endValue * T<float>?amount ^-> Color4
    ]
    |+> Instance [
        "r" =@ T<float>
        "g" =@ T<float>
        "b" =@ T<float>
        "a" =@ T<float>
        "toString" => T<unit> ^-> T<string>
        "asArray" => T<unit> ^-> !|T<float>
        "toHexString" => T<unit> ^-> T<string>
        "toColor3" => T<unit> ^-> Color3
        "set" => T<float>?r * T<float>?g * T<float>?b * T<float>?a ^-> TSelf
        "setAll" => T<float>?v ^-> TSelf
        "add" => Color4?otherColor ^-> Color4
        "addInPlace" => Color4?otherColor ^-> TSelf
        "subtract" => Color4?otherColor ^-> Color4
        "subtractInPlace" => Color4?otherColor ^-> TSelf
        "multiply" => Color4?otherColor ^-> Color4
        "multiplyInPlace" => Color4?otherColor ^-> TSelf
        "scale" => T<float>?scale ^-> Color4
        "scaleInPlace" => T<float>?scale ^-> TSelf
        "equals" => Color4?otherColor ^-> T<bool>
        "clone" => T<unit> ^-> Color4
    ]

    // ==================================================================
    // Scene graph
    // ==================================================================

    Node
    |+> Static [
        Constructor (T<string>?name * !?Scene?scene * !?T<bool>?isPure)
    ]
    |+> Instance [
        "name" =@ T<string>
        "id" =@ T<string>
        "uniqueId" =? T<int>
        "state" =@ T<string>
        "isDisposed" => T<unit> ^-> T<bool>
        "parent" =@ Node
        "isVisible" =@ T<bool>
        "getClassName" => T<unit> ^-> T<string>
        "getScene" => T<unit> ^-> Scene
        "getEngine" => T<unit> ^-> AbstractEngine
        "getWorldMatrix" => T<unit> ^-> Matrix
        "markAsDirty" => !?T<string>?propertyName ^-> Node
        "isEnabled" => !?T<bool>?checkAncestors ^-> T<bool>
        "setEnabled" => T<bool>?value ^-> T<unit>
        "isDescendantOf" => Node?ancestor ^-> T<bool>
        "getDescendants" =>
            !?T<bool>?directDescendantsOnly * !?(Node ^-> T<bool>)?predicate
            ^-> !|Node
        "getChildMeshes" =>
            !?T<bool>?directDescendantsOnly * !?(Node ^-> T<bool>)?predicate
            ^-> !|AbstractMesh
        "getChildren" =>
            !?(Node ^-> T<bool>)?predicate * !?T<bool>?directDescendantsOnly
            ^-> !|Node
        "dispose" =>
            !?T<bool>?doNotRecurse * !?T<bool>?disposeMaterialAndTextures ^-> T<unit>
        "computeWorldMatrix" => !?T<bool>?force ^-> Matrix
        "getHierarchyBoundingVectors" =>
            !?T<bool>?includeDescendants
            * !?(AbstractMesh ^-> T<bool>)?predicate
            ^-> MinMaxVectors
    ]

    TransformNode
    |+> Static [
        Constructor (T<string>?name * !?Scene?scene * !?T<bool>?isPure)
    ]
    |+> Instance [
        "position" =@ Vector3
        "rotation" =@ Vector3
        "rotationQuaternion" =@ Quaternion
        "scaling" =@ Vector3
        "absolutePosition" =? Vector3
        "absoluteScaling" =? Vector3
        "absoluteRotationQuaternion" =? Quaternion
        "forward" =? Vector3
        "up" =? Vector3
        "right" =? Vector3
        "billboardMode" =@ T<int>
        "scalingDeterminant" =@ T<float>
        "infiniteDistance" =@ T<bool>
        "ignoreNonUniformScaling" =@ T<bool>
        "getAbsolutePosition" => T<unit> ^-> Vector3
        "setAbsolutePosition" => Vector3?absolutePosition ^-> TransformNode
        "setPositionWithLocalVector" => Vector3?vector3 ^-> TransformNode
        "getPositionExpressedInLocalSpace" => T<unit> ^-> Vector3
        "locallyTranslate" => Vector3?vector3 ^-> TransformNode
        "lookAt" =>
            Vector3?targetPoint * !?T<float>?yawCor * !?T<float>?pitchCor
            * !?T<float>?rollCor * !?T<int>?space ^-> TransformNode
        "getDirection" => Vector3?localAxis ^-> Vector3
        "setDirection" =>
            Vector3?localAxis * !?T<float>?yawCor * !?T<float>?pitchCor
            * !?T<float>?rollCor ^-> TransformNode
        "setPivotPoint" => Vector3?point * !?T<int>?space ^-> TransformNode
        "getPivotPoint" => T<unit> ^-> Vector3
        "getAbsolutePivotPoint" => T<unit> ^-> Vector3
        "rotate" => Vector3?axis * T<float>?amount * !?T<int>?space ^-> TransformNode
        "rotateAround" =>
            Vector3?point * Vector3?axis * T<float>?amount ^-> TransformNode
        "translate" =>
            Vector3?axis * T<float>?distance * !?T<int>?space ^-> TransformNode
        "addRotation" => T<float>?x * T<float>?y * T<float>?z ^-> TransformNode
        "registerAfterWorldMatrixUpdate" =>
            (TransformNode ^-> T<unit>)?callback ^-> TransformNode
        "unregisterAfterWorldMatrixUpdate" =>
            (TransformNode ^-> T<unit>)?callback ^-> TransformNode
        "getDistanceToCamera" => !?Camera?camera ^-> T<float>
        "clone" =>
            T<string>?name * Node?newParent * !?T<bool>?doNotCloneChildren
            ^-> TransformNode
        "getChildTransformNodes" =>
            !?T<bool>?directDescendantsOnly * !?(Node ^-> T<bool>)?predicate
            ^-> !|TransformNode
    ]

    AbstractMesh
    |+> Instance [
        "isPickable" =@ T<bool>
        "isVisible" =@ T<bool>
        "visibility" =@ T<float>
        "alphaIndex" =@ T<int>
        "material" =@ Material
        "receiveShadows" =@ T<bool>
        "overlayColor" =@ Color3
        "overlayAlpha" =@ T<float>
        "outlineColor" =@ Color3
        "outlineWidth" =@ T<float>
        "edgesWidth" =@ T<float>
        "edgesColor" =@ Color4
        "renderingGroupId" =@ T<int>
        "layerMask" =@ T<int>
        "alwaysSelectAsActiveMesh" =@ T<bool>
        "getClassName" => T<unit> ^-> T<string>
        "getTotalVertices" => T<unit> ^-> T<int>
        "getTotalIndices" => T<unit> ^-> T<int>
        "getIndices" => T<unit> ^-> !|T<int>
        "getVerticesData" => T<string>?kind ^-> !|T<float>
        "setVerticesData" =>
            T<string>?kind * (!|T<float>)?data * !?T<bool>?updatable * !?T<int>?stride
            ^-> AbstractMesh
        "updateVerticesData" =>
            T<string>?kind * (!|T<float>)?data * !?T<bool>?updateExtends
            * !?T<bool>?makeItUnique ^-> AbstractMesh
        "isVerticesDataPresent" => T<string>?kind ^-> T<bool>
        "clone" =>
            T<string>?name * Node?newParent * !?T<bool>?doNotCloneChildren
            ^-> AbstractMesh
        "intersectsPoint" => Vector3?point ^-> T<bool>
        "movePOV" =>
            T<float>?amountRight * T<float>?amountUp * T<float>?amountForward
            ^-> AbstractMesh
        "rotatePOV" =>
            T<float>?flipBack * T<float>?twirlClockwise * T<float>?tiltRight
            ^-> AbstractMesh
        "normalizeToUnitCube" =>
            !?T<bool>?includeDescendants * !?T<bool>?ignoreRotation
            * !?(AbstractMesh ^-> T<bool>)?predicate ^-> AbstractMesh
        "dispose" =>
            !?T<bool>?doNotRecurse * !?T<bool>?disposeMaterialAndTextures
            ^-> T<unit>
    ]

    Mesh
    |+> Static [
        Constructor (
            T<string>?name * !?Scene?scene * !?Node?parent * !?Mesh?source
            * !?T<bool>?doNotCloneChildren
        )
        "CreateBox" =>
            T<string>?name * T<float>?size * !?Scene?scene * !?T<bool>?updatable
            * !?T<int>?sideOrientation ^-> Mesh
        "CreateSphere" =>
            T<string>?name * T<int>?segments * T<float>?diameter * !?Scene?scene
            * !?T<bool>?updatable * !?T<int>?sideOrientation ^-> Mesh
        "CreateGround" =>
            T<string>?name * T<float>?width * T<float>?height * T<int>?subdivisions
            * !?Scene?scene * !?T<bool>?updatable ^-> GroundMesh
        "CreateTorus" =>
            T<string>?name * T<float>?diameter * T<float>?thickness
            * T<int>?tessellation * !?Scene?scene * !?T<bool>?updatable
            * !?T<int>?sideOrientation ^-> Mesh
        "CreatePlane" =>
            T<string>?name * T<float>?size * !?Scene?scene * !?T<bool>?updatable
            * !?T<int>?sideOrientation ^-> Mesh
        "CreateLines" =>
            T<string>?name * (!|Vector3)?points * !?Scene?scene * !?T<bool>?updatable
            ^-> LinesMesh
        "CreateDashedLines" =>
            T<string>?name * (!|Vector3)?points * T<float>?dashSize * T<float>?gapSize
            * T<int>?dashNb * !?Scene?scene * !?T<bool>?updatable ^-> LinesMesh
    ]
    |+> Instance [
        "source" =? Mesh
        "sideOrientation" =@ T<int>
        "isUnIndexed" =@ T<bool>
        "onBeforeRenderObservable" =? Observable.[Mesh]
        "onAfterRenderObservable" =? Observable.[Mesh]
        "onBeforeDrawObservable" =? Observable.[Mesh]
        "createInstance" => T<string>?name ^-> AbstractMesh
    ]

    GroundMesh
    |+> Instance [
        "subdivisions" =? T<int>
        "subdivisionsX" =? T<int>
        "subdivisionsY" =? T<int>
        "getHeightAtCoordinates" => T<float>?x * T<float>?z ^-> T<float>
        "getNormalAtCoordinates" => T<float>?x * T<float>?z ^-> Vector3
        "updateCoordinateHeights" => T<unit> ^-> GroundMesh
    ]

    LinesMesh
    |+> Instance [
        "color" =@ Color3
        "alpha" =@ T<float>
        "intersectionThreshold" =@ T<float>
    ]

    // ==================================================================
    // Cameras
    // ==================================================================

    Camera
    |+> Static [
        Constructor (
            T<string>?name * Vector3?position * !?Scene?scene
            * !?T<bool>?setActiveOnSceneIfNoneActive
        )
    ]
    |+> Instance [
        "position" =@ Vector3
        "upVector" =@ Vector3
        "fov" =@ T<float>
        "minZ" =@ T<float>
        "maxZ" =@ T<float>
        "inertia" =@ T<float>
        "mode" =@ T<int>
        "layerMask" =@ T<int>
        "attachControl" => !?T<bool>?noPreventDefault ^-> T<unit>
        "attachControl" =>
            T<HTMLCanvasElement>?element * !?T<bool>?noPreventDefault ^-> T<unit>
        "detachControl" => T<unit> ^-> T<unit>
        "getClassName" => T<unit> ^-> T<string>
        "getViewMatrix" => !?T<bool>?force ^-> Matrix
        "getProjectionMatrix" => !?T<bool>?force ^-> Matrix
        "getTarget" => T<unit> ^-> Vector3
        "setTarget" => Vector3?target ^-> T<unit>
        "getDirection" => Vector3?localAxis ^-> Vector3
        "getForwardRay" => !?T<float>?length ^-> Ray
        "dispose" =>
            !?T<bool>?doNotRecurse * !?T<bool>?disposeMaterialAndTextures ^-> T<unit>
    ]

    TargetCamera
    |+> Instance [
        "rotation" =@ Vector3
        "rotationQuaternion" =@ Quaternion
        "speed" =@ T<float>
        "noRotationConstraint" =@ T<bool>
        "inverseRotationSpeed" =@ T<float>
        "getFrontPosition" => T<float>?distance ^-> Vector3
    ]

    ArcRotateCamera
    |+> Static [
        Constructor (
            T<string>?name * T<float>?alpha * T<float>?beta * T<float>?radius
            * Vector3?target * !?Scene?scene * !?T<bool>?setActiveOnSceneIfNoneActive
        )
    ]
    |+> Instance [
        "alpha" =@ T<float>
        "beta" =@ T<float>
        "radius" =@ T<float>
        "target" =@ Vector3
        "lowerAlphaLimit" =@ T<float>
        "upperAlphaLimit" =@ T<float>
        "lowerBetaLimit" =@ T<float>
        "upperBetaLimit" =@ T<float>
        "lowerRadiusLimit" =@ T<float>
        "upperRadiusLimit" =@ T<float>
        "wheelPrecision" =@ T<float>
        "panningSensibility" =@ T<float>
        "angularSensibilityX" =@ T<float>
        "angularSensibilityY" =@ T<float>
        "pinchPrecision" =@ T<float>
        "useNaturalPinchZoom" =@ T<bool>
        "allowUpsideDown" =@ T<bool>
        "zoomOnFactor" =@ T<float>
        "attachControl" =>
            T<HTMLCanvasElement>?element * !?T<bool>?noPreventDefault
            * !?T<bool>?useCtrlForPanning * !?T<int>?panningMouseButton ^-> T<unit>
        "detachControl" => T<unit> ^-> T<unit>
        "setPosition" => Vector3?position ^-> T<unit>
        "setTarget" =>
            Vector3?target * !?T<bool>?toBoundingCenter
            * !?T<bool>?allowSamePosition ^-> T<unit>
        "rebuildAnglesAndRadius" => T<unit> ^-> T<unit>
        "zoomOn" => (!|AbstractMesh)?meshes * !?T<bool>?doNotUpdateMaxZ ^-> T<unit>
        "getClassName" => T<unit> ^-> T<string>
    ]
    |> WithComment
        "An orbiting camera that always looks at a target from a given alpha, beta and radius."

    FreeCamera
    |+> Static [
        Constructor (
            T<string>?name * Vector3?position * !?Scene?scene
            * !?T<bool>?setActiveOnSceneIfNoneActive
        )
    ]
    |+> Instance [
        "ellipsoid" =@ Vector3
        "ellipsoidOffset" =@ Vector3
        "checkCollisions" =@ T<bool>
        "applyGravity" =@ T<bool>
        "angularSensibility" =@ T<float>
        "attachControl" => !?T<bool>?noPreventDefault ^-> T<unit>
        "attachControl" =>
            T<HTMLCanvasElement>?element * !?T<bool>?noPreventDefault ^-> T<unit>
        "detachControl" => T<unit> ^-> T<unit>
        "getClassName" => T<unit> ^-> T<string>
    ]

    // ==================================================================
    // Lights
    // ==================================================================

    Light
    |+> Instance [
        "diffuse" =@ Color3
        "specular" =@ Color3
        "intensity" =@ T<float>
        "range" =@ T<float>
        "radius" =@ T<float>
        "shadowEnabled" =@ T<bool>
        "includedOnlyMeshes" =@ !|AbstractMesh
        "excludedMeshes" =@ !|AbstractMesh
        "getClassName" => T<unit> ^-> T<string>
        "getTypeID" => T<unit> ^-> T<int>
        "getScaledIntensity" => T<unit> ^-> T<float>
        "canAffectMesh" => AbstractMesh?mesh ^-> T<bool>
        "setEnabled" => T<bool>?value ^-> T<unit>
        "dispose" =>
            !?T<bool>?doNotRecurse * !?T<bool>?disposeMaterialAndTextures ^-> T<unit>
    ]

    ShadowLight
    |+> Instance [
        "position" =@ Vector3
        "direction" =@ Vector3
        "shadowMinZ" =@ T<float>
        "shadowMaxZ" =@ T<float>
    ]

    HemisphericLight
    |+> Static [
        Constructor (
            T<string>?name * Vector3?direction * !?Scene?scene
            * !?T<bool>?dontAddToScene
        )
    ]
    |+> Instance [
        "groundColor" =@ Color3
        "direction" =@ Vector3
        "getClassName" => T<unit> ^-> T<string>
        "setDirectionToTarget" => Vector3?target ^-> Vector3
    ]

    DirectionalLight
    |+> Static [
        Constructor (
            T<string>?name * Vector3?direction * !?Scene?scene
            * !?T<bool>?dontAddToScene
        )
    ]
    |+> Instance [
        "shadowFrustumSize" =@ T<float>
        "shadowOrthoScale" =@ T<float>
        "autoUpdateExtends" =@ T<bool>
        "autoCalcShadowZBounds" =@ T<bool>
        "orthoLeft" =@ T<float>
        "orthoRight" =@ T<float>
        "orthoTop" =@ T<float>
        "orthoBottom" =@ T<float>
        "getClassName" => T<unit> ^-> T<string>
        "getTypeID" => T<unit> ^-> T<int>
    ]

    PointLight
    |+> Static [
        Constructor (
            T<string>?name * Vector3?position * !?Scene?scene
            * !?T<bool>?dontAddToScene
        )
    ]
    |+> Instance [
        "shadowAngle" =@ T<float>
        "direction" =@ Vector3
        "getClassName" => T<unit> ^-> T<string>
        "getTypeID" => T<unit> ^-> T<int>
        "needCube" => T<unit> ^-> T<bool>
    ]

    SpotLight
    |+> Static [
        Constructor (
            T<string>?name * Vector3?position * Vector3?direction * T<float>?angle
            * T<float>?exponent * !?Scene?scene * !?T<bool>?dontAddToScene
        )
    ]
    |+> Instance [
        "angle" =@ T<float>
        "innerAngle" =@ T<float>
        "exponent" =@ T<float>
        "shadowAngleScale" =@ T<float>
        "getClassName" => T<unit> ^-> T<string>
        "getTypeID" => T<unit> ^-> T<int>
    ]

    ShadowGenerator
    |+> Static [
        Constructor (
            T<int>?mapSize * ShadowLight?light * !?T<bool>?usefullFloatFirst
            * !?Camera?camera * !?T<bool>?useRedTextureType * !?T<bool>?forceGLSL
        )
    ]
    |+> Instance [
        "bias" =@ T<float>
        "normalBias" =@ T<float>
        "blurKernel" =@ T<int>
        "useKernelBlur" =@ T<bool>
        "depthScale" =@ T<float>
        "filter" =@ T<int>
        "usePoissonSampling" =@ T<bool>
        "useExponentialShadowMap" =@ T<bool>
        "useBlurExponentialShadowMap" =@ T<bool>
        "useCloseExponentialShadowMap" =@ T<bool>
        "useBlurCloseExponentialShadowMap" =@ T<bool>
        "usePercentageCloserFiltering" =@ T<bool>
        "filteringQuality" =@ T<int>
        "useContactHardeningShadow" =@ T<bool>
        "darkness" =@ T<float>
        "transparencyShadow" =@ T<bool>
        "enableSoftTransparentShadow" =@ T<bool>
        "forceBackFacesOnly" =@ T<bool>
        "mapSize" =@ T<int>
        "light" =@ ShadowLight
        "camera" =@ Camera
        "addShadowCaster" =>
            AbstractMesh?mesh * !?T<bool>?includeDescendants ^-> ShadowGenerator
        "removeShadowCaster" =>
            AbstractMesh?mesh * !?T<bool>?includeDescendants ^-> ShadowGenerator
        "getShadowMap" => T<unit> ^-> BaseTexture
        "getClassName" => T<unit> ^-> T<string>
        "dispose" => !?T<bool>?clearObservables ^-> T<unit>
    ]

    // ==================================================================
    // Materials / textures
    // ==================================================================

    Material
    |+> Static [
        Constructor (
            T<string>?name * !?Scene?scene * !?T<bool>?doNotAdd * !?T<bool>?forceGLSL
        )
    ]
    |+> Instance [
        "id" =? T<string>
        "uniqueId" =? T<int>
        "name" =@ T<string>
        "alpha" =@ T<float>
        "backFaceCulling" =@ T<bool>
        "cullBackFaces" =@ T<bool>
        "sideOrientation" =@ T<int>
        "wireframe" =@ T<bool>
        "pointsCloud" =@ T<bool>
        "fillMode" =@ T<int>
        "fogEnabled" =@ T<bool>
        "zOffset" =@ T<float>
        "alphaMode" =@ T<int>
        "needDepthPrePass" =@ T<bool>
        "disableDepthWrite" =@ T<bool>
        "disableColorWrite" =@ T<bool>
        "forceDepthWrite" =@ T<bool>
        "transparencyMode" =@ T<int>
        "getClassName" => T<unit> ^-> T<string>
        "isReady" =>
            !?AbstractMesh?mesh * !?T<bool>?useInstances ^-> T<bool>
        "getScene" => T<unit> ^-> Scene
        "markDirty" => !?T<bool>?forceMaterialDirty ^-> T<unit>
        "clone" => T<string>?name ^-> Material
        "freeze" => T<unit> ^-> T<unit>
        "unfreeze" => T<unit> ^-> T<unit>
        "getBindedMeshes" => T<unit> ^-> !|AbstractMesh
        "dispose" =>
            !?T<bool>?forceDisposeEffect * !?T<bool>?forceDisposeTextures
            * !?T<bool>?notBoundToMesh ^-> T<unit>
    ]

    StandardMaterial
    |+> Static [
        Constructor (
            T<string>?name * !?Scene?scene * !?T<bool>?forceGLSL
        )
    ]
    |+> Instance [
        "diffuseColor" =@ Color3
        "ambientColor" =@ Color3
        "specularColor" =@ Color3
        "emissiveColor" =@ Color3
        "diffuseTexture" =@ BaseTexture
        "ambientTexture" =@ BaseTexture
        "opacityTexture" =@ BaseTexture
        "reflectionTexture" =@ BaseTexture
        "emissiveTexture" =@ BaseTexture
        "specularTexture" =@ BaseTexture
        "bumpTexture" =@ BaseTexture
        "lightmapTexture" =@ BaseTexture
        "refractionTexture" =@ BaseTexture
        "specularPower" =@ T<float>
        "roughness" =@ T<float>
        "indexOfRefraction" =@ T<float>
        "invertRefractionY" =@ T<bool>
        "alphaCutOff" =@ T<float>
        "useAlphaFromDiffuseTexture" =@ T<bool>
        "useEmissiveAsIllumination" =@ T<bool>
        "linkEmissiveWithDiffuse" =@ T<bool>
        "useSpecularOverAlpha" =@ T<bool>
        "useReflectionOverAlpha" =@ T<bool>
        "disableLighting" =@ T<bool>
        "useObjectSpaceNormalMap" =@ T<bool>
        "useParallax" =@ T<bool>
        "useParallaxOcclusion" =@ T<bool>
        "parallaxScaleBias" =@ T<float>
        "useLightmapAsShadowmap" =@ T<bool>
        "invertNormalMapX" =@ T<bool>
        "invertNormalMapY" =@ T<bool>
        "twoSidedLighting" =@ T<bool>
        "maxSimultaneousLights" =@ T<int>
        "getClassName" => T<unit> ^-> T<string>
        "clone" =>
            T<string>?name * !?T<bool>?cloneTexturesOnlyOnce * !?T<string>?rootUrl
            ^-> StandardMaterial
        "dispose" =>
            !?T<bool>?forceDisposeEffect * !?T<bool>?forceDisposeTextures ^-> T<unit>
    ]

    BaseTexture
    |+> Instance [
        "uniqueId" =? T<int>
        "name" =@ T<string>
        "displayName" =@ T<string>
        "hasAlpha" =@ T<bool>
        "getAlphaFromRGB" =@ T<bool>
        "level" =@ T<float>
        "coordinatesIndex" =@ T<int>
        "coordinatesMode" =@ T<int>
        "wrapU" =@ T<int>
        "wrapV" =@ T<int>
        "wrapR" =@ T<int>
        "anisotropicFilteringLevel" =@ T<int>
        "isCube" =? T<bool>
        "gammaSpace" =@ T<bool>
        "invertZ" =@ T<bool>
        "isRenderTarget" =@ T<bool>
        "getClassName" => T<unit> ^-> T<string>
        "getScene" => T<unit> ^-> Scene
        "getTextureMatrix" => T<unit> ^-> Matrix
        "getReflectionTextureMatrix" => T<unit> ^-> Matrix
        "scale" => T<float>?ratio ^-> T<unit>
        "dispose" => T<unit> ^-> T<unit>
    ]

    Texture
    |+> Static [
        Constructor (
            T<string>?url * !?Scene?scene * !?T<bool>?noMipmap * !?T<bool>?invertY
            * !?T<int>?samplingMode * !?(T<unit> ^-> T<unit>)?onLoad
            * !?(T<string> ^-> T<unit>)?onError
        )
        "CreateFromBase64String" =>
            T<string>?data * T<string>?name * Scene?scene
            * !?T<bool>?noMipmap * !?T<bool>?invertY * !?T<int>?samplingMode
            * !?(T<unit> ^-> T<unit>)?onLoad ^-> Texture
    ]
    |+> Instance [
        "url" =? T<string>
        "uOffset" =@ T<float>
        "vOffset" =@ T<float>
        "uScale" =@ T<float>
        "vScale" =@ T<float>
        "uAng" =@ T<float>
        "vAng" =@ T<float>
        "wAng" =@ T<float>
        "noMipmap" =? T<bool>
        "invertY" =? T<bool>
        "isBlocking" =@ T<bool>
        "onLoadObservable" =? Observable.[Texture]
        "updateURL" =>
            T<string>?url * !?T<string>?buffer * !?(T<unit> ^-> T<unit>)?onLoad
            ^-> T<unit>
        "delayLoad" => T<unit> ^-> T<unit>
        "clone" => T<unit> ^-> Texture
        "getClassName" => T<unit> ^-> T<string>
        "dispose" => T<unit> ^-> T<unit>
    ]

    DynamicTexture
    |+> Static [
        Constructor (
            T<string>?name * T<int>?size * !?Scene?scene * !?T<bool>?generateMipMaps
            * !?T<int>?samplingMode * !?T<int>?format * !?T<bool>?invertY
        )
    ]
    |+> Instance [
        "getClassName" => T<unit> ^-> T<string>
        "canRescale" =? T<bool>
        "getContext" => T<unit> ^-> T<CanvasRenderingContext2D>
        "clear" => !?T<string>?clearColor ^-> T<unit>
        "scale" => T<float>?ratio ^-> T<unit>
        "scaleTo" => T<int>?width * T<int>?height ^-> T<unit>
        "update" =>
            !?T<bool>?invertY * !?T<bool>?premulAlpha
            * !?T<bool>?allowGPUOptimization ^-> T<unit>
        "drawText" =>
            T<string>?text * !?T<int>?x * !?T<int>?y * T<string>?fontName
            * !?T<string>?color * !?T<string>?fillColor * !?T<bool>?invertY
            * !?T<bool>?update ^-> T<unit>
        "clone" => T<unit> ^-> DynamicTexture
        "dispose" => T<unit> ^-> T<unit>
    ]
    |> WithComment
        "A texture rendered into a 2D canvas; use `getContext` to draw and `update` to upload it."

    GlowLayer
    |+> Static [
        Constructor (
            T<string>?name * !?Scene?scene * !?IGlowLayerOptions?options
        )
    ]
    |+> Instance [
        "blurKernelSize" =@ T<int>
        "intensity" =@ T<float>
        "addExcludedMesh" => Mesh?mesh ^-> T<unit>
        "removeExcludedMesh" => Mesh?mesh ^-> T<unit>
        "addIncludedOnlyMesh" => Mesh?mesh ^-> T<unit>
        "removeIncludedOnlyMesh" => Mesh?mesh ^-> T<unit>
        "setExcludedByDefault" => T<bool>?value ^-> T<unit>
        "getClassName" => T<unit> ^-> T<string>
    ]

    // ==================================================================
    // Picking / pointer info
    // ==================================================================

    Ray
    |+> Static [
        Constructor (
            Vector3?origin * Vector3?direction * !?T<float>?length
        )
        "Zero" => T<unit> ^-> Ray
        "CreateNew" =>
            T<float>?x * T<float>?y * !?T<float>?viewportWidth
            * !?T<float>?viewportHeight * Matrix?world * Matrix?view
            * Matrix?projection ^-> Ray
        "CreateNewFromTo" => Vector3?origin * Vector3?``end`` * !?Matrix?world ^-> Ray
    ]
    |+> Instance [
        "origin" =@ Vector3
        "direction" =@ Vector3
        "length" =@ T<float>
        "clone" => T<unit> ^-> Ray
    ]
    |> WithComment
        "A picking ray. Importing this type also registers Babylon's ray/picking support."

    PickingInfo
    |+> Instance [
        "hit" =? T<bool>
        "distance" =? T<float>
        "pickedPoint" =? Vector3
        "pickedMesh" =? AbstractMesh
        "faceId" =? T<int>
        "subMeshFaceId" =? T<int>
        "subMeshId" =? T<int>
        "bu" =? T<float>
        "bv" =? T<float>
    ]

    PointerInfo
    |+> Instance [
        "type" =? T<int>
        "event" =? IPointerEvent
        "pickInfo" =? PickingInfo
    ]
    |> WithComment
        "The payload of `Scene.onPointerObservable`; compare `type` with the values in `PointerEventTypes`."

    // ==================================================================
    // Engine
    // ==================================================================

    AbstractEngine
    |+> Instance [
        "runRenderLoop" => (T<unit> ^-> T<unit>)?renderFunction ^-> T<unit>
        "stopRenderLoop" => !?(T<unit> ^-> T<unit>)?renderFunction ^-> T<unit>
        "resize" => !?T<bool>?forceSetSize ^-> T<unit>
        "setSize" =>
            T<int>?width * T<int>?height * !?T<bool>?forceSetSize ^-> T<bool>
        "getRenderWidth" => !?T<bool>?useScreen ^-> T<int>
        "getRenderHeight" => !?T<bool>?useScreen ^-> T<int>
        "getRenderingCanvas" => T<unit> ^-> T<HTMLCanvasElement>
        "getFps" => T<unit> ^-> T<float>
        "getDeltaTime" => T<unit> ^-> T<float>
        "setHardwareScalingLevel" => T<float>?level ^-> T<unit>
        "getHardwareScalingLevel" => T<unit> ^-> T<float>
    ]

    Engine
    |+> Static [
        Constructor (
            T<HTMLCanvasElement>?canvasOrContext * !?T<bool>?antialias
            * !?EngineOptions?options * !?T<bool>?adaptToDeviceRatio
        )
    ]
    |+> Instance [
        "dispose" => T<unit> ^-> T<unit>
        "getClassName" => T<unit> ^-> T<string>
    ]

    // ==================================================================
    // Scene
    // ==================================================================

    Scene
    // Scene picking (`pick`, `pickWithRay`, `onPointerObservable` pick info)
    // is only functional once Babylon's `Culling/ray` module is loaded; use the
    // `Ray` type (see `Ray` above) so the module is imported.
    |+> Static [
        Constructor (AbstractEngine?engine)
    ]
    |+> Instance [
        "clearColor" =@ Color4
        "ambientColor" =@ Color3
        "environmentIntensity" =@ T<float>
        "autoClear" =@ T<bool>
        "autoClearDepthAndStencil" =@ T<bool>
        "animationsEnabled" =@ T<bool>
        "hoverCursor" =@ T<string>
        "defaultCursor" =@ T<string>
        "doNotHandleCursors" =@ T<bool>
        "preventDefaultOnPointerDown" =@ T<bool>
        "preventDefaultOnPointerUp" =@ T<bool>
        "activeCamera" =@ Camera
        "activeCameras" =@ !|Camera
        "rootNodes" =? !|Node
        "cameras" =? !|Camera
        "lights" =? !|Light
        "meshes" =? !|AbstractMesh
        "materials" =? !|Material
        "textures" =? !|BaseTexture
        "transformNodes" =? !|TransformNode
        "onBeforeRenderObservable" =? Observable.[Scene]
        "onAfterRenderObservable" =? Observable.[Scene]
        "onDisposeObservable" =? Observable.[Scene]
        "onPointerObservable" =? Observable.[PointerInfo]
        "onReadyObservable" =? Observable.[Scene]
        "getEngine" => T<unit> ^-> AbstractEngine
        "getAnimationRatio" => T<unit> ^-> T<float>
        "registerBeforeRender" => (T<unit> ^-> T<unit>)?callback ^-> T<unit>
        "unregisterBeforeRender" => (T<unit> ^-> T<unit>)?callback ^-> T<unit>
        "registerAfterRender" => (T<unit> ^-> T<unit>)?callback ^-> T<unit>
        "unregisterAfterRender" => (T<unit> ^-> T<unit>)?callback ^-> T<unit>
        "getMeshByName" => T<string>?name ^-> AbstractMesh
        "getNodeByName" => T<string>?name ^-> Node
        "render" =>
            !?T<bool>?updateCameras * !?T<bool>?ignoreAnimations ^-> T<unit>
        "dispose" => T<unit> ^-> T<unit>
        "isDisposed" =? T<bool>
        "pick" =>
            T<float>?x * T<float>?y * !?(AbstractMesh ^-> T<bool>)?predicate
            * !?T<bool>?fastCheck * !?Camera?camera ^-> PickingInfo
        "pickWithRay" =>
            Ray?ray * !?(AbstractMesh ^-> T<bool>)?predicate
            * !?T<bool>?fastCheck ^-> PickingInfo
        "setRenderingAutoClearDepthStencil" =>
            T<int>?renderingGroupId * T<bool>?autoClearDepthStencil
            ^-> T<unit>
        "getPointerOverMesh" => T<unit> ^-> AbstractMesh
    ]

    // ==================================================================
    // MeshBuilder
    // ==================================================================

    MeshBuilder
    |+> Static [
        "CreateBox" =>
            T<string>?name * !?BoxOptions?options * !?Scene?scene ^-> Mesh
        "CreateSphere" =>
            T<string>?name * !?SphereOptions?options * !?Scene?scene ^-> Mesh
        "CreateGround" =>
            T<string>?name * !?GroundOptions?options * !?Scene?scene ^-> GroundMesh
        "CreateCylinder" =>
            T<string>?name * !?CylinderOptions?options * !?Scene?scene ^-> Mesh
        "CreateTorus" =>
            T<string>?name * !?TorusOptions?options * !?Scene?scene ^-> Mesh
        "CreateTorusKnot" =>
            T<string>?name * !?TorusKnotOptions?options * !?Scene?scene ^-> Mesh
        "CreatePlane" =>
            T<string>?name * !?PlaneOptions?options * !?Scene?scene ^-> Mesh
        "CreateDisc" =>
            T<string>?name * !?DiscOptions?options * !?Scene?scene ^-> Mesh
        "CreateLines" =>
            T<string>?name * LinesOptions?options * !?Scene?scene ^-> LinesMesh
        "CreateCapsule" =>
            T<string>?name * !?CapsuleOptions?options * !?Scene?scene ^-> Mesh
        "CreateIcoSphere" =>
            T<string>?name * !?IcoSphereOptions?options * !?Scene?scene ^-> Mesh
        "CreatePolyhedron" =>
            T<string>?name * !?PolyhedronOptions?options * !?Scene?scene ^-> Mesh
        "CreateTube" =>
            T<string>?name * TubeOptions?options * !?Scene?scene ^-> Mesh
        "CreateRibbon" =>
            T<string>?name * RibbonOptions?options * !?Scene?scene ^-> Mesh
        "CreateLathe" =>
            T<string>?name * LatheOptions?options * !?Scene?scene ^-> Mesh
        "ExtrudeShape" =>
            T<string>?name * ExtrudeShapeOptions?options * !?Scene?scene ^-> Mesh
    ]

    // ==================================================================
    // Assembly
    // ==================================================================

    let Assembly =
        Assembly [
            Namespace "WebSharper.BabylonJs" [
                // math
                Vector2
                Vector3
                Vector4
                Quaternion
                Matrix
                Color3
                Color4
                // scene graph
                Node
                TransformNode
                AbstractMesh
                Mesh
                GroundMesh
                LinesMesh
                // cameras
                Camera
                TargetCamera
                ArcRotateCamera
                FreeCamera
                // lights
                Light
                ShadowLight
                HemisphericLight
                DirectionalLight
                PointLight
                SpotLight
                ShadowGenerator
                // materials
                Material
                StandardMaterial
                BaseTexture
                Texture
                DynamicTexture
                GlowLayer
                // picking
                Ray
                PickingInfo
                PointerInfo
                IPointerEvent
                // engine / scene
                AbstractEngine
                ThinEngine
                Engine
                Scene
                // builders
                MeshBuilder
                BoxOptions
                SphereOptions
                GroundOptions
                CylinderOptions
                TorusOptions
                TorusKnotOptions
                PlaneOptions
                DiscOptions
                LinesOptions
                CapsuleOptions
                IcoSphereOptions
                PolyhedronOptions
                TubeOptions
                RibbonOptions
                LatheOptions
                ExtrudeShapeOptions
                EngineOptions
                MinMaxVectors
                IGlowLayerOptions
                // enums / constants
                PowerPreference
                Space
                PointerEventTypes
                TextureSamplingMode
                AddressMode
                // observables
                Observable
                Observer
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member x.Assembly = Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()