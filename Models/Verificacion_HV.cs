using ConvocatoriaServices.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConvocatoriaApiServices.Models
{
   
        [Table("verificacion_hv")]
        public class Verificacion_HV
        {
            [Key]
            public string codigoinscripcion { get; set; }

            public Inscripcion_Convocatoria Inscripcion_Convocatoria { get; set; }

            public int formacion1 { get; set; }
            public int formacion2 { get; set; }
            public int formacion3 { get; set; }
            public int formacion4 { get; set; }
            public int totalFormacion { get; set; }
            public int fomacionIn1 { get; set; }
            public int fomacionIn2{ get; set; }
            public int totalFormacionIn { get; set; }
            
            public int produccion1 { get; set; }
            public int produccion2 { get; set; }
            public int produccion3 { get; set; }
            public int produccion4 { get; set; }
            public int produccion5 { get; set; }
            public int produccion6 { get; set; }
            public int produccion7 { get; set; }
            public int produccion8 { get; set; }
            public int produccion9 { get; set; }
            public int produccion10 { get; set; }
            public int produccion11 { get; set; }
            public int produccion12 { get; set; }
            public int totalProduccion { get; set; }
           
            public int experiencia1 { get; set; }
            public int experiencia2 { get; set; }
            public int experiencia3 { get; set; }
            public int experiencia4 { get; set; }
            public int totalExperiencia { get; set; }
           
            public int totalEvaluacion { get; set; }

            public string observacion1{ get; set; }
            public string observacion2{ get; set; }
            public string observacion3{ get; set; }
            public string observacion4{ get; set; }
            public string observacion5{ get; set; }
            public string observacion6{ get; set; }
            public string observacion7{ get; set; }
            public string observacion8{ get; set; }
            public string observacion9 { get; set; }
            public string observacion10{ get; set; }
            public string observacion11{ get; set; }
            public string observacion12{ get; set; }
            public string observacion13{ get; set; }
            public string observacion14{ get; set; }
            public string observacion15{ get; set; }
            public string observacion16{ get; set; }
            public string observacion17{ get; set; }
            public string observacion18{ get; set; }
            public string observacion19{ get; set; }
            public string observacion20{ get; set; }
            public string observacion21{ get; set; }
            public string observacion22{ get; set; }
        public string observacionGeneral { get; set; }

            public bool admitido { get; set; }
        }
    
}
