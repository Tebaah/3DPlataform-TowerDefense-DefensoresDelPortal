using Godot;
using System;

public partial class ShowFps : CanvasLayer
{
    private Label _fpsLabel;

    public override void _Ready()
    {
        _fpsLabel = GetNode<Label>("Label");
    }

    public override void _Process(double delta)
    {
        _fpsLabel.Text = $"FPS: {Engine.GetFramesPerSecond()}";
    }
}
