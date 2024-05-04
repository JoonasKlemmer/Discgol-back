namespace App.DTO.v1_0;

public class Disc
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public double Speed { get; set; }
    public double Glide { get; set; }
    public double Turn { get; set; }
    public double Fade { get; set; }
}