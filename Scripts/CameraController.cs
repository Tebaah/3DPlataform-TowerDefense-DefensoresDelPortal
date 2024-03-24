using Godot;
using System;

public partial class CameraController : Node3D
{
  // variables nodos hijos
    public Node3D yawNode;
    public Node3D pitchNode;
    public Camera3D camera;

    // variables de movimiento camara
    public float yaw = 0;
    public float pitch = 0;
    public float yawSensitive = 0.05f;
    public float pitchSensitive = 0.05f;
    public float yawAcceleration = 15f;
    public float pitchAcceleration = 15f;
    public float pitchMax = 75;
    public float pitchMin = -55;

    public override void _Ready()
    {
      // inicializar los nodos hijos
        yawNode = GetNode<Node3D>("CameraYaw");
        pitchNode = GetNode<Node3D>("CameraYaw/CameraPitch");
        camera = GetNode<Camera3D>("CameraYaw/CameraPitch/SpringArm3D/Camera3D");

      // ocultar el cursor
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
      if(@event is InputEventMouseMotion mouseMotion)
      {
        // obtener el movimiento del mouse
        yaw += mouseMotion.Relative.X * yawSensitive;
        pitch -= mouseMotion.Relative.Y * pitchSensitive;
      }
    }

    public override void _PhysicsProcess(double delta)
    { 
      // limita el movimiento de la camara
      pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

      // realiza el movimiento de la camara
      float newYaw = Mathf.Lerp(yawNode.RotationDegrees.Y, yaw, yawAcceleration * (float)delta);
      yawNode.RotationDegrees = new Vector3(yawNode.RotationDegrees.X, newYaw, yawNode.RotationDegrees.Z);

      float newPitch = Mathf.Lerp(pitchNode.RotationDegrees.X, pitch, pitchAcceleration * (float)delta);
      pitchNode.RotationDegrees = new Vector3(newPitch, pitchNode.RotationDegrees.Y, pitchNode.RotationDegrees.Z);

  
    }
}
