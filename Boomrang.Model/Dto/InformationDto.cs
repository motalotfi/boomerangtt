using System.ComponentModel.DataAnnotations.Schema;

namespace Boomrang.Model.Dto
{
    [ComplexType]
    public class InformationDto : IDbDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
