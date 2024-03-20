using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
  // variables
  // variables de movimiento
  [Export] public float speed;

  //variable de gravedad
  public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

  private Vector3 velocity = Vector3.Zero;
  // metodos
  public override void _Ready()
  {
      // Called every time the node is added to the scene.
      // Initialization here
  }

  public override void _PhysicsProcess(double delta)
  {
    
    // obtenemos los inputs del jugador
    Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
    Vector3 direction = (GlobalTransform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y)).Normalized();

    if(inputDirection != Vector2.Zero)
    {
      velocity.X = direction.X * speed;
      velocity.Z = direction.Z * speed;
    }
    else
    {
      velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
      velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
    }

    Velocity = velocity;
    MoveAndSlide();
  }
}
