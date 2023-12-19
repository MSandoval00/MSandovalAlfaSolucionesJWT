namespace ML
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string? Nombre { get; set; }
        public bool Genero { get; set; }
        public int Edad { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<object>? Alumnos { get; set; }
    }
}