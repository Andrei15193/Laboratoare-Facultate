using System.ComponentModel;
namespace Reservations.Model
{
    public enum HallLocation
    {
		[Description("Stal")]
		Stal,
		[Description("Loja 1")]
		Lodge1,
		[Description("Loja 2")]
		Lodge2,
		[Description("Loja 3")]
		Lodge3,
		[Description("Loja 4")]
		Lodge4,
		[Description("Loja 5")]
		Lodge5,
		[Description("Loja 6")]
		Lodge6,
		[Description("Balcon")]
		Balcony
    }
}