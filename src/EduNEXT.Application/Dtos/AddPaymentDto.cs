namespace EduNEXT.Application.Dtos;

public class AddPaymentDto
{
    public Guid StudentId { get; set; }
    
    public int Amount { get; set; }
}