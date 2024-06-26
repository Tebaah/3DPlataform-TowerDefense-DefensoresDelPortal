using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
  // variables
  // variables de movimiento
  [Export] public float speed;
  [Export] public float jumpForce;

  //variable de gravedad
  public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

  private Vector3 velocity = Vector3.Zero;

  // variables nodo hijos
  public Node3D visual;
  // metodos
  public override void _Ready()
  {
    // inicialzar nodos hijos
    visual = GetNode<Node3D>("Visual");
  }

    public override void _Input(InputEvent @event)
    {
      if(@event is InputEventMouseMotion mouseMotion)
      {
        RotateY(Mathf.DegToRad(-mouseMotion.Relative.X * 0.5f));
      }
    }

    public override void _PhysicsProcess(double delta)
  {
    
    // obtenemos los inputs del jugador
    Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
    Vector3 direction = (GlobalTransform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y)).Normalized();

    // aplicamos la gravedad
    if(!IsOnFloor())
    {
      velocity.Y -= gravity * 5 * (float)delta;
    }

    // aplicamos el movimiento
    if(inputDirection != Vector2.Zero)
    {
      // modelo ve en direccion del movimiento
      visual.LookAt(Position + direction * (float)delta);

      velocity.X = direction.X * speed;
      velocity.Z = direction.Z * speed;
    }
    else
    {
      velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
      velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
    }

    // aplicamos el salto
    if(IsOnFloor() && Input.IsActionJustPressed("jump"))
    {
      velocity.Y = jumpForce;
    }

    Velocity = velocity;
    MoveAndSlide();
  }
}
